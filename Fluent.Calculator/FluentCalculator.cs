using System;
using System.Collections.Generic;

namespace Fluent.Calculator
{
    public class FluentCalculator : IFluentOperations
    {
        private int _runningTotal;
        private readonly Stack<Func<int>> _undoOperations = new Stack<Func<int>>();
        private readonly Stack<Func<int>> _redoOperations = new Stack<Func<int>>();

        public IFluentOperations Seed(int startingValue)
        {
            _runningTotal = startingValue;
            return this;
        }

        public IFluentOperations Minus(int value)
        {
            CreateUndoOperation();
            PerformMinusOperation(value);
            return this;
        }

        public IFluentOperations Plus(int value)
        {
            CreateUndoOperation();
            PerformPlusOperation(value);
            return this;
        }

        public IFluentOperations Undo()
        {
            if (HasUndoOperation())
            {
                CreateRedoOperation();
                PerformUndoOperation();
            }

            return this;
        }

        public IFluentOperations Redo()
        {
            if (HasRedoOperation())
            {
                PerformRedoOperation();
            }
            return this;
        }

        public int Result()
        {
            return _runningTotal;
        }

        private void PerformRedoOperation()
        {
            var redoOperation = _redoOperations.Pop();
            _runningTotal = redoOperation.Invoke();
        }

        private bool HasRedoOperation()
        {
            return _redoOperations.Count > 0;
        }

        private void PerformPlusOperation(int value)
        {
            _runningTotal += value;
        }

        private void PerformMinusOperation(int value)
        {
            _runningTotal -= value;
        }

        private void PerformUndoOperation()
        {
            var undoOperation = _undoOperations.Pop();
            _runningTotal = undoOperation.Invoke();
        }

        private bool HasUndoOperation()
        {
            return _undoOperations.Count > 0;
        }

        private void CreateUndoOperation()
        {
            var valuePriorToOperation = _runningTotal;
            _undoOperations.Push(() => _runningTotal = valuePriorToOperation);
        }

        private void CreateRedoOperation()
        {
            var valuePriorToUndo = _runningTotal;
            _redoOperations.Push(()=> _runningTotal = valuePriorToUndo);
        }
    }
}