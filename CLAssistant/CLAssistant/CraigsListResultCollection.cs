using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLAssistant
{
    [Serializable()]
    public class CraigsListResultCollection
    {
        [XmlArray("CraigsListResults")]
        [XmlArrayItem("CraigsListResult", typeof(CraigsListResult))]
        public CraigsListResult[] CraigsListResult { get; set; }
    }
}
