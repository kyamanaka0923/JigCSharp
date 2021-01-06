using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JigCSharp.Parser.SyntaxData.Namespace
{
    public class NamespaceDataList
    {
        private List<NamespaceData> _namespaceDataList;

        public NamespaceDataList()
        {
            _namespaceDataList = new List<NamespaceData>();
        }

        private NamespaceDataList(IEnumerable<NamespaceData> namespaceDatas)
        {
            _namespaceDataList = namespaceDatas.ToList();
        }

        public void Add(NamespaceData namespaceData)
        {
            var selectedNamespaceData = _namespaceDataList.FirstOrDefault(x => x.Name == namespaceData.Name);
            if (selectedNamespaceData != null)
            {
                selectedNamespaceData.AddClassData(namespaceData.GetClassOrInterfaceDataCollection());
                return;
            }
            _namespaceDataList = _namespaceDataList.Concat(new List<NamespaceData>() {namespaceData}).ToList();
        }

        public NamespaceDataList Concat(NamespaceDataList namespaceDataList)
        {
            foreach (var namespaceData in namespaceDataList.ToEnumerable())
            {
                Add(namespaceData);
            }

            return new NamespaceDataList(_namespaceDataList);
        }

        public NamespaceDataList Exclude(IEnumerable<string> excludeNameList)
        {
            if (!_namespaceDataList.Any())
            {
                return new NamespaceDataList(_namespaceDataList);
            }

            if (!excludeNameList.Any())
            {
                return new NamespaceDataList(_namespaceDataList);
            }
                
            return new NamespaceDataList(_namespaceDataList.Where(x =>
                !excludeNameList.Any(y => x.Name.Name.StartsWith(y))));
        }

        public IEnumerable<NamespaceData> ToEnumerable()
        {
            return _namespaceDataList.AsEnumerable();
        }

        public string StringToPlantuml()
        {
            var returnStringBuilder = new StringBuilder();
            foreach (var namespaceData in _namespaceDataList)
            {
                returnStringBuilder.AppendLine(namespaceData.StringToPlantuml());
            }

            foreach (var namespaceData in _namespaceDataList)
            {
                returnStringBuilder.AppendLine(namespaceData.DisplayAssociation());
            }
            return returnStringBuilder.ToString();
        }

        public string DisplayList()
        {
            var returnStringBuilder = new StringBuilder();
            foreach (var namespaceData in _namespaceDataList)
            {
                returnStringBuilder.AppendLine(namespaceData.DisplayList());
            }

            return returnStringBuilder.ToString();
        }

        public IEnumerable<NamespaceDto> ToDto()
        {
            return _namespaceDataList.Select(x => x.ToDto());
        }
    }

}
