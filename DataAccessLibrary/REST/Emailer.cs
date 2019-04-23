using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.REST
{
    public class Emailer
    {
        static string serverAddress = "localhost";//"168.63.65.229";
        static string serverPort = "8081";

        private static string EmailURL = "/api/email";

        public static async Task<Res> SendEmail(EmailData emaildata)
        {
            HttpClient client = new HttpClient();

            var post = new Post { Title = "" + DateTime.Now.Ticks, Body = JsonConvert.SerializeObject(emaildata) };


            string URL = "http://" + serverAddress + ":" + serverPort + EmailURL;

            var content = new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json");


            var res = await client.PostAsync(URL, content);

            string resBody = await res.Content.ReadAsStringAsync();
            Res response = JsonConvert.DeserializeObject<Res>(resBody);//JsonParse(resBody);


            return response;
        }
    }
}
