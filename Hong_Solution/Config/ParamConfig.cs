using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hong_Solution
{
    public class ParamConfig
    {
        public NameValue NameValue { get; set; }
        public Age Age { get; set; }

        public Arraytest Arraytest { get; set; }
        
    }

    public class NameValue
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
    public class Age
    {
        public string ageisnaee { get; set; }
    }

    public class Arraytest
    {
        public string[] arraytest { get; set; } 
    }
    
}
