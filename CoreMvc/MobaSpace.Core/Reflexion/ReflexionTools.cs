using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MobaSpace.Core.Reflexion
{
    public static class ReflexionTools
    {
        public static IEnumerable<T> GetStaticsFields<T>(Type classType)
        {
            var fieldsInfo = classType.GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (var field in fieldsInfo)
            {
                yield return (T)field.GetValue(null);
            }
        }
    }
}
