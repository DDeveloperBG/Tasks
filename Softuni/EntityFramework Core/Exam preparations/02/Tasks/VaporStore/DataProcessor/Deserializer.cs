namespace VaporStore.DataProcessor
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Data;
    using VaporStore.DataProcessor.DTOs.Import;
    using VaporStore.Data.Models;

    using Newtonsoft.Json;
    using VaporStore.Data.Models.Enums;
    using System.IO;
    using System.Globalization;

    public static class Deserializer
    {
        public static readonly string ErrorMessage = "Invalid Data\r\n";
        public static readonly string SuccessfullyAddedGameMessage = "Added {0} ({1}) with {2} tags\r\n";
        public static readonly string SuccessfullyAddedUserMessage = "Imported {0} with {1} cards\r\n";
        public static readonly string SuccessfullyAddedPurchaseMessage = "Imported {0} for {1}\r\n";

        public static string ImportGames(VaporStoreDbContext context, string jsonString)
        {
            var gamesDTOs = JsonConvert.DeserializeObject<GameImportModel[]>(jsonString);

            StringBuilder result = new StringBuilder();
            var developers = new Dictionary<string, Developer>();
            var genres = new Dictionary<string, Genre>();
            var tags = new Dictionary<string, Tag>();

            foreach (var gameDTO in gamesDTOs)
            {
                if (!IsValid(gameDTO))
                {
                    result.Append(ErrorMessage);
                    continue;
                }

                developers.TryGetValue(gameDTO.Developer, out Developer dev);

                if (dev == null)
                {
                    dev = new Developer
                    {
                        Name = gameDTO.Developer
                    };

                    context.Developers.Add(dev);
                    developers.Add(gameDTO.Developer, dev);
                }

                genres.TryGetValue(gameDTO.Genre, out Genre genre);

                if (genre == null)
                {
                    genre = new Genre
                    {
                        Name = gameDTO.Genre
                    };

                    context.Genres.Add(genre);
                    genres.Add(gameDTO.Genre, genre);
                }

                Game game = new Game
                {
                    Name = gameDTO.Name,
                    Price = gameDTO.Price,
                    ReleaseDate = gameDTO.ReleaseDate,
                    Developer = dev,
                    Genre = genre
                };

                var gameTags = gameDTO.Tags.ToHashSet();

                if (gameTags.Count < gameDTO.Tags.Length)
                {
                    for (int i = 0; i < gameTags.Count - gameDTO.Tags.Length; i++)
                    {
                        result.Append(ErrorMessage);
                    }
                }

                context.Games.Add(game);
                result.Append(string.Format(SuccessfullyAddedGameMessage,
                    game.Name,
                    game.Genre.Name,
                    gameTags.Count));

                foreach (var tagName in gameTags)
                {
                    tags.TryGetValue(tagName, out Tag tag);

                    if (tag == null)
                    {
                        tag = new Tag
                        {
                            Name = tagName
                        };

                        context.Tags.Add(tag);
                        tags.Add(tagName, tag);
                    }

                    GameTag gameTag = new GameTag
                    {
                        Game = game,
                        Tag = tag
                    };

                    context.GameTags.Add(gameTag);
                }
            }

            context.SaveChanges();

            return result.ToString().Trim();
        }

        public static string ImportUsers(VaporStoreDbContext context, string jsonString)
        {
            var usersDTOs = JsonConvert.DeserializeObject<UserImportModel[]>(jsonString);

            StringBuilder result = new StringBuilder();

            foreach (var userDTOs in usersDTOs)
            {
                if (!IsValid(userDTOs))
                {
                    result.Append(ErrorMessage);
                    continue;
                }

                User user = new User
                {
                    Username = userDTOs.Username,
                    FullName = userDTOs.FullName,
                    Email = userDTOs.Email,
                    Age = userDTOs.Age,
                };

                context.Users.Add(user);

                var validCardsCount = 0;
                foreach (var cardDTO in userDTOs.Cards)
                {
                    if (!IsValid(cardDTO))
                    {
                        result.Append(ErrorMessage);
                        continue;
                    }

                    validCardsCount++;

                    Card card = new Card
                    {
                        Number = cardDTO.Number,
                        Cvc = cardDTO.CVC,
                        Type = Enum.Parse<CardType>(cardDTO.Type),
                        User = user
                    };

                    context.Cards.Add(card);
                }

                result.Append(string.Format(SuccessfullyAddedUserMessage,
                    user.Username,
                    validCardsCount));
            }

            context.SaveChanges();

            return result.ToString();
        }

        public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
        {
            var converter = new XmlSerializer(typeof(PurchaseImportModel[]), new XmlRootAttribute("Purchases"));

            var purchasesDTOs = converter.Deserialize(new StringReader(xmlString)) as PurchaseImportModel[];

            StringBuilder result = new StringBuilder();

            foreach (var purchaseDTO in purchasesDTOs)
            {
                if (!IsValid(purchaseDTO))
                {
                    result.Append(ErrorMessage);
                    continue;
                }

                var game = context.Games.Where(x => x.Name == purchaseDTO.GameName).FirstOrDefault();
                var card = context.Cards.Where(x => x.Number == purchaseDTO.CardNumber).FirstOrDefault();
                DateTime.TryParseExact(purchaseDTO.Date,
                    "dd/MM/yyyy HH:mm",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime puchaseDate);

                if (game == null || card == null || puchaseDate == null)
                {
                    result.Append(ErrorMessage);
                    continue;
                }

                Purchase purchase = new Purchase
                {
                    Type = Enum.Parse<PurchaseType>(purchaseDTO.Type),
                    ProductKey = purchaseDTO.ProductKey,
                    Date = puchaseDate,
                    Game = game,
                    Card = card,
                };

                context.Purchases.Add(purchase);
                result.Append(string.Format(SuccessfullyAddedPurchaseMessage,
                    purchaseDTO.GameName,
                    card.User.Username));
            }

            context.SaveChanges();

            return result.ToString();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}