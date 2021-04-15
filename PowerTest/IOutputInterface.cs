using System;
using System.Collections.Generic;
using System.Text;

namespace PowerTest
{
    public interface IOutputInterface
    {
        void WriteLine(string message);
        void WriteLine(ConsoleColor color, string message);
        void Write(string message);
        void Write(ConsoleColor color, string message);
    }
}
