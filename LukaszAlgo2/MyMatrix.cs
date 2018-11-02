using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LukaszAlgo2
{
    public class MyMatrix<T> where T : new()
    {
        public T[,] Matrix { get; }

        public MyMatrix(T[,] matrix)
        {
            Matrix = matrix;
        }

        public MyMatrix(int rows, int columns)
        {
            var random = new Random();
            var matrix = new T[rows, columns];
            double r;

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    if (matrix is double[,])
                    {
                        r = random.Next(-65536, 65535);
                        matrix[i, j] = (dynamic)(r / 65536);
                    }
                    else if (matrix is float[,])
                    {
                        r = random.Next(-65536, 65535);
                        matrix[i, j] = (dynamic)(float)(r / 65536);
                    }
                }
            }

            Matrix = matrix;
        }

        public void PrintMatrix()
        {
            for (int i=0; i<Rows(); i++)
            {
                for (int j=0; j<Columns(); j++)
                {
                    Console.Write(this[i,j]);
                }

                Console.WriteLine();
            }
        }

        public int Rows()
        {
            return Matrix.GetLength(0); //liczby określają wymiar
        }
        
        public int Columns()
        {
            return Matrix.GetLength(1);
        }

        public T this[int row, int col]
        {
            get
            {
                return Matrix[row, col];
            }
            set
            {
                Matrix[row, col] = value;
            }
        }

        public int[] FindMax(int selected)
        {
            var currentMaxIndex = new int[] { selected, selected };
            var currentMax = this[selected, selected] ;

            for (var i = selected; i < Rows(); i++)
            {
                for (var j = selected; j < Columns(); j++)
                {
                    if (this[i, j] > (dynamic)currentMax || this[i, j] < -(dynamic)currentMax)
                    {
                        currentMax = this[i, j];
                        currentMaxIndex = new int[] { i, j };
                    }
                }
            }

            return currentMaxIndex;
        }

        public static MyMatrix<T> operator +(MyMatrix<T> a, MyMatrix<T> b)
        {
            if (a.Rows() != b.Rows() || a.Columns() != b.Columns())
                throw new ArgumentException("Macierze mają różne rozmiary!");

            var output = new T[a.Rows(), a.Columns()];
            for(var row = 0; row < a.Rows(); row++)
            {
                for(var column = 0; column < a.Columns(); column++)
                {
                    output[row, column] = (dynamic)a[row, column] + (dynamic)b[row, column];
                }
            }

            return new MyMatrix<T>(output);
        }

        public static MyMatrix<T> operator *(MyMatrix<T> a, MyMatrix<T> b)
        {
            if (a.Columns() != b.Rows())
                throw new ArgumentException("Macierze mają różne rozmiary!");

            var output = new T[a.Rows(), b.Columns()];
            for(var row = 0; row < a.Rows(); row++)
            {
                for(var column = 0; column < b.Columns(); column++)
                {
                    var sum = new T();
                    for(var i = 0; i < a.Columns(); i++)
                    {
                        sum += (dynamic) a[row, i] * (dynamic) b [i, column];
                    }

                    output[row, column] = sum;
                }
            }

            return new MyMatrix<T>(output);
        }

        public static T[] operator *(MyMatrix<T> a, T[] b)
        {
            if (a.Columns() != b.Length)
                throw new ArgumentException("Matrix sizes are not equal.");

            var output = new T[a.Rows()];
            for (var row = 0; row < a.Rows(); row++)
            {
                output[row] = new T();
            }

            for (var row = 0; row < a.Rows(); row++)
            {
                for (var column = 0; column < b.Length; column++)
                {
                    output[row] += (dynamic)a[row, column] * (dynamic)b[column];
                }
            }

            return output;
        }

        public void GaussWithoutPivot(T[] vector) //obiekt ma w sobie macierz, ale nie posiada wektora
        {
            LeftBottomTriangle(vector); //redukcja lewego dolnego trójkąta
            RightTopTriangle(vector);   //redukcja prawego górnego trójkąta
            CalculateVector(vector); // obliczanie wektora
        }

        public void GaussPartialPivot(T[] vector)
        {
            LeftBottomTrianglePartialPivot(vector);
            RightTopTriangle(vector);
            CalculateVector(vector);
        }

        public void GaussFullPivot(T[] vector)
        {
            //ustalenie porządku kolumn
            var columnOrder = new int[Columns()];
            for (var i = 0; i < Columns(); i++)
            {
                columnOrder[i] = i;
            }

            LeftBottomTriangleFullPivot(vector, columnOrder);
            RightTopTriangle(vector);
            CalculateVector(vector);

            //przywrócenie porządku kolumn
            var orderedVector = new T[Columns()];
            for (var i = 0; i < Columns(); i++)
                orderedVector[columnOrder[i]] = vector[i];

            for (var i = 0; i < Columns(); i++)
                vector[i] = orderedVector[i];
        }

        public void LeftBottomTriangle(T[] vector)
        {
            //wybieranie rzędu do zredukowania rzędów poniżej; selected to wiersz, którym będę odejmował;
            for (var selected = 0; selected < Rows() - 1; selected++)
            {
                NoLeadingZero(selected);
                //pętla na każdym rzędie poniżej; current to wiersz, który będzie redukowany
                for (var current = selected + 1; current < Rows(); current++)
                {
                    ReduceRow(vector, selected, current);
                }
            }
        }

        public void SwapRow(int index1, int index2)
        {
            for (var i = 0; i < Columns(); i++)
            {
                var temp = this[index2, i];
                this[index2, i] = this[index1, i];
                this[index1, i] = temp;
            }
        }

        public void SwapColumn(int index1, int index2)
        {
            for (var i = 0; i < Columns(); i++)
            {
                var temp = this[i, index2];
                this[i, index2] = this[i, index1];
                this[i, index1] = temp;
            }
        }

        public int FindMaxInColumn(int selected)
        {
            //wybrany rząd ustawiony jako obecny max
            var currentMaxRowIndex = selected;
            var currentMax = this[selected, selected];

            //sprawdzanie każdego rzędu poniżej wybranego(selected)
            for(var i = selected; i < Rows(); i++)
            {
                if(this[i,selected] > (dynamic) currentMax)
                {
                    currentMax = this[i, selected];
                    currentMaxRowIndex = i;
                }
            }

            return currentMaxRowIndex;
        }

        public void ChoosePartialPivot(T[] vector, int selected)
        {
            var maxRow = FindMaxInColumn(selected);

            if (selected != maxRow)
            {
                //zamień rzędy wektora
                var temp = vector[selected];
                vector[selected] = vector[maxRow];
                vector[maxRow] = temp;
            }

            //zamień rzędy macierzy
            SwapRow(selected, maxRow);
        }
            
        public void LeftBottomTrianglePartialPivot(T[] vector)
        {
            //wybranie rzędu do redukowania rzędów poniżej
            for (var selected = 0; selected < Rows() - 1; selected++)
            {
                NoLeadingZero(selected);
                ChoosePartialPivot(vector, selected);

                //redukowanie rzędów poniżej
                for (var current = selected + 1; current < Rows(); current++)
                {
                    ReduceRow(vector, selected, current);
                }
            }
        }

        public void LeftBottomTriangleFullPivot(T[] vector, int[] columnOrder)
        {
            //wybranie rzędu do redukowania rzędów poniżej
            for (var selected = 0; selected < Rows() - 1; selected++)
            {
                var max = FindMax(selected);

                //zamiana kolumn
                var tempOrd = columnOrder[selected];
                columnOrder[selected] = columnOrder[max[1]];
                columnOrder[max[1]] = tempOrd;
                SwapColumn(selected, max[1]);

                //zamiana rzędu i wektora
                var temp = vector[selected];
                vector[selected] = vector[max[0]];
                vector[max[0]] = temp;
                SwapRow(selected, max[0]);

                //redukowanie rzędów poniżej
                for (var current = selected + 1; current < Rows(); current++)
                {
                    ReduceRow(vector, selected, current);
                }
            }
        }

        public void RightTopTriangle(T[] vector)
        {
            for (var selected = Rows() - 1; selected >= 1; selected--)
            {
                NoLeadingZero(selected);
                for (var current = selected - 1; current >= 0; current--)
                {
                    ReduceRow(vector, selected, current);
                }
            }
        }

        private void NoLeadingZero(int selected)
        {
            if (this[selected, selected] == (dynamic)new T()) //dynamic, bo dopiero czasie wykonania zostanie określony typ 
                    throw new ArgumentException("Znaleziono ZERO w diagonalnej macierzy!");
        }

        private void ReduceRow(T[] vector, int selected, int current)
        {
            //jeśli wybrany rząd jest zredukowany, return
            if (this[current, selected] == (dynamic)new T())
                return;

            //współczynnik do wyzerowania rzędu
            var scalar = this[current, selected] / (dynamic) this[selected, selected];

            //odejmowanie wybranego rzędu (pomnożonego przez scalar) od obecnego rzędu
            for (var col = 0; col < Columns(); col++)
            {
                //odejmowanie każdej kolumny
                this[current, col] -= this[selected, col] * scalar;
            }

            //odejmowanie rzędów wektora (pomnożone przez scalar)
            vector[current] -= vector[selected] * scalar;
        }

        public void CalculateVector(T[] v)
        {
            for (var i = 0; i < Rows(); i++)
            {
                v[i] = v[i] / (dynamic)this[i, i]; //obliczanie współczynnika równania
                this[i, i] = this[i, i] / (dynamic)this[i, i]; // zapisanie współczynnika równania w macierzy
            }
        }
    }
}
