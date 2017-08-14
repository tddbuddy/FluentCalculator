namespace Fluent.Calculator
{
    public interface IFluentOperations
    {
        IFluentOperations Plus(int value);
        IFluentOperations Minus(int value);
        IFluentOperations Undo();
        IFluentOperations Redo();

        int Result();
    }
}