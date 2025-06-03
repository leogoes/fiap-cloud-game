namespace FIAP.Cloud.Games.SDK.Games.Requests
{
    public record GameChangeRequest
    {
        public required Guid GameId { get; init; }
        public string? Name { get; init; }
        public byte? Category { get; init; }
        public decimal? Pricing { get; init; }
    }
}
