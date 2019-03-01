using System;
using System.Collections.Generic;
using System.Text;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.Data.Sqlite;
using Models;

namespace DataAccessLibrary
{

    public  class FireBaseDAO
    {
      
        public static async void AddNewTreatment(string treatmentName, Decimal price)
        {
            System.Diagnostics.Debug.WriteLine("Hello ");

            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "gOjxFlBGP1v8vufauNkO9VqnH5PiEwltVATNdUey",
                BasePath = "https://unlockpincode-d448d.firebaseio.com/"
            };

            FirebaseClient client;

            client = new FireSharp.FirebaseClient(config);

            if (client != null)
            {
                System.Diagnostics.Debug.WriteLine("Connection Establish! ");
            }

            var data = new Data
            {
                name = treatmentName,
                price = price
            };

            SetResponse resp = await client.SetTaskAsync("Treatment/" + treatmentName, data);

            Data results = resp.ResultAs<Data>();

            System.Diagnostics.Debug.WriteLine("Data Inseted! ");

            using (SqliteConnection db =
                new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query
                insertCommand.CommandText = "INSERT INTO treatment (treatmentName,price) VALUES (@TreatmentName ,@Price);";
                insertCommand.Parameters.AddWithValue("@TreatmentName", treatmentName);
                insertCommand.Parameters.AddWithValue("@Price", price);

                insertCommand.ExecuteReader();

                db.Close();
            }
        }
    }
}
