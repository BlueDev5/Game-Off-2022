namespace Utils.Init
{
    /// <summary>
    /// Should Be inherited by plain classes that use Wrappers and need To Do Something in OnDisable 
    /// Callback.
    /// </summary>
    public interface IOnDisable
    {
        void OnDisable();
    }
}