using System;
using System.Collections.Generic;
using System.Linq;

namespace JigCSharp.Parser.SyntaxData.Type
{
    public class TypeList
    {
        private List<TypeData> _typeList;

        public TypeList(IEnumerable<TypeData> typeList)
        {
            // 重複する型名を削除する
            _typeList = new List<TypeData>();
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

        public IEnumerable<TypeData> ToEnumerable()
        {
            return _typeList;
        }

        public TypeList Concat(TypeList typeList)
        {
            return new TypeList(_typeList.Concat(typeList.ToEnumerable()));
        }

        public void Display(TypeData fromTypeData)
        {
            var displayTypeList = _typeList.Where(x => x != new TypeData("void")).Where(x => x != fromTypeData);
            foreach (var type in displayTypeList)
            {
                Console.WriteLine($"{fromTypeData.TypeName} --> {type.TypeName}");
            }
        }
    }

}
