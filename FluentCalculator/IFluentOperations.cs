namespace FluentCalculator
{
    public interface IFluentOperations
    {
        IFluentOperations Plus(int value);
        IFluentOperations Minus(int value);
        IFluentOperations Undo();

        int Result();
    }
}