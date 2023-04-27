using App.Entities;
using App.Repositories;
using App;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace TestApp.Tests
{
    [TestFixture]
    public class CategoryRepositoryTests
    {
        private DbContextOptions<MyFinanceContext> _options;
        private MyFinanceContext _context;
        private CategoryRepository _categoryRepository;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<MyFinanceContext>()
                .UseSqlite("Data Source=mytestdatabase.db")
                .Options;
            _context = new MyFinanceContext(_options);
            _categoryRepository = new CategoryRepository(_context);
        }
        [Test]
        public async Task GetAllCategories_ReturnsAllCategories()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MyFinanceContext>()
                .UseSqlite("Data Source=mytestdatabase.db")
            .Options;

            using (var context = new MyFinanceContext(options))
            {
                var categoryRepository = new CategoryRepository(context);
                await categoryRepository.AddAsync(new Category { Name = "Category 1" });
                await categoryRepository.AddAsync(new Category { Name = "Category 2" });
                await categoryRepository.SaveChangesAsync();
            }

            using (var context = new MyFinanceContext(options))
            {
                var categoryRepository = new CategoryRepository(context);

                // Act
                var categories = await categoryRepository.GetAllAsync();

                // Assert
                NUnit.Framework.Assert.AreEqual(2, categories.Count());
                NUnit.Framework.Assert.IsTrue(categories.Any(c => c.Name == "Category 1"));
                NUnit.Framework.Assert.IsTrue(categories.Any(c => c.Name == "Category 2"));
            }
        }


        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }

}
