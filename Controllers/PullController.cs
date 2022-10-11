using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using TEPSClientInstallService_Master.Classes;

namespace TEPSClientInstallService_Master.Controllers
{
    public class PullController : ApiController
    {
        private loggingClass loggingClass = new loggingClass();
        private sqlServerInteractionClass sqlServerInteraction = new sqlServerInteractionClass();
        private utilityClass utilityClass = new utilityClass();

        //not implemented in agent yet
        public async Task<IHttpActionResult> GetHealthCheck(int id)
        {
            HttpClient httpClient = new HttpClient();
            string json = "";

            try
            {
                int enrolledInstanceType = utilityClass.parseRequestBodyEnrolledInstanceType(Request.Content.ReadAsStringAsync().Result);

                string[] exec = { id.ToString() };

                //httpClient.Timeout = TimeSpan.FromMinutes(10);
                var defaultRequestHeaders = httpClient.DefaultRequestHeaders;

                if (defaultRequestHeaders.Accept == null || !defaultRequestHeaders.Accept.Any(m => m.MediaType == "application/json"))
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new
                      MediaTypeWithQualityHeaderValue("application/json"));
                }

                var sqlID = sqlServerInteraction.returnClientName("GetClientByID", exec);

                var URI = $"http://{sqlID}:8080/Software//GetHealthCheck";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                HttpResponseMessage response = await httpClient.GetAsync(URI);

                json = await response.Content.ReadAsStringAsync();

                loggingClass.logEntryWriter($"response received {json}", "info");

                await utilityClass.parseJsonForMessage(sqlID, enrolledInstanceType, json);
            }
            catch (Exception ex)
            {
                loggingClass.logEntryWriter(ex.ToString(), "error");

                json = ex.Message;
            }

            return Json(json);
        }

        //not implemented in agent yet
        public async Task<IHttpActionResult> GetPresentFiles(int id)
        {
            HttpClient httpClient = new HttpClient();
            string json = "";

            try
            {
                int enrolledInstanceType = utilityClass.parseRequestBodyEnrolledInstanceType(Request.Content.ReadAsStringAsync().Result);

                string[] exec = { id.ToString() };

                //httpClient.Timeout = TimeSpan.FromMinutes(10);
                var defaultRequestHeaders = httpClient.DefaultRequestHeaders;

                if (defaultRequestHeaders.Accept == null || !defaultRequestHeaders.Accept.Any(m => m.MediaType == "application/json"))
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new
                      MediaTypeWithQualityHeaderValue("application/json"));
                }

                var sqlID = sqlServerInteraction.returnClientName("GetClientByID", exec);

                var URI = $"http://{sqlID}:8080/Software//GetPresentFiles";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                HttpResponseMessage response = await httpClient.GetAsync(URI);

                json = await response.Content.ReadAsStringAsync();

                loggingClass.logEntryWriter($"response received {json}", "info");

                await utilityClass.parseJsonForMessage(sqlID, enrolledInstanceType, json);
            }
            catch (Exception ex)
            {
                loggingClass.logEntryWriter(ex.ToString(), "error");

                json = ex.Message;
            }

            return Json(json);
        }

        public async Task<IHttpActionResult> GetInstalledSoftware(int id)
        {
            HttpClient httpClient = new HttpClient();
            string json = "";

            try
            {
                int enrolledInstanceType = utilityClass.parseRequestBodyEnrolledInstanceType(Request.Content.ReadAsStringAsync().Result);

                string[] exec = { id.ToString() };

                httpClient.Timeout = TimeSpan.FromMinutes(10);
                var defaultRequestHeaders = httpClient.DefaultRequestHeaders;

                if (defaultRequestHeaders.Accept == null || !defaultRequestHeaders.Accept.Any(m => m.MediaType == "application/json"))
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new
                      MediaTypeWithQualityHeaderValue("application/json"));
                }

                var sqlID = sqlServerInteraction.returnClientName("GetClientByID", exec);

                var URI = $"http://{sqlID}:8080/Software//GetInstalledSoftware";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                HttpResponseMessage response = await httpClient.GetAsync(URI);

                json = await response.Content.ReadAsStringAsync();

                loggingClass.logEntryWriter($"response received from {sqlID} {json}", "info");

                await utilityClass.parseJsonForMessage(sqlID, enrolledInstanceType, json);
            }
            catch (TaskCanceledException ex)
            {
                if (ex.CancellationToken.IsCancellationRequested)
                {
                    loggingClass.logEntryWriter($"cancellation was requested?", "debug");
                    loggingClass.logEntryWriter(ex.ToString(), "debug");

                    json = ex.ToString();
                }
            }
            catch (Exception ex)
            {
                loggingClass.logEntryWriter(ex.ToString(), "error");

                json = ex.Message;
            }

            return Json(json);
        }
    }
}