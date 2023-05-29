using Ftp.Domain.Models;
using Ftp.Domain.Services;
using Ftp.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ftp.Api.Config
{
    public static class ConfigServices
    {
        public static void ConfigureSetting(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SftpServiceConfig>(configuration.GetSection("SftpServiceConfig"));

        }

        public static void ConfigureIoc(IServiceCollection services)
        {
            services.AddScoped<ISftpService, SftpService>();
        }
    }
}
