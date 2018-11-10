using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Numerics;


namespace LukaszAlgo2
{
    class Test
    {
        public Ulamek[] GenerateRandomFractionVector(int size)
        {
            Random _random = new Random();
            var fractionVector = new Ulamek[size];
            for (var i = 0; i < size; i++)
            {
                var numerator = _random.Next(-65536, 65535);
                var denominator = 65536;
                if (numerator > denominator)
                    fractionVector[i] = new Ulamek(denominator, numerator);
                else
                    fractionVector[i] = new Ulamek(numerator, denominator);
            }
            return fractionVector;
        }

        public MyMatrix<double> DoubleMatrixFromFraction(MyMatrix<Ulamek> m)
        {
            var values = new double[m.Rows(), m.Columns()];
            for (var i = 0; i < m.Rows(); i++)
            {
                for (var j = 0; j < m.Columns(); j++)
                {
                    values[i, j] = (double)m.Matrix[i, j].licznik /
                                   (double)m.Matrix[i, j].mianownik;
                }
            }

            return new MyMatrix<double>(values);
        }

        public MyMatrix<float> FloatMatrixFromFraction(MyMatrix<Ulamek> m)
        {
            var values = new float[m.Rows(), m.Columns()];
            for (var i = 0; i < m.Rows(); i++)
            {
                for (var j = 0; j < m.Columns(); j++)
                {
                    values[i, j] = (float)m.Matrix[i, j].licznik /
                                   (float)m.Matrix[i, j].mianownik;
                }
            }

            return new MyMatrix<float>(values);
        }

        public double[] DoubleVectorFromFraction(Ulamek[] vector)
        {
            var values = new double[vector.Length];
            for (var j = 0; j < vector.Length; j++)
            {
                values[j] = (double)vector[j].licznik / (double)vector[j].mianownik;
            }

            return values;
        }

        public float[] FloatVectorFromFraction(Ulamek[] vector)
        {
            var values = new float[vector.Length];
            for (var j = 0; j < vector.Length; j++)
            {
                values[j] = (float)vector[j].licznik / (float)vector[j].mianownik;
            }

            return values;
        }

        public Ulamek[] CalculatedFractionVectorB(MyMatrix<Ulamek> m, Ulamek[] vector)
        {
            int size = vector.Length;
            var values = new Ulamek[size];
            for (var i = 0; i < m.Rows(); i++)
            {
                for (var j = 0; j < m.Columns(); j++)
                {
                    //wiem, że to jest źle, to jest ogólna postać tego co chcę zrobić bez żadnych rzutowań
                    values[i] = values[i] + m.Matrix[i, j].licznik / m.Matrix[i, j].mianownik * vector[j].licznik / vector[j].mianownik;
                }
            }

            return values;
        }

        public void TestFraction(int size)
        {
            MyMatrix<Ulamek> macierzUlamekA = new MyMatrix<Ulamek>(size, size);
            Ulamek[] wektorUlamekX = GenerateRandomFractionVector(size);
            Ulamek[] wektorUlamekB = new Ulamek[size];

            wektorUlamekB = CalculatedFractionVectorB(macierzUlamekA, wektorUlamekX);
        }

        public void GaussWithoutPivotTimeTest(int size, int count)
        {
            MyMatrix<Ulamek> macierzUlamek = new MyMatrix<Ulamek>(size, size);
            MyMatrix<Ulamek> _macierzUlamek = new MyMatrix<Ulamek>(size, size);
            MyMatrix<double> macierzDouble = new MyMatrix<double>(size, size);
            MyMatrix<double> _macierzDouble = new MyMatrix<double>(size, size);
            MyMatrix<float> macierzFloat = new MyMatrix<float>(size, size);
            MyMatrix<float> _macierzFloat = new MyMatrix<float>(size, size);
            Ulamek[] wektorUlamek = GenerateRandomFractionVector(size);
            Ulamek[] _wektorUlamek = new Ulamek[size];
            double[] wektorDouble = new double[size];
            double[] _wektorDouble = new double[size];
            float[] wektorFloat = new float[size];
            float[] _wektorFloat = new float[size];


            //Przygotwanie macierzy i wektorów
            macierzDouble = DoubleMatrixFromFraction(macierzUlamek);
            macierzFloat = FloatMatrixFromFraction(macierzUlamek);
            wektorDouble = DoubleVectorFromFraction(wektorUlamek);
            wektorFloat = FloatVectorFromFraction(wektorUlamek);

            //kopiowanie macierzy i wektorów
            for (var i = 0; i < macierzUlamek.Rows(); i++)
            {
                for (var j = 0; j < macierzUlamek.Columns(); j++)
                {
                    _macierzUlamek[i, j] = macierzUlamek[i, j];
                    _macierzDouble[i, j] = macierzDouble[i, j];
                    _macierzFloat[i, j] = macierzFloat[i, j];
                }
            }

            for (var i = 0; i < wektorUlamek.Length; i++)
            {
                _wektorUlamek[i] = wektorUlamek[i];
                _wektorDouble[i] = wektorDouble[i];
                _wektorFloat[i] = wektorFloat[i];
            }

            //liczenie czasu
            double[] czasyUlamek = new double[count];
            double[] czasyDouble = new double[count];
            double[] czasyFloat = new double[count];
            double sumaUlamek = 0.0;
            double sumaDouble = 0.0;
            double sumaFloat = 0.0;
            double sredniaUlamek = 0.0;
            double sredniaDouble = 0.0;
            double sredniaFloat = 0.0;

            //CZAS UŁAMKA
            for (int i = 0; i < count; i++)
            {
                var watchFraction = Stopwatch.StartNew();
                macierzUlamek.GaussWithoutPivot(wektorUlamek);
                watchFraction.Stop();
                var elapsedMsFraction = watchFraction.ElapsedMilliseconds;
                czasyUlamek[i] = elapsedMsFraction;

                for (var j = 0; j < macierzUlamek.Rows(); j++)
                {
                    for (var k = 0; k < macierzUlamek.Columns(); k++)
                    {
                        macierzUlamek[j, k] = _macierzUlamek[j, k];
                        wektorUlamek[j] = _wektorUlamek[j];
                    }
                }
                sumaUlamek += czasyUlamek[i];
            }

            //CZAS DOUBLE
            for (int i = 0; i < count; i++)
            {
                var watchDouble = Stopwatch.StartNew();
                macierzDouble.GaussWithoutPivot(wektorDouble);
                watchDouble.Stop();
                var elapsedMsDouble = watchDouble.ElapsedMilliseconds;
                czasyDouble[i] = elapsedMsDouble;

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

            //CZAS FLOAT
            for (int i = 0; i < count; i++)
            {
                var watchFloat = Stopwatch.StartNew();
                macierzFloat.GaussWithoutPivot(wektorFloat);
                watchFloat.Stop();
                var elapsedMsFloat = watchFloat.ElapsedMilliseconds;
                czasyFloat[i] = elapsedMsFloat;

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

            sredniaUlamek = sumaUlamek / count;
            sredniaDouble = sumaDouble / count;
            sredniaFloat = sumaFloat / count;

            StreamWriter writer = new StreamWriter("CzasGaussWithoutPivot.csv", append: true);
            if (writer != null)
            {
                writer.WriteLine(String.Format(size + "x" + size + ";" + sredniaUlamek + ";" + sredniaDouble + ";" + sredniaFloat));
            }
            writer.Close();

            Console.WriteLine("GAUSS WITHOUT PIVOT TEST: " + size + "x" + size);
            Console.WriteLine("Średni czas FRACTION: " + sredniaUlamek + "ms");
            Console.WriteLine("Średni czas DOUBLE: " + sredniaDouble + "ms");
            Console.WriteLine("Średni czas FLOAT: " + sredniaFloat + "ms");
        }

        public void GaussPartialPivotTimeTest(int size, int count)
        {
            MyMatrix<Ulamek> macierzUlamek = new MyMatrix<Ulamek>(size, size);
            MyMatrix<Ulamek> _macierzUlamek = new MyMatrix<Ulamek>(size, size);
            MyMatrix<double> macierzDouble = new MyMatrix<double>(size, size);
            MyMatrix<double> _macierzDouble = new MyMatrix<double>(size, size);
            MyMatrix<float> macierzFloat = new MyMatrix<float>(size, size);
            MyMatrix<float> _macierzFloat = new MyMatrix<float>(size, size);
            Ulamek[] wektorUlamek = GenerateRandomFractionVector(size);
            Ulamek[] _wektorUlamek = new Ulamek[size];
            double[] wektorDouble = new double[size];
            double[] _wektorDouble = new double[size];
            float[] wektorFloat = new float[size];
            float[] _wektorFloat = new float[size];


            //Przygotwanie macierzy i wektorów
            macierzDouble = DoubleMatrixFromFraction(macierzUlamek);
            macierzFloat = FloatMatrixFromFraction(macierzUlamek);
            wektorDouble = DoubleVectorFromFraction(wektorUlamek);
            wektorFloat = FloatVectorFromFraction(wektorUlamek);

            //kopiowanie macierzy i wektorów
            for (var i = 0; i < macierzUlamek.Rows(); i++)
            {
                for (var j = 0; j < macierzUlamek.Columns(); j++)
                {
                    _macierzUlamek[i, j] = macierzUlamek[i, j];
                    _macierzDouble[i, j] = macierzDouble[i, j];
                    _macierzFloat[i, j] = macierzFloat[i, j];
                }
            }

            for (var i = 0; i < wektorUlamek.Length; i++)
            {
                _wektorUlamek[i] = wektorUlamek[i];
                _wektorDouble[i] = wektorDouble[i];
                _wektorFloat[i] = wektorFloat[i];
            }

            //liczenie czasu
            double[] czasyUlamek = new double[count];
            double[] czasyDouble = new double[count];
            double[] czasyFloat = new double[count];
            double sumaUlamek = 0.0;
            double sumaDouble = 0.0;
            double sumaFloat = 0.0;
            double sredniaUlamek = 0.0;
            double sredniaDouble = 0.0;
            double sredniaFloat = 0.0;

            //CZAS UŁAMKA
            for (int i = 0; i < count; i++)
            {
                var watchFraction = Stopwatch.StartNew();
                macierzUlamek.GaussPartialPivot(wektorUlamek);
                watchFraction.Stop();
                var elapsedMsFraction = watchFraction.ElapsedMilliseconds;
                czasyUlamek[i] = elapsedMsFraction;

                for (var j = 0; j < macierzUlamek.Rows(); j++)
                {
                    for (var k = 0; k < macierzUlamek.Columns(); k++)
                    {
                        macierzUlamek[j, k] = _macierzUlamek[j, k];
                        wektorUlamek[j] = _wektorUlamek[j];
                    }
                }
                sumaUlamek += czasyUlamek[i];
            }

            //CZAS DOUBLE
            for (int i = 0; i < count; i++)
            {
                var watchDouble = Stopwatch.StartNew();
                macierzDouble.GaussPartialPivot(wektorDouble);
                watchDouble.Stop();
                var elapsedMsDouble = watchDouble.ElapsedMilliseconds;
                czasyDouble[i] = elapsedMsDouble;

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

            //CZAS FLOAT
            for (int i = 0; i < count; i++)
            {
                var watchFloat = Stopwatch.StartNew();
                macierzFloat.GaussPartialPivot(wektorFloat);
                watchFloat.Stop();
                var elapsedMsFloat = watchFloat.ElapsedMilliseconds;
                czasyFloat[i] = elapsedMsFloat;

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

            sredniaUlamek = sumaUlamek / count;
            sredniaDouble = sumaDouble / count;
            sredniaFloat = sumaFloat / count;

            StreamWriter writer = new StreamWriter("CzasGaussPartialPivot.csv", append: true);
            if (writer != null)
            {
                writer.WriteLine(String.Format(size + "x" + size + ";" + sredniaUlamek + ";" + sredniaDouble + ";" + sredniaFloat));
            }
            writer.Close();

            Console.WriteLine("GAUSS PARTIAL PIVOT TEST: " + size + "x" + size);
            Console.WriteLine("Średni czas FRACTION: " + sredniaUlamek + "ms");
            Console.WriteLine("Średni czas DOUBLE: " + sredniaDouble + "ms");
            Console.WriteLine("Średni czas FLOAT: " + sredniaFloat + "ms");
        }

        public void GaussFullPivotTimeTest(int size, int count)
        {
            MyMatrix<Ulamek> macierzUlamek = new MyMatrix<Ulamek>(size, size);
            MyMatrix<Ulamek> _macierzUlamek = new MyMatrix<Ulamek>(size, size);
            MyMatrix<double> macierzDouble = new MyMatrix<double>(size, size);
            MyMatrix<double> _macierzDouble = new MyMatrix<double>(size, size);
            MyMatrix<float> macierzFloat = new MyMatrix<float>(size, size);
            MyMatrix<float> _macierzFloat = new MyMatrix<float>(size, size);
            Ulamek[] wektorUlamek = GenerateRandomFractionVector(size);
            Ulamek[] _wektorUlamek = new Ulamek[size];
            double[] wektorDouble = new double[size];
            double[] _wektorDouble = new double[size];
            float[] wektorFloat = new float[size];
            float[] _wektorFloat = new float[size];


            //Przygotwanie macierzy i wektorów
            macierzDouble = DoubleMatrixFromFraction(macierzUlamek);
            macierzFloat = FloatMatrixFromFraction(macierzUlamek);
            wektorDouble = DoubleVectorFromFraction(wektorUlamek);
            wektorFloat = FloatVectorFromFraction(wektorUlamek);

            //kopiowanie macierzy i wektorów
            for (var i = 0; i < macierzUlamek.Rows(); i++)
            {
                for (var j = 0; j < macierzUlamek.Columns(); j++)
                {
                    _macierzUlamek[i, j] = macierzUlamek[i, j];
                    _macierzDouble[i, j] = macierzDouble[i, j];
                    _macierzFloat[i, j] = macierzFloat[i, j];
                }
            }

            for (var i = 0; i < wektorUlamek.Length; i++)
            {
                _wektorUlamek[i] = wektorUlamek[i];
                _wektorDouble[i] = wektorDouble[i];
                _wektorFloat[i] = wektorFloat[i];
            }

            //liczenie czasu
            double[] czasyUlamek = new double[count];
            double[] czasyDouble = new double[count];
            double[] czasyFloat = new double[count];
            double sumaUlamek = 0.0;
            double sumaDouble = 0.0;
            double sumaFloat = 0.0;
            double sredniaUlamek = 0.0;
            double sredniaDouble = 0.0;
            double sredniaFloat = 0.0;

            //CZAS UŁAMKA
            for (int i = 0; i < count; i++)
            {
                var watchFraction = Stopwatch.StartNew();
                macierzUlamek.GaussFullPivot(wektorUlamek);
                watchFraction.Stop();
                var elapsedMsFraction = watchFraction.ElapsedMilliseconds;
                czasyUlamek[i] = elapsedMsFraction;

                for (var j = 0; j < macierzUlamek.Rows(); j++)
                {
                    for (var k = 0; k < macierzUlamek.Columns(); k++)
                    {
                        macierzUlamek[j, k] = _macierzUlamek[j, k];
                        wektorUlamek[j] = _wektorUlamek[j];
                    }
                }
                sumaUlamek += czasyUlamek[i];
            }

            //CZAS DOUBLE
            for (int i = 0; i < count; i++)
            {
                var watchDouble = Stopwatch.StartNew();
                macierzDouble.GaussFullPivot(wektorDouble);
                watchDouble.Stop();
                var elapsedMsDouble = watchDouble.ElapsedMilliseconds;
                czasyDouble[i] = elapsedMsDouble;

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

            //CZAS FLOAT
            for (int i = 0; i < count; i++)
            {
                var watchFloat = Stopwatch.StartNew();
                macierzFloat.GaussFullPivot(wektorFloat);
                watchFloat.Stop();
                var elapsedMsFloat = watchFloat.ElapsedMilliseconds;
                czasyFloat[i] = elapsedMsFloat;

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

            sredniaUlamek = sumaUlamek / count;
            sredniaDouble = sumaDouble / count;
            sredniaFloat = sumaFloat / count;

            StreamWriter writer = new StreamWriter("CzasGaussFullPivot.csv", append: true);
            if (writer != null)
            {
                writer.WriteLine(String.Format(size + "x" + size + ";" + sredniaUlamek + ";" + sredniaDouble + ";" + sredniaFloat));
            }
            writer.Close();

            Console.WriteLine("GAUSS FULL PIVOT TEST: " + size + "x" + size);
            Console.WriteLine("Średni czas FRACTION: " + sredniaUlamek + "ms");
            Console.WriteLine("Średni czas DOUBLE: " + sredniaDouble + "ms");
            Console.WriteLine("Średni czas FLOAT: " + sredniaFloat + "ms");
        }

        public void GaussWithoutPivotAccuracyTest(int size, int count)
        {
            MyMatrix<Ulamek> macierzUlamekA = new MyMatrix<Ulamek>(size, size);
            MyMatrix<Ulamek> _macierzUlamekA = new MyMatrix<Ulamek>(size, size);
            MyMatrix<double> macierzDoubleA = new MyMatrix<double>(size, size);
            MyMatrix<double> _macierzDoubleA = new MyMatrix<double>(size, size);
            MyMatrix<float> macierzFloatA = new MyMatrix<float>(size, size);
            MyMatrix<float> _macierzFloatA = new MyMatrix<float>(size, size);
            Ulamek[] wektorUlamekX = GenerateRandomFractionVector(size);
            Ulamek[] wektorUlamekB = new Ulamek[size];
            Ulamek[] _wektorUlamekB = new Ulamek[size];
            Ulamek[] wektorNormyUlamek = new Ulamek[size];
            double[] wektorDoubleX = new double[size];
            double[] wektorDoubleB = new double[size];
            double[] _wektorDoubleB = new double[size];
            double[] wektorNormyDouble = new double[size];
            float[] wektorFloatX = new float[size];
            float[] wektorFloatB = new float[size];
            float[] _wektorFloatB = new float[size];
            float[] wektorNormyFloat = new float[size];
            double normaUlamek = 0;
            double sredniaNormaUlamek = 0;
            double normaDouble = 0;
            double sredniaNormaDouble = 0;
            float normaFloat = 0;
            float sredniaNormaFloat = 0;

            //Przygotwanie macierzy i wektorów
            macierzDoubleA = DoubleMatrixFromFraction(macierzUlamekA);
            macierzFloatA = FloatMatrixFromFraction(macierzUlamekA);
            wektorDoubleX = DoubleVectorFromFraction(wektorUlamekX);
            wektorFloatX = FloatVectorFromFraction(wektorUlamekX);

            //kopiowanie macierzy i wektorów
            for (var i = 0; i < macierzUlamekA.Rows(); i++)
            {
                for (var j = 0; j < macierzUlamekA.Columns(); j++)
                {
                    _macierzUlamekA[i, j] = macierzUlamekA[i, j];
                    _macierzDoubleA[i, j] = macierzDoubleA[i, j];
                    _macierzFloatA[i, j] = macierzFloatA[i, j];
                }
            }

            //LICZENIE UŁAMKA

            wektorUlamekB = CalculatedFractionVectorB(macierzUlamekA, wektorUlamekX);

            for (var i = 0; i < wektorUlamekB.Length; i++)
            {
                _wektorUlamekB[i] = wektorUlamekB[i];
            }

            for (var i = 0; i < count; i++)
            {
                macierzUlamekA.GaussWithoutPivot(wektorUlamekB);
                for (var j = 0; j < wektorUlamekX.Length; j++)
                {
                    wektorNormyUlamek[j] = wektorUlamekB[j] - wektorUlamekX[j];
                    for (var k = 0; k < wektorNormyUlamek.Length; k++)
                    {
                        normaUlamek += Math.Pow((double)wektorNormyUlamek[k], 2);
                        //normaDouble = Math.Sqrt(normaDouble);
                        macierzUlamekA[j, k] = _macierzUlamekA[j, k];
                        wektorUlamekB[j] = _wektorUlamekB[j];
                    }
                    //Console.WriteLine(wektorNormyDouble[j]);
                    //wektorNormyUlamek[j] = 0;
                }
            }
            normaUlamek = Math.Sqrt(normaUlamek);
            sredniaNormaUlamek = normaUlamek / count;

            Console.WriteLine("Norma ulamek: " + normaUlamek);
            Console.WriteLine("Średnia norma ulamek: " + sredniaNormaUlamek);

            ////LICZENIE DOUBLE
            //for (var i = 0; i < macierzUlamekA.Rows(); i++)
            //{
            //    for (var j = 0; j < macierzUlamekA.Columns(); j++)
            //    {
            //        //wektorUlamekB[i] += macierzUlamekA[i, j] * wektorUlamekX[j];
            //        wektorDoubleB[i] += macierzDoubleA[i, j] * wektorDoubleX[j];
            //    }
            //}

            //for (var i = 0; i < wektorDoubleB.Length; i++)
            //{
            //    //_wektorUlamekB[i] = wektorUlamekB[i];
            //    _wektorDoubleB[i] = wektorDoubleB[i];
            //    //_wektorFloatB[i] = wektorFloatB[i];
            //}

            ////Console.WriteLine("Wektor normy double: ");
            //for (var i = 0; i < count; i++)
            //{
            //    macierzDoubleA.GaussWithoutPivot(wektorDoubleB);
            //    for (var j = 0; j < wektorDoubleX.Length; j++)
            //    {
            //        wektorNormyDouble[j] = wektorDoubleB[j] - wektorDoubleX[j];
            //        for (var k = 0; k < wektorNormyDouble.Length; k++)
            //        {
            //            normaDouble += Math.Pow(wektorNormyDouble[k], 2);
            //            //normaDouble = Math.Sqrt(normaDouble);
            //            macierzDoubleA[j, k] = _macierzDoubleA[j, k];
            //            wektorDoubleB[j] = _wektorDoubleB[j];
            //        }
            //        //Console.WriteLine(wektorNormyDouble[j]);
            //        wektorNormyDouble[j] = 0;
            //    }
            //}
            //normaDouble = Math.Sqrt(normaDouble);
            //sredniaNormaDouble = normaDouble / count;

            ////LICZENIE FLOAT

            //for (var i = 0; i < macierzFloatA.Rows(); i++)
            //{
            //    for (var j = 0; j < macierzFloatA.Columns(); j++)
            //    {
            //        wektorFloatB[i] += macierzFloatA[i, j] * wektorFloatX[j];
            //    }
            //}

            //for (var i = 0; i < wektorFloatB.Length; i++)
            //{
            //    _wektorFloatB[i] = wektorFloatB[i];
            //}

            ////Console.WriteLine("Wektor normy float: ");
            //for (var i = 0; i < count; i++)
            //{
            //    macierzFloatA.GaussWithoutPivot(wektorFloatB);
            //    for (var j = 0; j < wektorFloatX.Length; j++)
            //    {
            //        wektorNormyFloat[j] = wektorFloatB[j] - wektorFloatX[j];
            //        for (var k = 0; k < wektorNormyFloat.Length; k++)
            //        {
            //            normaFloat += (float)Math.Pow(wektorNormyFloat[k], 2);
            //            //normaDouble = Math.Sqrt(normaDouble);
            //            macierzFloatA[j, k] = _macierzFloatA[j, k];
            //            wektorFloatB[j] = _wektorFloatB[j];
            //        }
            //        //Console.WriteLine(wektorNormyFloat[j]);
            //        wektorNormyFloat[j] = 0;
            //    }
            //}
            //normaFloat = (float)Math.Sqrt(normaFloat);
            //sredniaNormaFloat = normaFloat / count;

            //StreamWriter writer = new StreamWriter("NormaGaussWithoutPivot.csv", append: true);
            //if (writer != null)
            //{
            //    writer.WriteLine(String.Format(size + "x" + size + ";" + normaDouble + ";" + normaFloat));
            //}
            //writer.Close();

            //Console.WriteLine("GAUSS WITHOUT PIVOT:");
            //Console.WriteLine("Norma double: " + normaDouble);
            //Console.WriteLine("Średnia norma double: " + sredniaNormaDouble);
            //Console.WriteLine("Norma float: " + normaFloat);
            //Console.WriteLine("Średnia norma float: " + sredniaNormaFloat);
        }

        public void GaussPartialPivotAccuracyTest(int size, int count)
        {
            MyMatrix<Ulamek> macierzUlamekA = new MyMatrix<Ulamek>(size, size);
            MyMatrix<Ulamek> _macierzUlamekA = new MyMatrix<Ulamek>(size, size);
            MyMatrix<double> macierzDoubleA = new MyMatrix<double>(size, size);
            MyMatrix<double> _macierzDoubleA = new MyMatrix<double>(size, size);
            MyMatrix<float> macierzFloatA = new MyMatrix<float>(size, size);
            MyMatrix<float> _macierzFloatA = new MyMatrix<float>(size, size);
            Ulamek[] wektorUlamekX = GenerateRandomFractionVector(size);
            Ulamek[] wektorUlamekB = new Ulamek[size];
            Ulamek[] _wektorUlamekB = new Ulamek[size];
            double[] wektorDoubleX = new double[size];
            double[] wektorDoubleB = new double[size];
            double[] _wektorDoubleB = new double[size];
            double[] wektorNormyDouble = new double[size];
            float[] wektorFloatX = new float[size];
            float[] wektorFloatB = new float[size];
            float[] _wektorFloatB = new float[size];
            float[] wektorNormyFloat = new float[size];
            double normaUlamek = 0;
            double sredniaNormaUlamek = 0;
            double normaDouble = 0;
            double sredniaNormaDouble = 0;
            float normaFloat = 0;
            float sredniaNormaFloat = 0;

            //Przygotwanie macierzy i wektorów
            macierzDoubleA = DoubleMatrixFromFraction(macierzUlamekA);
            macierzFloatA = FloatMatrixFromFraction(macierzUlamekA);
            wektorDoubleX = DoubleVectorFromFraction(wektorUlamekX);
            wektorFloatX = FloatVectorFromFraction(wektorUlamekX);

            //kopiowanie macierzy i wektorów
            for (var i = 0; i < macierzUlamekA.Rows(); i++)
            {
                for (var j = 0; j < macierzUlamekA.Columns(); j++)
                {
                    _macierzUlamekA[i, j] = macierzUlamekA[i, j];
                    _macierzDoubleA[i, j] = macierzDoubleA[i, j];
                    _macierzFloatA[i, j] = macierzFloatA[i, j];
                }
            }

            //LICZENIE DOUBLE
            for (var i = 0; i < macierzUlamekA.Rows(); i++)
            {
                for (var j = 0; j < macierzUlamekA.Columns(); j++)
                {
                    //wektorUlamekB[i] += macierzUlamekA[i, j] * wektorUlamekX[j];
                    wektorDoubleB[i] += macierzDoubleA[i, j] * wektorDoubleX[j];
                }
            }

            for (var i = 0; i < wektorDoubleB.Length; i++)
            {
                //_wektorUlamekB[i] = wektorUlamekB[i];
                _wektorDoubleB[i] = wektorDoubleB[i];
            }

            //Console.WriteLine("Wektor normy double: ");
            for (var i = 0; i < count; i++)
            {
                macierzDoubleA.GaussPartialPivot(wektorDoubleB);
                for (var j = 0; j < wektorDoubleX.Length; j++)
                {
                    wektorNormyDouble[j] = wektorDoubleB[j] - wektorDoubleX[j];
                    for (var k = 0; k < wektorNormyDouble.Length; k++)
                    {
                        normaDouble += Math.Pow(wektorNormyDouble[k], 2);
                        //normaDouble = Math.Sqrt(normaDouble);
                        macierzDoubleA[j, k] = _macierzDoubleA[j, k];
                        wektorDoubleB[j] = _wektorDoubleB[j];
                    }
                    //Console.WriteLine(wektorNormyDouble[j]);
                    wektorNormyDouble[j] = 0;
                }
            }
            normaDouble = Math.Sqrt(normaDouble);
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
                macierzFloatA.GaussPartialPivot(wektorFloatB);
                for (var j = 0; j < wektorFloatX.Length; j++)
                {
                    wektorNormyFloat[j] = wektorFloatB[j] - wektorFloatX[j];
                    for (var k = 0; k < wektorNormyFloat.Length; k++)
                    {
                        normaFloat += (float)Math.Pow(wektorNormyFloat[k], 2);
                        //normaDouble = Math.Sqrt(normaDouble);
                        macierzFloatA[j, k] = _macierzFloatA[j, k];
                        wektorFloatB[j] = _wektorFloatB[j];
                    }
                    //Console.WriteLine(wektorNormyFloat[j]);
                    wektorNormyFloat[j] = 0;
                }
            }
            normaFloat = (float)Math.Sqrt(normaFloat);
            sredniaNormaFloat = normaFloat / count;

            StreamWriter writer = new StreamWriter("NormaGaussPartialPivot.csv", append: true);
            if (writer != null)
            {
                writer.WriteLine(String.Format(size + "x" + size + ";" + normaDouble + ";" + normaFloat));
            }
            writer.Close();

            Console.WriteLine("GAUSS PARTIAL PIVOT:");
            Console.WriteLine("Norma double: " + normaDouble);
            Console.WriteLine("Średnia norma double: " + sredniaNormaDouble);
            Console.WriteLine("Norma float: " + normaFloat);
            Console.WriteLine("Średnia norma float: " + sredniaNormaFloat);
        }

        public void GaussFullPivotAccuracyTest(int size, int count)
        {
            MyMatrix<Ulamek> macierzUlamekA = new MyMatrix<Ulamek>(size, size);
            MyMatrix<Ulamek> _macierzUlamekA = new MyMatrix<Ulamek>(size, size);
            MyMatrix<double> macierzDoubleA = new MyMatrix<double>(size, size);
            MyMatrix<double> _macierzDoubleA = new MyMatrix<double>(size, size);
            MyMatrix<float> macierzFloatA = new MyMatrix<float>(size, size);
            MyMatrix<float> _macierzFloatA = new MyMatrix<float>(size, size);
            Ulamek[] wektorUlamekX = GenerateRandomFractionVector(size);
            Ulamek[] wektorUlamekB = new Ulamek[size];
            Ulamek[] _wektorUlamekB = new Ulamek[size];
            double[] wektorDoubleX = new double[size];
            double[] wektorDoubleB = new double[size];
            double[] _wektorDoubleB = new double[size];
            double[] wektorNormyDouble = new double[size];
            float[] wektorFloatX = new float[size];
            float[] wektorFloatB = new float[size];
            float[] _wektorFloatB = new float[size];
            float[] wektorNormyFloat = new float[size];
            double normaUlamek = 0;
            double sredniaNormaUlamek = 0;
            double normaDouble = 0;
            double sredniaNormaDouble = 0;
            float normaFloat = 0;
            float sredniaNormaFloat = 0;

            //Przygotwanie macierzy i wektorów
            macierzDoubleA = DoubleMatrixFromFraction(macierzUlamekA);
            macierzFloatA = FloatMatrixFromFraction(macierzUlamekA);
            wektorDoubleX = DoubleVectorFromFraction(wektorUlamekX);
            wektorFloatX = FloatVectorFromFraction(wektorUlamekX);

            //kopiowanie macierzy i wektorów
            for (var i = 0; i < macierzUlamekA.Rows(); i++)
            {
                for (var j = 0; j < macierzUlamekA.Columns(); j++)
                {
                    _macierzUlamekA[i, j] = macierzUlamekA[i, j];
                    _macierzDoubleA[i, j] = macierzDoubleA[i, j];
                    _macierzFloatA[i, j] = macierzFloatA[i, j];
                }
            }

            //LICZENIE DOUBLE
            for (var i = 0; i < macierzUlamekA.Rows(); i++)
            {
                for (var j = 0; j < macierzUlamekA.Columns(); j++)
                {
                    //wektorUlamekB[i] += macierzUlamekA[i, j] * wektorUlamekX[j];
                    wektorDoubleB[i] += macierzDoubleA[i, j] * wektorDoubleX[j];
                }
            }

            for (var i = 0; i < wektorDoubleB.Length; i++)
            {
                //_wektorUlamekB[i] = wektorUlamekB[i];
                _wektorDoubleB[i] = wektorDoubleB[i];
                _wektorFloatB[i] = wektorFloatB[i];
            }

            //Console.WriteLine("Wektor normy double: ");
            for (var i = 0; i < count; i++)
            {
                macierzDoubleA.GaussFullPivot(wektorDoubleB);
                for (var j = 0; j < wektorDoubleX.Length; j++)
                {
                    wektorNormyDouble[j] = wektorDoubleB[j] - wektorDoubleX[j];
                    for (var k = 0; k < wektorNormyDouble.Length; k++)
                    {
                        normaDouble += Math.Pow(wektorNormyDouble[k], 2);
                        //normaDouble = Math.Sqrt(normaDouble);
                        macierzDoubleA[j, k] = _macierzDoubleA[j, k];
                        wektorDoubleB[j] = _wektorDoubleB[j];
                    }
                    //Console.WriteLine(wektorNormyDouble[j]);
                    wektorNormyDouble[j] = 0;
                }
            }
            normaDouble = Math.Sqrt(normaDouble);
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
                macierzFloatA.GaussFullPivot(wektorFloatB);
                for (var j = 0; j < wektorFloatX.Length; j++)
                {
                    wektorNormyFloat[j] = wektorFloatB[j] - wektorFloatX[j];
                    for (var k = 0; k < wektorNormyFloat.Length; k++)
                    {
                        normaFloat += (float)Math.Pow(wektorNormyFloat[k], 2);
                        //normaDouble = Math.Sqrt(normaDouble);
                        macierzFloatA[j, k] = _macierzFloatA[j, k];
                        wektorFloatB[j] = _wektorFloatB[j];
                    }
                    //Console.WriteLine(wektorNormyFloat[j]);
                    wektorNormyFloat[j] = 0;
                }
            }
            normaFloat = (float)Math.Sqrt(normaFloat);
            sredniaNormaFloat = normaFloat / count;

            StreamWriter writer = new StreamWriter("NormaGaussFullPivot.csv", append: true);
            if (writer != null)
            {
                writer.WriteLine(String.Format(size + "x" + size + ";" + normaDouble + ";" + normaFloat));
            }
            writer.Close();

            Console.WriteLine("GAUSS FULL PIVOT:");
            Console.WriteLine("Norma double: " + normaDouble);
            Console.WriteLine("Średnia norma double: " + sredniaNormaDouble);
            Console.WriteLine("Norma float: " + normaFloat);
            Console.WriteLine("Średnia norma float: " + sredniaNormaFloat);
        }
    }
}
