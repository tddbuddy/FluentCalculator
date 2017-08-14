﻿using System;
using System.Collections.Generic;

namespace Fluent.Calculator
{
    public class FluentCalculator : IFluentOperations
    {
        private int _runningTotal;
        private readonly Stack<Func<int>> _undoStack = new Stack<Func<int>>();
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
            if (StackHasOperations())
            {
                CreateRedoOperation();
                PerformUndoOperation();
            }

            return this;
        }

        private void PerformUndoOperation()
        {
            var undoOperation = _undoStack.Pop();
            _runningTotal = undoOperation.Invoke();
        }

        private bool StackHasOperations()
        {
            return _undoStack.Count > 0;
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
            _undoStack.Push(() => _runningTotal = valuePriorToOperation);
        }

        private void CreateRedoOperation()
        {
            var valuePriorToUndo = _runningTotal;
            _redo = () => _runningTotal = valuePriorToUndo;
        }
    }
}