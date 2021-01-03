using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JigCSharp.Parser.SyntaxData.Class;
using JigCSharp.Parser.SyntaxData.Common;

namespace JigCSharp.Parser.SyntaxData.Namespace
{
    public class NamespaceData
    {
        public DeclarationName Name { get; }

        private List<ClassOrInterfaceData> _classDataList;

        public NamespaceData(DeclarationName name)
        {
            Name = name;
            _classDataList = new List<ClassOrInterfaceData>();
        }

        public void AddClassData(ClassOrInterfaceData classOrInterfaceData)
        {
            _classDataList.Add(classOrInterfaceData);
        }

        public void AddClassData(IEnumerable<ClassOrInterfaceData> classOrInterfaceDatas)
        {
            _classDataList = _classDataList.Concat(classOrInterfaceDatas).ToList();
        }

        public string StringToPlantuml()
        {
            var returnStringBuilder = new StringBuilder();

            returnStringBuilder.AppendLine($"package \"{Name.Name}{Name._displayName}\"{{");
            foreach (var classData in _classDataList)
            {
                returnStringBuilder.AppendLine(classData.DisplayPlantuml());
            }
            returnStringBuilder.AppendLine("}");
            return returnStringBuilder.ToString();
        }

        public string DisplayAssociation()
        {
            var returnStringBuilder = new StringBuilder();

            foreach (var classData in _classDataList)
            {
                returnStringBuilder.AppendLine(classData.DisplayAccessPlantuml());
            }

            return returnStringBuilder.ToString();
        }

        /// <summary>
        /// クラス/インタフェースのリストを取得する
        /// </summary>
        /// <returns></returns>
        public IReadOnlyCollection<ClassOrInterfaceData> GetClassOrInterfaceDataCollection()
        {
            return _classDataList;
        }

        public string DisplayList()
        {
            var returnStringBuilder = new StringBuilder();

            foreach (var classOrInterfaceData in _classDataList)
            {
                returnStringBuilder.AppendLine(classOrInterfaceData.DisplayList());
            }

            return returnStringBuilder.ToString();
        }

        /// <summary>
        /// NamespaceのDTOを返す
        /// </summary>
        /// <returns></returns>
        public NamespaceDto ToDto()
        {
            var classDtos = _classDataList.Select(x => { return x.ToDto(); });
            return new NamespaceDto(Name.Name, Name.DisplayName, classDtos);
        }
    }
}
