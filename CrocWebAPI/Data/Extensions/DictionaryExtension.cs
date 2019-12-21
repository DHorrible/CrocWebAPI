using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrocWebAPI.Data.Extensions
{
    public static class DictionaryExtension
    {
        public static T Get<T>(this IDictionary<string, object> data, string key) 
        {
            if (data.TryGetValue(key, out var value))
            {
                if (!(value is T))
                {
                    throw new FormatException();
                }
                    
                return (T)value;
                
            }
            
            throw new KeyNotFoundException();
        }

        public static T TryGet<T>(this IDictionary<string, object> data, string key)
            where T : class
        {
            if (data.TryGetValue(key, out var value))
            {
                if (!(value is T))
                {
                    throw new FormatException();
                }

                return value as T;
            }
            
            return null;
        }
    }
}
