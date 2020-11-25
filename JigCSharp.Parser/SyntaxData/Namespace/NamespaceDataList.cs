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

        public NamespaceDataList Add(NamespaceData namespaceData)
        {
            return new NamespaceDataList(_namespaceDataList.Concat(new List<NamespaceData>() {namespaceData}));;
        }

        public NamespaceDataList Concat(NamespaceDataList namespaceDataList)
        {
            return new NamespaceDataList(_namespaceDataList.Concat(namespaceDataList.ToEnumerable()));
        }

        public IEnumerable<NamespaceData> ToEnumerable()
        {
            return _namespaceDataList.AsEnumerable();
        }

        public string Display()
        {
            var returnStringBuilder = new StringBuilder();
            foreach (var namespaceData in _namespaceDataList)
            {
                returnStringBuilder.AppendLine(namespaceData.DisplayPackage());
            }

            foreach (var namespaceData in _namespaceDataList)
            {
                returnStringBuilder.AppendLine(namespaceData.DisplayAssociation());
            }
            return returnStringBuilder.ToString();
        }
    }

}
