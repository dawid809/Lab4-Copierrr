using System;
using System.Collections.Generic;
using System.Text;

namespace Zadanie3
{
    public interface IDevice
    {
        enum State { on, off };

        void PowerOn(); // uruchamia urządzenie, zmienia stan na `on`
        void PowerOff(); // wyłącza urządzenie, zmienia stan na `off
        State GetState(); // zwraca aktualny stan urządzenia

        int Counter { get; }  // zwraca liczbę charakteryzującą eksploatację urządzenia,
                              // np. liczbę uruchomień, liczbę wydrukow, liczbę skanów, ...
    }

    public abstract class BaseDevice : IDevice
    {
        protected IDevice.State state = IDevice.State.off;
        public IDevice.State GetState() => state;

        public virtual void PowerOff()
        {
            state = IDevice.State.off;
            Console.WriteLine($"Device { GetType().Name.ToLower()} is off !");
        }

        public void PowerOn()
        {
            if (state == IDevice.State.off)
                Counter++;
            state = IDevice.State.on;

            Console.WriteLine($"Device { GetType().Name.ToLower()} is on !");
        }

        public int Counter { get; private set; } = 0;
    }

    public interface IPrinter : IDevice
    {
        int PrintCounter { get; set; }
        /// <summary>
        /// Dokument jest drukowany, jeśli urządzenie włączone. W przeciwnym przypadku nic się nie wykonuje
        /// </summary>
        /// <param name="document">obiekt typu IDocument, różny od `null`</param>
        void Print(in IDocument document);
    }

    public interface IScanner : IDevice
    {
        int ScanCounter { get; set; }
        // dokument jest skanowany, jeśli urządzenie włączone
        // w przeciwnym przypadku nic się dzieje
        void Scan(out IDocument document, IDocument.FormatType formatType);
    }

    public interface IFax : IDevice
    {
        int FaxCounter { get; set; }
        // dokument jest przesyłany(faxowany?) na podanu numer, jeśli urządzenie włączone
        // w przeciwnym przypadku nic się dzieje
        void SendFax(in IDocument document, int FaxNumber);
    }
}
