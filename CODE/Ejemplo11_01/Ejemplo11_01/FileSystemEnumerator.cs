/*=============================================================================
    FileSystemEnumerator.cs: Lazy enumerator for finding files in subdirectories.

    Copyright (c) 2006 Carl Daniel. Distributed under the Boost
    Software License, Version 1.0. (See accompanying file
    LICENSE.txt or copy at http://www.boost.org/LICENSE_1_0.txt)
=============================================================================*/

// ---------------------------------------------------------------------------
// FileSystemEnumerator implementation
// ---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace FindFiles
{
  namespace Win32
  {
    /// <summary>
    /// Structure that maps to WIN32_FIND_DATA
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    class FindData
    {
      public int fileAttributes;
      public int creationTime_lowDateTime;
      public int creationTime_highDateTime;
      public int lastAccessTime_lowDateTime;
      public int lastAccessTime_highDateTime;
      public int lastWriteTime_lowDateTime;
      public int lastWriteTime_highDateTime;
      public int nFileSizeHigh;
      public int nFileSizeLow;
      public int dwReserved0;
      public int dwReserved1;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
      public String fileName;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
      public String alternateFileName;
    }

    /// <summary>
    /// Wrapper for P/Invoke methods used by FileSystemEnumerator
    /// </summary>
    [SecurityPermissionAttribute(SecurityAction.Demand, UnmanagedCode = true)]
    static class SafeNativeMethods
    {
      [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
      public static extern IntPtr FindFirstFile(String fileName, [In, Out] FindData findFileData);

      [DllImport("kernel32", CharSet = CharSet.Auto)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool FindNextFile(IntPtr hFindFile, [In, Out] FindData lpFindFileData);

      [DllImport("kernel32", CharSet = CharSet.Auto)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool FindClose(IntPtr hFindFile);
    }

    /// <summary>
    /// SafeHandle class for holding find handles
    /// </summary>
    class SafeFindHandle : Microsoft.Win32.SafeHandles.SafeHandleMinusOneIsInvalid
    {
      /// <summary>
      /// Constructor
      /// </summary>
      /// <param name="h">Find handle returned by FindFirstFile</param>
      public SafeFindHandle(IntPtr h)
        : base(true)
      {
        handle = h;
      }

      /// <summary>
      /// Release the find handle
      /// </summary>
      /// <returns>true if the handle was released</returns>
      protected override bool ReleaseHandle()
      {
        return SafeNativeMethods.FindClose(handle);
      }
    }
  }

  /// <summary>
  /// File system enumerator.  This class provides an easy to use, efficient mechanism for searching a list of
  /// directories for files matching a list of file specifications.  The search is done incrementally as matches
  /// are consumed, so the overhead before processing the first match is always kept to a minimum.
  /// </summary>
  public sealed class FileSystemEnumerator : IDisposable
  {
    /// <summary>
    /// Information that's kept in our stack for simulated recursion
    /// </summary>
    private struct SearchInfo
    {
      /// <summary>
      /// Find handle returned by FindFirstFile
      /// </summary>
      public Win32.SafeFindHandle Handle;

      /// <summary>
      /// Path that was searched to yield the find handle.
      /// </summary>
      public string Path;

      /// <summary>
      /// Constructor
      /// </summary>
      /// <param name="h">Find handle returned by FindFirstFile.</param>
      /// <param name="p">Path corresponding to find handle.</param>
      public SearchInfo(Win32.SafeFindHandle h, string p)
      {
        Handle = h;
        Path = p;
      }
    }

    /// <summary>
    /// Stack of open scopes.  This is a member (instead of a local variable)
    /// to allow Dispose to close any open find handles if the object is disposed
    /// before the enumeration is completed.
    /// </summary>
    private Stack<SearchInfo> m_scopes;

    /// <summary>
    /// Array of paths to be searched.
    /// </summary>
    private string[] m_paths;

    /// <summary>
    /// Array of regular expressions that will detect matching files.
    /// </summary>
    private List<Regex> m_fileSpecs;

    /// <summary>
    /// If true, sub-directories are searched.
    /// </summary>
    private bool m_includeSubDirs;

    #region IDisposable implementation

    /// <summary>
    /// IDisposable.Dispose
    /// </summary>
    public void Dispose()
    {
      while (m_scopes.Count > 0)
      {
        SearchInfo si = m_scopes.Pop();
        si.Handle.Close();
      }
    }

    #endregion

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="pathsToSearch">Semicolon- or comma-delimitted list of paths to search.</param>
    /// <param name="fileTypesToMatch">Semicolon- or comma-delimitted list of wildcard filespecs to match.</param>
    /// <param name="includeSubDirs">If true, subdirectories are searched.</param>
    public FileSystemEnumerator(string pathsToSearch, string fileTypesToMatch, bool includeSubDirs)
    {
      m_scopes = new Stack<SearchInfo>();

      m_includeSubDirs = includeSubDirs;
      m_paths = pathsToSearch.Split(new char[] { ';', ',' });

      string[] specs = fileTypesToMatch.Split(new char[] { ';', ',' });
      m_fileSpecs = new List<Regex>(specs.Length);
      foreach (string spec in specs)
      {
        // trim whitespace off file spec and convert Win32 wildcards to regular expressions
        string pattern = spec
          .Trim()
          .Replace(".", @"\.")
          .Replace("*", @".*")
          .Replace("?", @".?")
          ;
        m_fileSpecs.Add(
          new Regex("^" + pattern + "$", RegexOptions.IgnoreCase)
          );
      }
    }

    /// <summary>
    /// Get an enumerator that returns all of the files that match the wildcards that
    /// are in any of the directories to be searched.
    /// </summary>
    /// <returns>An IEnumerable that returns all matching files one by one.</returns>
    /// <remarks>The enumerator that is returned finds files using a lazy algorithm that
    /// searches directories incrementally as matches are consumed.</remarks>
    public IEnumerable<FileInfo> Matches()
    {
      foreach (string rootPath in m_paths)
      {
        string path = rootPath.Trim();

        // we "recurse" into a new directory by jumping to this spot
      top:
        Win32.FindData findData = new Win32.FindData();
        Win32.SafeFindHandle handle = new Win32.SafeFindHandle(
          Win32.SafeNativeMethods.FindFirstFile(Path.Combine(path, "*"), findData)
          );
        m_scopes.Push(new SearchInfo(handle, path));
        bool restart = false;

        // we "return" from a sub-directory by jumping to this spot
      restart:
        if (!handle.IsInvalid)
        {
          do
          {
            // if we restarted the loop (unwound a recursion), fetch the next match
            if (restart)
            {
              restart = false;
              continue;
            }

            // don't match . or ..
            if (findData.fileName.Equals(@".") || findData.fileName.Equals(@".."))
              continue;

            if ((findData.fileAttributes & (int)FileAttributes.Directory) != 0)
            {
              if (m_includeSubDirs)
              {
                // it's a directory - recurse into it
                path = Path.Combine(path, findData.fileName);
                goto top;
              }
            }
            else
            {
              // it's a file, see if any of the filespecs matches it
              foreach (Regex fileSpec in m_fileSpecs)
              {
                // if this spec matches, return this file's info
                if (fileSpec.IsMatch(findData.fileName))
                  yield return new FileInfo(Path.Combine(path, findData.fileName));
              }
            }
          } while (Win32.SafeNativeMethods.FindNextFile(handle.DangerousGetHandle(), findData));

          // close this find handle
          handle.Close();

          // unwind the stack - are we still in a recursion?
          m_scopes.Pop();
          if (m_scopes.Count > 0)
          {
            SearchInfo si = m_scopes.Peek();
            handle = si.Handle;
            path = si.Path;
            restart = true;
            goto restart;
          }
        }
      }
    }
  }
}

