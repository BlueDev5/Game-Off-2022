namespace Utils.Init
{
    /// <summary>
    /// Should Be inherited by plain classes that use Wrappers and need To Do Something in OnEnable 
    /// Callback.
    /// </summary>
    public interface IOnEnable
    {
        void OnEnable();
    }
}