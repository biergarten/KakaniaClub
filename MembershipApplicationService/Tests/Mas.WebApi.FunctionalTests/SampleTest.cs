
using Docker.DotNet.Models;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Networks;
using Mas.Domain.Aggregate;
using Mas.Domain.Enums;
using Mas.Domain.ValueObjects;
using Mas.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics.Contracts;
using Testcontainers.MsSql;

namespace Mas.WebApi.FunctionalTests
{
    public sealed class SampleTest : IAsyncLifetime
    {
        Application _application = new Application(DateTime.Today, new Person("Jan", "Rodes", "jan.rodes@gmail.com", "89899"), MembershipType.OnlyGym,"whatever_emaillocation");

        ApplicationContext _context;
        const int _port = 62077;
        const string _password = "yourStrong(!)Password";

        private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder()
            .WithPortBinding(_port, 1433)
            .WithPassword(_password)
            .Build();

        public SampleTest()
        {
            
        }

        private void UpWithTheDatabase()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseSqlServer(
                 $"Server=127.0.0.1," +
                 $"{_port};Database=MasDB;" +
                 $"User Id=sa;Password={_password};" +
                 $"TrustServerCertificate=True");

            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information).EnableSensitiveDataLogging();
            var _options = optionsBuilder.Options;
            _context = new ApplicationContext(_options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }
        public Task DisposeAsync()
        {
            return _msSqlContainer.DisposeAsync().AsTask();
        }

        public Task InitializeAsync()
        {
            return _msSqlContainer.StartAsync();
        }

        [Fact]
        public void NewContractStoresCorrectId()
        {
            UpWithTheDatabase();
            var assignedId = _application.Id;
            _context.Applications.Add(_application);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            var applicationFromDB = _context.Applications.FirstOrDefault();
            Assert.Equal(assignedId, applicationFromDB.Id);
        }
    }
}