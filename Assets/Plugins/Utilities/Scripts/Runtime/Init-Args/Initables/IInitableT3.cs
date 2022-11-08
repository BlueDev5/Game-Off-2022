namespace Utils.Init
{
    /// <summary>
    /// Base Class for all initable Classes (for eg. Plain Class Wrapper) with 3 Generic Arg
    /// </summary>
    public interface IInitable<Arg1, Arg2, Arg3> : IThreeArguments
    {
        void Init(Arg1 arg1, Arg2 arg2, Arg3 arg3);
    }
}