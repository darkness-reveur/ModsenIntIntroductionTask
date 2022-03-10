using System.ComponentModel.DataAnnotations;

namespace MeetupPlatform.Common.Models.MeetUps
{
    public class Place
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }    

        public string? LinkToGoogleMap { get; set; }

        public int? CountOfPlaces { get; set; }

        public string? Description { get; set; }
    }
}
