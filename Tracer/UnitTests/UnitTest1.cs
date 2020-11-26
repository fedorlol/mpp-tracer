using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tracer;
using System.Threading;
using System.Collections.Generic;

namespace UnitTests
{

    [TestClass]
    public class UnitTest1
    {

        static ITracer tracer;

        [TestMethod]
        public void SimpleTest()
        {
            tracer = new Tracer.Tracer();
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
            Result res = tracer.GetResult();
            Assert.AreNotEqual(0, res.ThreadsList[0].Time);
        }

        [TestMethod]
        public void Method1Test()
        {
            TestClass TestClass = new TestClass();
            tracer = new Tracer.Tracer();
            tracer.StartTrace();
            TestClass.Method1();
            tracer.StopTrace();
            Result res = tracer.GetResult();
            Assert.AreEqual(res.ThreadsList[0].Time, 42);
        }

        [TestMethod]
        public void Method2Test()
        {
            TestClass TestClass = new TestClass();
            tracer = new Tracer.Tracer();
            tracer.StartTrace();
            TestClass.Method2();
            tracer.StopTrace();
            Result res = tracer.GetResult();
            Assert.AreNotEqual(0, res.ThreadsList[0].Time);
        }

    }

    class TestClass
    {
        internal void Method1()
        {

            Thread.Sleep(42);

        }
        internal void Method2()
        {

            Thread.Sleep(322);
            Method1();

        }

    }
}
