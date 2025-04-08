using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace T1_PR2_API.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public List<Game> RatedGames { get; set; } = new List<Game>();
    }
}
