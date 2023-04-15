using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataView
{
    public class EmailProperty
    {

        public PropertyData PropertyData { get; set; }
        public string Traduction { get; set; }

        public EmailProperty(PropertyData propertyData,string traduction)
        {
            PropertyData = propertyData;    
            Traduction = traduction;
        }

    }
}
