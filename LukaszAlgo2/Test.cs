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
        private readonly Stopwatch stopwatch;
        private TimeSpan time;
        
        //macierze i wektor typu double
        private MyMatrix<double> _macierzDouble1;
        private double[,] macierzDouble1 => (double[,])_macierzDouble1.Matrix.Clone();

        private MyMatrix<double> _macierzDouble2;
        private double[,] macierzDouble2 => (double[,])_macierzDouble2.Matrix.Clone();

        private MyMatrix<double> _macierzDouble3;
        private double[,] macierzDouble3 => (double[,])_macierzDouble3.Matrix.Clone();

        private double[] _wektorDouble;
        private double[] wektorDouble => (double[])_wektorDouble.Clone();

        //macierze i wektor typu float
        private MyMatrix<float> _macierzFloat1;
        private float[,] macierzFloat1 => (float[,])_macierzFloat1.Matrix.Clone();

        private MyMatrix<float> _macierzFloat2;
        private float[,] macierzFloat2 => (float[,])_macierzFloat2.Matrix.Clone();

        private MyMatrix<float> _macierzFloat3;
        private float[,] macierzFloat3 => (float[,])_macierzFloat1.Matrix.Clone();

        private float[] _wektorFloat;
        private float[] wektorFloat => (float[])_wektorFloat.Clone();

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
    }
}
