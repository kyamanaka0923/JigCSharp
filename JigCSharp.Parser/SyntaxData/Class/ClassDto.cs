using System.Collections.Generic;
using JigCSharp.Parser.SyntaxData.Method;

namespace JigCSharp.Parser.SyntaxData.Class
{
    public class ClassDto
    {
        public string Name { get; }
        
        public string DisplayName { get; }
        
        public string Modifier { get; }
        
        public IEnumerable<MethodDto> Methods { get; }
        
        public string ValueKind { get; }

        public ClassDto(string name, string displayName, string modifier, IEnumerable<MethodDto> methodDtos, string valueKind)
        {
            Name = name;
            DisplayName = displayName;
            Modifier = modifier;
            Methods = methodDtos;
            ValueKind = valueKind;
        }
    }
}