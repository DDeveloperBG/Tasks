namespace MusicHub
{
    using System;
    using System.Linq;
    using System.Globalization;

    using Data;
    using Initializer;
    using System.Collections.Generic;

    public class StartUp
    {
        public static void Main()
        {
            MusicHubDbContext context =
                new MusicHubDbContext();

            DbInitializer.ResetDatabase(context);

            //Console.WriteLine(Task1.ExportAlbumsInfo(context, 9));
            Console.WriteLine(ExportSongsAboveDuration(context, 4));
        }

        // For judge to pass
        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            return Task1.ExportAlbumsInfo(context, producerId);
        }

        // For judge to pass
        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            return Task2.ExportSongsAboveDuration(context, duration);
        }

        private static class Task1
        {
            public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
            {
                var albums = context.Albums//.ToList() <- FOR JUDGE TO PASS !!!
                    .Where(x => x.ProducerId == producerId)
                    .Select(x => new TempAlbum
                    {
                        AlbumName = x.Name,

                        ReleaseDate = x.ReleaseDate
                            .ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),

                        ProducerName = x.Producer.Name,

                        Songs = x.Songs
                            .Select(y => new TempSong
                            {
                                SongName = y.Name,
                                Price = y.Price,
                                WriterName = y.Writer.Name
                            })
                            .OrderByDescending(y => y.SongName)
                            .ThenBy(y => y.WriterName)
                            .ToList(),

                        AlbumPrice = x.Price
                    })
                    .ToList()
                    .OrderByDescending(x => x.AlbumPrice);

                var formatedAlbums = albums
                     .Select(x => x
                        .ToString(SongsToString(x.Songs)));

                return string.Join(Environment.NewLine, formatedAlbums);
            }

            private static string SongsToString(IEnumerable<TempSong> songs)
            {
                return string.Join(Environment.NewLine, songs
                                .Select((x, index) => x.ToString(index + 1)));
            }

            private class TempSong
            {
                public string SongName { get; set; }
                public decimal Price { get; set; }
                public string WriterName { get; set; }

                private static readonly string songPattern = "---#{0}" + Environment.NewLine +
                        "---SongName: {1}" + Environment.NewLine +
                        "---Price: {2:F2}" + Environment.NewLine +
                        "---Writer: {3}";

                public string ToString(int index)
                {
                    return string.Format(songPattern,
                        index,
                        SongName,
                        Price,
                        WriterName);
                }
            }

            private class TempAlbum
            {
                public string AlbumName { get; set; }

                public string ReleaseDate { get; set; }

                public string ProducerName { get; set; }

                public IEnumerable<TempSong> Songs { get; set; }

                public decimal AlbumPrice { get; set; }

                private static readonly string albumPattern = "-AlbumName: {0}" + Environment.NewLine +
                         "-ReleaseDate: {1}" + Environment.NewLine +
                         "-ProducerName: {2}" + Environment.NewLine +
                         "-Songs:" + Environment.NewLine +
                         "{3}" + Environment.NewLine +
                         "-AlbumPrice: {4:F2}";

                public string ToString(string songsStringFormat)
                {
                    return string.Format(albumPattern,
                        AlbumName,
                        ReleaseDate,
                        ProducerName,
                        songsStringFormat,
                        AlbumPrice);
                }
            }
        }

        private static class Task2
        {
            public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
            {
                var wantedDuration = new TimeSpan(0, 0, duration);

                var songs = context.Songs
                    .Where(x => x.Duration > wantedDuration)
                    .Select(x => new TempSong
                    {
                        SongName = x.Name,

                        PerformerFullName = x.SongPerformers
                                .Select(x => x.Performer.FirstName + " " + x.Performer.LastName)
                                .FirstOrDefault(),

                        WriterName = x.Writer.Name,

                        AlbumProducer = x.Album.Producer.Name,

                        Duration = x.Duration.ToString(@"hh\:mm\:ss")
                    })
                    .OrderBy(x => x.SongName)
                    .ThenBy(x => x.WriterName)
                    .ThenBy(x => x.PerformerFullName)
                    .ToList();

                var formedSongs = songs
                        .Select((x, index) => x.ToString(index + 1));

                return string.Join(Environment.NewLine, formedSongs);
            }

            private class TempSong
            {
                public string SongName { get; set; }
                public string PerformerFullName { get; set; }
                public string WriterName { get; set; }
                public string AlbumProducer { get; set; }
                public string Duration { get; set; }

                static private readonly string songPattern = "-Song #{0}" + Environment.NewLine +
                    "---SongName: {1}" + Environment.NewLine +
                    "---Writer: {2}" + Environment.NewLine +
                    "---Performer: {3}" + Environment.NewLine +
                    "---AlbumProducer: {4}" + Environment.NewLine +
                    "---Duration: {5}";

                public string ToString(int index)
                {
                    return string.Format(songPattern,
                        index,
                        SongName,
                        WriterName,
                        PerformerFullName,
                        AlbumProducer,
                        Duration);
                }
            }
        }
    }
}