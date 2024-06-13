using Data.Data;

namespace Veriate.Service
{
    public class ExpiredUrlCleanupService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<ExpiredUrlCleanupService> _logger;

        public ExpiredUrlCleanupService(IServiceScopeFactory scopeFactory, ILogger<ExpiredUrlCleanupService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var urlRepository = scope.ServiceProvider.GetRequiredService<IUrlRepository>();
                    var expiredUrls = await urlRepository.GetExpiredUrlMappingsAsync();
                    await urlRepository.RemoveUrlMappingsAsync(expiredUrls);
                }
                _logger.LogInformation("Expired URLs cleaned up.");
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken); // Runs every hour
            }
        }
    }

}
