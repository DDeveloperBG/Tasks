using System.Linq;

namespace VaporStore.DataProcessor.DTOs.Export
{
    public class GenreExportModel
    {
        public int Id { get; set; }

        public string Genre { get; set; }

        public GameExportModel[] Games { get; set; }

        public int TotalPlayers { get => Games.Sum(x => x.Players); set { } }
    }

    public class GameExportModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Developer { get; set; }

        public string Tags { get; set; }

        public int Players { get; set; }
    }
}
