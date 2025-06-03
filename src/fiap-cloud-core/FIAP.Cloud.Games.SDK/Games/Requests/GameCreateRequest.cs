namespace FIAP.Cloud.Games.SDK.Games.Requests
{
    public record GameCreateRequest
    {
        public required string Name { get; init; }
        public required decimal Pricing { get; init; }
        public required byte Category { get; init; }
    }
}
