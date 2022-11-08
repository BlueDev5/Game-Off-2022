namespace Utils.DI
{
    /// <summary>
    /// Interface for Factory instance type Resolution.
    /// </summary>
    public interface IFactory
    {
        /// <summary>
        /// Create an instance of the object of the type created by the factory.
        /// </summary>
        /// <returns>he instance of the created object.</returns>
        object Create();
    }
}