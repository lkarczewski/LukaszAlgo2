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
            //Random _random = new Random();
            //int matrixSize = 2;
            //var fractionValues = new Ulamek[matrixSize, matrixSize];
            //for (var i = 0; i < matrixSize; i++)
            //{
            //    for (var j = 0; j < matrixSize; j++)
            //    {
            //        var numerator = _random.Next(-65536, 65535);
            //        var denominator = 65536;
            //        if (Math.Abs(numerator) > Math.Abs(denominator))
            //            fractionValues[i, j] = new Ulamek(denominator, numerator);
            //        else
            //            fractionValues[i, j] = new Ulamek(numerator, denominator);
            //    }
            //}

            //MyMatrix<Ulamek> macierz = new MyMatrix<Ulamek>(matrixSize, matrixSize);

            //for (var i = 0; i < matrixSize; i++)
            //{
            //    for (var j = 0; j < matrixSize; j++)
            //    {
            //        macierz[i,j] = fractionValues[i,j];
            //    }
            //}

            //for (var i = 0; i < matrixSize; i++)
            //{
            //    for (var j = 0; j < matrixSize; j++)
            //    {
            //        Console.WriteLine(macierz[i, j]);
            //    }
            //}

            //var fractionVector = new Ulamek[matrixSize];
            //for (var j = 0; j < matrixSize; j++)
            //{
            //    var numerator = _random.Next(10);
            //    var denominator = _random.Next(1, 10);
            //    if (numerator > denominator)
            //        fractionVector[j] = new Ulamek(denominator, numerator);
            //    else
            //        fractionVector[j] = new Ulamek(numerator, denominator);
            //}

            //macierz.GaussWithoutPivot(fractionVector);

            //Console.WriteLine();
            //for (var i = 0; i < matrixSize; i++)
            //{
            //    Console.WriteLine((double)fractionVector[i]);
            //}


            Test t1 = new Test();

            //StreamWriter writer1 = new StreamWriter("CzasGaussWithoutPivot.csv", append: true);
            //writer1.WriteLine("rozmiar;czas-double;czas-float");
            //writer1.Close();
            //for (int i = 10; i <= 200; i += 10)
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
