using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P03_FootballBetting.Data.Models 
{
    public class Team
    {
        public Team()
        {
            HomeGames = new HashSet<Game>();
            AwayGames = new HashSet<Game>();

            Players = new HashSet<Player>();
        }

        [Key]
        public int TeamId{ get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string LogoUrl { get; set; }

        [Required]
        public string Initials { get; set; }

        [Required]
        public decimal Budget { get; set; }

        public int PrimaryKitColorId { get; set; }
        public virtual Color PrimaryKitColor { get; set; }
       
        public int SecondaryKitColorId { get; set; }
        public virtual Color SecondaryKitColor { get; set; }
       
        public int TownId { get; set; }
        public virtual Town Town { get; set; }

        public virtual ICollection<Game> HomeGames { get; set; }
        public virtual ICollection<Game> AwayGames { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}
