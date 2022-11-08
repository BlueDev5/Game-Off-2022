namespace Utils
{
    public interface IInteractable
    {
        void Interact();
        void Interact(IInteractor interactor);
    }
}