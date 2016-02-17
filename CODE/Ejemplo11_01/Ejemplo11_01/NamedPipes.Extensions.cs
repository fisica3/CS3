using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Pipes;

namespace PlainConcepts.Linq
{
    public static partial class Extensions
    {
        public static IEnumerable<string> GetMessages(
            this NamedPipeClientStream pipeStream)
        {
            Decoder decoder = Encoding.UTF8.GetDecoder();
            Byte[] bytes = new Byte[10];
            Char[] chars = new Char[10];

            pipeStream.Connect();
            pipeStream.ReadMode = PipeTransmissionMode.Message;

            int numBytes;
            do
            {
                string message = "";
                do
                {
                    numBytes = pipeStream.Read(bytes, 0, bytes.Length);
                    int numChars = decoder.GetChars(bytes, 0, numBytes, chars, 0);
                    message += new String(chars, 0, numChars);
                } while (!pipeStream.IsMessageComplete);
                decoder.Reset();
                // *** producir el mensaje
                yield return message;
            } while (numBytes != 0);
        }
    }
}
