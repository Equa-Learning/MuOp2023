using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MuOp2023.Infrastructure
{
    /// <summary>
    /// uses extension methods to convert enums with hypens in their names to underscore and other variants
    /// I'm not sure this is a good idea. While it makes that section of the code much much nicer to maintain, it 
    /// also incurs a performance hit via reflection. To circumvent this, I've added a dictionary so all the lookup can be done once at 
    /// load time. It requires that all enums involved in this extension are in this assembly.
    /// </summary>
    public static class EnumExtensions
    {
        //To avoid collisions, every Enum type has its own hash table
        private static readonly Dictionary<Type, Dictionary<object, string>> EnumToStringDictionary =
            new Dictionary<Type, Dictionary<object, string>>();

        private static readonly Dictionary<Type, Dictionary<string, object>> StringToEnumDictionary =
            new Dictionary<Type, Dictionary<string, object>>();

        private static void LoadType(Type type)
        {
            EnumToStringDictionary.Add(type, new Dictionary<object, string>());
            StringToEnumDictionary.Add(type, new Dictionary<string, object>());

            var values = Enum.GetValues(type);
            foreach (var value in values)
            {
                var fieldInfo = type.GetField(value.ToString());

                if (Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute))
                    is DescriptionAttribute attribute
                   )
                {
                    EnumToStringDictionary[type].Add(value, attribute.Description);
                    StringToEnumDictionary[type].Add(attribute.Description, value);
                }
                else
                {
                    EnumToStringDictionary[type].Add(value, value.ToString());
                    StringToEnumDictionary[type].Add(value.ToString(), value);
                }
            }
        }

        public static string GetDescription(this Enum value)
        {
            var type = value.GetType();
            if (!StringToEnumDictionary.ContainsKey(type))
                LoadType(type);

            return EnumToStringDictionary[type][value];
        }

        public static T ToEnum<T>(this string value)
        {
            var type = typeof(T);
            if (!StringToEnumDictionary.ContainsKey(type))
                LoadType(type);

            return (T) StringToEnumDictionary[type][value];
        }

        public static T ToEnumOrFallback<T>(this string value, T fallbackValue)
        {
            var type = typeof(T);
            if (!StringToEnumDictionary.ContainsKey(type))
                LoadType(type);

            if (!StringToEnumDictionary[type].ContainsKey(value)) return fallbackValue;
            return (T) StringToEnumDictionary[type][value];
        }
        public static T ToEnumOrDefault<T>(this string value)
        {
            var type = typeof(T);
            if (!StringToEnumDictionary.ContainsKey(type))
                LoadType(type);

            if (!StringToEnumDictionary[type].ContainsKey(value)) return default;
            return (T) StringToEnumDictionary[type][value];
        }        
    }
}