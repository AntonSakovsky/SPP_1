using Tracer.Interfaces;

namespace Demonstration
{
    public class SomeClass
    {
        private AnotherClass _anotherClass;
        private ITracer _tracer;

        public SomeClass(ITracer tracer)
        {
            _tracer = tracer;
            _anotherClass = new AnotherClass(_tracer);
        }

        public void SomeMethod(int delay)
        {
            _tracer.StartTrace();

            Thread.Sleep(delay);

            _anotherClass.InnerMethod(delay);

            _tracer.StopTrace();

            _tracer.GetTraceResult();

        }
    }
}
