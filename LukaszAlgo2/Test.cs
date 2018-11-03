using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace LukaszAlgo2
{
    class Test
    {
        public void GaussWithoutPivotTest(int size, int count)
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
                    }
                }
                sumaFloat += czasyFloat[i];
            }

            sredniaDouble = sumaDouble / count;
            sredniaFloat = sumaFloat / count;
            Console.WriteLine("Średni czas DOUBLE: " + sredniaDouble + "ms");
            Console.WriteLine("Średni czas FLOAT: " + sredniaFloat + "ms");
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
    }
}
