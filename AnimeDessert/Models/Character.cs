using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AnimeDessert.Models
{
    public class Character
    {
        [Key]
        public int CharacterId { get; set; }

        public required string CharacterName { get; set; }

        // A Character can have many CharacterVersions
        public ICollection<CharacterVersion>? CharacterVersions { get; set; }

        // A Character can have many Desserts
        public ICollection<Dessert>? Desserts { get; set; }
    }

    public class CharacterDto
    {
        public int CharacterId { get; set; }

        [DisplayName("Character Name")]
        public required string CharacterName { get; set; }

        public List<CharacterVersionDto>? CharacterVersionDtos { get; set; }

        // public List<DessertDto>? DessertDtos { get; set; }
    }

    public class UpdateCharacterRequest
    {
        public required string CharacterName { get; set; }
    }

    public class AddCharacterRequest
    {
        public required string CharacterName { get; set; }

        public required List<IFormFile> ImageFiles { get; set; } = [];
    }

    public class AddVersionToCharacterRequest
    {
        public required string VersionName { get; set; }

        public required List<IFormFile> ImageFiles { get; set; } = [];
    }

    public class RemoveVersionsFromCharacterRequest
    {
        public required List<int> CharacterVersionIds { get; set; } = [];
    }
}
