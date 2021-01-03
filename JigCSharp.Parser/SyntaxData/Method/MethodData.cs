using System;
using JigCSharp.Parser.SyntaxData.Common;
using JigCSharp.Parser.SyntaxData.Type;

namespace JigCSharp.Parser.SyntaxData.Method
{
    public class MethodData
    {
        public DeclarationName Name { get; }

        public TypeData ReturnTypeData { get; }

        public string Modifier { get; }

        public MethodData(DeclarationName name, string modifier, TypeData returnTypeData)
        {
            Name = name;
            Modifier = modifier;
            ReturnTypeData = returnTypeData;
        }

        public void DisplayPlantuml()
        {
            Console.WriteLine($"    {ReturnTypeData.TypeName}:{Modifier}{Name.Name}");
        }

        public string DisplayList()
        {
            return $"### メソッド : {Name.Name} ({Name.DisplayName})\n#### 戻り値 : {ReturnTypeData.TypeName}";
        }

        /// <summary>
        /// DTO型へ変換
        /// </summary>
        /// <returns>MethodのDTO型</returns>
        public MethodDto ToDto()
        {
            return new MethodDto(Name.Name, Name.DisplayName, Modifier, ReturnTypeData.TypeName);
        }
    }
}
