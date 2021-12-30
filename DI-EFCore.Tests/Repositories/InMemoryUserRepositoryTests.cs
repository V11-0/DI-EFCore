using System;
using System.Data.Common;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DI_EFCore.Models;
using DI_EFCore.Tests.Repositories.Base;

namespace DI_EFCore.Tests.Repositories {

    [TestClass]
    public class InMemoryUserRepositoryTests : UserRepositoryTests, IDisposable {

        private readonly DbConnection _connection;

        public InMemoryUserRepositoryTests() : base(new DbContextOptionsBuilder<AppDbContext>()
                .UseLazyLoadingProxies()
                .UseSqlite(CreateInMemoryDatabase())
                .Options) {

            _connection = RelationalOptionsExtension.Extract(_contextOptions).Connection!;
        }

        private static DbConnection CreateInMemoryDatabase() {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            return connection;
        }

        public void Dispose() => _connection.Dispose();
    }
}