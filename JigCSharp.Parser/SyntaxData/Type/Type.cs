using System;
using System.Collections.Generic;
using System.Text;

namespace JigCSharp.Parser
{
    public class Type : ValueObject<Type>
    {
        public string TypeName { get; }

        public Type(string typeName)
        {
            if (typeName.Contains("<"))
            {
                TypeName = $"\"{typeName}\"";
                return;
            }

            TypeName = typeName;
        }

        protected override bool EqualsCore(Type other)
        {
            return TypeName == other.TypeName;
        }
    }
}
