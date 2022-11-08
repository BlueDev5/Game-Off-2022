using System;

namespace Utils.DI
{
    /// <summary>
    /// Defines A binding Info
    /// </summary>
    public class BindingInfo
    {
        /// <summary>
        /// The type for which the binding is.
        /// </summary>
        public Type type { get; private set; }

        /// <summary>
        /// The Value to provided for this Type.
        /// </summary>
        public object value { get; private set; }

        /// <summary>
        /// Th identifier for this binding. Used for grouping Bindings.
        /// </summary>
        public object identifier;

        /// <summary>
        /// The binding tags.
        /// </summary>
        public string[] Tags;

        public BindingInstance BindingInstance;

        public BindingInfo(Type type, object value, BindingInstance bindingInstance)
        {
            this.type = type;
            this.value = value;
            this.BindingInstance = bindingInstance;
            this.Tags = new string[0];
        }

        /// <summary>
        /// Gets the type of the value.
        /// </summary>
        /// <returns>The Value Type.</returns>
        public Type GetValueType() => value is Type ? (Type)value : value.GetType();

        /// <summary>
        /// Convert the object to string
        /// </summary>
        /// <returns>Object converted to string</returns>
        public override string ToString()
        {
            return string.Format("Type: {0} \n" +
                                 "Bound To: {1} ({2}) \n" +
                                 "Identifier: {3} \n" +
                                 "Tags: {4} \n",
                                 type.FullName,
                                 value == null ? "-" : value.ToString(),
                                 value == null ? "-" : value is Type ? "type" : $"instance [{value.GetHashCode()}]",
                                 identifier == null ? "-" : identifier.ToString(),
                                 string.Join(", ", Tags));
        }
    }
}