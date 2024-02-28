using System;

namespace Tracer.Interfaces
{
    public interface IResultOutput
    {
        void ConsoleOutput(string result);
        void FileOutput(string result, string path);
    }
}
