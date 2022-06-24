using dotnet_BlogApp.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace dotnet_BlogApp.Data.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly BlogDbContext _context;
        private readonly ILogger<DbInitializer> _logger;
        public DbInitializer(BlogDbContext context, ILogger<DbInitializer> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task Initialize()
        {
            try
            {
                if ((await _context.Database.GetPendingMigrationsAsync()).Count() > 0)
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during migration.");
            }

            if (await _context.Posts.AnyAsync())
            {
                return;
            }

            var postArraySeed = new Post[]
            {
                new Post()
                {
                    Id = Guid.NewGuid(),
                    Title = "First Blog Post",
                    Body = "This is the first blog post seeded into this application just to fill it up with content so that the User Interface can be shown. " +
                    "It is just a simple application, made from several days of hard work from an aspiring Angular / .Net developer. There is much to be improved on. " +
                    "The main purpose of this app, however, is just as a portfolio and not meant to be a super-scaled production application. " +
                    "I'm not a designer, so I know the design of this application is not mind blowing. " +
                    "My Thank you for reading this! My github link where this app's code is placed is https://github.com/KieranMerv/ ." +
                    "The rest of the seed posts will just be Lorem Ipsum because I don't want to spend too much time manually generating data to seed this.",
                    Created = DateTime.Parse("16 May 2016"),
                    Updated = DateTime.Parse("28 Dec 2017")
                },
                new Post()
                {
                    Id = Guid.NewGuid(),
                    Title = "Second Blog Post",
                    Body = "Lorem ipsum dolor sit amet. Aut esse numquam id nesciunt consectetur hic accusamus culpa. " +
                    "Est impedit earum non deserunt error aut quia corporis ut sunt ducimus vel voluptatem eaque cum adipisci dolorem eum nihil fugit. " +
                    "Et labore quia eos iure sequi hic magni fugiat. Est exercitationem aperiam sit tempora aliquam et quia eveniet qui animi eligendi At similique tenetur ut accusantium inventore. " +
                    "33 quia perferendis aut sint quia et fugiat necessitatibus et blanditiis nobis. Ut numquam ipsam quo odio libero hic deleniti corporis! " +
                    "Et praesentium vitae sit internos beatae quo maiores quos. Et maiores enim cum cupiditate repudiandae vel enim voluptatum non officiis laboriosam et soluta voluptates in explicabo iusto. " +
                    "Est quam dolorem hic deserunt sint qui totam sint. Et modi perspiciatis in fugit voluptatum  voluptas dolorum. " +
                    "Sit ducimus minus aut voluptatum eius ut quaerat culpa qui culpa quia eos consequatur cupiditate et soluta consectetur. " +
                    "Vel voluptatem doloribus quo eveniet quidem et consectetur quae est voluptatum corporis rem nulla reiciendis vel distinctio voluptas",
                    Created = DateTime.Parse("5 Jun 2018"),
                    Updated = DateTime.Parse("6 Jun 2018")
                },
                new Post()
                {
                    Id = Guid.NewGuid(),
                    Title = "Third Blog Post",
                    Body = "Lorem ipsum dolor sit amet. Eos fuga temporibus eos dicta distinctio et vero eaque ducimus deleniti et minima voluptate ea fugiat Quis. " +
                    "Non reprehenderit totam cumque molestiae ex odio accusantium qui repellendus Quis et corrupti dolore et minima doloremque et galisum quam. " +
                    "Est amet dolores aut nisi dolorem id ullam velit ut facilis sequi ut saepe ratione ut explicabo architecto. " +
                    "Et officiis ipsa aut natus eius eum magni quia et molestias voluptatem quo cupiditate nesciunt et necessitatibus esse qui quas dolor. " +
                    "Qui beatae minus cum fuga galisum qui architecto repellat in dolorem repellendus qui labore reiciendis. " +
                    "Eum corporis beatae ea voluptates illo id autem consectetur ea exercitationem saepe impedit esse qui illo ipsum." +
                    "Et minus rerum hic repudiandae nihil non porro dicta et dolor molestiae et dolor dolore et fugit molestiae. " +
                    "Et eius commodi sit voluptatem internos ut quaerat exercitationem ut iure omnis mollitia dignissimos. " +
                    "Et reiciendis veritatis qui officia dolore hic vero minima non omnis magnam At ratione earum et quisquam totam a eaque deleniti!",
                    Created = DateTime.Parse("11 Jul 2018"),
                    Updated = DateTime.Parse("15 Jul 2018")
                },
                new Post()
                {
                    Id = Guid.NewGuid(),
                    Title = "Fourth Blog Post",
                    Body = "Lorem ipsum dolor sit amet. Ab harum voluptatem qui omnis quas ex velit voluptas vel animi saepe non cupiditate placeat est mollitia recusandae. " +
                    "Hic atque accusamus aut quisquam quibusdam et similique rerum eos sunt molestias sed ullam dolor ut blanditiis molestiae non culpa nihil. " +
                    "Non iure eveniet aut vero eveniet et natus atque vel odio ratione a temporibus necessitatibus aut architecto enim et iure doloribus!" +
                    "Aut autem accusamus ut harum aspernatur ab necessitatibus quia in aliquid voluptates est dolorem fugiat quo exercitationem nulla. " +
                    "Ut harum dolore est voluptates laudantium sed repellendus eaque sit reprehenderit debitis a quia maxime. Non rerum quia est saepe nihil et autem velit. " +
                    "Et sint fuga sed facilis unde eos earum blanditiis a voluptatem quod ut iusto delectus." +
                    "Qui consequuntur quia et provident ullam et sint veritatis aut eveniet voluptate ab dolores rerum qui vitae provident et veritatis dolores. " +
                    "Et neque veniam ut animi consequatur aut internos vero aut perferendis ullam. Ut repudiandae quam non soluta sequi ea numquam voluptas rem deserunt natus ducimus nobis. " +
                    "Non esse provident id galisum porro et provident doloremque.",
                    Created = DateTime.Parse("21 Jul 2018"),
                    Updated = DateTime.Parse("21 Jul 2018")
                },
                new Post()
                {
                    Id = Guid.NewGuid(),
                    Title = "Fifth and Final Seeded Blog Post",
                    Body = "Lorem ipsum dolor sit amet. Non commodi alias ea beatae atque cum iste quia aut quaerat facere et vitae aliquam et atque quia quo dolorem enim. " +
                    "Hic minima voluptas est saepe odit nam porro asperiores et voluptatem voluptatem At galisum vitae. " +
                    "Et tenetur dolorem aut molestiae doloremque et expedita quaerat ut vero nemo in nihil sequi qui consequatur labore in optio totam. " +
                    "Aut pariatur tempore ea odit reprehenderit ut omnis iste. Ut Quis veniam ad nobis itaque et voluptatum quisquam qui minima fuga! " +
                    "Qui rerum quidem et molestiae corrupti ut rerum deleniti ad odit aspernatur? Sed harum enim ex sint neque id omnis error sed possimus dolor. " +
                    "Rem blanditiis eaque qui distinctio voluptate qui pariatur magni est perferendis laboriosam cum voluptates dicta qui dolores beatae! " +
                    "Ut quas libero voluptates accusamus eum dolores tempore et mollitia pariatur! In consectetur rerum qui iste voluptatum non distinctio nisi non doloribus unde At debitis dolorem! " +
                    "Quo nesciunt quia sit accusamus provident est suscipit voluptatum a pariatur atque et sapiente mollitia et quia voluptatem?",
                    Created = DateTime.Parse("24 Jun 2022"),
                    Updated = DateTime.Parse("24 Jun 2022")
                }
            };

            await _context.AddRangeAsync(postArraySeed);

            await _context.SaveChangesAsync();
        }
    }
}