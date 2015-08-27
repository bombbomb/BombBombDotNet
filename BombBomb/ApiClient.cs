using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BombBomb
{
    public class ApiClient
    {
        private String username;
        private String password;
        private string apiKey;

        public ApiClient(String username, String password) {
            this.username = username;
            this.password = password;
        }

        public ApiClient(String apiKey) {
            this.apiKey = apiKey;
        }

        public Boolean isLoginValid()
        {
            try { 
                var response = this.getAuthenticatedResponse("IsValidLogin", new Dictionary<string,string>());
                return (string)response["status"] == "success";
            } catch (Exception e) {
                return false;
            }
        }

        public JObject getAuthenticatedResponse(String methodName, Dictionary<string, string> parameters)
        {
            if (this.apiKey == null)
            {
                parameters.Add("email", this.username);
                parameters.Add("pw", this.password);
            }
            else
            {
                parameters.Add("api_key", this.apiKey);
            }
            return this.getApiResponse(methodName, parameters);
        }

        public JObject getApiResponse(String methodName, Dictionary<string, string> parameters)
        {
            WebClient webClient = new WebClient();
            webClient.QueryString.Add("method", methodName);

            foreach (KeyValuePair<string, string> param in parameters)
            {
                webClient.QueryString.Add(param.Key, param.Value);
            }

            try {
                string result = webClient.DownloadString("http://app.bombbomb.com/app/api/api.php");
                return this.getJsonFromString(result);
            } catch (WebException e) {
                throw new Exception("Error: " + e.Message);
            }

        }

        private JObject getJsonFromString(string jsonString) {
            var thinger = (JObject)JsonConvert.DeserializeObject(jsonString);
            return thinger;
        }
    }
}
