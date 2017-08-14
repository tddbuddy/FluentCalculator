using NUnit.Framework;

namespace Fluent.Calculator.Tests
{
    [TestFixture]
    public class FluentCalculatorTests
    {
        [Test]
        public void Ctor_WhenConstructing_ShouldNotThrowException()
        {
            //---------------Set up test pack-------------------
            //---------------Execute Test ----------------------
            //---------------Test Result -----------------------
            Assert.DoesNotThrow(() => new Fluent.Calculator.FluentCalculator());
        }

        [Test]
        public void Result_WhenNoOperations_ShouldReturnZero()
        {
            //---------------Set up test pack-------------------
            var calculator = CreateFluentCalculator();
            //---------------Execute Test ----------------------
            var result = calculator.Result();
            //---------------Test Result -----------------------
            Assert.AreEqual(0, result);
        }

        [Test]
        public void Seed_WhenNoOtherOperationsChained_ShouldReturnSeedValue()
        {
            //---------------Set up test pack-------------------
            var expected = 1;
            var calculator = CreateFluentCalculator();
            //---------------Execute Test ----------------------
            var result = calculator
                         .Seed(1)
                         .Result();
            //---------------Test Result -----------------------
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Plus_WhenAddingToSeed_ShouldReturnSum()
        {
            //---------------Set up test pack-------------------
            var expected = 3;
            var calculator = CreateFluentCalculator();
            //---------------Execute Test ----------------------
            var result = calculator
                        .Seed(1)
                        .Plus(2)
                        .Result();
            //---------------Test Result -----------------------
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Minus_WhenSubtractingFromSeed_ShouldReturnDifference()
        {
            //---------------Set up test pack-------------------
            var expected = -1;
            var calculator = CreateFluentCalculator();
            //---------------Execute Test ----------------------
            var result = calculator
                         .Seed(1)
                         .Minus(2)
                         .Result();
            //---------------Test Result -----------------------
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Undo_WhenCalledAfterPlus_ShouldReturnSeed()
        {
            //---------------Set up test pack-------------------
            var expected = 10;
            var calculator = CreateFluentCalculator();
            //---------------Execute Test ----------------------
            var result = calculator
                         .Seed(10)
                         .Plus(2)
                         .Undo()
                         .Result();
            //---------------Test Result -----------------------
            Assert.AreEqual(expected, result);
        }

        private static FluentCalculator CreateFluentCalculator()
        {
            var calculator = new FluentCalculator();
            return calculator;
        }
    }
}
