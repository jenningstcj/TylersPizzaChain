using TylersPizzaChain.Database;

namespace TylersPizzaChain.Pipelines
{
    public class ImpureDependencies
    {
		public readonly TylersPizzaDbContext _dbContext;
		public readonly IConfiguration _configuration;
		public readonly IHttpClientFactory _httpClientFactory;
		public ILogger _logger = null!;

        public ImpureDependencies(TylersPizzaDbContext dbContext, IConfiguration configuration, IHttpClientFactory httpClientFactory)
		{
			_dbContext = dbContext;
			_configuration = configuration;
			_httpClientFactory = httpClientFactory;
		}

		public void SetLogger<T>(ILogger<T> logger)
		{
			_logger = logger;
		}
	}
}

