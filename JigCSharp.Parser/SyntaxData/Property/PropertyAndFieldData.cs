using System;

namespace JigCSharp.Parser.SyntaxData.Property
{
    public class PropertyAndFieldData : ValueObject<PropertyAndFieldData>
    {
        public DeclarationName Name { get; }
        public Type Type { get; }

        public PropertyAndFieldData(DeclarationName name, Type type)
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
