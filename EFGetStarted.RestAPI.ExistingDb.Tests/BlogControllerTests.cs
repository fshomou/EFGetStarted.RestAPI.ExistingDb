using EFGetStarted.RestAPI.ExistingDb.Controllers;
using EFGetStarted.RestAPI.ExistingDb.Data;
using EFGetStarted.RestAPI.ExistingDb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace EFGetStarted.RestAPI.ExistingDb.Tests
{
    [TestClass]
    public class BlogControllerTests
    {
        [TestMethod]
        public async Task Add_writes_to_database()
        {
            try
            {
                string connection = @"Server=(localdb)\mssqllocaldb;Database=BloggingTest;Trusted_Connection=True;ConnectRetryCount=0";
                var options = new DbContextOptionsBuilder<BloggingContext>()
               .UseSqlServer(connection)
               .Options;

                //// Create the schema in the database
                //using (var context = new BloggingContext(options))
                //{
                //    context.Database.EnsureCreated();
                //}

                // Run the test against one instance of the context
                using (var context = new BloggingContext(options))
                {
                    IBlogRepository blogsRepository = new BlogsRepository(context);

                    //var BlogsController = new BlogsController(blogsRepository);
                    //var result = await BlogsController.GetBlogs();
                }

                //Use a separate instance of the context to verify correct data was saved to database
                //using (var context = new BloggingContext(options))
                //{
                //    Assert.AreEqual(1, context.Blogs.Count());
                //    Assert.AreEqual("http://sample.com", context.Blogs.Single().Url);
                //}
            }
            catch (Exception e)
            {
            }
            finally
            {
            }
        }
    }
}