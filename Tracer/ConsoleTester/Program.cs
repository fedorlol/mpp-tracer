using System;
using System.Threading;
using Tracer;
using System.IO;
using System.Collections.Generic;

namespace ConsoleTester
{
    class Program
    {

        private static ITracer tracer;

        static void Method1()
        {
            tracer.StartTrace();

            Thread.Sleep(42);

            tracer.StopTrace();
        }
        static void Method2()
        {
            tracer.StartTrace();

            Thread.Sleep(322);
            Method1();
            (new Thread(new ThreadStart(Method1))).Start();

            tracer.StopTrace();
        }

        static void Method3(int threadsCount)
        {

            tracer.StartTrace();

            var threads = new List<Thread>();
            Thread newThread;
            for (int i = 0; i < threadsCount; i++)
            {
                newThread = new Thread(Method1);
                threads.Add(newThread);
            }
            foreach (Thread thread in threads)
            {
                thread.Start();
            }
            tracer.StartTrace();
            Thread.Sleep(42);
            tracer.StopTrace();
            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            tracer.StopTrace();
        }

        static void Main(string[] args)
        {

            tracer = new Tracer.Tracer();
            Method3(6);
            Result res = tracer.GetResult();

            ISerializer js = new JSONSerializer();
            IStreamWriter cw = new ConsoleWriter();
            cw.Write(res, js);
            Console.WriteLine(res.ThreadsList.Count);
            Console.ReadKey();

        }
    }
}
