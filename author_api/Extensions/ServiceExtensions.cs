using author_data_access;
using author_data_access.Repositories;
using Microsoft.AspNetCore.HttpLogging;

namespace author_api.Extensions
{
    public static class ServiceExtensions
    {

        public static void ConfigureDb(this IServiceCollection services) => services.AddDbContext<AuthorContext>();

        public static void ConfigureLogging(this IServiceCollection services)
        {
            services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All;
                logging.RequestHeaders.Add("sec-ch-ua");
                logging.ResponseHeaders.Add("MyResponseHeader");
                logging.MediaTypeOptions.AddText("application/javascript");
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;
            });
        }


    }
}
