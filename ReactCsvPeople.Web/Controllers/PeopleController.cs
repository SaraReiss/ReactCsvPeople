using Microsoft.AspNetCore.Mvc;
using ReactCsvPeople.Data;
using System.Text;
using Bogus;
using CsvHelper;
using System.Globalization;

namespace ReactCsvPeople.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {

        private string _connectionString;

        public PeopleController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        [HttpGet]
        [Route("getall")]
        public List<FakerPerson> GetAll()
        {
            var repo = new PeopleRepository(_connectionString);
            return repo.GetPeople();
        }

        [HttpPost]
        [Route("deleteall")]
        public void Delete()
        {
            var repo = new PeopleRepository(_connectionString);
            repo.DeletePeople();
        }

        [HttpGet]
        [Route("generate")]
        public IActionResult Generate(int amount)
        {
           var people = GenerateFakerPeople(amount);
            var csv = BuildPeopleCsv(people);
            return File(Encoding.UTF8.GetBytes(csv), "text/csv", "people.csv");
        }



        private static List<FakerPerson> GenerateFakerPeople(int amount)

        {
            return Enumerable.Range(1, amount).Select(_ => new FakerPerson
            {
                FirstName = Faker.Name.First(),
                LastName = Faker.Name.Last(),
                Age = Faker.RandomNumber.Next(11, 100),
                Address = Faker.Address.StreetAddress(),
                Email = Faker.Internet.Email()
            }).ToList();
        }
        private static string BuildPeopleCsv(List<FakerPerson> people)
        {
            var builder = new StringBuilder();
            var stringWriter = new StringWriter(builder);
            using var csv = new CsvWriter(stringWriter, CultureInfo.InvariantCulture);
            csv.WriteRecords(people);
            return builder.ToString();
        }
    }
}
