using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace CarDealer.DTOs.Import
{
    [XmlType("Car")]
    public class CarDTO
    {
        [XmlElement("make")]
        public string Make { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("TraveledDistance")]
        public long TravelledDistance { get; set; }

        [XmlArray(ElementName = "parts")]
        [XmlArrayItem(ElementName = "partId")]
        public HashSet<CarPartId> Parts { get; set; }
    }

    public class CarPartId : IEquatable<CarPartId>
    {
        [XmlAttribute(AttributeName = "id")]
        public int Id { get; set; }

        public bool Equals(CarPartId other)
        {
            return this.Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
