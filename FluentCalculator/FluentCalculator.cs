using FluentCalculator;

namespace Fluent.Calculator
{
    public class FluentCalculator : IFluentOperations
    {
        private int _runningTotal;

        public IFluentOperations Seed(int startingValue)
        {
            _runningTotal = startingValue;
            return this;
        }

        public IFluentOperations Minus(int value)
        {
            _runningTotal -= value;
            return this;
        }

        public IFluentOperations Plus(int value)
        {
            _runningTotal += value;
            return this;
        }

        public IFluentOperations Undo()
        {

            return this;
        }

        public int Result()
        {
            return _runningTotal;
        }
    }
}