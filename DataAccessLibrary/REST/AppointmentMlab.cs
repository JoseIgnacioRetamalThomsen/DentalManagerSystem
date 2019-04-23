using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json;

namespace DataAccessLibrary.REST
{
    public class AppointmentMlab
    {
        static string serverAddress = "168.63.65.229";
        static string serverPort = "8081";

        private static string AddAppointmentURL = "/api/appointment";
        private static string WeekAppointmentsURL = "/api/appointmentweek";


        public static async Task<AppointmentM> GetAppointmentByID(string id,string email)
        {
            Debug.WriteLine("heredfasdfasdf ");
            AppointmentM appointment = null;

            HttpClient client = new HttpClient();

            //add header with user id
            client.DefaultRequestHeaders.Accept.Add
                       (new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("id", id);

            client.DefaultRequestHeaders.Accept.Add
                       (new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("email", email);


            string URL = "http://" + serverAddress + ":" + serverPort + AddAppointmentURL;

          

           

            var res = await client.GetAsync(URL);

            if (res.IsSuccessStatusCode)
            {
                Debug.WriteLine("heredfasdfasdf ");
                var contentr = await res.Content.ReadAsStringAsync();

                appointment = JsonConvert.DeserializeObject<AppointmentM>(contentr);
                Debug.WriteLine("heredfasdfasdf ");
                Debug.WriteLine(appointment.PatientID);

            }
            return appointment;
        }

        public static async Task<Res> AddNewAppointment(Appointment appointment, string email)
        {
            Debug.WriteLine("hwreeeeeeeeee " + email);
            AppointmentUserEmail app = new AppointmentUserEmail(appointment, email); 

            HttpClient client = new HttpClient();

            var post = new Post { Title = "" + DateTime.Now.Ticks, Body = JsonConvert.SerializeObject(app) };

            string URL = "http://" + serverAddress + ":" + serverPort + AddAppointmentURL;

            //var content =  (JsonConvert.SerializeObject(post));
            var content = new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json");

            var res = await client.PostAsync(URL, content);

            string resBody = await res.Content.ReadAsStringAsync();
            Res response = JsonConvert.DeserializeObject<Res>(resBody);//JsonParse(resBody);
            Debug.Write("res " + response.Success);

            return response;
        }

        public static async Task<List<AppointmentM>> GetAppointmentsWeek(DateTime startingDay,string email)
        {
            Debug.WriteLine("response");
            List<AppointmentM> aps= new List<AppointmentM>();//= new List<AppointmentM>();
            DateTime endDate = startingDay.AddDays(7);

            DateRange dr = new DateRange(startingDay, endDate,email);

            using (HttpClient client = new HttpClient())
            {

                var post = new Post { Title = "" + DateTime.Now.Ticks, Body = JsonConvert.SerializeObject(dr) };

                string URL = "http://" + serverAddress + ":" + serverPort + WeekAppointmentsURL;

                //var content =  (JsonConvert.SerializeObject(post));
                var content = new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json");

                var res = await client.PostAsync(URL, content);

                Debug.WriteLine("response");
                Debug.WriteLine(res.Content);
                if (res.IsSuccessStatusCode)
                {

                    var contentre = await res.Content.ReadAsStringAsync();

                    Debug.WriteLine("response2");
                    Debug.WriteLine(contentre);


                    aps = JsonConvert.DeserializeObject<List<AppointmentM>>(contentre); ;
                }
                Debug.WriteLine("count " +aps.Count);
            }
            return aps;
        }

        public static async Task<Res> UpdateAppointment(AppointmentM appointment,string email)
        {
            HttpClient client = new HttpClient();

            

            client.DefaultRequestHeaders.Accept.Add
                     (new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("email", email);

            var post = new Put { Title = "New Post", Body = JsonConvert.SerializeObject(appointment) };

            string URL = "http://" + serverAddress + ":" + serverPort + AddAppointmentURL;

            var content = new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json");

            var res = await client.PutAsync(URL, content);

            string resBody = await res.Content.ReadAsStringAsync();
            Res response = JsonConvert.DeserializeObject<Res>(resBody);//JsonParse(resBody);

            return response;
            
        }

    }

    public class Put
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
