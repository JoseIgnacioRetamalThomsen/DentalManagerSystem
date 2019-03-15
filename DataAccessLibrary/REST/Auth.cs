using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.REST
{
    public class Auth
    {
        static string serverAddress = "168.63.65.229";
        static string serverPort = "8081";

        public static async Task<Res> AddNewUser(User user)
        {
            HttpClient client = new HttpClient();

            // var userIn = new User{ Name ="name1", Password = "122324", Email = "m1@gmai1l.com" };

            var post = new Post { Title = "" + DateTime.Now.Ticks, Body = JsonConvert.SerializeObject(user) };

            string URL = "http://" + serverAddress + ":" + serverPort + "/api/newuser";

            //var content =  (JsonConvert.SerializeObject(post));
            var content = new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json");

            var res = await client.PostAsync(URL, content);

            string resBody = await res.Content.ReadAsStringAsync();
            Res response = JsonConvert.DeserializeObject<Res>(resBody);//JsonParse(resBody);
            Debug.Write("res " + response.Success);

            return response;
        }

        public static async Task<Res> SignIn(User user)
        {
            Res response = null;
            using (HttpClient httpClient = new HttpClient())
            {
                var post = new Post { Title = "" + DateTime.Now.Ticks, Body = JsonConvert.SerializeObject(user) };

                string URL = "http://" + serverAddress + ":" + serverPort + "/api/signin";

                var content = new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json");

                var res = await httpClient.PostAsync(URL, content);

                string resBody = await res.Content.ReadAsStringAsync();

                response = JsonConvert.DeserializeObject<Res>(resBody);//JsonParse(resBody);

                Debug.Write("res " + response.Success);

            }
                return response;
        }

        public static async Task<UserRes> GetUser(User user)
        {
            UserRes response = null;
            using (HttpClient httpClient = new HttpClient())
            {
                var post = new Post { Title = "" + DateTime.Now.Ticks, Body = JsonConvert.SerializeObject(user) };

                string URL = "http://" + serverAddress + ":" + serverPort + "/api/user";

                var content = new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json");

                var res = await httpClient.PostAsync(URL, content);

                string resBody = await res.Content.ReadAsStringAsync();

                response = JsonConvert.DeserializeObject<UserRes>(resBody);//JsonParse(resBody);

                Debug.Write("res " + response.DisplayName);

            }
            return response;
        }
    }

    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
    public class Res
    {
        public bool Success { get; set; }
        public string Msg { get; set; }
        public string Code { get; set; }
    }
    public class UserRes
    {
        public string Uid { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string PhotoURL { get; set; }
        public string ProviderId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
