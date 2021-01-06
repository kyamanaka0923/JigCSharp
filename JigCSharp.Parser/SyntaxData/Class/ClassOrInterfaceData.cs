using System;
using System.Collections.Generic;
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
        
        private ClassBelongedLayerKind ClassBelongedLayerKind { get; }
        
        private readonly BaseTypeList _baseTypeList;

        public ValueKind ValueKind
        {
            get
            {
                if (_baseTypeList.Exist("Enumeration"))
                {
                    return ValueKind.Enumeration;
                }

                if (_baseTypeList.Exist("ValueObject"))
                {
                    return ValueKind.ValueObject;
                }

                if (_baseTypeList.Exist("Exception"))
                {
                    return ValueKind.Exception;
                }

                return ValueKind.None;
            }
        }

        public ClassOrInterfaceData(DeclarationName symbolName, ClassOrInterfaceType type, BaseTypeList baseTypeList, ClassBelongedLayerKind classBelongedLayerKind)
        {
            SymbolName = symbolName;
            _methodDataList = new MethodDataList();
            _propertyDataList = new PropertyDataList();
            Type = type;
            _baseTypeList = baseTypeList;
            ClassBelongedLayerKind = classBelongedLayerKind;
        }

        public void AddMethod(MethodData methodData)
        {
            _methodDataList = _methodDataList.Add(methodData);
        }

        public void AddProperty(PropertyAndFieldData propertyAndField)
        {
            _propertyDataList = _propertyDataList.Add(propertyAndField);
        }

        public string DisplayPlantuml()
        {
            var returnString = new StringBuilder();

            var type = "class";
            if (Type == ClassOrInterfaceType.Interface)
            {
                type = "interface";
            }

            returnString.AppendLine($"  {type} \"{SymbolName.DisplayName}\" as {SymbolName.Name}{{");
            returnString.AppendLine("  }");

            //_methodDataList.DisplayPlantuml();

            //_propertyDataList.DisplayPlantuml();

            return returnString.ToString();
        }

        public ClassDto ToDto()
        {
            return new ClassDto(this.SymbolName.Name, this.SymbolName.DisplayName, "dummyModifier",
                _methodDataList.ToDtos(), ValueKind.Name, ClassBelongedLayerKind.Name);
        }

        public string DisplayAccessPlantuml()
        {
            var typeList = _propertyDataList.GetTypeList();
            typeList = typeList.Concat(_methodDataList.GetTypeList());

            return typeList.DisplayPlantuml(new TypeData(SymbolName.Name));
        }

        public string DisplayList()
        {
            var returnStringBuilder = new StringBuilder();
            returnStringBuilder.AppendLine($"## クラス名 : {SymbolName.Name}");
            returnStringBuilder.AppendLine($"### 別名 : {SymbolName.DisplayName}");
            returnStringBuilder.AppendLine(_methodDataList.DisplayList());

            return returnStringBuilder.ToString();
        }

        public bool IsClass()
        {
            return Type == ClassOrInterfaceType.Class;
        }

    }

}
