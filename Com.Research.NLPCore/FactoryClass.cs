using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Research.NLPCore
{
    public class FactoryClass<T>
    {

          /// <summary>
        /// A dictionary to store pairs of type name and its class type
        /// </summary>
        private Dictionary<string, Type> _types = new Dictionary<string, Type>();

        public FactoryClass()
        {
            RegisterTypes();
        }

        /// <summary>
        /// Creates correspoding type by type name
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public T Create(string typeName)
        {
            if (!_types.ContainsKey(typeName))
                throw new ApplicationException("TypeFactory: Target type is not found");

            return (T)Activator.CreateInstance(_types[typeName]);
        }

        /// <summary>
        /// Register types from assembly which implement interface T
        /// </summary>
        private void RegisterTypes()
        {
            Type[] types = typeof(T).Assembly.GetTypes();
            foreach (Type type in types)
            {
                if (type.GetInterfaces().Contains(typeof(T)))
                    _types.Add(type.Name, type);
            }
        }
    }
}
