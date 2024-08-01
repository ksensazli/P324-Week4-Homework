using Bookstore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Context;

public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                if (context.Books.Any() || context.Authors.Any() || context.Genres.Any())
                {
                    return;
                }

                var fantasyGenre = new Genre { Name = "Fantasy", IsActive = true };
                var sciFiGenre = new Genre { Name = "Science Fiction", IsActive = true };

                context.Genres.AddRange(fantasyGenre, sciFiGenre);

                var tolkien = new Author { Name = "J.R.R. Tolkien" };
                var asimov = new Author { Name = "Isaac Asimov" };

                context.Authors.AddRange(tolkien, asimov);

                var lotr = new Book
                {
                    Title = "Lord of The Rings",
                    Genre = fantasyGenre,
                    PageCount = 200,
                    PublishDate = DateTime.SpecifyKind(new DateTime(2001, 06, 12), DateTimeKind.Utc),
                    Author = tolkien
                };

                var silmarillion = new Book
                {
                    Title = "Silmarillion",
                    Genre = fantasyGenre,
                    PageCount = 999,
                    PublishDate = DateTime.SpecifyKind(new DateTime(2001, 06, 12), DateTimeKind.Utc),
                    Author = tolkien
                };

                var foundation = new Book
                {
                    Title = "Foundation",
                    Genre = sciFiGenre,
                    PageCount = 255,
                    PublishDate = DateTime.SpecifyKind(new DateTime(1951, 06, 01), DateTimeKind.Utc),
                    Author = asimov
                };

                context.Books.AddRange(lotr, silmarillion, foundation);

                context.SaveChanges();
            }
        }
    }