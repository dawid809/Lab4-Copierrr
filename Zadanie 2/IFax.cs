using System;
using System.Collections.Generic;
using System.Text;
using Zadanie1;

namespace Zadanie2
{
    public interface IFax : IDevice
    {
        // dokument jest przesyłany(faxowany?) na podanu numer, jeśli urządzenie włączone
        // w przeciwnym przypadku nic się dzieje
        public void SendFax (in IDocument document, int faxNumber);
    }
}
