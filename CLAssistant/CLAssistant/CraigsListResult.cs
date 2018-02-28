using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLAssistant
{
    [Serializable()]
    public class CraigsListResult
    {
        [XmlElement("date")]
        public string date { get; set; }

        [XmlElement("hyperlink")]
        public string hyperlink { get; set; }

        [XmlElement("description")]
        public string description { get; set; }

        [XmlElement("price")]
        public string price { get; set; }

        [XmlElement("location")]
        public string location { get; set; }

        [XmlElement("hasPicture")]
        public bool hasPicture { get; set; }
    }
}
