using System;

namespace LVS
{
    public class EnumConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valueId"></param>
        /// <returns></returns>
        public static T GetEnumObjectByValue<T>(int valueId)
        {
            return (T)Enum.ToObject(typeof(T), valueId);
        }
        public static T GetEnumObjectByStringValue<T>(string value)
        {
            return (T)Enum.ToObject(typeof(T), value);
        }
    }
}
