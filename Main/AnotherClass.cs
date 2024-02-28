using Tracer.Interfaces;

namespace Demonstration
{
    internal class AnotherClass
    {
        private ITracer _tracer;

        internal AnotherClass(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void InnerMethod(int delay)
        {
            _tracer.StartTrace();

            Thread.Sleep(delay); //

            _tracer.StopTrace();
        }
    }
}
