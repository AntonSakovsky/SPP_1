using Tracer;

namespace Demonstration
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Output resultOutput = new Output();
            Tracer.Tracer tracer = new Tracer.Tracer(resultOutput);

            SomeClass someClass = new SomeClass(tracer);

            Thread[] threads = new Thread[5];

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(() => someClass.MyMethod(0));
                threads[i].Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            resultOutput.ConsoleOutput(tracer.GetTraceResult().toXML());
            resultOutput.FileOutput(tracer.GetTraceResult().toXML(), "test.xml");
            resultOutput.FileOutput(tracer.GetTraceResult().toJSON(), "test.json");
        }

    }
}