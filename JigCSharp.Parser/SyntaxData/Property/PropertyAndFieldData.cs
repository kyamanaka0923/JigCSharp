using System;
using JigCSharp.Parser.SyntaxData.Common;
using JigCSharp.Parser.SyntaxData.Type;

namespace JigCSharp.Parser.SyntaxData.Property
{
    public class PropertyAndFieldData : ValueObject<PropertyAndFieldData>
    {
        public DeclarationName Name { get; }
        public TypeData Type { get; }

        public PropertyAndFieldData(DeclarationName name, TypeData type)
        {
            Type = type;
            Name = name;
        }

        public void Display()
        {
            Console.WriteLine($"    {Name._displayName}:{Type.TypeName}");
        }

        protected override bool EqualsCore(PropertyAndFieldData other)
        {
            return other.Name == Name;
        }
    }

}
