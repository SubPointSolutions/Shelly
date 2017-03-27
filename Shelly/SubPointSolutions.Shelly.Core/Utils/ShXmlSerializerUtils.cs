using System;
using System.Collections.Generic;
using SubPointSolutions.Shelly.Core.Services;

namespace SubPointSolutions.Shelly.Core.Utils
{
    public class ShXmlSerializerUtils
    {
        #region methods

        public static string SerializeToString(object obj)
        {
            return SerializeToString(obj, new Type[] { });
        }

        public static string SerializeToString(object obj, IEnumerable<Type> extraTypes)
        {
            var serializer = new ShSerializationDataService();

            if (extraTypes != null)
                ShSerializationDataService.RegisterKnownTypes(extraTypes);

            return serializer.Serialize(obj);
        }

        public static T DeserializeFromString<T>(string value)
        {
            return DeserializeFromString<T>(value, new Type[] { });
        }

        public static T DeserializeFromString<T>(string value, IEnumerable<Type> extraTypes)
        {
            var serializer = new ShSerializationDataService();

            if (extraTypes != null)
                ShSerializationDataService.RegisterKnownTypes(extraTypes);

            var result = serializer.Deserialize(typeof(T), value);

            if (result != null)
                return (T)result;

            return default(T);
        }

        #endregion
    }
}
