using System.ComponentModel.DataAnnotations;
using VideoGamesRental.Infrastructure.Core;

// ReSharper disable UnusedMember.Global - Implicit use
// ReSharper disable PropertyCanBeMadeInitOnly.Global - Implicit use

namespace VideoGamesRental.Infrastructure.VideoGames;

public class VideoGameDataModel : IGuidEntity
{
    [Key]
    public Guid Id { get; set; }
    
    [StringLength(128)]
    public string Name { get; set; } = string.Empty;
    
    public DateOnly ReleaseDate { get; set; }
}