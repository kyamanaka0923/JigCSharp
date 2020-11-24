using System.Collections.Generic;
using System.Linq;

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

        public void Display()
        {
            foreach (var namespaceData in _namespaceDataList)
            {
                namespaceData.Display();
            }
        }
    }

}
