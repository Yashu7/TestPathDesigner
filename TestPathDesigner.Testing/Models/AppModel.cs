using System;
using System.Collections.Generic;
using System.Text;

namespace TestPathDesigner.Testing.Models
{
    public class AppModel
    {
        public string Name { get; set; }
        public string PackageFamilyName { get; set; }
        public AppModel(string name, string packageFamilyName)
        {
            Name = name;
            PackageFamilyName = packageFamilyName;
        }
        public AppModel()
        {
            
        }

    }
}
