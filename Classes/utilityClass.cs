using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TEPSClientInstallService_Master.Classes
{
    public class utilityClass
    {
        private loggingClass loggingClass = new loggingClass();

        public async Task parseJsonForMessage(string clientname, int EnrolledInstanceType, string message)
        {
            var data = JsonConvert.DeserializeObject<List<tupleData>>(message);

            foreach (var item in data)
            {
                if (item.responseCode.Equals("400 Bad Request"))
                {
                    await loggingClass.submitSQLError(item.message);
                }
                if (item.responseCode.Equals("200 OK"))
                {
                    if (item.message.Contains("Installed"))
                    {
                        await loggingClass.submitSQLInstallLog(clientname, EnrolledInstanceType, item.message);
                    }
                    else
                    {
                        await loggingClass.submitSQLUninstallInstallLog(clientname, EnrolledInstanceType, item.message);
                    }
                }
            }
        }

        public int parseRequestBodyEnrolledInstanceType(string body)
        {
            dynamic jsonObj = JsonConvert.DeserializeObject(body);

            return jsonObj["Instance"];
        }
    }
}

public class tupleData
{
    public string responseCode { get; set; }
    public string message { get; set; }
}