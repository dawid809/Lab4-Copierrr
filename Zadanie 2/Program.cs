using System;
using Zadanie1;

namespace Zadanie2
{
    class Program
    {
        static void Main(string[] args)
        {
            var multiFunDev = new MultifunctionalDevice();
            multiFunDev.PowerOn();
            IDocument doc1 = new PDFDocument("aaa.pdf");

            multiFunDev.ScanAndSend(123456789);
            multiFunDev.ScanAndSend(123456789);

            Console.WriteLine();
            Console.WriteLine($"Fax counter: {multiFunDev.FaxCounter}");
            Console.WriteLine($"Scan counter: {multiFunDev.ScanCounter}");

            Console.WriteLine();
            multiFunDev.PowerOff();
            multiFunDev.ScanAndSend(987654321);

            Console.WriteLine("Nothing happens when off!");
        }
    }
}
