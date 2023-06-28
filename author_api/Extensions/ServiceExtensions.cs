using author_data_access.Repositories;

namespace author_api.Extensions
{
    public static class ServiceExtensions
    {

        public static void ConfigureInMemoryDb(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorRepository, AuthorRepository>();
        }
    }
}
