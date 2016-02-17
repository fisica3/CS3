using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Pipes;

using PlainConcepts.Linq;

namespace Ejemplo11_01
{
    class Program
    {
        static void Main(string[] args)
        {
            var c1 =
                from f in System.IO.Directory.GetFiles(
                    "C:\\Users\\Octavio\\Pictures", "*.jpg",
                    SearchOption.AllDirectories)
                where f.ToUpper().Contains("DIANA")
                orderby f
                select f.ToUpper();

            DirectoryInfo dirInfo =
                    new System.IO.DirectoryInfo("C:\\Users\\Octavio\\Pictures");
            var c2 =
                from f in dirInfo.GetFiles("*.jpg", SearchOption.AllDirectories)
                where f.Length > 10240
                orderby f.CreationTime descending
                select f.FullName.ToUpper();

            var c3 =
                from f in dirInfo.GetFileInfos("*.jpg", true)
                where f.Length > 10240
                orderby f.CreationTime descending
                select f.FullName.ToUpper();

            foreach (var c in c3)
                Console.WriteLine(c);

            // *** CLIENTE
            //Decoder decoder = Encoding.UTF8.GetDecoder();
            //Byte[] bytes = new Byte[10];
            //Char[] chars = new Char[10];

            //using (NamedPipeClientStream pipeStream =
            //        new NamedPipeClientStream(".", "CS3", PipeDirection.InOut))
            //{
            //    pipeStream.Connect();
            //    pipeStream.ReadMode = PipeTransmissionMode.Message;

            //    int numBytes;
            //    do
            //    {
            //        string message = "";
            //        do
            //        {
            //            numBytes = pipeStream.Read(bytes, 0, bytes.Length);
            //            int numChars = decoder.GetChars(bytes, 0, numBytes, chars, 0);
            //            message += new String(chars, 0, numChars);
            //        } while (!pipeStream.IsMessageComplete);
            //        decoder.Reset();
            //        Console.WriteLine(message);
            //    } while (numBytes != 0);
            //}
            using (NamedPipeClientStream pipeStream =
                    new NamedPipeClientStream(".", "CS3", PipeDirection.InOut))
            {

                var entrada = from s in pipeStream.GetMessages()
                              where string.Compare(s, "3") < 0
                              orderby s
                              select s;
                foreach (var s in entrada)
                    Console.WriteLine(s);
            }
        }
    }
}
