using System;
using System.Collections.Generic;
using System.Text;

namespace TestPathDesigner.Testing
{
    public class TestModel
    {
        public string ElementName { get; private set; }
        public ElementTypeEnum ElementType { get; private set; }
        public TestModel(string elementName, ElementTypeEnum elementType)
        {
            ElementName = elementName;
            ElementType = elementType;
        }
    }
}
