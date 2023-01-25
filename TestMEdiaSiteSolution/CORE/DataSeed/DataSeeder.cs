using CORE.DbContexts;
using CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CORE.DataSeed;

public class DataSeeder
{
    public static async Task SeedAsync(MediaContext context, ILoggerFactory loggerFactory)
    {
        using (var dbTransaction = context.Database.BeginTransaction())
        {
            try
            {
                if (!context.MediaTypes.Any())
                {
                    var mediaTypeList = new List<MediaType>
                    {
                        new()
                        {
                            MediaTypeId = 1,
                            MediaTypeTitle = "ფილმი"
                        },
                        new()
                        {
                            MediaTypeId = 2,
                            MediaTypeTitle = "ანიმაციური ფილმი"
                        },
                        new()
                        {
                            MediaTypeId = 3,
                            MediaTypeTitle = "სერიალი"
                        }
                    };


                    context.MediaTypes.AddRange(mediaTypeList);

                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[MediaTypes] ON");

                    context.SaveChanges();

                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[MediaTypes] OFF");
                }

                if (!context.Medias.Any())
                {
                    var mediaList = new List<Media>
                    {
                        new()
                        {
                            MediaId = 1,
                            MediaTypeId = 1,
                            MediaTitle = "ნიუ იორკის ბანდები",
                            PictureUrl =
                                "https://srulad.com/assets/uploads/posters/501/501_374e92f700d17195139e76af5bf95358.jpg"
                        },
                        new()
                        {
                            MediaId = 2,
                            MediaTypeId = 2,
                            MediaTitle = "ჩაო, ალბერტო",
                            PictureUrl =
                                "https://kinogo-net.cc/uploads/mini/200x300/99/548edb6fdbb09e107a9e37ab426a03.jpg"
                        },
                        new()
                        {
                            MediaId = 3,
                            MediaTypeId = 3,
                            MediaTitle = "ტულსის მეფე",
                            PictureUrl =
                                "https://kinogo-la.net/uploads/mini/full/15/be269f6d33a64e15b3a2f9c14e07ac.webp"
                        },
                        new()
                        {
                            MediaId = 4,
                            MediaTypeId = 1,
                            MediaTitle = "ოპერაცია Fortune: გამარჯვების ხელოვნება",
                            PictureUrl =
                                "https://kinogo-la.net/uploads/mini/full/9d/6bf86f41d937f7ff03861c763709fe.webp"
                        }
                    };


                    context.Medias.AddRange(mediaList);

                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Medias] ON");

                    context.SaveChanges();

                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Medias] OFF");
                }


                if (!context.WatchLists.Any())
                {
                    var watchList = new List<WatchList>
                    {
                        new()
                        {
                            UserId = 1,
                            MediaId = 1,
                            Watched = true
                        },
                        new()
                        {
                            UserId = 1,
                            MediaId = 2,
                            Watched = false
                        },
                        new()
                        {
                            UserId = 1,
                            MediaId = 3,
                            Watched = true
                        }
                    };


                    context.AddRange(watchList);

                    await context.SaveChangesAsync();
                }

                dbTransaction.Commit();
            }

            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<DataSeeder>();
                logger.LogError(ex.Message);
                dbTransaction.Rollback();
            }
        }
    }
}