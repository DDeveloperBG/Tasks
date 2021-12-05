namespace Theatre.DataProcessor
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;
    using Theatre.Data;
    using Theatre.Data.Models;
    using Theatre.Data.Models.Enums;
    using Theatre.DataProcessor.ImportDto;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        static string GetSuccessfulImportPlayMessage(string playTitle, string genreType, float rating)
            => $"Successfully imported {playTitle} with genre {genreType} and a rating of {rating}!";

        static string GetSuccessfulImportActorMessage(string fullName, bool isMainCharacter)
            => $"Successfully imported actor {fullName} as a {(isMainCharacter ? "main" : "lesser")} character!";

        static string GetSuccessfulImportTheatreMessage(string theatreName, int totalNumber)
            => $"Successfully imported theatre {theatreName} with #{totalNumber} tickets!";

        public static string ImportPlays(TheatreContext context, string xmlString)
        {
            var serializer = new XmlSerializer(
                    typeof(PlayImportModel[]),
                    new XmlRootAttribute("Plays")
                );

            var playsModels = serializer
                .Deserialize(new StringReader(xmlString)) as PlayImportModel[];
            var result = new StringBuilder();

            foreach (var playModel in playsModels)
            {
                if (!IsValid(playModel))
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                TimeSpan duration = TimeSpan.ParseExact(playModel.Duration, "c", CultureInfo.InvariantCulture);

                if (duration < new TimeSpan(1, 0, 0))
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                var play = new Play
                {
                    Title = playModel.Title,
                    Duration = duration,
                    Rating = playModel.Rating,
                    Genre = Enum.Parse<Genre>(playModel.Genre),
                    Description = playModel.Description,
                    Screenwriter = playModel.Screenwriter,
                };

                context.Plays.Add(play);

                result.AppendLine(
                    GetSuccessfulImportPlayMessage(
                            play.Title,
                            play.Genre.ToString(),
                            play.Rating
                        ));
            }

            context.SaveChanges();

            return result.ToString();
        }

        public static string ImportCasts(TheatreContext context, string xmlString)
        {
            var serializer = new XmlSerializer(
                    typeof(CastImportModel[]),
                    new XmlRootAttribute("Casts")
                );

            var castsModels = serializer
                .Deserialize(new StringReader(xmlString)) as CastImportModel[];
            var result = new StringBuilder();

            foreach (var castModel in castsModels)
            {
                if (!IsValid(castModel))
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                var cast = new Cast
                {
                    FullName = castModel.FullName,
                    IsMainCharacter = castModel.IsMainCharacter,
                    PhoneNumber = castModel.PhoneNumber,
                    PlayId = castModel.PlayId
                };

                context.Casts.Add(cast);

                result.AppendLine(
                    GetSuccessfulImportActorMessage(
                            cast.FullName,
                            cast.IsMainCharacter
                        ));
            }

            context.SaveChanges();

            return result.ToString();
        }

        public static string ImportTtheatersTickets(TheatreContext context, string jsonString)
        {
            var theatresModels = JsonConvert.DeserializeObject<TheatreImportModel[]>(jsonString);
            StringBuilder result = new StringBuilder();

            foreach (var theatreModel in theatresModels)
            {
                if (!IsValid(theatreModel))
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                var theatre = new Theatre
                {
                    Name = theatreModel.Name,
                    NumberOfHalls = theatreModel.NumberOfHalls,
                    Director = theatreModel.Director,
                };
                context.Theatres.Add(theatre);

                int couter = 0;
                foreach (var ticketModel in theatreModel.Tickets)
                {
                    if (!IsValid(ticketModel))
                    {
                        result.AppendLine(ErrorMessage);
                        continue;
                    }

                    couter++;
                    context.Tickets.Add(new Ticket
                    {
                        Price = ticketModel.Price,
                        RowNumber = ticketModel.RowNumber,
                        PlayId = ticketModel.PlayId,
                        Theatre = theatre
                    });
                }

                result.AppendLine(
                    GetSuccessfulImportTheatreMessage(
                            theatre.Name,
                            couter
                        ));
            }

            context.SaveChanges();

            return result.ToString();
        }

        private static bool IsValid(object obj)
        {
            var validator = new ValidationContext(obj);
            var validationRes = new List<ValidationResult>();

            var result = Validator.TryValidateObject(obj, validator, validationRes, true);
            return result;
        }
    }
}
