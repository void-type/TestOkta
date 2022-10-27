using Okta.AspNetCore;

namespace OktaTest.Auth
{
    public static class OktaStartupExtensions
    {
        public static IServiceCollection AddOktaAuthentication(this IServiceCollection services, IConfiguration configuration)
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
