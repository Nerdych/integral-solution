using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculateLibrary;

namespace IntegralCalculating.Tests
{
    [TestClass]
    public class TrapCalculatorTests
    {
        [TestMethod]
        public void Intergrate_xx_Gives_Correct_Result()
        {
            // Arrange
            double expected = 291.6666666;
            double a = 5;
            double b = 10;
            long n = 10000;
            TrapCalculator trapCalculator = new TrapCalculator();
            Func<double, double> f = x => x * x;

            // Act
            double actual = trapCalculator.Calculate(a, b, n, f);

            // Assert
            Assert.AreEqual(expected, actual, 0.0001);
        }

        [TestMethod]
        public void Intergrate_xx_Gives_0()
        {
            // Arrange
            double expected = 0;
            double a = 0;
            double b = a;
            long n = 100000;
            TrapCalculator rectangleCalculator = new TrapCalculator();
            Func<double, double> f = x => x * x;

            // Act
            double actual = rectangleCalculator.Calculate(a, b, n, f);

            // Assert
            Assert.AreEqual(expected, actual, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Intergrate_xx_negative_n()
        {
            // Arrange
            double a = 0;
            double b = a;
            long n = -10;
            TrapCalculator rectangleCalculator = new TrapCalculator();
            Func<double, double> f = x => x * x;

            // Act
            double actual = rectangleCalculator.Calculate(a, b, n, f);
        }
    }
}
