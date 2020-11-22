using System;
using System.Collections.Generic;
using System.Text;

namespace ProbabilityCalc
{
    class Fraction
    {
        public long Numerator { get; set; }
        public long Denominator { get; set; }
        public Fraction(long num, long den)
        {
            Numerator = num;
            Denominator = den;
            Simplify();
        }
        public static explicit operator double(Fraction a)
        {
            return 1.0 * a.Numerator / a.Denominator;
        }
        public static Fraction operator *(Fraction a, Fraction b)
            => new Fraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator);
        public override string ToString()
        {
            return String.Format("{0}/{1}", Numerator, Denominator);
        }
        public void Simplify()
        {
            if (Denominator < 0)
            {
                Numerator *= -1;
                Denominator *= -1;
            }
            long a = Math.Abs(Numerator);
            long b = Denominator;
            //Find GCD
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }
            a |= b;
            Numerator /= a;
            Denominator /= a;
        }
    }
}
