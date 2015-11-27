using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Research.NLPCore.Logging
{
    public static class ErrorLogger
    {

        public static void logError(string message)
        {
            Console.WriteLine("Error occured at:" +message);
        }

    }
}
