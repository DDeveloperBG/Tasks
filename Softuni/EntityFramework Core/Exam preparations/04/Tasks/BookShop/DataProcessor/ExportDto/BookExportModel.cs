using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml.Serialization;

namespace BookShop.DataProcessor.ExportDto
{
    [XmlType("Book")]
    public class BookExportModel
    {
        [XmlAttribute]
        public int Pages { get; set; }

        public string Name { get; set; }

        [XmlIgnore]
        public DateTime DateAsDateTime { get; set; }

        public string Date 
        {
            get => DateAsDateTime
                .ToString("d", CultureInfo.InvariantCulture);

            set { } }
    }
}
