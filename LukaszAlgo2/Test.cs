using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace LukaszAlgo2
{
    class Test
    {
        public Ulamek[] GenerateFractionMatrix (int size)
        {
            Random _random = new Random();
            int matrixSize = 2;
            var fractionValues = new Ulamek[matrixSize, matrixSize];
            for (var i = 0; i < matrixSize; i++)
            {
                for (var j = 0; j < matrixSize; j++)
                {
                    var numerator = _random.Next(-65536, 65535);
                    var denominator = 65536;
                    if (Math.Abs(numerator) > Math.Abs(denominator))
                        fractionValues[i, j] = new Ulamek(denominator, numerator);
                    else
                        fractionValues[i, j] = new Ulamek(numerator, denominator);
                }
            }

            MyMatrix<Ulamek> macierz = new MyMatrix<Ulamek>(matrixSize, matrixSize);

            for (var i = 0; i < matrixSize; i++)
            {
                for (var j = 0; j < matrixSize; j++)
                {
                    macierz[i,j] = fractionValues[i,j];
                }
            }

            for (var i = 0; i < matrixSize; i++)
            {
                for (var j = 0; j < matrixSize; j++)
                {
                    Console.WriteLine(macierz[i, j]);
                }
            }

            var fractionVector = new Ulamek[matrixSize];
            for (var j = 0; j < matrixSize; j++)
            {
                var numerator = _random.Next(10);
                var denominator = _random.Next(1, 10);
                if (numerator > denominator)
                    fractionVector[j] = new Ulamek(denominator, numerator);
                else
                    fractionVector[j] = new Ulamek(numerator, denominator);
            }
        }

        public double[] GenerateDoubleVector(int size)
        {
            var random = new Random();
            var doubleVector = new double[size];
            double r;

            for (var i = 0; i < size; i++)
            {
                r = random.Next(-65536, 65535);
                doubleVector[i] = (dynamic)(r / 65536);
            }

            return doubleVector;
        }

        public float[] GenerateFloatVector(int size)
        {
            var random = new Random();
            var floatVector = new float[size];
            double r;

            for (var i = 0; i < size; i++)
            {
                r = random.Next(-65536, 65535);
                floatVector[i] = (dynamic)(float)(r / 65536);
            }

            return floatVector;
        }

        public void GaussWithoutPivotTimeTest(int size, int count)
        { 
            MyMatrix<double> macierzDouble = new MyMatrix<double>(size, size);
            MyMatrix<double> _macierzDouble = new MyMatrix<double>(size, size);
            MyMatrix<float> macierzFloat = new MyMatrix<float>(size, size);
            MyMatrix<float> _macierzFloat = new MyMatrix<float>(size, size);
            double[] wektorDouble = GenerateDoubleVector(size);
            double[] _wektorDouble = GenerateDoubleVector(size);
            float[] wektorFloat = GenerateFloatVector(size);
            float[] _wektorFloat = GenerateFloatVector(size);

            for (var i = 0; i < macierzDouble.Rows(); i++)
            {
                for (var j = 0; j < macierzDouble.Columns(); j++)
                {
                    macierzFloat[i, j] = (float)macierzDouble[i, j];
                    _macierzDouble[i, j] = macierzDouble[i, j];
                    _macierzFloat[i, j] = macierzFloat[i, j];
                }
            }

            for (var i = 0; i < wektorDouble.Length; i++)
            {
                wektorFloat[i] = (float)wektorDouble[i];
                _wektorDouble[i] = wektorDouble[i];
                _wektorFloat[i] = wektorFloat[i];
            }

            int[] czasyDouble = new int[count];
            int[] czasyFloat = new int[count];
            double sumaDouble = 0;
            double sumaFloat = 0;
            double sredniaDouble = 0;
            double sredniaFloat = 0;

            for (int i = 0; i < count; i++)
            {
                var watchDouble = Stopwatch.StartNew();
                macierzDouble.GaussWithoutPivot(wektorDouble);
                watchDouble.Stop();
                var elapsedMsDouble = watchDouble.ElapsedMilliseconds;
                czasyDouble[i] = (int)elapsedMsDouble;

                for (var j = 0; j < macierzDouble.Rows(); j++)
                {
                    for (var k = 0; k < macierzDouble.Columns(); k++)
                    {
                        macierzDouble[j, k] = _macierzDouble[j, k];
                        wektorDouble[j] = _wektorDouble[j];
                    }
                }
                sumaDouble += czasyDouble[i];
            }

            for (int i = 0; i < count; i++)
            {
                var watchFloat = Stopwatch.StartNew();
                macierzFloat.GaussWithoutPivot(wektorFloat);
                watchFloat.Stop();
                var elapsedMsFloat = watchFloat.ElapsedMilliseconds;
                czasyFloat[i] = (int)elapsedMsFloat;

                for (var j = 0; j < macierzFloat.Rows(); j++)
                {
                    for (var k = 0; k < macierzFloat.Columns(); k++)
                    {
                        macierzFloat[j, k] = _macierzFloat[j, k];
                        wektorFloat[j] = _wektorFloat[j];
                    }
                }
                sumaFloat += czasyFloat[i];
            }

            sredniaDouble = sumaDouble / count;
            sredniaFloat = sumaFloat / count;

            StreamWriter writer = new StreamWriter("CzasGaussWithoutPivot.csv", append: true);
            if (writer != null)
            {
                writer.WriteLine(String.Format(size + "x" + size + ";" + sredniaDouble + ";" + sredniaFloat));
            }
            writer.Close();

            Console.WriteLine("GAUSS WITHOUT PIVOT TEST: " + size + "x" + size);
            Console.WriteLine("Średni czas DOUBLE: " + sredniaDouble + "ms");
            Console.WriteLine("Średni czas FLOAT: " + sredniaFloat + "ms");
        }
        
        public void GaussPartialPivotTimeTest(int size, int count)
        {
            MyMatrix<double> macierzDouble = new MyMatrix<double>(size, size);
            MyMatrix<double> _macierzDouble = new MyMatrix<double>(size, size);
            MyMatrix<float> macierzFloat = new MyMatrix<float>(size, size);
            MyMatrix<float> _macierzFloat = new MyMatrix<float>(size, size);
            double[] wektorDouble = GenerateDoubleVector(size);
            double[] _wektorDouble = GenerateDoubleVector(size);
            float[] wektorFloat = GenerateFloatVector(size);
            float[] _wektorFloat = GenerateFloatVector(size);

            for (var i = 0; i < macierzDouble.Rows(); i++)
            {
                for (var j = 0; j < macierzDouble.Columns(); j++)
                {
                    macierzFloat[i, j] = (float)macierzDouble[i, j];
                    _macierzDouble[i, j] = macierzDouble[i, j];
                    _macierzFloat[i, j] = macierzFloat[i, j];
                }
            }

            for (var i = 0; i < wektorDouble.Length; i++)
            {
                wektorFloat[i] = (float)wektorDouble[i];
                _wektorDouble[i] = wektorDouble[i];
                _wektorFloat[i] = wektorFloat[i];
            }

            int[] czasyDouble = new int[count];
            int[] czasyFloat = new int[count];
            double sumaDouble = 0;
            double sumaFloat = 0;
            double sredniaDouble = 0;
            double sredniaFloat = 0;

            for (int i = 0; i < count; i++)
            {
                var watchDouble = Stopwatch.StartNew();
                macierzDouble.GaussPartialPivot(wektorDouble);
                watchDouble.Stop();
                var elapsedMsDouble = watchDouble.ElapsedMilliseconds;
                czasyDouble[i] = (int)elapsedMsDouble;

                for (var j = 0; j < macierzDouble.Rows(); j++)
                {
                    for (var k = 0; k < macierzDouble.Columns(); k++)
                    {
                        macierzDouble[j, k] = _macierzDouble[j, k];
                        wektorDouble[j] = _wektorDouble[j];
                    }
                }
                sumaDouble += czasyDouble[i];
            }

            for (int i = 0; i < count; i++)
            {
                var watchFloat = Stopwatch.StartNew();
                macierzFloat.GaussPartialPivot(wektorFloat);
                watchFloat.Stop();
                var elapsedMsFloat = watchFloat.ElapsedMilliseconds;
                czasyFloat[i] = (int)elapsedMsFloat;

                for (var j = 0; j < macierzFloat.Rows(); j++)
                {
                    for (var k = 0; k < macierzFloat.Columns(); k++)
                    {
                        macierzFloat[j, k] = _macierzFloat[j, k];
                        wektorFloat[j] = _wektorFloat[j];
                    }
                }
                sumaFloat += czasyFloat[i];
            }

            sredniaDouble = sumaDouble / count;
            sredniaFloat = sumaFloat / count;

            StreamWriter writer = new StreamWriter("CzasGaussPartialPivot.csv", append: true);
            if (writer != null)
            {
                writer.WriteLine(String.Format(size + "x" + size + ";" + sredniaDouble + ";" + sredniaFloat));
            }
            writer.Close();

            Console.WriteLine("GAUSS PARTIAL PIVOT TEST: " + size + "x" + size);
            Console.WriteLine("Średni czas DOUBLE: " + sredniaDouble + "ms");
            Console.WriteLine("Średni czas FLOAT: " + sredniaFloat + "ms");
        }

        public void GaussFullPivotTimeTest(int size, int count)
        {
            MyMatrix<double> macierzDouble = new MyMatrix<double>(size, size);
            MyMatrix<double> _macierzDouble = new MyMatrix<double>(size, size);
            MyMatrix<float> macierzFloat = new MyMatrix<float>(size, size);
            MyMatrix<float> _macierzFloat = new MyMatrix<float>(size, size);
            double[] wektorDouble = GenerateDoubleVector(size);
            double[] _wektorDouble = GenerateDoubleVector(size);
            float[] wektorFloat = GenerateFloatVector(size);
            float[] _wektorFloat = GenerateFloatVector(size);

            for (var i = 0; i < macierzDouble.Rows(); i++)
            {
                for (var j = 0; j < macierzDouble.Columns(); j++)
                {
                    macierzFloat[i, j] = (float)macierzDouble[i, j];
                    _macierzDouble[i, j] = macierzDouble[i, j];
                    _macierzFloat[i, j] = macierzFloat[i, j];
                }
            }

            for (var i = 0; i < wektorDouble.Length; i++)
            {
                wektorFloat[i] = (float)wektorDouble[i];
                _wektorDouble[i] = wektorDouble[i];
                _wektorFloat[i] = wektorFloat[i];
            }

            int[] czasyDouble = new int[count];
            int[] czasyFloat = new int[count];
            double sumaDouble = 0;
            double sumaFloat = 0;
            double sredniaDouble = 0;
            double sredniaFloat = 0;

            for (int i = 0; i < count; i++)
            {
                var watchDouble = Stopwatch.StartNew();
                macierzDouble.GaussFullPivot(wektorDouble);
                watchDouble.Stop();
                var elapsedMsDouble = watchDouble.ElapsedMilliseconds;
                czasyDouble[i] = (int)elapsedMsDouble;

                for (var j = 0; j < macierzDouble.Rows(); j++)
                {
                    for (var k = 0; k < macierzDouble.Columns(); k++)
                    {
                        macierzDouble[j, k] = _macierzDouble[j, k];
                        wektorDouble[j] = _wektorDouble[j];
                    }
                }
                sumaDouble += czasyDouble[i];
            }

            for (int i = 0; i < count; i++)
            {
                var watchFloat = Stopwatch.StartNew();
                macierzFloat.GaussFullPivot(wektorFloat);
                watchFloat.Stop();
                var elapsedMsFloat = watchFloat.ElapsedMilliseconds;
                czasyFloat[i] = (int)elapsedMsFloat;

                for (var j = 0; j < macierzFloat.Rows(); j++)
                {
                    for (var k = 0; k < macierzFloat.Columns(); k++)
                    {
                        macierzFloat[j, k] = _macierzFloat[j, k];
                        wektorFloat[j] = _wektorFloat[j];
                    }
                }
                sumaFloat += czasyFloat[i];
            }
            sredniaDouble = sumaDouble / count;
            sredniaFloat = sumaFloat / count;

            StreamWriter writer = new StreamWriter("CzasGaussFullPivot.csv", append: true);
            if (writer != null)
            {
                writer.WriteLine(String.Format(size + "x" + size + ";" + sredniaDouble + ";" + sredniaFloat));
            }
            writer.Close();

            Console.WriteLine("GAUSS FULL PIVOT TEST: " + size + "x" + size);
            Console.WriteLine("Średni czas DOUBLE: " + sredniaDouble + "ms");
            Console.WriteLine("Średni czas FLOAT: " + sredniaFloat + "ms");
        }

        public void GaussWithoutPivotAccuracyTest(int size, int count)
        {
            MyMatrix<double> macierzDoubleA = new MyMatrix<double>(size, size);
            MyMatrix<double> _macierzDoubleA = new MyMatrix<double>(size, size);
            MyMatrix<float> macierzFloatA = new MyMatrix<float>(size, size);
            MyMatrix<float> _macierzFloatA = new MyMatrix<float>(size, size);
            double[] wektorDoubleX = GenerateDoubleVector(size);
            double[] wektorDoubleB = new double[size];
            double[] _wektorDoubleB = new double[size];
            float[] wektorFloatX = GenerateFloatVector(size);
            float[] wektorFloatB = new float[size];
            float[] _wektorFloatB = new float[size];
            double[] wektorNormyDouble = new double[size];
            double normaDouble = 0;
            double sredniaNormaDouble = 0;
            float[] wektorNormyFloat = new float[size];
            float normaFloat = 0;
            float sredniaNormaFloat = 0;

            //PRZYGOTOWANIE MACIERZY I WEKTORÓW
            for (var i = 0; i < macierzDoubleA.Rows(); i++)
            {
                for (var j = 0; j < macierzDoubleA.Columns(); j++)
                {
                    macierzFloatA[i, j] = (float)macierzDoubleA[i, j];
                    _macierzDoubleA[i, j] = macierzDoubleA[i, j];
                    _macierzFloatA[i, j] = macierzFloatA[i, j];
                }
            }

            for (var i = 0; i < wektorDoubleX.Length; i++)
            {
                wektorFloatX[i] = (float)wektorDoubleX[i];
            }

            //LICZENIE DOUBLE
            for (var i = 0; i < macierzDoubleA.Rows(); i++)
            {
                for (var j = 0; j < macierzDoubleA.Columns(); j++)
                {
                    wektorDoubleB[i] += macierzDoubleA[i, j] * wektorDoubleX[j]; 
                }
            }

            for (var i = 0; i < wektorDoubleB.Length; i++)
            {
                _wektorDoubleB[i] = wektorDoubleB[i];
            }

            //Console.WriteLine("Wektor normy double: ");
            for (var i = 0; i < count; i++)
            {
                macierzDoubleA.GaussWithoutPivot(wektorDoubleB);
                for (var j = 0; j < wektorDoubleX.Length; j++)
                {
                    wektorNormyDouble[j] = wektorDoubleB[j] - wektorDoubleX[j];
                    for (var k = 0; k < wektorNormyDouble.Length; k++)
                    {
                        normaDouble += Math.Pow(wektorNormyDouble[k], 2);
                        macierzDoubleA[j, k] = _macierzDoubleA[j, k];
                        wektorDoubleB[j] = _wektorDoubleB[j];
                    }
                    //Console.WriteLine(wektorNormyDouble[j]);
                    wektorNormyDouble[j] = 0;
                }
            }
            sredniaNormaDouble = normaDouble / count;

            //LICZENIE FLOAT

            for (var i = 0; i < macierzFloatA.Rows(); i++)
            {
                for (var j = 0; j < macierzFloatA.Columns(); j++)
                {
                    wektorFloatB[i] += macierzFloatA[i, j] * wektorFloatX[j];
                }
            }

            for (var i = 0; i < wektorFloatB.Length; i++)
            {
                _wektorFloatB[i] = wektorFloatB[i];
            }

            //Console.WriteLine("Wektor normy float: ");
            for (var i = 0; i < count; i++)
            {
                macierzFloatA.GaussWithoutPivot(wektorFloatB);
                for (var j = 0; j < wektorFloatX.Length; j++)
                {
                    wektorNormyFloat[j] = wektorFloatB[j] - wektorFloatX[j];
                    for (var k = 0; k < wektorNormyFloat.Length; k++)
                    {
                        normaFloat += (float)Math.Pow(wektorNormyFloat[k], 2);
                        macierzFloatA[j, k] = _macierzFloatA[j, k];
                        wektorFloatB[j] = _wektorFloatB[j];
                    }
                    //Console.WriteLine(wektorNormyFloat[j]);
                    wektorNormyFloat[j] = 0;
                }
            }
            sredniaNormaFloat = normaFloat / count;

            StreamWriter writer = new StreamWriter("NormaGaussWithoutPivot.csv", append: true);
            if (writer != null)
            {
                writer.WriteLine(String.Format(size + "x" + size + ";" + normaDouble + ";" + normaFloat));
            }
            writer.Close();

            Console.WriteLine("Norma double: " + normaDouble);
            Console.WriteLine("Średnia norma double: " + sredniaNormaDouble);
            Console.WriteLine("Norma float: " + normaFloat);
            Console.WriteLine("Średnia norma float: " + sredniaNormaFloat);
        }
    }
}
