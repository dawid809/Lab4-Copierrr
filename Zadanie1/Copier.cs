using System;
using System.Collections.Generic;
using System.Text;

namespace Zadanie1
{
    public class Copier : BaseDevice, IPrinter, IScanner
    {

        public int PrintCounter { get; private set; } = 0;
        public int ScanCounter { get; private set; } = 0;

        public void Print(in IDocument document)
        {
            if (state == IDevice.State.on)
            {
                Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
                PrintCounter++;
            }
        }
         

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
                string type;
            switch (formatType)
            {
                case IDocument.FormatType.PDF:
                    type = "PDF";
                    break;
                case IDocument.FormatType.TXT:
                    type = "Text";
                    break;
                default:
                    type = "Image";
                    break;
             }
            string fileName = $"{type}Scan{ScanCounter +1}.{formatType.ToString().ToLower()}.";
            if (IDocument.FormatType.PDF == formatType)
                document = new PDFDocument(fileName);
            else if (IDocument.FormatType.JPG == formatType)
                document = new ImageDocument(fileName);
            else
                document = new TextDocument(fileName);

            if (state == IDevice.State.on)
            {
                Console.WriteLine($"{DateTime.Now} Scan: {document.GetFileName()}");
                ScanCounter++;
            }
        }

        public void ScanAndPrint()
        {
            if (state == IDevice.State.on)
            {
                Scan(out IDocument document);
                Print(document);
            }
        }
    }
}
