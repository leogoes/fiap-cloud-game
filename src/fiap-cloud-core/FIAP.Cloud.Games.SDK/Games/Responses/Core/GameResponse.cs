namespace FIAP.Cloud.Games.SDK.Games.Responses.Core
{
    public class GameResponse
    {
        public Guid GameId { get; set; }
        public string Name { get; set; }
        public decimal Pricing { get; set; }
        public string Category { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
