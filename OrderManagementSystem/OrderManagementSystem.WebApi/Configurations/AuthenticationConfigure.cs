using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace OrderManagementSystem.WebApi.Configurations
{
    public static class AuthenticationConfigure
    {
        public static void AddAuthenticationService(this IServiceCollection services, string secretKey)
        {
            var hollyOriginsPolicy = "_nebilHollyOrigins";
            var key = Encoding.ASCII.GetBytes(secretKey);
            services.AddMvc().AddJsonOptions(s =>
            {
                s.JsonSerializerOptions.PropertyNamingPolicy = null;
                s.JsonSerializerOptions.DictionaryKeyPolicy = null;
            });
            services.AddAuthentication(s =>
            {
                s.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                s.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });
            services.AddCors(option =>
            {
                option.AddPolicy(hollyOriginsPolicy,
                    builder => { builder.WithOrigins("http://localhost:4200/"); });
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}
