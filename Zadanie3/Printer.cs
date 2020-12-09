using System;
using System.Collections.Generic;
using System.Text;

namespace Zadanie3
{
   public class Printer : BaseDevice, IPrinter
    {
        public int PrintCounter { get;  set; } = 0;

        public void Print(in IDocument document)
        {
            if (state == IDevice.State.on)
            {
                Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
                PrintCounter++;
            }
        }
    }
}
