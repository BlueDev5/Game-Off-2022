namespace Utils.Init
{
    /// <summary>
    /// Base Class for all initable Classes (for eg. Plain Class Wrapper) with 2 Generic Arg
    /// </summary>
    public interface IInitable<Arg1, Arg2> : ITwoArguments
    {
        void Init(Arg1 arg1, Arg2 arg2);
    }

}