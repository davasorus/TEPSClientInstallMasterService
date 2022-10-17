using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using TEPSClientInstallService_Master.Classes;

namespace TEPSClientInstallService_Master.Controllers
{
    public class ManageController : ApiController
    {
        private loggingClass loggingClass = new loggingClass();
        private sqlServerInteractionClass sqlServerInteraction = new sqlServerInteractionClass();
        private utilityClass utilityClass = new utilityClass();

        //not implemented yet
        public async Task<IHttpActionResult> Post(int id)
        {
            string json = "";
            HttpClient httpClient = new HttpClient();

            try
            {
                string[] exec = { id.ToString() };

                httpClient.Timeout = TimeSpan.FromMinutes(10);
                var defaultRequestHeaders = httpClient.DefaultRequestHeaders;

                if (defaultRequestHeaders.Accept == null || !defaultRequestHeaders.Accept.Any(m => m.MediaType == "application/json"))
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new
                      MediaTypeWithQualityHeaderValue("application/json"));
                }

                var sqlID = sqlServerInteraction.returnClientName("GetClientByID", exec);

                var URI = $"http://{sqlID}:8080/Software//";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                var package = Request.Content.ReadAsStringAsync().Result;

                var stringContent = new StringContent(package, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(URI, stringContent);

                json = await response.Content.ReadAsStringAsync();

                loggingClass.logEntryWriter($"response received from {sqlID} {json}", "info");
            }
            catch (Exception ex)
            {
                loggingClass.logEntryWriter(ex.ToString(), "error");

                json = ex.Message;
            }

            return Json(json);
        }

        public async Task<IHttpActionResult> PostUpdateSettingsDB(int id)
        {
            string json = "";

            try
            {
                await utilityClass.configureSettingsTableAsync(Request.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                loggingClass.logEntryWriter(ex.ToString(), "error");

                json = ex.Message;

                return BadRequest(json);
            }

            return Ok("DataBase Updated");
        }

        public async Task<IHttpActionResult> GetAllClients()
        {
            string[] executionText = { };

            var response = sqlServerInteraction.returnClientTable(executionText);

            return Json(response);
        }

        public async Task<IHttpActionResult> GetAllClientsByID(int ID)
        {
            string[] executionText = { ID.ToString() };

            var response = sqlServerInteraction.returnClientTable(executionText);

            return Json(response);
        }

        public async Task<IHttpActionResult> GetAllInstalledCatalogs()
        {
            string[] executionText = { };

            var response = sqlServerInteraction.returnCatalogTable(executionText);

            return Json(response);
        }

        public async Task<IHttpActionResult> GetInstalledCatalogsByID(int ID)
        {
            string[] executionText = { ID.ToString() };

            var response = sqlServerInteraction.returnCatalogTable(executionText);

            return Json(response);
        }

        public async Task<IHttpActionResult> GetTop50Errors()
        {
            var response = sqlServerInteraction.returnErrorTable("GetTop50Errors");

            return Json(response);
        }

        public async Task<IHttpActionResult> Get1000Errors()
        {
            var response = sqlServerInteraction.returnErrorTable("GetTop1000Errors");

            return Json(response);
        }
    }
}