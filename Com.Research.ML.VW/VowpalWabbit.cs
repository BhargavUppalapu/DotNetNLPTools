using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Research.ML
{
    public class VowpalWabbit : IDisposable
    {
        private IntPtr _vw;

        public VowpalWabbit(string options)
        {
            _vw = VowpalWabbitInterface.Initialize(options);
        }

        public float Learn(string example)
        {
            IntPtr examplePtr = IntPtr.Zero;
            try
            {
                examplePtr = VowpalWabbitInterface.ReadExample(_vw, example);
                return VowpalWabbitInterface.Learn(_vw, examplePtr);
            }
            finally
            {
                if (examplePtr != IntPtr.Zero)
                    VowpalWabbitInterface.FinishExample(_vw, examplePtr);
            }
        }


        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {}

            if (_vw != IntPtr.Zero)
            {
                VowpalWabbitInterface.Finish(_vw);
                _vw = IntPtr.Zero;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
