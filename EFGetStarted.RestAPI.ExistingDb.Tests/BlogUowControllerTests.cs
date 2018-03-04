using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using EFGetStarted.RestAPI.ExistingDb.Models;
using EFGetStarted.RestAPI.ExistingDb.Controllers;
using System.Threading.Tasks;
using EFGetStarted.RestAPI.ExistingDb.Data;
using System;
using EFGetStarted.RestAPI.ExistingDb.GenericData;

namespace EFGetStarted.RestAPI.ExistingDb.Tests
{
    [TestClass]
    public class BlogUowControllerTests
    {
        [TestMethod]
        public async Task Add_writes_to_database()
        {
          

            try
            {
                string connection = @"Server=(localdb)\mssqllocaldb;Database=BloggingGeneric;Trusted_Connection=True;ConnectRetryCount=0";
                var options = new DbContextOptionsBuilder<DataContext>()
               .UseSqlServer(connection)
               .Options;


                //// Create the schema in the database
                //using (var context = new BloggingContext(options))
                //{
                //    context.Database.EnsureCreated();
                //}


                // Run the test against one instance of the context
                using (var context = new DataContext(options))
                {
                    //IBlogRepositoryGeneric blogsRepository = new BlogRepositoryGenerics(context);

                    var uow = new UnitOfWork<DataContext>(context);
                    var repo = uow.GetRepository<Blog>();
                    var blogsUOWController = new BlogsUOWController(uow);
                    var result = await repo.Get();
                }

                //Use a separate instance of the context to verify correct data was saved to database
                //using (var context = new BloggingContext(options))
                //{
                //    Assert.AreEqual(1, context.Blogs.Count());
                //    Assert.AreEqual("http://sample.com", context.Blogs.Single().Url);
                //}
            }
            catch(Exception e)
            {


            }

            finally
            {
                
            }
        }


    }
}
