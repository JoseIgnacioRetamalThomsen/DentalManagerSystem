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


    public static class FireBaseDAO
    {

        public static async void AddNewTreatment(Treatment treatment)
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

           FirebaseResponse resp2 = await client.GetTaskAsync("Counter/node");

             FbCnt get = resp2.ResultAs<FbCnt>();

            //System.Diagnostics.Debug.WriteLine("Counter " + get.cnt);
           

             TreatmentData treatmentData = new TreatmentData
             {
                 //FBCnt = (Convert.ToInt32(get.cnt) + 1).ToString(),
                 iD = treatment.iD.ToString(),
                 name = treatment.name,
                 price = treatment.price
             };


             string tableName = "mark" + "Treatments" + "/";
             SetResponse resp = await client.SetTaskAsync(tableName + treatment.iD, treatmentData);

             TreatmentData results = resp.ResultAs<TreatmentData>();

             var obj = new FbCnt
             {
                 cnt = (Convert.ToInt32(get.cnt) + 1).ToString()
             };

             SetResponse reponse_1 = await client.SetTaskAsync("Counter/node", obj);

        }


        public static async void UpdateTreatment(Treatment treatment)
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

            



            TreatmentData treatmentData = new TreatmentData
            {
                // FBCnt = (Convert.ToInt32(get.cnt) + 1).ToString(),
                iD = treatment.iD,
                name = treatment.name,
                price = treatment.price
            };


            string tableName = "mark" + "Treatments" + "/";

            FirebaseResponse resp = await client.UpdateTaskAsync(tableName + treatment.iD, treatmentData);
      

        }


        public static async void ReadTreatment()
        {


            IFirebaseConfig config = new FirebaseConfig

            {
                AuthSecret = "gOjxFlBGP1v8vufauNkO9VqnH5PiEwltVATNdUey",
                BasePath = "https://unlockpincode-d448d.firebaseio.com/"
            };

            FirebaseClient client;

            client = new FireSharp.FirebaseClient(config);

            string tableName = "mark" + "Treatments" + "/";

      


            if (client != null)
            {
                System.Diagnostics.Debug.WriteLine("Connection Establish! ");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Can't connect to Firebase! ");
            }


            int i = 0;
            FirebaseResponse resp = await client.GetTaskAsync("Counter/node");

            FbCnt obj = resp.ResultAs<FbCnt>();

            int cnt = Convert.ToInt32(obj.cnt);

            System.Diagnostics.Debug.WriteLine("Counter " + obj.cnt);

            while (true)
            {
                if (i == cnt)
                {
                    break;
                }
                i++;

                try
                {
                    FirebaseResponse response = await client.GetTaskAsync(tableName+i);
                    TreatmentData deatils = response.ResultAs<TreatmentData>();
                    System.Diagnostics.Debug.WriteLine("Counter " + deatils.iD);
                    System.Diagnostics.Debug.WriteLine("Counter " + deatils.name);
                    System.Diagnostics.Debug.WriteLine("Counter " + deatils.price);

                    System.Diagnostics.Debug.WriteLine("Nice"+i);
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("Hymmm ");
                }
            }

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
                id = id,
                firstName = firstName,
                surname = surname,
                DOB = DOB,
                street = street,
                city = city,
                province = province,
                country = country,
                postcode = postcode,
                mobileNum = mobileNum,
                fixNum = fixNum,
                email = email,
                comments = comments,
            };

            string tableName = "mark" + "Customers" + "/";
            SetResponse resp = await client.SetTaskAsync(tableName + customerData.cnt, customerData);

            CustomerData results = resp.ResultAs<CustomerData>();

            var obj = new FbCnt
            {
                cnt = Convert.ToInt32(customerData.cnt).ToString()
            };

            SetResponse reponse_1 = await client.SetTaskAsync("counter/node", obj);

            System.Diagnostics.Debug.WriteLine("Data Inseted! ");


        }


    }
}
