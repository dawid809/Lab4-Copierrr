using System;
using System.Collections.Generic;
using System.Text;

namespace Zadanie3
{

    public class MultidimensionalDevice : BaseDevice
    {
        private IFax Fax { get; set; }
        private IPrinter Printer { get; set; }
        private IScanner Scanner { get; set; }

        public int ScanCounter => Scanner.ScanCounter;
        public int PrintCounter => Printer.PrintCounter;
        public int FaxCounter => Fax.FaxCounter;

        public MultidimensionalDevice()
        {
            Fax = new Fax();
            Printer = new Printer();
            Scanner = new Scanner();
        }

        public override void PowerOff()
        {
            Printer.PowerOff();
            Scanner.PowerOff();
            Fax.PowerOff();
            base.PowerOff();
        }

        public void DeviceScannerOn()
        {
            if (state == IDevice.State.on)
                Scanner.PowerOn();
        }

        public void DeviceScannerOff()
        {
            Scanner.PowerOff();
        }

        public void DevicePrinterOn()
        {
            if (state == IDevice.State.on)
                Printer.PowerOn();
        }

        public void DevicePrinterOff()
        {
            Printer.PowerOff();
        }

        public void DeviceFaxOn()
        {
            if (state == IDevice.State.on)
                Fax.PowerOn();
        }

        public void DeviceFaxOff()
        {
            Fax.PowerOff();
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

        public void SendFax(in IDocument document, int faxNumber) => Fax.SendFax(document, faxNumber);

        public void ScanAndSend(int faxNumber)
        {
            Scan(out IDocument document);
            SendFax(document, faxNumber);
        }

    }

}
