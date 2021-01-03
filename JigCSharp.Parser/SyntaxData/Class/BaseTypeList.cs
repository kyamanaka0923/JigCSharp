using System.Collections.Generic;
using System.Linq;

namespace JigCSharp.Parser.SyntaxData.Class
{
    public class BaseTypeList
    {
        private IEnumerable<BaseType> _baseTypeList;

        public BaseTypeList(IEnumerable<BaseType> baseTypes)
        {
            _baseTypeList = baseTypes;
        }

        public static BaseTypeList Empty()
        {
            return new BaseTypeList(new List<BaseType>());
        }

        public bool Exist(string name)
        {
             return _baseTypeList.Any(x => x.TypeName.Contains(name));
        }
    }
}