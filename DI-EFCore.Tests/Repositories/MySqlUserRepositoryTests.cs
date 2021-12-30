using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DI_EFCore.Models;
using DI_EFCore.Tests.Repositories.Base;

namespace DI_EFCore.Tests.Repositories {

    [TestClass]
    public class MySqlUserRepositoryTests : UserRepositoryTests {

        private static string connectionString = "server=172.17.0.4;user id=root;password=1234;database=EFCoreDemo-Tests";
        private static MySqlServerVersion serverVersion = new MySqlServerVersion(new Version(8, 0, 25));

        public MySqlUserRepositoryTests() : base(new DbContextOptionsBuilder<AppDbContext>()
                .UseLazyLoadingProxies()
                .UseMySql(connectionString, serverVersion)
                .Options) { }
    }
}