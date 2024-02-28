using System;

namespace Tracer.Interfaces
{
    public interface IOutput
    {
        void ConsoleOutput(string result);
        void FileOutput(string result, string path);
    }
}
