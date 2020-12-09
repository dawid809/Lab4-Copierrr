using System;
using System.Collections.Generic;
using System.Text;

namespace Zadanie3
{
   public class Fax : BaseDevice, IFax
    {
        public int FaxCounter { get; set; } = 0;

        public void SendFax(in IDocument document, int faxNumber)
        {
            if (state == IDevice.State.on)
            {
                Console.WriteLine($"Send: {document.GetFileName()} to: {faxNumber}");
                FaxCounter++;
            }
        }
    }
}
