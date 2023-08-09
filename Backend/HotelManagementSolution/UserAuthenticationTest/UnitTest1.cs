using Microsoft.EntityFrameworkCore;
using UserAuthentication.Interfaces;
using UserAuthentication.Models;
using UserAuthentication.Models.Context;
using UserAuthentication.Repository;

namespace UserAuthenticationTest
{
    [TestClass]
    public class UnitTest1
    {
        public DbContextOptions<UserContext> GetDbcontextOption()
        {
            var contextOptions = new DbContextOptionsBuilder<UserContext>()
                                   .UseInMemoryDatabase(databaseName: "testMemory")
                                    .Options;
            return contextOptions;
        }
        [TestMethod]
        public async Task TestGetAllAgents()
        {
            using (var userContext = new UserContext(GetDbcontextOption()))
            {

                userContext.TravelAgents.Add(new TravelAgent
                {
                    Id = 1,
                    UserId = 1,
                    DateOfBirth = new DateTime(2001, 09, 12),
                    City = "Pollachi",
                    State = "Tamilnadu",
                    Country = "India",
                    User = new User() { Id = 1, Name = "Sangamithra",Username = "Mithra",EmailId = "sangamithra@gmail.com", PhoneNumber = "9867542315",PasswordHash = new byte[] { }, PasswordKey = new byte[] { }},
                }); ; ;
                await userContext.SaveChangesAsync();
                IAgentRepo<TravelAgent, int> repo = new AgentRepo(userContext);
                var data = await repo.GetAll();
                Assert.AreEqual(1, data.ToList().Count);

            }

        }
    }
}