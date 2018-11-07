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

            for (int i = 0; i < count; i++)
            {
                macierzDouble.GaussFullPivot(wektorDouble);

                for (var j = 0; j < macierzDouble.Rows(); j++)
                {
                    for (var k = 0; k < macierzDouble.Columns(); k++)
                    {
                        macierzDouble[j, k] = _macierzDouble[j, k];
                    }
                }
            }
        }
    }
}
