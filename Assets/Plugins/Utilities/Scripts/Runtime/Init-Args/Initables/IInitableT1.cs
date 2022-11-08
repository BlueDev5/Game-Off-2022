namespace Utils.Init
{
    /// <summary>
    /// Base Class for all initable Classes (for eg. Plain Class Wrapper) with 1 Generic Arg
    /// </summary>
    public interface IInitable<Arg1> : IOneArgument
    {
        void Init(Arg1 arg1);
    }
}