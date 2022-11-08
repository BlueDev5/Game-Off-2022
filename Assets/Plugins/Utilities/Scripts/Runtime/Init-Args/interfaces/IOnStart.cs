namespace Utils.Init
{
    /// <summary>
    /// Should Be inherited by plain classes that use Wrappers and need To Do Something in Start 
    /// Callback.
    /// </summary>
    public interface IOnStart
    {
        void Start();
    }
}