using AnimeDessert.Interfaces;
using AnimeDessert.Models;

namespace AnimeDessert.Services
{
    public class ImageService : IImageService
    {
        public static ImageDto ToImageDto(Image image)
        {
            if (image.Anime != null)
            {
                image.Anime.Images = null;
            }

            if (image.CharacterVersion != null)
            {
                image.CharacterVersion.Images = null;
            }

            if (image.Dessert != null)
            {
                image.Dessert.Images = null;
            }

            return new ImageDto
            {
                ImageId = image.ImageId,
                ImagePath = $"/assets/images/{image.ImageFilename}",
                AnimeDto = image.Anime == null ? null : AnimeService.ToAnimeDto(image.Anime),
                CharacterVersionDto = image.CharacterVersion == null ? null : CharacterVersionService.ToCharacterVersionDto(image.CharacterVersion),
                DessertDto = null
            };
        }

        public static readonly List<string> ValidContentTypes = ["image/jpeg", "image/png", "image/gif", "image/webp"];
    }
}
