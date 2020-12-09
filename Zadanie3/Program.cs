using System;

namespace Zadanie3
{
    class Program
    {
        static void Main(string[] args)
        {
            var xerox = new Copier();

            xerox.PowerOn();
            xerox.CopierPrinterOn();
            IDocument doc1 = new PDFDocument("aaa.pdf");
            xerox.Print(in doc1);

            xerox.CopierScannerOn();
            IDocument doc2;
            xerox.Scan(out doc2);
            xerox.Print(in doc2);

            xerox.ScanAndPrint();

            Console.WriteLine();
            xerox.PowerOff();

            Console.WriteLine();
            Console.WriteLine($"Counter: {xerox.Counter}");
            Console.WriteLine($"Print counter: {xerox.PrintCounter}");
            Console.WriteLine($"Scan counter: {xerox.ScanCounter}");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            var multiDevice = new MultidimensionalDevice();

            multiDevice.PowerOn();
            multiDevice.DevicePrinterOn();
            IDocument doc11 = new PDFDocument("123.pdf");
            multiDevice.Print(in doc11);

            multiDevice.DeviceScannerOn();
            IDocument doc22;
            multiDevice.Scan(out doc22);
            multiDevice.Print(in doc22);

            multiDevice.ScanAndPrint();

            multiDevice.DeviceFaxOn();

            multiDevice.ScanAndSend(123456789);
            multiDevice.ScanAndSend(987654321);

            Console.WriteLine();
            multiDevice.PowerOff();
            Console.WriteLine();
            multiDevice.PowerOn();
            Console.WriteLine();
            multiDevice.PowerOff();

            Console.WriteLine();
            Console.WriteLine($"Counter: {multiDevice.Counter}");
            Console.WriteLine($"Print counter: {multiDevice.PrintCounter}");
            Console.WriteLine($"Scan counter: {multiDevice.ScanCounter}");
            Console.WriteLine($"Fax counter: {multiDevice.FaxCounter}");

        }
    }
}
