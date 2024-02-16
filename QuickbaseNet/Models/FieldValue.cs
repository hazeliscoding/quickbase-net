using Newtonsoft.Json;
using System;

namespace QuickbaseNet.Models
{
    /// <summary>
    /// Represents a value of a field in a QuickBase record.
    /// </summary>
    public class FieldValue
    {
        /// <summary>
        /// Gets or sets the value of the field.
        /// </summary>
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public object Value { get; set; }

        /// <summary>
        /// Gets the strongly-typed value of the field.
        /// </summary>
        /// <typeparam name="T">The type to convert the value to.</typeparam>
        /// <returns>The strongly-typed value of the field.</returns>
        public T GetValue<T>()
        {
            return Value == null ? default : (T)Convert.ChangeType(Value, typeof(T));
        }
    }
}