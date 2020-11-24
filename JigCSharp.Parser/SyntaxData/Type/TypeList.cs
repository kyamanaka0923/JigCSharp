using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JigCSharp.Parser
{
    public class TypeList
    {
        private List<Type> _typeList;

        public TypeList(IEnumerable<Type> typeList)
        {
            // 重複する型名を削除する
            _typeList = new List<Type>();
            foreach (var type in typeList)
            {
                if (!_typeList.Exists(x => x.TypeName == type.TypeName))
                {
                    _typeList.Add(type);
                }
            }
        }

        public TypeList(TypeList typeList)
        {
            _typeList = typeList.ToEnumerable().ToList();
        }

        public IEnumerable<Type> ToEnumerable()
        {
            return _typeList;
        }

        public TypeList Concat(TypeList typeList)
        {
            return new TypeList(_typeList.Concat(typeList.ToEnumerable()));
        }

        public void Display(Type fromType)
        {
            var displayTypeList = _typeList.Where(x => x != new Type("void")).Where(x => x != fromType);
            foreach (var type in displayTypeList)
            {
                Console.WriteLine($"{fromType.TypeName} --> {type.TypeName}");
            }
        }
    }

}
