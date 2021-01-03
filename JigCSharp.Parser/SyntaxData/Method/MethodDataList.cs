using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        /// <summary>
        /// MethodのDTOリストを取得する
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MethodDto> ToDtos()
        {
            return _methodDataList.Select(x => x.ToDto());
        }

        public MethodDataList Add(MethodData methodData)
        {
            return new MethodDataList(_methodDataList.Concat(new List<MethodData>() {methodData}));
        }

        public TypeList GetTypeList()
        {
            return new TypeList(_methodDataList.Select(x => x.ReturnTypeData));
        }

        public void DisplayPlantml()
        {
            foreach (var method in _methodDataList)
            {
                method.DisplayPlantuml();
            }
        }

        public string DisplayList()
        {
            var returnStringBuilder = new StringBuilder();
            foreach (var method in _methodDataList.Where(x => x.Modifier == "+"))
            {
                returnStringBuilder.AppendLine(method.DisplayList());
            }

            return returnStringBuilder.ToString();
        }
    }
}
