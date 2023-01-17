using Google.Apis.Sheets.v4;
using Google.Apis.Services;
using Google.Apis.Auth.OAuth2;

namespace warehouse_app.utils {

    public class GoogleSheetsHelper
    {
        public SheetsService Service {get; set;} = null!; // it means that Service CANNOT be null
        const string APPLICATION_NAME = "WarehouseApp";
        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };

        public GoogleSheetsHelper()
        {
            InitializeService();
        }

        private void InitializeService()
        {
            var credential = GetCredentialsFromFile();
            Service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = APPLICATION_NAME
            });
        }

        private GoogleCredential GetCredentialsFromFile()
        {
            GoogleCredential credential;

            using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }

            return credential;
        }
    }
}