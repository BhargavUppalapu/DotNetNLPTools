using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Research.ML.TextExtraction;
//using Com.Research.NLPCore;


namespace ClassificationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string features = "";
            var predictor = new DisclaimerPredict(@"D:\GitHub\DotNetNLPTools\trunk\resources\ModelFiles\VowpalWabbit\disclaimer.vw", @"D:\GitHub\DotNetNLPTools\trunk\resources\ModelFiles\VowpalWabbit\disclaimer.dict", 3000, 5.0f);
            predictor.Predict(features);
        
        }
    }
}
