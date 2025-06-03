namespace FIAP.Cloud.Games.SDK.Games.Requests
{
    public record GameFindRequest
    {
        public string? Name { get; set; }
        public decimal? Pricing { get; set; }
        public byte? Category { get; set; }
    }
}
