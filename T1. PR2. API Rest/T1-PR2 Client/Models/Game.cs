namespace T1_PR2_Client.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DeveloperTeam { get; set; }
        public string ImageUrl { get; set; }
        public List<string> RatedUsers { get; set; } = new List<string>();
    }
}
