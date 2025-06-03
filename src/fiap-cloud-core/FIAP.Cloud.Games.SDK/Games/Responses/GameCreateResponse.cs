namespace FIAP.Cloud.Games.SDK.Games.Responses
{
    public record GameCreateResponse
    {
        public string Name { get; set; }
        public decimal Pricing { get; set; }
        public byte Category { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
