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
 

    public  static class FireBaseDAO
    {
   
        public static async void AddNewTreatment(string treatmentName, Decimal price)
        {
            

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
            else
            {
                System.Diagnostics.Debug.WriteLine("Can't connect to Firebase! ");
            }

            FirebaseResponse resp2 = await client.GetTaskAsync("counter/node");

            FbCnt get = resp2.ResultAs<FbCnt>();

            System.Diagnostics.Debug.WriteLine("Counter "+ get.cnt);

             var data = new Data
             {
                 iD= (Convert.ToInt32(get.cnt) + 1).ToString(),
                 name = treatmentName,
                 price = price
             };

             string tableName = "mark" +"Treatment"+"/";
             SetResponse resp = await client.SetTaskAsync(tableName + data.iD, data);

             Data results = resp.ResultAs<Data>();

            var obj = new FbCnt
            {
                cnt = Convert.ToInt32(data.iD).ToString()
            };

            SetResponse reponse_1 = await client.SetTaskAsync("counter/node", obj);

        }


        public static async void AddNewCustomer(string id, string firstName, string surname, string DOB, string street, string city, string province, string country, string postcode, string mobileNum, string fixNum, string email, string comments)
        {

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
            else
            {
                System.Diagnostics.Debug.WriteLine("Can't connect to Firebase! ");
            }

            FirebaseResponse resp2 = await client.GetTaskAsync("counter/node");

            FbCnt get = resp2.ResultAs<FbCnt>();

            System.Diagnostics.Debug.WriteLine("Counter " + get.cnt);

            var customerData = new CustomerData
            {
                cnt = (Convert.ToInt32(get.cnt) + 1).ToString(),
                id=id,
                firstName = firstName,
                surname = surname,
                DOB= DOB,
                street= street,
                city= city,
                province= province,
                country= country,
                postcode= postcode,
                mobileNum= mobileNum,
                fixNum= fixNum,
                email= email,
                comments= comments,
            };

            string tableName = "mark" + "Treatment" + "/";
            SetResponse resp = await client.SetTaskAsync(tableName + customerData.cnt, customerData);

            Data results = resp.ResultAs<Data>();

            var obj = new FbCnt
            {
                cnt = Convert.ToInt32(customerData.cnt).ToString()
            };

            SetResponse reponse_1 = await client.SetTaskAsync("counter/node", obj);

            System.Diagnostics.Debug.WriteLine("Data Inseted! ");


        }


    }
}
