using Newtonsoft.Json;
using System;

namespace QuickbaseNet.Models
{
    public class FieldValue
    {
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public object Value { get; set; }

        public T GetValue<T>()
        {
            return Value == null ? default : (T)Convert.ChangeType(Value, typeof(T));
        }
    }
}