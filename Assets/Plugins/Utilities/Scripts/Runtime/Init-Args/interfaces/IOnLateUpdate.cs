namespace Utils.Init
{
    /// <summary>
    /// Should Be inherited by plain classes that use Wrappers and need To Do Something in LateUpdate 
    /// Callback.
    /// </summary>
    public interface IOnLateUpdate
    {
        void LateUpdate();
    }
}