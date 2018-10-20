using System;
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
            double[,] test = new double[3, 3] { { 1, 1, -1 }, { 1, -1, 2 }, { 2, 1, 1 } };
            double[] vector = new double[] { 7.0, 3.0, 9.0 };
            double[] vector1 = (dynamic)vector.Clone();

            MyMatrix<double> macierz1 = new MyMatrix<double>(test);
            //macierz1.GaussWithoutPivot(vector1);
            //macierz1.GaussPartialPivot(vector1);
            macierz1.GaussFullPivot(vector1);

            for (int i = 0; i < macierz1.Rows(); i++)
            {
                Console.WriteLine(vector1[i]);
            }

            //MyMatrix<double> macierz1 = new MyMatrix<double>(5, 5);
            //macierz1.PrintMatrix();

            //MyMatrix<float> macierz2 = new MyMatrix<float>(5, 5);
            //macierz2.PrintMatrix();
        }
    }
}
