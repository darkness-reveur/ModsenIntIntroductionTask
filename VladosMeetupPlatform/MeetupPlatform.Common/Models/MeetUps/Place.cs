using System.ComponentModel.DataAnnotations;

namespace MeetupPlatform.Common.Models.MeetUps
{
    public class Place
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public string LinkToGoogleMap { get; set; }

        [Required]
        public int CountOfPlaces { get; set; }

        public string? Description { get; set; }

        public virtual List<Meetup> Meetups { get; set; }
    }
}
