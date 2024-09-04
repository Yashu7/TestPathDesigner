using System;
using System.Collections.Generic;
using System.Text;

namespace TestPathDesigner.Testing
{
    public class TestModel
    {
        public string ElementName { get; private set; }
        public ElementTypeEnum ElementType { get; private set; }
        public ActionEnum Action { get; set; }
        public TestModel(string elementName, ElementTypeEnum elementType, ActionEnum action)
        {
            ElementName = elementName;
            ElementType = elementType;
            Action = action;
        }
    }
}
