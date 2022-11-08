namespace Utils.Init
{
    /// <summary>
    /// Should Be inherited by plain classes that use Wrappers and need To Do Something in FixedUpdate 
    /// Callback.
    /// </summary>
    public interface IOnFixedUpdate
    {
        void FixedUpdate();
    }
}