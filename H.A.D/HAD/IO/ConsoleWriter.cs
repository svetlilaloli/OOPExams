using System;
using System.IO;
using System.Text;
using HAD.Contracts;

namespace HAD.IO
{
    public class ConsoleWriter : IWriter
    {
        private readonly StringBuilder sb;

        public ConsoleWriter()
        {
            this.sb = new StringBuilder();
        }

        public void WriteLine(string text)
        {
            sb.AppendLine(text);
            //Console.WriteLine(text);
        }

        public void Flush()
        {
            Console.Write(sb.ToString().Trim());
        }
    }
}