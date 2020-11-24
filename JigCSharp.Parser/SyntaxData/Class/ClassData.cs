using System;
using System.Collections.Generic;
using JigCSharp.Parser.SyntaxData.Method;
using JigCSharp.Parser.SyntaxData.Property;

namespace JigCSharp.Parser.SyntaxData.Class
{

    public class ClassData
    {
        public DeclarationName SymbolName { get; }

        private MethodDataList _methodDataList;

        private PropertyDataList _propertyDataList;

        public ClassData(DeclarationName symbolName)
        {
            SymbolName = symbolName;
            _methodDataList = new MethodDataList();
            _propertyDataList = new PropertyDataList();
        }

        public void AddMethod(MethodData methodData)
        {
            _methodDataList = _methodDataList.Add(methodData);
        }

        public void AddProperty(PropertyAndFieldData propertyAndField)
        {
            _propertyDataList = _propertyDataList.Add(propertyAndField);
        }

        public void Display()
        {
            Console.WriteLine($"  class \"{SymbolName.DisplayName}\" as {SymbolName.Name}{{");

            //_methodDataList.Display();

            //_propertyDataList.Display();

            Console.WriteLine("  }");

        }

        public void DisplayAccess()
        {
            var typeList = _propertyDataList.GetTypeList();
            typeList = typeList.Concat(_methodDataList.GetTypeList());

            typeList.Display(new Type(SymbolName.Name));
        }

    }

}
