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
            Assert.DoesNotThrow(() => new FluentCalculator());
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

        [Test]
        public void Redo_WhenCalledAfterUndo_ShouldReturnLastOperationResult()
        {
            //---------------Set up test pack-------------------
            var expected = 18;
            var calculator = CreateFluentCalculator();
            //---------------Execute Test ----------------------
            var result = calculator
                .Seed(15)
                .Plus(3)
                .Undo()
                .Redo()
                .Result();
            //---------------Test Result -----------------------
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Undo_WhenCalledAfterUndo_ShouldReturnTwoOperationsBackResult()
        {
            //---------------Set up test pack-------------------
            var expected = 120;
            var calculator = CreateFluentCalculator();
            //---------------Execute Test ----------------------
            var result = calculator
                .Seed(100)
                .Plus(20)
                .Minus(10)
                .Plus(1)
                .Undo()
                .Undo()
                .Result();
            //---------------Test Result -----------------------
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Undo_WhenCalledAfterManyUndo_ShouldNotThrowException()
        {
            //---------------Set up test pack-------------------
            var calculator = CreateFluentCalculator();
            //---------------Execute Test ----------------------
            //---------------Test Result -----------------------
            Assert.DoesNotThrow(() => calculator
                .Seed(100)
                .Plus(20)
                .Undo()
                .Undo()
                .Result());
        }

        [Test]
        public void Redo_WhenCalledTimesAfterUndoWithOperationBetween_ShouldReturnLastOperationResult()
        {
            //---------------Set up test pack-------------------
            var expected = 116;
            var calculator = CreateFluentCalculator();
            //---------------Execute Test ----------------------
            var result = calculator
                .Seed(100)
                .Plus(20)
                .Minus(10)
                .Plus(1)
                .Undo()
                .Redo()
                .Plus(5)
                .Undo()
                .Redo()
                .Result();
            //---------------Test Result -----------------------
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Redo_WhenCalledManyTimesAfterUndo_ShouldNotThrowException()
        {
            // ---------------Set up test pack------------------ -
            var calculator = CreateFluentCalculator();
            //---------------Execute Test----------------------
            //-------------- - Test Result---------------------- -
              Assert.DoesNotThrow(() => calculator
                  .Seed(100)
                  .Plus(20)
                  .Undo()
                  .Redo()
                  .Redo()
                  .Result());
        }

        //todo: when two stacked redo with single undo?

        [Test]
        public void Redo_WhenCalledAfterPlusOperation_ShouldReturnLastOperationResult()
        {
            //---------------Set up test pack-------------------
            var expected = 149;
            var calculator = CreateFluentCalculator();
            //---------------Execute Test ----------------------
            var result = calculator
                .Seed(100)
                .Plus(25)
                .Plus(19)
                .Plus(1)
                .Undo()
                .Undo()
                .Redo()
                .Plus(5)
                .Redo()
                .Result();
            //---------------Test Result -----------------------
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Redo_WhenCalledAfterMinusOperation_ShouldReturnLastOperationResult()
        {
            //---------------Set up test pack-------------------
            var expected = 122;
            var calculator = CreateFluentCalculator();
            //---------------Execute Test ----------------------
            var result = calculator
                .Seed(100)
                .Plus(15)
                .Plus(9)
                .Plus(1)
                .Undo()
                .Undo()
                .Redo()
                .Minus(2)
                .Redo()
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
