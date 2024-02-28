using Tracer.Interfaces;

namespace Tracer
{
    public class Output : IResultOutput
    {
        public void ConsoleOutput(string result)
        {
            Console.WriteLine(result);
        }

        public async void FileOutput(string result, string path)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path, false))
                {
                    await sw.WriteAsync(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
