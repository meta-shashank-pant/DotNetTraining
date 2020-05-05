using System;
using System.Collections.Generic;
using System.Text;

#region Garbage Collection Theory

/// <summary>
/// 1. IDisposable: It Dispose the object of unmanaged resource as soon as their task completed 
///     and also the object of the class implementing IDisposable interface.
///     Unmanaged resouce contains Database, File Handling, etc.
/// 2. Garbage Collection balance between memory and collections.
///     Memory should be Available and Contiguous.
///     Collections should be Infrequent(So they don't slow down the application) and Efficient.
/// 3. There should be static analysis as well use of 
///     profiler(it shows number of objects created by applicationas along with total memory consumed) 
///     to check the performance of the application.
/// 4. BEST PRACTICES:
///     (i) Dispose of IDisposable object as soon as we can.
///     (ii) If we use IDisposable object as instance fields, implement IDisposable
///     (iii) Allow Dispose() method to be called multiple time and don't throw the exceptions.
///     (iv) Implement IDisposable to support disposing resources in a class hierarchy.
///     (v) If we use unmanaged resources, declare a finalizer which cleans them up.
///     (vi) Enable code analysis with CA2000 enabled but don't rely on it.
///     (vii) If you implement an interface and use IDisposable fields, extend our interface with IDisposable
///     (viii) If we implements IDisposable, don't implement it explicitly.
/// </summary>

#endregion

namespace CSharpAdvanced
{
    /// <summary>
    /// The class GarbageCollection implements IDisposable interface that means, it can be disposed in the calling class.
    /// This class performs the basic function of getting and setting date and return to callee.
    /// </summary>
    class GarbageCollection : IDisposable
    {
        Calender calender;

        //Current stat of the variable/object, true if disposed and false otherwise.
        bool disposing;

        /// <summary>
        /// Hardcoded value of Date is seted.
        /// </summary>
        /// <returns>It is string holding the date.</returns>
        public string GetDate()
        {
            //If object is already disposed this condition will be true.
            if (disposing)
            {
                throw new ObjectDisposedException("GarbageCollection");
            }
            //If calender object is null then an object of that type is created.
            if(calender == null)
            {
                calender = new Calender();
               
            }
            //Calender object property will get assigned and returned.
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

        /// <summary>
        /// Object is disposed with help of this method it can be done,
        /// manually or it can be called automatically by "using" statement.
        /// </summary>
        public void Dispose()
        {
            //Here another dispose method from this class is called, this is the part of best practices.
            Dispose(true);
        }

        /// <summary>
        /// It is class method and can be used by child class of this class, if any.
        /// Dispose the object which will not come in use further on the program.
        /// This will reduce the overhead for garbage collection.
        /// </summary>
        /// <param name="disposing">Boolean parameter telling whether to dispose the object or not.</param>
        protected virtual void Dispose(bool disposing)
        {
            //If object is already disposed.
            if (this.disposing)
                return;

            //If object not disposed, it will dispose the calender object and make it null and also set the "disposing" class
            // field to true.
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
    
    /// <summary>
    /// This class is used only to set and get the Date and it is also disposable.
    /// </summary>
    class Calender : IDisposable
    {
        public DateTime Date { get; set; }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
