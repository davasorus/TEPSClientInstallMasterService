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
    public class PushController : ApiController
    {
        private utilityClass utilityClass = new utilityClass();

        private loggingClass loggingClass = new loggingClass();
        private sqlServerInteractionClass sqlServerInteraction = new sqlServerInteractionClass();

        //not implemented in agent yet
        public async Task<IHttpActionResult> Post(int id)
        {
            string json = "";
            HttpClient httpClient = new HttpClient();

            try
            {
                string[] exec = { id.ToString() };

                //httpClient.Timeout = TimeSpan.FromMinutes(10);
                var defaultRequestHeaders = httpClient.DefaultRequestHeaders;

                if (defaultRequestHeaders.Accept == null || !defaultRequestHeaders.Accept.Any(m => m.MediaType == "application/json"))
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new
                      MediaTypeWithQualityHeaderValue("application/json"));
                }

                var sqlID = sqlServerInteraction.returnClientName("GetClientByID", exec);

                var URI = $"http://{sqlID}:8080/install/";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                var package = Request.Content.ReadAsStringAsync().Result;

                var stringContent = new StringContent(package, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(URI, stringContent);

                json = await response.Content.ReadAsStringAsync();

                loggingClass.logEntryWriter($"response received {json}", "info");
            }
            catch (Exception ex)
            {
                loggingClass.logEntryWriter(ex.ToString(), "error");

                json = ex.Message;
            }

            return Json(json);
        }

        #region pre req redirect uninstall

        public async Task<IHttpActionResult> PostUninstallSQLCE35(int id)
        {
            string json = "";
            HttpClient httpClient = new HttpClient();

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

                var URI = $"http://{sqlID}:8080/uninstall/PostUninstallSQLCE35Async";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                var package = Request.Content.ReadAsStringAsync().Result;

                var stringContent = new StringContent(package, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(URI, stringContent);

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

        public async Task<IHttpActionResult> PostUninstallGIS(int id)
        {
            string json = "";
            HttpClient httpClient = new HttpClient();

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

                var URI = $"http://{sqlID}:8080/uninstall/PostUninstallGISAsync";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                var package = Request.Content.ReadAsStringAsync().Result;

                var stringContent = new StringContent(package, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(URI, stringContent);

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

        public async Task<IHttpActionResult> PostUninstallUpdater(int id)
        {
            string json = "";
            HttpClient httpClient = new HttpClient();

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

                var URI = $"http://{sqlID}:8080/uninstall/PostUninstallUpdaterAsync";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                var package = Request.Content.ReadAsStringAsync().Result;

                var stringContent = new StringContent(package, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(URI, stringContent);

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

        public async Task<IHttpActionResult> PostUninstallScenePD(int id)
        {
            string json = "";
            HttpClient httpClient = new HttpClient();

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

                var URI = $"http://{sqlID}:8080/uninstall/PostUninstallScenePDAsync";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                var package = Request.Content.ReadAsStringAsync().Result;

                var stringContent = new StringContent(package, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(URI, stringContent);

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

        public async Task<IHttpActionResult> PostUninstallSQLCE40(int id)
        {
            string json = "";
            HttpClient httpClient = new HttpClient();

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

                var URI = $"http://{sqlID}:8080/uninstall/PostUninstallSQLCE40Async";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                var package = Request.Content.ReadAsStringAsync().Result;

                var stringContent = new StringContent(package, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(URI, stringContent);

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

        public async Task<IHttpActionResult> PostUninstallSQLCLR(int id)
        {
            string json = "";
            HttpClient httpClient = new HttpClient();

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

                var URI = $"http://{sqlID}:8080/uninstall/PostUninstallSQLCLRAsync";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                var package = Request.Content.ReadAsStringAsync().Result;

                var stringContent = new StringContent(package, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(URI, stringContent);

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

        #endregion pre req redirect uninstall

        #region pre req redirect install

        public async Task<IHttpActionResult> PostInstallDotNet(int id)
        {
            string json = "";
            HttpClient httpClient = new HttpClient();

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

                var URI = $"http://{sqlID}:8080/install/PostDotNetInstall";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                var package = Request.Content.ReadAsStringAsync().Result;

                var stringContent = new StringContent(package, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(URI, stringContent);

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

        public async Task<IHttpActionResult> PostInstallSQLCE35(int id)
        {
            string json = "";
            HttpClient httpClient = new HttpClient();

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

                var URI = $"http://{sqlID}:8080/install/PostSQLCE35Install";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                var package = Request.Content.ReadAsStringAsync().Result;

                var stringContent = new StringContent(package, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(URI, stringContent);

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

        public async Task<IHttpActionResult> PostInstallGIS(int id)
        {
            string json = "";
            HttpClient httpClient = new HttpClient();

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

                var URI = $"http://{sqlID}:8080//install/PostGISInstall";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                var package = Request.Content.ReadAsStringAsync().Result;

                var stringContent = new StringContent(package, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(URI, stringContent);

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

        public async Task<IHttpActionResult> PostInstallDBProviders(int id)
        {
            string json = "";
            HttpClient httpClient = new HttpClient();

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

                var URI = $"http://{sqlID}:8080/install/PostDBProviderInstall";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                var package = Request.Content.ReadAsStringAsync().Result;

                var stringContent = new StringContent(package, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(URI, stringContent);

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

        public async Task<IHttpActionResult> PostInstallUpdater(int id)
        {
            string json = "";
            HttpClient httpClient = new HttpClient();

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

                var URI = $"http://{sqlID}:8080/install/PostUpdaterInstall";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                var package = Request.Content.ReadAsStringAsync().Result;

                var stringContent = new StringContent(package, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(URI, stringContent);

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

        public async Task<IHttpActionResult> PostInstallScenePD(int id)
        {
            string json = "";
            HttpClient httpClient = new HttpClient();

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

                var URI = $"http://{sqlID}:8080/install/PostScenePDInstall";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                var package = Request.Content.ReadAsStringAsync().Result;

                var stringContent = new StringContent(package, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(URI, stringContent);

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

        public async Task<IHttpActionResult> PostInstallSQLCE40(int id)
        {
            string json = "";
            HttpClient httpClient = new HttpClient();

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

                var URI = $"http://{sqlID}:8080/install/PostSQLCE40Install";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                var package = Request.Content.ReadAsStringAsync().Result;

                var stringContent = new StringContent(package, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(URI, stringContent);

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

        public async Task<IHttpActionResult> PostInstallVS2010(int id)
        {
            string json = "";
            HttpClient httpClient = new HttpClient();

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

                var URI = $"http://{sqlID}:8080/install/Postvs2010Install";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                var package = Request.Content.ReadAsStringAsync().Result;

                var stringContent = new StringContent(package, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(URI, stringContent);

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

        public async Task<IHttpActionResult> PostInstallSQLCLR2008(int id)
        {
            string json = "";
            HttpClient httpClient = new HttpClient();

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

                var URI = $"http://{sqlID}:8080/install/PostSQLCLR2008Install";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                var package = Request.Content.ReadAsStringAsync().Result;

                var stringContent = new StringContent(package, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(URI, stringContent);

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

        public async Task<IHttpActionResult> PostInstallSQLCLR2012(int id)
        {
            string json = "";
            HttpClient httpClient = new HttpClient();

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

                var URI = $"http://{sqlID}:8080/install/PostSQLCLR2012Install";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                var package = Request.Content.ReadAsStringAsync().Result;

                var stringContent = new StringContent(package, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(URI, stringContent);

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

        #endregion pre req redirect install

        #region client redirect uninstall

        public async Task<IHttpActionResult> PostUninstallMSP(int id)
        {
            string json = "";
            HttpClient httpClient = new HttpClient();

            try
            {
                string[] exec = { id.ToString() };

                //httpClient.Timeout = TimeSpan.FromMinutes(10);
                var defaultRequestHeaders = httpClient.DefaultRequestHeaders;

                if (defaultRequestHeaders.Accept == null || !defaultRequestHeaders.Accept.Any(m => m.MediaType == "application/json"))
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new
                      MediaTypeWithQualityHeaderValue("application/json"));
                }

                var sqlID = sqlServerInteraction.returnClientName("GetClientByID", exec);

                var URI = $"http://{sqlID}:8080/uninstall/PostUninstallMSPAsync";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                var package = Request.Content.ReadAsStringAsync().Result;

                var stringContent = new StringContent(package, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(URI, stringContent);

                json = await response.Content.ReadAsStringAsync();

                loggingClass.logEntryWriter($"response received {json}", "info");
            }
            catch (Exception ex)
            {
                loggingClass.logEntryWriter(ex.ToString(), "error");

                json = ex.Message;
            }

            return Json(json);
        }

        public async Task<IHttpActionResult> PostUninstallCAD(int id)
        {
            string json = "";
            HttpClient httpClient = new HttpClient();

            try
            {
                string[] exec = { id.ToString() };

                //httpClient.Timeout = TimeSpan.FromMinutes(10);
                var defaultRequestHeaders = httpClient.DefaultRequestHeaders;

                if (defaultRequestHeaders.Accept == null || !defaultRequestHeaders.Accept.Any(m => m.MediaType == "application/json"))
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new
                      MediaTypeWithQualityHeaderValue("application/json"));
                }

                var sqlID = sqlServerInteraction.returnClientName("GetClientByID", exec);

                var URI = $"http://{sqlID}:8080/uninstall/PostUninstallCADAsync";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                var package = Request.Content.ReadAsStringAsync().Result;

                var stringContent = new StringContent(package, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(URI, stringContent);

                json = await response.Content.ReadAsStringAsync();

                loggingClass.logEntryWriter($"response received {json}", "info");
            }
            catch (Exception ex)
            {
                loggingClass.logEntryWriter(ex.ToString(), "error");

                json = ex.Message;
            }

            return Json(json);
        }

        public async Task<IHttpActionResult> PostUninstallMobile(int id)
        {
            string json = "";
            HttpClient httpClient = new HttpClient();

            try
            {
                string[] exec = { id.ToString() };

                //httpClient.Timeout = TimeSpan.FromMinutes(10);
                var defaultRequestHeaders = httpClient.DefaultRequestHeaders;

                if (defaultRequestHeaders.Accept == null || !defaultRequestHeaders.Accept.Any(m => m.MediaType == "application/json"))
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new
                      MediaTypeWithQualityHeaderValue("application/json"));
                }

                var sqlID = sqlServerInteraction.returnClientName("GetClientByID", exec);

                var URI = $"http://{sqlID}:8080/uninstall/PostUninstallMobileAsync";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                var package = Request.Content.ReadAsStringAsync().Result;

                var stringContent = new StringContent(package, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(URI, stringContent);

                json = await response.Content.ReadAsStringAsync();

                loggingClass.logEntryWriter($"response received {json}", "info");
            }
            catch (Exception ex)
            {
                loggingClass.logEntryWriter(ex.ToString(), "error");

                json = ex.Message;
            }

            return Json(json);
        }

        public async Task<IHttpActionResult> PostUninstallObserver(int id)
        {
            string json = "";
            HttpClient httpClient = new HttpClient();

            try
            {
                string[] exec = { id.ToString() };

                //httpClient.Timeout = TimeSpan.FromMinutes(10);
                var defaultRequestHeaders = httpClient.DefaultRequestHeaders;

                if (defaultRequestHeaders.Accept == null || !defaultRequestHeaders.Accept.Any(m => m.MediaType == "application/json"))
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new
                      MediaTypeWithQualityHeaderValue("application/json"));
                }

                var sqlID = sqlServerInteraction.returnClientName("GetClientByID", exec);

                var URI = $"http://{sqlID}:8080/uninstall/PostUninstallObserverAsync";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                var package = Request.Content.ReadAsStringAsync().Result;

                var stringContent = new StringContent(package, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(URI, stringContent);

                json = await response.Content.ReadAsStringAsync();

                loggingClass.logEntryWriter($"response received {json}", "info");
            }
            catch (Exception ex)
            {
                loggingClass.logEntryWriter(ex.ToString(), "error");

                json = ex.Message;
            }

            return Json(json);
        }

        #endregion client redirect uninstall

        #region client redirect install

        public async Task<IHttpActionResult> PostInstallMSP(int id)
        {
            string json = "";
            HttpClient httpClient = new HttpClient();

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

                var URI = $"http://{sqlID}:8080/install/PostMSPClientInstall";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                var package = Request.Content.ReadAsStringAsync().Result;

                var stringContent = new StringContent(package, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(URI, stringContent);

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

        public async Task<IHttpActionResult> PostInstallCAD(int id)
        {
            string json = "";
            HttpClient httpClient = new HttpClient();

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

                var URI = $"http://{sqlID}:8080/install/PostCADClientInstall";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                var package = Request.Content.ReadAsStringAsync().Result;

                var stringContent = new StringContent(package, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(URI, stringContent);

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

        #endregion client redirect install

        #region client redirect mobile config

        public async Task<IHttpActionResult> PostLawMobile(int id)
        {
            string json = "";
            HttpClient httpClient = new HttpClient();

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

                var URI = $"http://{sqlID}:8080/install/PostLawMobileConfig";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                var package = Request.Content.ReadAsStringAsync().Result;

                var stringContent = new StringContent(package, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(URI, stringContent);

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

        public async Task<IHttpActionResult> PostFireMobile(int id)
        {
            string json = "";
            HttpClient httpClient = new HttpClient();

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

                var URI = $"http://{sqlID}:8080/install/PostFireMobileConfig";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                var package = Request.Content.ReadAsStringAsync().Result;

                var stringContent = new StringContent(package, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(URI, stringContent);

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

        public async Task<IHttpActionResult> PostMobileMerge(int id)
        {
            string json = "";
            HttpClient httpClient = new HttpClient();

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

                var URI = $"http://{sqlID}:8080/install/PostMergeMobileConfig";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                var package = Request.Content.ReadAsStringAsync().Result;

                var stringContent = new StringContent(package, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(URI, stringContent);

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

        #endregion client redirect mobile config

        #region block testing

        public async Task<IHttpActionResult> PostUninstallBlockPreReq(int id)
        {
            string json = "";
            HttpClient httpClient = new HttpClient();

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

                var URI = $"http://{sqlID}:8080/uninstall/Post99TESTUninstallPreReqAsync";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                var package = Request.Content.ReadAsStringAsync().Result;

                var stringContent = new StringContent(package, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(URI, stringContent);

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

        public async Task<IHttpActionResult> PostInstallBlockPreReq(int id)
        {
            string json = "";
            HttpClient httpClient = new HttpClient();

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

                var URI = $"http://{sqlID}:8080/install/Post99TESTPreReqInstall";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                var package = Request.Content.ReadAsStringAsync().Result;

                var stringContent = new StringContent(package, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(URI, stringContent);

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

        public async Task<IHttpActionResult> PostUninstallBlockClient(int id)
        {
            string json = "";
            HttpClient httpClient = new HttpClient();

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

                var URI = $"http://{sqlID}:8080/uninstall/Post99TESTUninstallClientAsync";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                var package = Request.Content.ReadAsStringAsync().Result;

                var stringContent = new StringContent(package, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(URI, stringContent);

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

        public async Task<IHttpActionResult> PostInstallBlockClient(int id)
        {
            string json = "";
            HttpClient httpClient = new HttpClient();

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

                var URI = $"http://{sqlID}:8080/install/Post99TESTClientInstall";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                var package = Request.Content.ReadAsStringAsync().Result;

                var stringContent = new StringContent(package, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(URI, stringContent);

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

        public async Task<IHttpActionResult> PostUpdaterConfigBlockMobile(int id)
        {
            string json = "";
            HttpClient httpClient = new HttpClient();

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

                var URI = $"http://{sqlID}:8080/install/Post99TESTMobileConfig";

                loggingClass.logEntryWriter($"forwarding message to {sqlID}", "info");
                loggingClass.logEntryWriter($"message forwarded {URI}", "info");

                var package = Request.Content.ReadAsStringAsync().Result;

                var stringContent = new StringContent(package, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(URI, stringContent);

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

        #endregion block testing
    }
}