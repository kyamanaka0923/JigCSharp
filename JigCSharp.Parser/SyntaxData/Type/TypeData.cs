namespace JigCSharp.Parser.SyntaxData.Type
{
    public class TypeData : ValueObject<TypeData>
    {
        public string TypeName { get; }

        public TypeData(string typeName)
        {
            if (typeName.Contains("<"))
            {
                TypeName = $"\"{typeName}\"";
                return;
            }

            TypeName = typeName;
        }

        protected override bool EqualsCore(TypeData other)
        {
            return TypeName == other.TypeName;
        }
    }
}
