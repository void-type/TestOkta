using Okta.AspNetCore;

namespace OktaTest.Auth
{
    public static class OktaStartupExtensions
    {
        /// <summary>
        /// Used for configuring and setting up Web API auth
        /// </summary>
        public static IServiceCollection AddOktaApiAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = OktaDefaults.ApiAuthenticationScheme;
                    options.DefaultChallengeScheme = OktaDefaults.ApiAuthenticationScheme;
                    options.DefaultSignInScheme = OktaDefaults.ApiAuthenticationScheme;
                })
                .AddOktaWebApi(new OktaWebApiOptions()
                {
                    OktaDomain = configuration["Okta:OktaDomain"],
                    AuthorizationServerId = configuration["Okta:AuthorizationServerId"],
                    Audience = configuration["Okta:Audience"],
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("PrivilegedOnly", policy => policy.RequireClaim("groups", "privileged-user"));
            });

            return services;
        }
    }
}
