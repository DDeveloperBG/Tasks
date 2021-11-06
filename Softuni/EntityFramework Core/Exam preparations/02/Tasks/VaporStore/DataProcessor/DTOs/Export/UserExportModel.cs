using System;
using System.Globalization;
using System.Linq;
using System.Xml.Serialization;

namespace VaporStore.DataProcessor.DTOs.Export
{
    [XmlType("User")]
    public class UserExportModel
    {
        [XmlAttribute("username")]
        public string Username { get; set; }

        public PurchaseExportModel[] Purchases { get; set; }

        public decimal TotalSpent 
        { 
            get => Purchases.Sum(x => x.Game.Price); 
            set { } 
        }
    }

    [XmlType("Purchase")]
    public class PurchaseExportModel
    {
        public string Card { get; set; }

        public string Cvc { get; set; }

        [XmlIgnore]
        public DateTime Date { get; set; }

        [XmlElement("Date")]
        public string DateAsString 
        {
            get => Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture); 
            set { } 
        }

        public GameExportModel2 Game { get; set; }
    }

    [XmlType("Game")]
    public class GameExportModel2
    {
        [XmlAttribute("title")]
        public string Title { get; set; }

        public string Genre { get; set; }

        public decimal Price { get; set; }
    }
}
