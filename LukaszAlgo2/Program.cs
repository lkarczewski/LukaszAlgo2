using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LukaszAlgo2
{
    class Program
    {
        static void Main(string[] args)
        {
            MyMatrix<double> macierzDouble1 = new MyMatrix<double>(100, 100);
            MyMatrix<double> macierzDouble2 = new MyMatrix<double>(250, 250);
            MyMatrix<double> macierzDouble3 = new MyMatrix<double>(500, 500);
            MyMatrix<float> macierzFloat1 = new MyMatrix<float>(100,100);
            MyMatrix<float> macierzFloat2 = new MyMatrix<float>(250, 250);
            MyMatrix<float> macierzFloat3 = new MyMatrix<float>(500, 500);
            double[] _wektorDouble1 = new double[macierzDouble1.Rows()];
            double[] _wektorDouble2 = new double[macierzDouble2.Rows()];
            double[] _wektorDouble3 = new double[macierzDouble3.Rows()];
            float[] _wektorFloat1 = new float[macierzFloat1.Rows()];
            float[] _wektorFloat2 = new float[macierzFloat2.Rows()];
            float[] _wektorFloat3 = new float[macierzFloat3.Rows()];

            var random = new Random();
            double r;

            for (var i = 0; i < macierzDouble1.Rows(); i++)
            {
                r = random.Next(-65536, 65535);
                _wektorDouble1[i] = (dynamic)(r / 65536);
            }

            for (var i = 0; i < macierzDouble2.Rows(); i++)
            {
                r = random.Next(-65536, 65535);
                _wektorDouble2[i] = (dynamic)(r / 65536);
            }

            for (var i = 0; i < macierzDouble3.Rows(); i++)
            {
                r = random.Next(-65536, 65535);
                _wektorDouble3[i] = (dynamic)(r / 65536);
            }

            double[] wektorDouble1 = (dynamic)_wektorDouble1.Clone();
            double[] wektorDouble2 = (dynamic)_wektorDouble1.Clone();
            double[] wektorDouble3 = (dynamic)_wektorDouble1.Clone();

            for (var i = 0; i < macierzFloat1.Rows(); i++)
            {
                r = random.Next(-65536, 65535);
                _wektorFloat1[i] = (dynamic)(float)(r / 65536);
            }

            for (var i = 0; i < macierzFloat2.Rows(); i++)
            {
                r = random.Next(-65536, 65535);
                _wektorFloat2[i] = (dynamic)(float)(r / 65536);
            }

            for (var i = 0; i < macierzFloat3.Rows(); i++)
            {
                r = random.Next(-65536, 65535);
                _wektorFloat3[i] = (dynamic)(float)(r / 65536);
            }

            float[] wektorFloat1 = (dynamic)_wektorFloat1.Clone();
            float[] wektorFloat2 = (dynamic)_wektorFloat2.Clone();
            float[] wektorFloat3 = (dynamic)_wektorFloat3.Clone();

            var watchDouble1 = Stopwatch.StartNew();
            macierzDouble1.GaussWithoutPivot(_wektorDouble1);
            watchDouble1.Stop();
            var elapsedMsDouble1 = watchDouble1.ElapsedMilliseconds;
            Console.WriteLine("Macierz double 100x100: " + elapsedMsDouble1);

            var watchDouble2 = Stopwatch.StartNew();
            macierzDouble2.GaussWithoutPivot(_wektorDouble2);
            watchDouble2.Stop();
            var elapsedMsDouble2 = watchDouble2.ElapsedMilliseconds;
            Console.WriteLine("Macierz double 250x250: " + elapsedMsDouble2);

            var watchDouble3 = Stopwatch.StartNew();
            macierzDouble3.GaussWithoutPivot(_wektorDouble3);
            watchDouble3.Stop();
            var elapsedMsDouble3 = watchDouble3.ElapsedMilliseconds;
            Console.WriteLine("Macierz double 500x500: " + elapsedMsDouble3);

            var watchFloat1 = Stopwatch.StartNew();
            macierzFloat1.GaussWithoutPivot(_wektorFloat1);
            watchFloat1.Stop();
            var elapsedMsFloat1 = watchFloat1.ElapsedMilliseconds;
            Console.WriteLine("Macierz float 100x100: " + elapsedMsFloat1);

            var watchFloat2 = Stopwatch.StartNew();
            macierzFloat2.GaussWithoutPivot(_wektorFloat2);
            watchFloat2.Stop();
            var elapsedMsFloat2 = watchFloat2.ElapsedMilliseconds;
            Console.WriteLine("Macierz float 250x250: " + elapsedMsFloat2);

            var watchFloat3 = Stopwatch.StartNew();
            macierzFloat3.GaussWithoutPivot(_wektorFloat3);
            watchFloat3.Stop();
            var elapsedMsFloat3 = watchFloat3.ElapsedMilliseconds;
            Console.WriteLine("Macierz float 500x500: " + elapsedMsFloat3);
        }
    }
}
