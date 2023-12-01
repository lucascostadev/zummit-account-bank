﻿using System.Xml.Serialization;

namespace Conversion.Infrastructure.CrossCutting.Model
{
    [XmlRoot(ElementName = "Envelope", Namespace = "http://www.gesmes.org/xml/2002-08-01")]
    public class Envelope
    {
        [XmlElement(Namespace = "http://www.gesmes.org/xml/2002-08-01")]
        public string subject { get; set; } = string.Empty;

        [XmlElement(Namespace = "http://www.gesmes.org/xml/2002-08-01")]
        public Sender Sender { get; set; } = new Sender();

        [XmlElement(Namespace = "http://www.ecb.int/vocabulary/2002-08-01/eurofxref")]
        public Cube Cube { get; set; } = new Cube();
    }

    public class Sender
    {
        [XmlElement(Namespace = "http://www.gesmes.org/xml/2002-08-01")]
        public string name { get; set; } = string.Empty;
    }

    public class Cube
    {
        [XmlAttribute("time")]
        public string Time { get; set; } = string.Empty;

        [XmlElement("Cube", Namespace = "http://www.ecb.int/vocabulary/2002-08-01/eurofxref")]
        public ICollection<Cube> Cubes { get; set; } = new HashSet<Cube>();
    }

    public class Currency
    {
        [XmlAttribute("currency")]
        public string CurrencyCode { get; set; } = string.Empty;

        [XmlAttribute("rate")]
        public string Rate { get; set; } = string.Empty;
    }

}