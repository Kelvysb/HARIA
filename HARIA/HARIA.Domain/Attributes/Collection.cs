using System;

namespace HARIA.Domain.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    internal sealed class Collection : Attribute
    {
        private readonly string name;

        public Collection(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get { return name; }
        }
    }
}