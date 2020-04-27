using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAdvanced
{
    class GarbageCollection : IDisposable
    {
        Calender calender;
        bool disposing;
        public string GetDate()
        {
            if (disposing)
            {
                throw new ObjectDisposedException("GarbageCollection");
            }
            if(calender == null)
            {
                calender = new Calender();
               
            }
            try
            {
                calender.Date = new DateTime(2015, 12, 25, 10, 30, 45);
                return calender.Date.ToString();
            }
            finally
            {
                calender.Dispose();
                //or, we can also call Dispose method in this class.
                // Dispose();
            }


        }
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposing)
                return;

            if (disposing)
            {
                if(calender != null)
                {
                    calender.Dispose();
                    calender = null;
                }
                this.disposing = true;
            }
        }
    }
    
    class Calender : IDisposable
    {
        public DateTime Date { get; set; }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
