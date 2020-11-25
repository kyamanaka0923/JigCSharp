using System;
using System.Text;
using JigCSharp.Parser.SyntaxData.Common;
using JigCSharp.Parser.SyntaxData.Method;
using JigCSharp.Parser.SyntaxData.Property;
using JigCSharp.Parser.SyntaxData.Type;

namespace JigCSharp.Parser.SyntaxData.Class
{

    public class ClassOrInterfaceData
    {
        public DeclarationName SymbolName { get; }

        private MethodDataList _methodDataList;

        private PropertyDataList _propertyDataList;

        private ClassOrInterfaceType Type { get; }

        public ClassOrInterfaceData(DeclarationName symbolName, ClassOrInterfaceType type)
        {
            SymbolName = symbolName;
            _methodDataList = new MethodDataList();
            _propertyDataList = new PropertyDataList();
            Type = type;
        }

        public void AddMethod(MethodData methodData)
        {
            _methodDataList = _methodDataList.Add(methodData);
        }

        public void AddProperty(PropertyAndFieldData propertyAndField)
        {
            _propertyDataList = _propertyDataList.Add(propertyAndField);
        }

        public string Display()
        {
            var returnString = new StringBuilder();

            var type = "class";
            if (Type == ClassOrInterfaceType.Interface)
            {
                type = "interface";
            }

            returnString.AppendLine($"  {type} \"{SymbolName.DisplayName}\" as {SymbolName.Name}{{");
            returnString.AppendLine("  }");

            //_methodDataList.Display();

            //_propertyDataList.Display();

            return returnString.ToString();

        }

        public string DisplayAccess()
        {
            var typeList = _propertyDataList.GetTypeList();
            typeList = typeList.Concat(_methodDataList.GetTypeList());

            return typeList.Display(new TypeData(SymbolName.Name));
        }

    }

}
