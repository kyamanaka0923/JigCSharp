using System.Collections.Generic;
using System.Linq;
using JigCSharp.Parser.SyntaxData.Type;

namespace JigCSharp.Parser.SyntaxData.Method
{
    class MethodDataList
    {
        private readonly List<MethodData> _methodDataList;

        public MethodDataList()
        {
            _methodDataList = new List<MethodData>();
        }

        MethodDataList(IEnumerable<MethodData> methodDataList)
        {
            _methodDataList = methodDataList.ToList();
        }

        public MethodDataList Add(MethodData methodData)
        {
            return new MethodDataList(_methodDataList.Concat(new List<MethodData>() {methodData}));
        }

        public TypeList GetTypeList()
        {
            return new TypeList(_methodDataList.Select(x => x.ReturnTypeData));
        }

        public void Display()
        {
            foreach (var method in _methodDataList)
            {
                method.Display();
            }
        }
    }
}
