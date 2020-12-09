using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Zadanie3;

namespace Zadanie3UnitTest
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
    public class UnitTest3
    {
        [TestMethod]
        public void Copier_GetState_StateOff()
        {
            var copier = new Copier();
            copier.PowerOff();

            Assert.AreEqual(IDevice.State.off, copier.GetState());
        }

        [TestMethod]
        public void Copier_GetState_StateOn()
        {
            var copier = new Copier();
            copier.PowerOn();

            Assert.AreEqual(IDevice.State.on, copier.GetState());
        }

        // weryfikacja, czy po wywo�aniu metody `Print` i w��czonej kopiarce oraz w��czonej drukarce w napisie pojawia si� s�owo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_Print_DeviceOn_PrinterOn()
        {
            var copier = new Copier();
            copier.PowerOn();
            copier.CopierPrinterOn();
            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                copier.Print(in doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywo�aniu metody `Print` i w��czonej kopiarce ale wy��czonej drukarce w napisie pojawia si� s�owo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_Print_DeviceOn_PrinterOff()
        {
            var copier = new Copier();
            copier.PowerOn();  // Drukarka nie w��czona
            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                copier.Print(in doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }


        // weryfikacja, czy po wywo�aniu metody `Scan` i w��czonej kopiarce oraz wy��czonym skanerze w napisie pojawia si� s�owo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_Scan_DeviceOn_ScannerOn()
        {
            var copier = new Copier();
            copier.PowerOn();
            copier.CopierScannerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                copier.Scan(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywo�aniu metody `Scan` i wy��czonej kopiarce oraz wy��czonym skanerze w napisie pojawia si� s�owo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_Scan_DeviceOff_ScannerOff()
        {
            var copier = new Copier();
            copier.PowerOn();
            copier.CopierScannerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                copier.Scan(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy wywo�anie metody `Scan` z parametrem okre�laj�cym format dokumentu
        // zawiera odpowiednie rozszerzenie (`.jpg`, `.txt`, `.pdf`)
        [TestMethod]
        public void Copier_Scan_FormatTypeDocument()
        {
            var copier = new Copier();
            copier.PowerOn();
            copier.CopierScannerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                copier.Scan(out doc1, formatType: IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                copier.Scan(out doc1, formatType: IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                copier.Scan(out doc1, formatType: IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywo�aniu metody `ScanAndPrint` i w��czonej kopiarce oraz w��czonym skanerze i drukarce w napisie pojawia si� s�owo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_ScanAndPrint_DeviceOn_ScannerOn_PrinterOn()
        {
            var copier = new Copier();
            copier.PowerOn();
            copier.CopierScannerOn();
            copier.CopierPrinterOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                copier.Scan(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywo�aniu metody `ScanAndPrint` i w��czonej kopiarce ale wy��czonym skanerze i drukarce w napisie pojawia si� s�owo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_ScanAndPrint_DeviceOn_ScannerOff_PrinterOff()
        {
            var copier = new Copier();
            copier.PowerOn();
            copier.CopierScannerOff();
            copier.CopierPrinterOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                copier.Scan(out doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void Copier_PrintCounter()
        {
            var copier = new Copier();
            copier.PowerOn();
            copier.CopierPrinterOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            copier.Print(in doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            copier.Print(in doc2);
            IDocument doc3 = new ImageDocument("aaa.jpg");
            copier.Print(in doc3);

            copier.PowerOff();
            copier.Print(in doc3);
            copier.Scan(out doc1);
            copier.PowerOn();

            copier.CopierPrinterOn();
            copier.CopierScannerOn();

            copier.ScanAndPrint();

            // 4 wydruk�w, gdy urz�dzenie w��czone
            Assert.AreEqual(4, copier.PrintCounter);
        }

        [TestMethod]
        public void Copier_ScanCounter()
        {
            var copier = new Copier();
            copier.PowerOn();
            copier.CopierScannerOn();

            IDocument doc1;
            copier.Scan(out doc1);
            IDocument doc2;
            copier.Scan(out doc2);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            copier.Print(in doc3);

            copier.PowerOff();
            copier.Print(in doc3);
            copier.Scan(out doc1);
            copier.PowerOn();

            copier.CopierScannerOn();
            copier.CopierPrinterOn();

            copier.ScanAndPrint();

            // 3 skany, gdy urz�dzenie w��czone
            Assert.AreEqual(3, copier.ScanCounter);
        }

        [TestMethod]
        public void Copier_PowerOnCounter()
        {
            var copier = new Copier();
            copier.PowerOn();
            copier.PowerOn();
            copier.PowerOn();

            IDocument doc1;
            copier.Scan(out doc1);
            IDocument doc2;
            copier.Scan(out doc2);

            copier.PowerOff();
            copier.PowerOff();
            copier.PowerOff();
            copier.PowerOn();

            IDocument doc3 = new ImageDocument("aaa.jpg");
            copier.Print(in doc3);

            copier.PowerOff();
            copier.Print(in doc3);
            copier.Scan(out doc1);
            copier.PowerOn();

            copier.ScanAndPrint();
            copier.ScanAndPrint();

            // 3 w��czenia
            Assert.AreEqual(3, copier.Counter);
        }

        [TestMethod]
        public void MultidimensionalDevice_GetState_StateOff()
        {
            var multiFunDev = new MultidimensionalDevice();
            multiFunDev.PowerOff();

            Assert.AreEqual(IDevice.State.off, multiFunDev.GetState());
        }

        [TestMethod]
        public void MultidimensionalDevice_GetState_StateOn()
        {
            var multiFunDev = new MultidimensionalDevice();
            multiFunDev.PowerOn();

            Assert.AreEqual(IDevice.State.on, multiFunDev.GetState());
        }

        // weryfikacja, czy po wywo�aniu metody `ScanAndSend` i w��czonym urz�dzeniu wielofunkcyjnym oraz w��czonym skanerze i faxie w napisie pojawia si� s�owo `Send`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultidimensionalDevice_Print_DeviceOn()
        {
            var multiFunDev = new MultidimensionalDevice();
            multiFunDev.PowerOn();
            multiFunDev.DeviceScannerOn();
            multiFunDev.DeviceFaxOn();

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

        // weryfikacja, czy po wywo�aniu metody `ScanAndSend` i wy��czonym urz�dzeniu wielofunkcyjnym  w napisie pojawia si� s�owo `Send`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultidimensionalDevice_Print_DeviceOff()
        {
            var multiFunDev = new MultidimensionalDevice();
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
        public void MultidimensionalDevice_FaxCounter()
        {
            var multiFunDev = new MultidimensionalDevice();
            multiFunDev.PowerOn();
            multiFunDev.DeviceScannerOn();
            multiFunDev.DeviceFaxOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            multiFunDev.Print(in doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            multiFunDev.Print(in doc2);

            multiFunDev.ScanAndSend(123456789);

            multiFunDev.PowerOff();
            multiFunDev.ScanAndSend(987654321);
            multiFunDev.Scan(out doc1);

            multiFunDev.PowerOn();
            multiFunDev.DeviceFaxOn();
            multiFunDev.DeviceScannerOn();
            multiFunDev.ScanAndSend(987654321);

            multiFunDev.ScanAndPrint();
            multiFunDev.ScanAndPrint();

            // 2 wys�ane faxy, gdy urz�dzenie, skaner oraz fax jest w��czony
            Assert.AreEqual(2, multiFunDev.FaxCounter);
        }

        [TestMethod]
        public void MultidimensionalDevice_PowerOnCounter()
        {
            var multiFunDev = new MultidimensionalDevice();
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

            // 4 w��czenia
            Assert.AreEqual(4, multiFunDev.Counter);
        }
    }
}
