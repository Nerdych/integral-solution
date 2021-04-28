using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateLibrary
{
    public class TrapCalculator : ICalculator
    {
        public double CalculatePos(double a, double b, long n, Func<double, double> f)
        {
            if (n < 0) throw new ArgumentException("аргумент n не должен быть меньше 0");

            double h = (b - a) / n;
         
            double sum = 0;
            for (int i = 1; i < n; i++)
            {
                sum += f(a + h * i);
            }

            sum += (f(a) + f(b))/2;

            return sum * h;
        }

        public double CalculateParallel(double a, double b, long n, Func<double, double> f)
        {
            if (n < 0) throw new ArgumentException("аргумент n не должен быть меньше 0");

            double h = (b - a) / n;

            var tmp = new double[n];
            Parallel.For(1, n, (i) => { tmp[i] = (f(a + h * i)); });

            return (tmp.Sum() + (f(a) + f(b)) / 2) * h;
        }
    }
}
