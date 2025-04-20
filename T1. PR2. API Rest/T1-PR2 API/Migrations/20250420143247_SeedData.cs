using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace T1_PR2_API.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "DeveloperTeam", "ImageUrl", "Title" },
                values: new object[,]
                {
                    { -6, "Simulador de vida y granja en el que cultivas, crías animales, exploras minas y te integras en la comunidad de Pueblo Pelícano, con infinitas posibilidades de personalización.", "ConcernedApe", "https://cdn.cloudflare.steamstatic.com/steam/apps/413150/header.jpg", "Stardew Valley" },
                    { -5, "Sumérgete en el Salvaje Oeste con Arthur Morgan y la banda de Van der Linde en una épica historia de supervivencia, lealtad y decadencia en un mundo abierto detallado.", "Rockstar Games", "https://cdn.cloudflare.steamstatic.com/steam/apps/1174180/header.jpg", "Red Dead Redemption 2" },
                    { -3, "Un desafiante juego de plataformas donde ayudas a Madeline a escalar la montaña Celeste, enfrentando obstáculos físicos y emocionales en una historia de superación personal.", "Matt Makes Games", "https://cdn.cloudflare.steamstatic.com/steam/apps/504230/header.jpg", "Celeste" },
                    { -2, "Explora las profundidades de Hallownest en este metroidvania de acción y plataformas, enfrentando criaturas letales y desvelando misterios antiguos en un mundo dibujado a mano.", "Team Cherry", "https://cdn.cloudflare.steamstatic.com/steam/apps/367520/header.jpg", "Hollow Knight" },
                    { -1, "Aventura gráfica interactiva en la que controlas a tres androides (Kara, Connor y Markus) en un futuro distópico de Detroit, donde las decisiones morales y las consecuencias cambian radicalmente el destino de la humanidad y los androides.", "Quantic Dream", "https://image.api.playstation.com/vulcan/ap/rnd/202010/3016/0qg0N4k9T1n0l8lZqR9v9r2j.png", "Detroit: Become Human" },
                    { 4, "Aventura de mundo abierto en la que exploras el vasto reino de Hyrule, resolviendo puzles, luchando contra enemigos y descubriendo secretos en la piel de Link.", "Nintendo EPD", "https://assets.nintendo.com/image/upload/f_auto/q_auto/dpr_2.0/c_scale,w_400/ncom/en_US/games/switch/t/the-legend-of-zelda-breath-of-the-wild-switch/hero", "The Legend of Zelda: Breath of the Wild" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: -6);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: -5);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
