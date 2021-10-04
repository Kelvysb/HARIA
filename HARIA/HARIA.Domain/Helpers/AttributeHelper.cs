using System;
using System.Linq;
using HARIA.Domain.Attributes;
using HARIA.Domain.Entities;

namespace HARIA.Domain.Helpers
{
    public static class AttributeHelper
    {
        public static string GetCollectionName<T>()
           where T : EntityBase, new()
        {
            var attribute =
                (Collection)Attribute.GetCustomAttributes(typeof(T), typeof(Collection)).FirstOrDefault();

            return attribute != null ? attribute.Name : null;
        }
    }
}