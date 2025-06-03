namespace FIAP.Cloud.Games.Identity.API.Configurations.Auth.Policies
{
    public static class PolicyExtensions
    {
        public static void AddCustomPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddCustomPolicies();
            });
        }
    }
}
