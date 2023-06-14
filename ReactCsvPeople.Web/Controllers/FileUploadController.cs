using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactCsvPeople.Data;
using ReactCsvPeople.Web.ViewModels;
using System.Globalization;
using System;
using System.Text;

namespace ReactCsvPeople.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private string _connectionString;

        public FileUploadController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        [HttpPost]
        [Route("Upload")]
        public void Upload(UploadViewModel viewModel)
        {
            string base64 = viewModel.Base64.Substring(viewModel.Base64.IndexOf(",") + 1);
            byte[] csvBytes = Convert.FromBase64String(base64);
            System.IO.File.WriteAllBytes($"uploads/{viewModel.Name}", csvBytes);


            var ppl = GetCsvFromBytes(csvBytes);
            var repo = new PeopleRepository(_connectionString);
            repo.AddPeople(ppl);

        }

        [HttpGet]
        [Route("view")]
        public IActionResult ViewFile(string name)
        {
            byte[] fileData = System.IO.File.ReadAllBytes($"uploads/{name}");
            return File(fileData, "application/octet-stream", name);
        }

        private static List<FakerPerson> GetCsvFromBytes(byte[] csvBytes)
        {
            using var memoryStream = new MemoryStream(csvBytes);
            var streamReader = new StreamReader(memoryStream);
            using var reader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            return reader.GetRecords<FakerPerson>().ToList();
        }

       





    }
}
