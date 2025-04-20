using T1_PR2_API.Models;
using Microsoft.EntityFrameworkCore;

namespace T1_PR2_API.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().HasData(
                new Game
                {
                    Id = -1,
                    Title = "Detroit: Become Human",
                    Description = "Aventura gráfica interactiva en la que controlas a tres androides (Kara, Connor y Markus) en un futuro distópico de Detroit, donde las decisiones morales y las consecuencias cambian radicalmente el destino de la humanidad y los androides.",
                    DeveloperTeam = "Quantic Dream",
                    ImageUrl = "https://image.api.playstation.com/vulcan/ap/rnd/202010/3016/0qg0N4k9T1n0l8lZqR9v9r2j.png"
                },
                new Game
                {
                    Id = -2,
                    Title = "Hollow Knight",
                    Description = "Explora las profundidades de Hallownest en este metroidvania de acción y plataformas, enfrentando criaturas letales y desvelando misterios antiguos en un mundo dibujado a mano.",
                    DeveloperTeam = "Team Cherry",
                    ImageUrl = "https://cdn.cloudflare.steamstatic.com/steam/apps/367520/header.jpg"
                },
                new Game
                {
                    Id = -3,
                    Title = "Celeste",
                    Description = "Un desafiante juego de plataformas donde ayudas a Madeline a escalar la montaña Celeste, enfrentando obstáculos físicos y emocionales en una historia de superación personal.",
                    DeveloperTeam = "Matt Makes Games",
                    ImageUrl = "https://cdn.cloudflare.steamstatic.com/steam/apps/504230/header.jpg"
                },
                new Game
                {
                    Id = 4,
                    Title = "The Legend of Zelda: Breath of the Wild",
                    Description = "Aventura de mundo abierto en la que exploras el vasto reino de Hyrule, resolviendo puzles, luchando contra enemigos y descubriendo secretos en la piel de Link.",
                    DeveloperTeam = "Nintendo EPD",
                    ImageUrl = "https://assets.nintendo.com/image/upload/f_auto/q_auto/dpr_2.0/c_scale,w_400/ncom/en_US/games/switch/t/the-legend-of-zelda-breath-of-the-wild-switch/hero"
                },
                new Game
                {
                    Id = -5,
                    Title = "Red Dead Redemption 2",
                    Description = "Sumérgete en el Salvaje Oeste con Arthur Morgan y la banda de Van der Linde en una épica historia de supervivencia, lealtad y decadencia en un mundo abierto detallado.",
                    DeveloperTeam = "Rockstar Games",
                    ImageUrl = "https://cdn.cloudflare.steamstatic.com/steam/apps/1174180/header.jpg"
                },
                new Game
                {
                    Id = -6,
                    Title = "Stardew Valley",
                    Description = "Simulador de vida y granja en el que cultivas, crías animales, exploras minas y te integras en la comunidad de Pueblo Pelícano, con infinitas posibilidades de personalización.",
                    DeveloperTeam = "ConcernedApe",
                    ImageUrl = "https://cdn.cloudflare.steamstatic.com/steam/apps/413150/header.jpg"
                }
            );
        }

    }
}
