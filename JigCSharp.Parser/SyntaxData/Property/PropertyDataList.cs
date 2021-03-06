﻿using System.Collections.Generic;
using System.Linq;
using JigCSharp.Parser.SyntaxData.Type;

namespace JigCSharp.Parser.SyntaxData.Property
{
    public class PropertyDataList
    {
        private readonly List<PropertyAndFieldData> _propertyDataList;

        public PropertyDataList()
        {
            _propertyDataList = new List<PropertyAndFieldData>();
        }

        PropertyDataList(IEnumerable<PropertyAndFieldData> propertyAndFieldDatas)
        {
            _propertyDataList = propertyAndFieldDatas.ToList();
        }

        public PropertyDataList Add(PropertyAndFieldData propertyAndFieldData)
        {
            return new PropertyDataList(_propertyDataList.Concat(new List<PropertyAndFieldData>() { propertyAndFieldData }));
        }

        public void Display()
        {
            foreach (var propertyData in _propertyDataList)
            {
                propertyData.Display();
            }
        }

        public void DisplayTypeFrom(TypeData type)
        {
            var typeList = GetTypeList();

            typeList.DisplayPlantuml(type);
        }

        public TypeList GetTypeList()
        {
            var typeList = _propertyDataList.Select(propertyData => propertyData.Type);
            return new TypeList(typeList);
        }
    }
}
