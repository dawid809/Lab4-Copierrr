using System;

namespace Zadanie1
{
    class Program
    {
        static void Main(string[] args)
        {
            var xerox = new Copier();
            xerox.PowerOn();
            IDocument doc1 = new PDFDocument("aaa.pdf");
            xerox.Print(in doc1);

            IDocument doc2;
            xerox.Scan(out doc2);

            
            xerox.PowerOn();
            xerox.ScanAndPrint();
            Console.WriteLine();
            Console.WriteLine($"Counter: {xerox.Counter}");
            Console.WriteLine($"Print counter: {xerox.PrintCounter}");
            Console.WriteLine($"Scan counter: {xerox.ScanCounter}");

            Console.WriteLine();
            xerox.PowerOff();
            xerox.Print(doc1);
            xerox.Scan(out doc1);
            Console.WriteLine("Nothing happens when off!");
        }
    }
}