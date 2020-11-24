using System;
using JigCSharp.Parser.SyntaxData.Common;
using JigCSharp.Parser.SyntaxData.Type;

namespace JigCSharp.Parser.SyntaxData.Method
{
    public class MethodData
    {
        public DeclarationName Name { get; }

        public TypeData ReturnTypeData { get; }

        private string Modifier { get; }

        public MethodData(DeclarationName name, string modifier, TypeData returnTypeData)
        {
            Name = name;
            Modifier = modifier;
            ReturnTypeData = returnTypeData;
        }

        public void Display()
        {
            Console.WriteLine($"    {ReturnTypeData.TypeName}:{Modifier}{Name.Name}");
        }
    }
}
