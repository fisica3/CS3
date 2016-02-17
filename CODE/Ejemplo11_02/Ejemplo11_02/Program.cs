using System;
using System.Text;
using System.IO;
using System.IO.Pipes;

namespace Ejemplo11_02
{
    class Program
    {
        static void Main(string[] args)
        {
            // *** SERVIDOR
            UTF8Encoding encoding = new UTF8Encoding();

            using (NamedPipeServerStream pipeStream = 
                new NamedPipeServerStream("CS3", PipeDirection.InOut, 1,
                        PipeTransmissionMode.Message, PipeOptions.None))
            {
                pipeStream.WaitForConnection();
                // envío de mensajes
                for (int i = 0; i < 100; i++)
                {
                    string msg = i.ToString();
                    byte[] bytes = encoding.GetBytes(msg);
                    pipeStream.Write(bytes, 0, bytes.Length);
                }
            }
        }
    }
}
