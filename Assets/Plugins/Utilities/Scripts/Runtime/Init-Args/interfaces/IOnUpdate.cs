namespace Utils.Init
{
    /// <summary>
    /// Should Be inherited by plain classes that use Wrappers and need To Do Something in Update 
    /// Callback.
    /// </summary>
    public interface IOnUpdate
    {
        void Update();
    }
}