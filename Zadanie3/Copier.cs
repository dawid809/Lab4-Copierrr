using System;
using System.Collections.Generic;
using System.Text;

namespace Zadanie3
{
    public class Copier : BaseDevice
    {

        public Printer Printer { get; private set; }
        public Scanner Scanner { get; private set; }
        public int PrintCounter => Printer.PrintCounter;
        public int ScanCounter => Scanner.ScanCounter;

        public Copier()
        {
            Printer = new Printer();
            Scanner = new Scanner();
        }

        public override void PowerOff()
        {
            Printer.PowerOff();
            Scanner.PowerOff();
            base.PowerOff();
        }

        public void CopierScannerOn()
        {
            if (state == IDevice.State.on)
            Scanner.PowerOn();
        }

        public void CopierScannerOff()
        {
                Scanner.PowerOff();
        }

        public void CopierPrinterOn()
        {
            if (state == IDevice.State.on)
            Printer.PowerOn();
        }

        public void CopierPrinterOff()
        {
            Printer.PowerOff();
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.PDF) => Scanner.Scan(out document, formatType);
        public void Print(in IDocument document) => Printer.Print(document);

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
