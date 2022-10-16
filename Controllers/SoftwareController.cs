using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using TEPSClientInstallService_Master.Classes;

namespace TEPSClientInstallService_Master.Controllers
{
    public class SoftwareController : ApiController
    {
        private loggingClass loggingClass = new loggingClass();
        private sqlServerInteractionClass sqlServerInteraction = new sqlServerInteractionClass();
        private utilityClass utilityClass = new utilityClass();

        public async Task<IHttpActionResult> GETNewPreReq()
        {
            int enrolledInstanceType = utilityClass.parseRequestBodyEnrolledInstanceType(Request.Content.ReadAsStringAsync().Result);
            string requiredPreReq = utilityClass.parseRequestBodyFileName(Request.Content.ReadAsStringAsync().Result);
            string[] exec = { enrolledInstanceType.ToString(), requiredPreReq };

            var filename = "";
            var filePath = "";

            if (requiredPreReq != null)
            {
                DataTable table = sqlServerInteraction.returnPreReqTable(exec);

                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        filename = row[2].ToString();
                        filePath = row[3].ToString();
                    }
                }
                else
                {
                    return NotFound();
                }

                var bytes = File.ReadAllBytes(filePath);

                var holder = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(bytes)
                };
                holder.Content.Headers.ContentDisposition =
                    new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = filename
                    };
                holder.Content.Headers.ContentType =
                    new MediaTypeHeaderValue("application/octet-stream");

                IHttpActionResult result = this.ResponseMessage(holder);

                return result;
            }
            return NotFound();
        }

        public async Task<IHttpActionResult> POSTNewPreReq()
        {
            int enrolledInstanceType = utilityClass.parseRequestBodyEnrolledInstanceType(Request.Content.ReadAsStringAsync().Result);
            string requiredPreReq = utilityClass.parseRequestBodyFileName(Request.Content.ReadAsStringAsync().Result);
            string[] exec = { enrolledInstanceType.ToString(), requiredPreReq };

            var filename = "";
            var filePath = "";

            if (requiredPreReq != null)
            {
                DataTable table = sqlServerInteraction.returnPreReqTable(exec);

                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        filename = row[2].ToString();
                        filePath = row[3].ToString();
                    }
                }
                else
                {
                    return NotFound();
                }

                var bytes = File.ReadAllBytes(filePath);

                var holder = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(bytes)
                };
                holder.Content.Headers.ContentDisposition =
                    new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = filename
                    };
                holder.Content.Headers.ContentType =
                    new MediaTypeHeaderValue("application/octet-stream");

                IHttpActionResult result = this.ResponseMessage(holder);

                return result;
            }
            return NotFound();
        }
    }
}