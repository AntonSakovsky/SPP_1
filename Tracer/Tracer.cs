using System.Diagnostics;
using Tracer.Interfaces;

namespace Tracer
{
    public class Tracer : ITracer
    {
        private List<ThreadInfo> Threads { get; set; }
        private readonly object _lockObj = new();

        public Tracer()
        {
            Threads = new List<ThreadInfo>();
        }
        public void StartTrace()
        {
            lock (_lockObj)
            {
                int threadId = Thread.CurrentThread.ManagedThreadId;

                var threadInfo = Threads.FirstOrDefault(t => t.Id == threadId);

                if (threadInfo == null)
                {
                    threadInfo = new ThreadInfo(threadId);
                    Threads.Add(threadInfo);
                }

                StackTrace stackTrace = new StackTrace();
                StackFrame? frame = stackTrace.GetFrame(1);

                string? methodName = frame?.GetMethod()?.Name;
                string? className = frame?.GetMethod()?.DeclaringType?.Name;

                MethodInfo methodInfo = new MethodInfo(methodName, className);

                if (threadInfo?.Stack.Count == 0)
                {
                    threadInfo.Methods.Add(methodInfo);
                }
                else
                {
                    threadInfo?.Stack.Peek().Methods.Add(methodInfo);
                }                    

                threadInfo?.Stack.Push(methodInfo);
                methodInfo.Stopwatch.Start();
            }

        }
        public void StopTrace()
        {
            lock (_lockObj)
            {
                int threadId = Thread.CurrentThread.ManagedThreadId;

                var threadInfo = Threads.FirstOrDefault(t => t.Id == threadId);

                var methodInfo = threadInfo.Stack.Pop();

                methodInfo.Stopwatch.Stop();

                methodInfo.ExecutionTime = methodInfo.Stopwatch.Elapsed.TotalMilliseconds;
            }
        }

        public TraceResult GetTraceResult()
        {
            var ResultThreads = new List<ThreadInfo>();

            foreach (var threadInfo in Threads)
            {
                var time = threadInfo.Methods.Sum(CalcTotalTime);
                ResultThreads.Add(new ThreadInfo(threadInfo.Id, $"{Math.Round(time)}ms", threadInfo.Methods));
            }
            return new TraceResult(ResultThreads);


            double CalcTotalTime(MethodInfo method)
            {
                double time = 0;

                if (method.Methods.Count != 0)
                {
                    foreach (var methodInfo in method.Methods)
                    {
                        time += CalcTotalTime(methodInfo);
                    }
                }

                time += method.ExecutionTime;

                return time;
            }
        }
    }
}
