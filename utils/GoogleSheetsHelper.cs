using Google.Apis.Sheets.v4;
using Google.Apis.Services;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.SecretManager.V1;


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
            var credential = GetCredentials(); //GetCredentialsFromFile();
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

        private GoogleCredential GetCredentials()
        {

            GoogleCredential credential;
            // TEST
            // Create the client
            var client = SecretManagerServiceClient.Create();

            // Retrieve the secret value by name
            var response = client.AccessSecretVersion(new AccessSecretVersionRequest
            {
                SecretVersionName = new SecretVersionName("computas-dk-playground", "warehouse_client_secrets", "latest")
            });

            // Get the secret value
            credential = GoogleCredential.FromJson(response.Payload.Data.ToStringUtf8());
            return credential;
        }
    }
}