namespace UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public async void XmlSerializationTest()
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

            string result = tracer.GetTraceResult().toXML();
            resultOutput.FileOutput(tracer.GetTraceResult().toXML(), "test.xml");
            string fileText;
            using (var reader = new StreamReader("test.xml"))
            {
                fileText = await reader.ReadToEndAsync();
            }
            Assert.Equal(fileText, result);
        }

        [Fact]
        public async void JsonSerializationTest()
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

            string result = tracer.GetTraceResult().toJSON();
            resultOutput.FileOutput(tracer.GetTraceResult().toJSON(), "test.json");
            string fileText;
            using (var reader = new StreamReader("test.json"))
            {
                fileText = await reader.ReadToEndAsync();
            }
            Assert.Equal(fileText, result);
        }
    }
}