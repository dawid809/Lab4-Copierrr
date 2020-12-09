using System;
using System.Collections.Generic;
using System.Text;

namespace Zadanie3
{
   public class Scanner : BaseDevice, IScanner
    {
        public int ScanCounter { get; set; } = 0;

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
            string fileName = $"{type}Scan{ScanCounter + 1}.{formatType.ToString().ToLower()}.";
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
    }
}
