using System;

namespace Fluent.Calculator
{
    public class FluentCalculator : IFluentOperations
    {
        private int _runningTotal;
        private Func<int> _undo;
        private Func<int> _redo;

        public IFluentOperations Seed(int startingValue)
        {
            _runningTotal = startingValue;
            return this;
        }

        public IFluentOperations Minus(int value)
        {
            CreateUndoOperation();
            _runningTotal -= value;
            return this;
        }

        public IFluentOperations Plus(int value)
        {
            CreateUndoOperation();
            _runningTotal += value;
            return this;
        }

        public IFluentOperations Undo()
        {
            CreateRedoOperation();
            _runningTotal = _undo.Invoke();
            return this;
        }

        public IFluentOperations Redo()
        {
            _runningTotal = _redo.Invoke();
            return this;
        }

        public int Result()
        {
            return _runningTotal;
        }

        private void CreateUndoOperation()
        {
            var valuePriorToOperation = _runningTotal;
            _undo = () => _runningTotal = valuePriorToOperation;
        }

        private void CreateRedoOperation()
        {
            var valuePriorToUndo = _runningTotal;
            _redo = () => _runningTotal = valuePriorToUndo;
        }
    }
}