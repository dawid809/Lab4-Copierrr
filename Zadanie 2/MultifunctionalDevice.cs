using System;
using System.Collections.Generic;
using System.Text;
using Zadanie1;

namespace Zadanie2
{
   public class MultifunctionalDevice : BaseDevice, IPrinter, IScanner, IFax
    {
        public int PrintCounter { get; private set; } = 0;
        public int ScanCounter { get; private set; } = 0;
        public int FaxCounter { get; private set; } = 0;

        public int FaxNumber { get;}

        public void Print(in IDocument document)
        {
           if(state == IDevice.State.on)
            {
                Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
                PrintCounter ++;
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
            string fileName = $"{DateTime.Now} Scan: {type}{ScanCounter+1}.{formatType.ToString().ToLower()}";

            if (IDocument.FormatType.JPG == formatType)
                document = new ImageDocument(fileName);
            else if (IDocument.FormatType.TXT == formatType)
                document = new TextDocument(fileName);
            else
                document = new PDFDocument(fileName);

            if(state == IDevice.State.on)
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

        public void SendFax(in IDocument document, int faxNumber)
        {
           if(state == IDevice.State.on)
            {
                Console.WriteLine($"Send: {document.GetFileName()} to: {faxNumber}");
                FaxCounter++;
            }
        }

        public void ScanAndSend(int faxNumber)
        {
            Scan(out IDocument document);
            SendFax(document, faxNumber);
        }
    }
}
