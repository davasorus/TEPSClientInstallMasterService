# TEPSClientInstallService-Master

Designed to programmatically interact with the [Agent Service](https://github.com/davasorus/TEPSClientInstallServiceAgent)


## Testing the API 
- [Download Postman](https://www.postman.com/) or [insomnia](https://insomnia.rest/)
- follow install directions (if running for the first time)
- interact with API via api interaction tool
  - check logging for end point and port
  - logging should be C:\ProgramData\Tyler Technologies\Public Safety\Tyler-Client-Install-Master-Service\Logging\TEPS Automated Client Install Master Service (release number).json
     - EX C:\ProgramData\Tyler Technologies\Public Safety\Tyler-Client-Install-Master-Service\Logging\TEPS Automated Client Install Master Service 1.22.10.3.json
   - TEPS testing pre reqs https://tylersftp.tylertech.com/w/f-9141c57b-f4b0-4d7f-b76d-40d62769c99d
   - Below is the recommended JSON test package (Feel free to chage the values themselves to whatever you prefer)
   - {
    "ESSServer" : "TEST1",
    "MSPServer" : "TEST2",
    "CADServer" : "TEST3",
    "GISServer" : "TEST4",
    "GISInstance" : "TEST5",
    "MobileServer" : "Test6",
    "Instance": 2,
    "PoliceList": [
    {
      "FieldName": "ORI1",
      "ORI": "TX1234567"
    },
    {
      "FieldName": "ORI2",
      "ORI": "TX0987654"
    }
  ],
  "FireList": [
    {
      "FieldName": "FDID1",
      "FDID": "12345"
    },
    {
      "FieldName": "FDID2",
      "FDID": "09876"
    }
  ]
}
  
## Install the service
  
## Uninstall the service
-  open up a command prompt terminal in admin mode
- paste cd C:\Windows\Microsoft.NET\Framework\v4.0.30319 in and press enter
- navigate to the folder that the service is running out of
  - EX C:\Services\Tyler-Client-Install-Master-Service
- type in installutil.exe -u (PATH TO SERVICE EXECUTABLE IN YOUR DEBUG OR RELEASE DIR)\TEPSAutomatedClientInstallAgent.exe
  - EX installutil.exe -u C:\Services\Tyler-Client-Install-Master-Service\TEPSClientInstallMasterService.exe
- press enter 
