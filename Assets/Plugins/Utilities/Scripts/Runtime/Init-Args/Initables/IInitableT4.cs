namespace Utils.Init
{
    /// <summary>
    /// Base Class for all initable Classes (for eg. Plain Class Wrapper) with 4 Generic Arg
    /// </summary>
    public interface IInitable<Arg1, Arg2, Arg3, Arg4> : IFourArguments
    {
        void Init(Arg1 arg1, Arg2 arg2, Arg3 arg3, Arg4 arg4);
    }
}