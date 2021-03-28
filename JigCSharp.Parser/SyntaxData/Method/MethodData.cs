using System;
using System.Collections.Generic;
using System.Text;
using JigCSharp.Parser.SyntaxData.Common;
using JigCSharp.Parser.SyntaxData.Type;

namespace JigCSharp.Parser.SyntaxData.Method
{
    public class MethodData
    {
        public DeclarationName Name { get; }

        public TypeData ReturnTypeData { get; }

        public string Modifier { get; }
        
        public List<XmlDocument.ParamComment> _ParamCommentList { get; }

        public MethodData(DeclarationName name, string modifier, TypeData returnTypeData, IEnumerable<XmlDocument.ParamComment> paramComments)
        {
            Name = name;
            Modifier = modifier;
            ReturnTypeData = returnTypeData;
            _ParamCommentList = new List<XmlDocument.ParamComment>(paramComments);
        }

        public void DisplayPlantuml()
        {
            Console.WriteLine($"    {ReturnTypeData.TypeName}:{Modifier}{Name.Name}");
        }

        public string DisplayList()
        {
            var returnStringBuilder = new StringBuilder();
            returnStringBuilder.AppendLine($"### メソッド : {Name.Name} ({Name.DisplayName})");
            returnStringBuilder.AppendLine($"#### 戻り値 : {ReturnTypeData.TypeName}");
            returnStringBuilder.AppendLine($"### 引数");
            foreach (var param in _ParamCommentList)
            {
                returnStringBuilder.AppendLine($"{param.Name}:{param.AliasName}");
            }

            return returnStringBuilder.ToString();
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
