using System;
using System.Globalization;
using System.Linq;
using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ExportDto
{
    [XmlType("Prisoner")]
    public class PrisonerExportModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [XmlIgnore]
        public DateTime IncarcerationDateAsDate { get; set; }

        public string IncarcerationDate 
        {
            get => IncarcerationDateAsDate
                .ToString("yyyy-MM-dd", CultureInfo.InvariantCulture); 
            set { }
        }

        public Message[] EncryptedMessages { get; set; }
    }

    public class Message
    {
        private string description = string.Empty;

        public string Description
        {
            get => description;
            set 
            {
                description = string.Join("", value.Reverse());
            }
        }
    }
}
