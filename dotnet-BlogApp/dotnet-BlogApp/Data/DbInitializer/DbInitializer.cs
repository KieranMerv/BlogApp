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
                    Updated = DateTime.Parse("28 Dec 2017"),
                    IsPrivate = false
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
                    Updated = DateTime.Parse("6 Jun 2018"),
                    IsPrivate = false
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
                    Updated = DateTime.Parse("15 Jul 2018"),
                    IsPrivate = false
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
                    Updated = DateTime.Parse("21 Jul 2018"),
                    IsPrivate = false
                },
                new Post()
                {
                    Id = Guid.NewGuid(),
                    Title = "Fifth Post",
                    Body = "Lorem ipsum dolor sit amet. Non commodi alias ea beatae atque cum iste quia aut quaerat facere et vitae aliquam et atque quia quo dolorem enim. " +
                    "Hic minima voluptas est saepe odit nam porro asperiores et voluptatem voluptatem At galisum vitae. " +
                    "Et tenetur dolorem aut molestiae doloremque et expedita quaerat ut vero nemo in nihil sequi qui consequatur labore in optio totam. " +
                    "Aut pariatur tempore ea odit reprehenderit ut omnis iste. Ut Quis veniam ad nobis itaque et voluptatum quisquam qui minima fuga! " +
                    "Qui rerum quidem et molestiae corrupti ut rerum deleniti ad odit aspernatur? Sed harum enim ex sint neque id omnis error sed possimus dolor. " +
                    "Rem blanditiis eaque qui distinctio voluptate qui pariatur magni est perferendis laboriosam cum voluptates dicta qui dolores beatae! " +
                    "Ut quas libero voluptates accusamus eum dolores tempore et mollitia pariatur! In consectetur rerum qui iste voluptatum non distinctio nisi non doloribus unde At debitis dolorem! " +
                    "Quo nesciunt quia sit accusamus provident est suscipit voluptatum a pariatur atque et sapiente mollitia et quia voluptatem?",
                    Created = DateTime.Parse("24 Jun 2022"),
                    Updated = DateTime.Parse("24 Jun 2022"),
                    IsPrivate = false
                },
                new Post()
                {
                    Id = Guid.NewGuid(),
                    Title = "Sixth (private) Post",
                    Body = "Lorem ipsum dolor sit amet. Sit distinctio iure aut ratione quam et eligendi quia non quia fuga ut quibusdam voluptas vel earum ducimus est libero esse. " +
                    "Qui ratione nobis 33 doloremque nihil nam illum veniam et expedita temporibus et alias porro. " +
                    "In iure molestiae eum cumque labore eos beatae dolores aut iure odit est quisquam modi qui nemo aliquam." +
                    "Qui commodi autem aut nulla repellendus hic doloremque voluptas est debitis repellendus. " +
                    "Rem placeat ipsam cum soluta odit quo harum illum hic ipsam illo est voluptatem odit qui blanditiis magnam. " +
                    "Et rerum sunt eos voluptatem atque id possimus voluptatibus et aperiam molestiae. Non quia voluptatem et magnam dolor ut exercitationem incidunt!" +
                    "Aut totam voluptatem est voluptas voluptates id dolorum culpa. Sed dolores galisum et dolores laborum cum reprehenderit deleniti hic quia numquam qui quam dolorem ut voluptatum doloremque cum molestiae ipsum. " +
                    "33 sunt debitis sit dolores architecto sed alias eligendi sit aliquam molestiae et dolorem tenetur est corrupti sint in quidem incidunt.",
                    Created = DateTime.Parse("24 Jun 2022"),
                    Updated = DateTime.Parse("24 Jun 2022"),
                    IsPrivate = true
                },
                new Post()
                {
                    Id = Guid.NewGuid(),
                    Title = "Seventh (private) Post",
                    Body = "Lorem ipsum dolor sit amet. Cum quos dolor ut nisi libero nam excepturi fugit perferendis possimus non quaerat consectetur! " +
                    "Sed aspernatur omnis ea sunt ipsum ab enim accusantium sit autem obcaecati ut magni quas ut veniam voluptatem." +
                    "Ab ducimus repellendus At illum exercitationem quo eius dolorum et perspiciatis ducimus? " +
                    "Ut temporibus quas aut maiores repellendus ea suscipit modi aut cumque praesentium. " +
                    "Non ducimus ullam et mollitia ratione aut quia facere et eaque molestiae eum esse sunt et consequuntur quia et pariatur dolorem. " +
                    "Sed autem dolor aut tempore eligendi cum illo debitis. Aut fugiat unde sed molestias nesciunt sed labore facilis ut dicta aliquid 33 maxime laudantium. " +
                    "A aperiam consectetur cum nihil perferendis ducimus aliquam ut libero culpa.",
                    Created = DateTime.Parse("24 Jun 2022"),
                    Updated = DateTime.Parse("24 Jun 2022"),
                    IsPrivate = true
                },
                new Post()
                {
                    Id = Guid.NewGuid(),
                    Title = "Eighth (private) Post",
                    Body = "Lorem ipsum dolor sit amet. Ea odit fuga sed voluptas error aut ullam dicta ut maxime minus et officiis labore hic sapiente voluptatem non sunt galisum. " +
                    "Aut magni repellat et iste nemo sed illo consectetur. In consequatur repellat ut veniam deserunt quo sunt illo ex inventore blanditiis et recusandae provident. " +
                    "Ea voluptas ullam vel expedita reiciendis et blanditiis necessitatibus qui illo voluptas eos possimus culpa est inventore quaerat et enim consequatur." +
                    "Rem iure eligendi ea Quis reiciendis 33 beatae reprehenderit ut corporis rerum aut esse velit qui totam recusandae sed pariatur vero. " +
                    "Ut dicta recusandae qui incidunt ratione qui necessitatibus totam vel corrupti consequatur in numquam temporibus." +
                    "Ea mollitia laudantium ut autem molestiae in voluptatem officiis et iure autem nam fugit maxime qui distinctio molestiae cum consequatur blanditiis. " +
                    "Et voluptas consequatur eum sapiente dolor sit reprehenderit omnis non deleniti optio eos quibusdam autem? Ut alias consequatur ut illum esse est autem nobis est perspiciatis dolor.",
                    Created = DateTime.Parse("24 Jun 2022"),
                    Updated = DateTime.Parse("24 Jun 2022"),
                    IsPrivate = true
                },
                new Post()
                {
                    Id = Guid.NewGuid(),
                    Title = "Ninth (private) Post",
                    Body = "Lorem ipsum dolor sit amet. Qui voluptatem dignissimos hic dignissimos voluptas aut incidunt saepe ex architecto maxime eos minima reiciendis ut Quis assumenda a odio minima. " +
                    "Aut ducimus pariatur ut veritatis cupiditate ut iusto accusantium sed enim quos 33 repudiandae dolorem est aperiam inventore ut voluptatem esse. " +
                    "Et eveniet voluptates rem repudiandae voluptas non accusamus velit qui porro suscipit aut quas earum est autem quia et iusto libero. " +
                    "Et asperiores cumque est veniam voluptas ut eaque dolores eum sequi vitae!" +
                    "Ut deleniti laborum At eveniet eveniet a officia laboriosam a impedit beatae id voluptas accusamus ut impedit esse qui reiciendis iure. " +
                    "Et debitis deleniti et possimus tempore ut totam ducimus unde fugiat id rerum maiores? " +
                    "Non molestiae quis ut deleniti doloremque ut suscipit velit et magnam pariatur ea nesciunt consequuntur. " +
                    "Qui magnam aspernatur et libero obcaecati et molestiae quas aut ullam nulla." +
                    "Aut molestias neque est voluptatem dignissimos est molestiae vero. In ratione velit est totam dolor et omnis velit aut expedita alias. " +
                    "Ut voluptatem quia hic voluptas sed consectetur veritatis et quia nihil quo fugiat aspernatur nam deserunt dolor.",
                    Created = DateTime.Parse("24 Jun 2022"),
                    Updated = DateTime.Parse("24 Jun 2022"),
                    IsPrivate = true
                },
                new Post()
                {
                    Id = Guid.NewGuid(),
                    Title = "Tenth and final seeded (private) Post",
                    Body = "Lorem ipsum dolor sit amet. Et quia error qui libero quia eum deserunt quia qui cupiditate atque et explicabo recusandae. " +
                    "Id repellendus iste et galisum sunt vel unde voluptatibus et nihil animi et impedit quaerat et veniam aspernatur sed incidunt esse?" +
                    "Et totam quod et beatae consequatur et harum autem. Eos ducimus nobis et excepturi recusandae sed nihil Quis. " +
                    "Qui debitis aliquam sed sapiente placeat est perferendis magnam eos dolor odit. Cum voluptatem cupiditate est earum ratione et dolorum nisi hic iure saepe a praesentium aperiam. " +
                    "Et nihil sint et quos blanditiis et fugiat ipsum ad consectetur temporibus. " +
                    "Ea ullam voluptatibus et eveniet dolor qui omnis repellat sit facere voluptas sed nihil rerum sit sint voluptatibus. " +
                    "Aut quae dolore aut voluptate libero ab odit doloremque sit nihil harum. Est adipisci consequuntur non magnam voluptas in deleniti molestias.",
                    Created = DateTime.Parse("24 Jun 2022"),
                    Updated = DateTime.Parse("24 Jun 2022"),
                    IsPrivate = true
                }
            };

            await _context.AddRangeAsync(postArraySeed);

            await _context.SaveChangesAsync();
        }
    }
}