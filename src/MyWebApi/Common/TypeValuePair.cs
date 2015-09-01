namespace MyWebApi.Common
{
    using System;

    /// <summary>
    /// Type-value pair containing type and value for an object.
    /// </summary>
    public class TypeValuePair
    {
        /// <summary>
        /// Gets or sets the type of the object in the type-value pair.
        /// </summary>
        /// <value>Object's type.</value>
        public Type Type { get; set; }

        /// <summary>
        /// Gets or sets the value of the object in the type-value pair.
        /// </summary>
        /// <value>Object's value.</value>
        public object Value { get; set; }
    }
}
