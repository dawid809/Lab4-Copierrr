using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Zadanie1;
using Zadanie2;

namespace Zadanie2UnitTest
{
    public class ConsoleRedirectionToStringWriter : IDisposable
    {
        private StringWriter stringWriter;
        private TextWriter originalOutput;

        public ConsoleRedirectionToStringWriter()
        {
            stringWriter = new StringWriter();
            originalOutput = Console.Out;
            Console.SetOut(stringWriter);
        }

        public string GetOutput()
        {
            return stringWriter.ToString();
        }

        public void Dispose()
        {
            Console.SetOut(originalOutput);
            stringWriter.Dispose();
        }
    }
    [TestClass]
    public class UnitTesMultifuncionalDevice
    {

        [TestMethod]
        public void MultifunctionalDevice_GetState_StateOff()
        {
            var multiFunDev = new MultifunctionalDevice();
            multiFunDev.PowerOff();

            Assert.AreEqual(IDevice.State.off, multiFunDev.GetState());
        }

        [TestMethod]
        public void MultifunctionalDevice_GetState_StateOn()
        {
            var multiFunDev = new MultifunctionalDevice();
            multiFunDev.PowerOn();

            Assert.AreEqual(IDevice.State.on, multiFunDev.GetState());
        }

        // weryfikacja, czy po wywo³aniu metody `ScanAndSend` i w³¹czonym urz¹dzeniu wielofunkcyjnym w napisie pojawia siê s³owo `Send`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Print_DeviceOn()
        {
            var multiFunDev = new MultifunctionalDevice();
            multiFunDev.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                multiFunDev.ScanAndSend(123456789);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Send"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywo³aniu metody `ScanAndSend` i wy³¹czonym urz¹dzeniu wielofunkcyjnym  w napisie pojawia siê s³owo `Send`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Print_DeviceOff()
        {
            var multiFunDev = new MultifunctionalDevice();
            multiFunDev.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                multiFunDev.ScanAndSend(123456789);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Send"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_FaxCounter()
        {
            var multiFunDev = new MultifunctionalDevice();
            multiFunDev.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            multiFunDev.Print(in doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            multiFunDev.Print(in doc2);

            multiFunDev.ScanAndSend(123456789);

            multiFunDev.PowerOff();
            multiFunDev.ScanAndSend(987654321);
            multiFunDev.Scan(out doc1);
            multiFunDev.PowerOn();
            multiFunDev.ScanAndSend(987654321);

            multiFunDev.ScanAndPrint();
            multiFunDev.ScanAndPrint();

            // 2 wys³ane faxy, gdy urz¹dzenie w³¹czone
            Assert.AreEqual(2, multiFunDev.FaxCounter);
        }

        [TestMethod]
        public void MultifunctionalDevice_PowerOnCounter()
        {
            var multiFunDev = new MultifunctionalDevice();
            multiFunDev.PowerOn();       // 1
            multiFunDev.PowerOn();
            multiFunDev.PowerOn();

            IDocument doc1;
            multiFunDev.Scan(out doc1);
            IDocument doc2;
            multiFunDev.Scan(out doc2);

            multiFunDev.PowerOff();
            multiFunDev.PowerOff();
            multiFunDev.PowerOff();
            multiFunDev.PowerOn();       // 2

            IDocument doc3 = new ImageDocument("aaa.jpg");
            multiFunDev.Print(in doc3);

            multiFunDev.PowerOff();
            multiFunDev.Print(in doc3);
            multiFunDev.Scan(out doc1);
            multiFunDev.PowerOn();      // 3
            multiFunDev.PowerOff();
            multiFunDev.PowerOn();      // 4
            multiFunDev.PowerOn();

            multiFunDev.ScanAndPrint();
            multiFunDev.ScanAndPrint();

            // 4 w³¹czenia
            Assert.AreEqual(4, multiFunDev.Counter);
        }
    }
}
