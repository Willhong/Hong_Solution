using System;
using System.IO;
using System.Net.Sockets;

namespace Hong_Solution
{
    class AsyncObject
    {
        public byte[] Buffer;
        public Socket WorkingSocket;
        public readonly int BufferSize;

        public AsyncObject(int bufferSize)
        {
            BufferSize = bufferSize;
            Buffer = new byte[BufferSize];
        }

        public void ClearBuffer()
        {
            Array.Clear(Buffer, 0, BufferSize);
        }


        public bool bIsFile = false;
        public int fileLength = 0, totalLength = 0;
        public string fileName;

        public FileStream filestr;
        public BinaryWriter writer;
    }
}
