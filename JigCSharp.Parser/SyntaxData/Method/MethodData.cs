using System;
using System.Collections.Generic;
using System.Text;

namespace JigCSharp.Parser
{
    public class MethodData
    {
        public DeclarationName Name { get; }

        public Type ReturnType { get; }

        private string Modifier { get; }

        public MethodData(DeclarationName name, string modifier, Type returnType)
        {
            Name = name;
            Modifier = modifier;
            ReturnType = returnType;
        }

        public void Display()
        {
            Console.WriteLine($"    {ReturnType.TypeName}:{Modifier}{Name.Name}");
        }
    }
}
