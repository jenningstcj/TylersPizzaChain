using TylersPizzaChain.Database;

namespace TylersPizzaChain.Pipelines
{
    public class ImpureDependencies
    {
		public readonly TylersPizzaDbContext _dbContext;
		public readonly IConfiguration _configuration;
		public readonly IHttpClientFactory _httpClientFactory;

        public ImpureDependencies(TylersPizzaDbContext dbContext, IConfiguration configuration, IHttpClientFactory httpClientFactory)
		{
			_dbContext = dbContext;
			_configuration = configuration;
			_httpClientFactory = httpClientFactory;
		}
	}
}

