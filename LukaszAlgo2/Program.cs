﻿using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LukaszAlgo2
{
    class Program
    {
        static void Main(string[] args)
        {
            Test t1 = new Test();
            //t1.TestFraction(3);
            t1.GaussWithoutPivotAccuracyTest(5, 5);

            //StreamWriter writer1 = new StreamWriter("CzasGaussWithoutPivot.csv", append: true);
            //writer1.WriteLine("rozmiar;czas-double;czas-float");
            //writer1.Close();
            //for (int i = 10; i <= 50; i += 10)
            //{
            //    t1.GaussWithoutPivotTimeTest(i, 50);
            //}

            //StreamWriter writer2 = new StreamWriter("CzasGaussPartialPivot.csv", append: true);
            //writer2.WriteLine("rozmiar;czas-double;czas-float");
            //writer2.Close();
            //for (int i = 10; i <= 200; i += 10)
            //{
            //    t1.GaussPartialPivotTimeTest(i, 50);
            //}

            //StreamWriter writer3 = new StreamWriter("CzasGaussFullPivot.csv", append: true);
            //writer3.WriteLine("rozmiar;czas-double;czas-float");
            //writer3.Close();
            //for (int i = 10; i <= 200; i += 10)
            //{
            //    t1.GaussFullPivotTimeTest(i, 50);
            //}

            //StreamWriter writer4 = new StreamWriter("NormaGaussWithoutPivot.csv", append: true);
            //writer4.WriteLine("rozmiar", "norma-double", "norma-float");
            //writer4.Close();
            //for (int i = 10; i <= 100; i += 10)
            //{
            //    t1.GaussWithoutPivotAccuracyTest(i, 50);
            //}
        }
    }
}
