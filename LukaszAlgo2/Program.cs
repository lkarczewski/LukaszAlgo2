using System;
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

            //TESTY WYDAJNOŚCI

            //StreamWriter writer = new StreamWriter("CzasGaussWithoutPivotUlamek.csv", append: true);
            //writer.WriteLine("rozmiar;czas-ulamek");
            //writer.Close();
            //for (int i = 10; i <= 80 += 10)
            //{
            //    t1.GaussWithoutPivotTimeTest(i, 5);
            //}
            //StreamWriter writer = new StreamWriter("CzasGaussPartialPivotUlamek.csv", append: true);
            //writer.WriteLine("rozmiar;czas-ulamek");
            //writer.Close();
            //for (int i = 10; i <= 80; i += 10)
            //{
            //    t1.GaussPartialPivotTimeTest(i, 5);
            //}

            //StreamWriter writer = new StreamWriter("CzasGaussFullPivotUlamek.csv", append: true);
            //writer.WriteLine("rozmiar;czas-ulamek");
            //writer.Close();
            //for (int i = 10; i <= 80; i += 10)
            //{
            //    t1.GaussFullPivotTimeTest(i, 5);
            //}

            //StreamWriter writer = new StreamWriter("CzasGaussWithoutPivot.csv", append: true);
            //writer.WriteLine("rozmiar;czas-float;czas-double");
            //writer.Close();
            //for (int i = 10; i <= 250; i += 10)
            //{
            //    t1.GaussWithoutPivotTimeTest(i, 250);
            //}

            //StreamWriter writer = new StreamWriter("CzasGaussPartialPivot.csv", append: true);
            //writer.WriteLine("rozmiar;czas-float;czas-double");
            //writer.Close();
            //for (int i = 10; i <= 250; i += 10)
            //{
            //    t1.GaussPartialPivotTimeTest(i, 250);
            //}


            //StreamWriter writer = new StreamWriter("CzasGaussFullPivot.csv", append: true);
            //writer.WriteLine("rozmiar;czas-float;czas-double");
            //writer.Close();
            //for (int i = 10; i <= 250; i += 10)
            //{
            //    t1.GaussFullPivotTimeTest(i, 250);
            //}

            //StreamWriter writer = new StreamWriter("CzasGaussWithoutPivot.csv", append: true);
            //writer.WriteLine("rozmiar;czas-double;czas-float");
            //writer.Close();
            //t1.GaussWithoutPivotTimeTest(500,50);

            //StreamWriter writer = new StreamWriter("CzasGaussPartialPivot.csv", append: true);
            //writer.WriteLine("rozmiar;czas-float;czas-double");
            //writer.Close();
            //t1.GaussPartialPivotTimeTest(500,50);

            //StreamWriter writer = new StreamWriter("CzasGaussFullPivot.csv", append: true);
            //writer.WriteLine("rozmiar;czas-float;czas-double");
            //writer.Close();
            //t1.GaussFullPivotTimeTest(500,50);

            //TESTY DOKŁADNOŚCI

            //StreamWriter writer = new StreamWriter("NormaGaussWithoutPivot.csv", append: true);
            //writer.WriteLine("rozmiar", "norma-double", "norma-float");
            //writer.Close();
            //for (int i = 10; i <= 100; i += 10)
            //{
            //    t1.GaussWithoutPivotAccuracyTest(i, 250);
            //}
            //
            //for (int i = 110; i <= 250; i += 10)
            //{
            //    t1.GaussWithoutPivotAccuracyTest(i, 50);
            //}

            //StreamWriter writer = new StreamWriter("NormaGaussPartialPivot.csv", append: true);
            //writer.WriteLine("rozmiar", "norma-double", "norma-float");
            //writer.Close();
            //for (int i = 10; i <= 100; i += 10)
            //{
            //    t1.GaussPartialPivotAccuracyTest(i, 250);
            //}
            //
            //for (int i = 110; i <= 250; i += 10)
            //{
            //    t1.GaussPartialPivotAccuracyTest(i, 50);
            //}

            //StreamWriter writer = new StreamWriter("NormaGaussFullPivot.csv", append: true);
            //writer.WriteLine("rozmiar", "norma-double", "norma-float");
            //writer.Close();
            //for (int i = 10; i <= 100; i += 10)
            //{
            //    t1.GaussPartialPivotAccuracyTest(i, 250);
            //}
            //
            //for (int i = 110; i <= 250; i += 10)
            //{
            //    t1.GaussFullPivotAccuracyTest(i, 50);
            //}
        }
    }
}
