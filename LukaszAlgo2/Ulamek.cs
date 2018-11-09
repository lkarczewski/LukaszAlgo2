using System;
using System.Collections.Generic;
using System.Numerics;

namespace LukaszAlgo2
{
    class Ulamek
    {
        public BigInteger licznik { get; set; }
        public BigInteger mianownik { get; set; }

        public Ulamek()
        {
            licznik = 0;
            mianownik = 1;
        }

        public Ulamek(BigInteger _licznik, BigInteger _mianownik)
        {
            if (_mianownik == 0)
                throw new ArgumentException("BŁĄD! Mianownik nie może być równy 0!!");

            licznik = _licznik;
            mianownik = _mianownik;
            Uprosc();
        }

        public static BigInteger Nwd(BigInteger a, BigInteger b)
        {
            while (b != 0)
            {
                var tmp = b;
                b = a % b;
                a = tmp;
            }

            return a;
        }

        public void Uprosc()
        {
            var nwd = Nwd(licznik, mianownik);
            licznik /= nwd;
            mianownik /= nwd;

            if (mianownik < 0)
            {
                mianownik *= -1;
                licznik *= -1;
            }
        }

        public static explicit operator double(Ulamek ulamek)   //jak ma rzutować typ Ulamek to Double, żeby zwrócił właściwą wartość
        {
            ulamek.Uprosc();
            return (double) ulamek.licznik / (double) ulamek.mianownik;
        }

        public override string ToString()
        {
            return this.licznik.ToString() + "/" + this.mianownik.ToString();
        }

        public static Ulamek operator +(Ulamek a, Ulamek b)
        {
            return new Ulamek(a.licznik * b.mianownik + b.licznik * a.mianownik, a.mianownik * b.mianownik);
        }

        public static Ulamek operator *(Ulamek a, Ulamek b)
        {
            return new Ulamek(a.licznik * b.licznik, a.mianownik * b.mianownik);
        }

        public static Ulamek operator /(Ulamek a, Ulamek b)
        {
            return new Ulamek(a.licznik * b.mianownik, a.mianownik * b.licznik);
        }

        public static Ulamek operator -(Ulamek a)
        {
            return new Ulamek(-a.licznik, a.mianownik);
        }

        public static Ulamek operator -(Ulamek a, Ulamek b)
        {
            return a + -b;
        }

        public static int Compare(Ulamek a, Ulamek b)
        {
            if (a.licznik * b.mianownik == b.licznik * a.mianownik)
                return 0;

            if (a.licznik * b.mianownik > b.licznik * a.mianownik)
                return 1;
            else return -1;
        }

        public static bool operator ==(Ulamek a, Ulamek b)
        {
            return Compare(a, b) == 0;
        }

        public static bool operator !=(Ulamek a, Ulamek b)
        {
            return !(a == b);
        }

        public static bool operator <(Ulamek a, Ulamek b)
        {
            return Compare(a, b) < 0;
        }

        public static bool operator >(Ulamek a, Ulamek b)
        {
            return Compare(a, b) > 0;
        }
    }
}
