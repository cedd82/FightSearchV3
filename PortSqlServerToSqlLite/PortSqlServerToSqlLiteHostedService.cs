using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace PortSqlServerToSqlLite
{
    public class PortSqlServerToSqlLiteHostedService: IHostedService, IDisposable
    {
        private readonly ISqLiteMigration _sqLiteMigration;
        
        public PortSqlServerToSqlLiteHostedService(ISqLiteMigration sqLiteMigration)
        {
            _sqLiteMigration = sqLiteMigration;
        }
 
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Timed Background Service is starting.");
 
            //_timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            DoWork();
            return Task.CompletedTask;
        }
 
        //private void DoWork(object state)
        private void DoWork()
        {
            _sqLiteMigration.DoMigration();
        }
 
        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Timed Background Service is stopping.2");
            
            return Task.CompletedTask;
        }
 
        public void Dispose()
        {
            
        }

    }
}