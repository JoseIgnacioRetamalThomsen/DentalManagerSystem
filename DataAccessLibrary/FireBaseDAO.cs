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

            //Create a new row  with the updated values
            //await firebase.Child(node).Child(treatment.iD.ToString).PostAsync<TreatmentData>(treatmentData);

            await firebase.Child(node).PostAsync<TreatmentData>(treatmentData);
        }

             FbCnt get = resp2.ResultAs<FbCnt>();

            //System.Diagnostics.Debug.WriteLine("Counter " + get.cnt);


             TreatmentData treatmentData = new TreatmentData
             {
                 //FBCnt = (Convert.ToInt32(get.cnt) + 1).ToString(),
                 iD = treatment.iD.ToString(),
                 name = treatment.name,
                 price = treatment.price
             };

            var results = await firebase.Child(node).OnceAsync<TreatmentData>();
            foreach (var details in results)
            {

                if (Convert.ToInt32(details.Object.iD) == treatment.iD)
                {
                    //Delete the old row by key Id
                     await firebase.Child(node).Child(details.Key).DeleteAsync();
                    break;
                }
            }

          //Create a new row  with the updated values
          await firebase.Child(node).PostAsync<TreatmentData>(treatmentData);
        }

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

            var results = await firebase.Child(node).OnceAsync<CustomerData>();
            foreach (var details in results)
            {
                if (id == details.Object.id)
                {
                    FoundiD = true;
                    break;
                }


            if (client != null)
            {
                System.Diagnostics.Debug.WriteLine("Connection Establish! ");
            }
            else
            {
                await firebase.Child(node).PostAsync<CustomerData>(customerData);
            }





            TreatmentData treatmentData = new TreatmentData
            {
                // FBCnt = (Convert.ToInt32(get.cnt) + 1).ToString(),
                iD = treatment.iD,
                name = treatment.name,
                price = treatment.price
            };

            var results = await firebase.Child(node).OnceAsync<CustomerData>();
            foreach (var details in results)
            {

                if (customerID == details.Object.id)
                {
                    //Delete the old row by key id
                    await firebase.Child(node).Child(details.Key).DeleteAsync();
                    break;
                }
            }

               //Add the new customer row
               await firebase.Child(node).PostAsync<CustomerData>(customerData);
        }


        public static async void ReadTreatment()
        {


            IFirebaseConfig config = new FirebaseConfig

            {
                AuthSecret = "gOjxFlBGP1v8vufauNkO9VqnH5PiEwltVATNdUey",
                BasePath = "https://unlockpincode-d448d.firebaseio.com/"
            };

            await firebase.Child(node).PostAsync<TreatmentPlanData>(treatmentPlanData);
        }

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

            await firebase.Child(node).PostAsync<TreatmentPlanTreatmentsData>(treatmentPlanTreatmentsData);
        }




        public static async void AddNewCustomer(string id, string firstName, string surname, string DOB, string street, string city, string province, string country, string postcode, string mobileNum, string fixNum, string email, string comments)
        {

            IFirebaseConfig config = new FirebaseConfig

            {
                AuthSecret = "gOjxFlBGP1v8vufauNkO9VqnH5PiEwltVATNdUey",
                BasePath = "https://unlockpincode-d448d.firebaseio.com/"
            };

            var results = await firebase.Child(node).OnceAsync<TreatmentPlanTreatmentsData>();
            foreach (var details in results)
            {

                if (t.TreatmentPlanTreatmentsID.ToString() == details.Object.treatmentPlanTreatmentsID)
                {
                    //Delete the old row by key id
                    await firebase.Child(node).Child(details.Key).DeleteAsync();
                    break;
                }
            }

            //add the new row
            await firebase.Child(node).PostAsync<TreatmentPlanTreatmentsData>(treatmentPlanTreatmentsData);
        }

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

        /// <summary>
        /// Read from Firebase, delete everything in all 5 tables and repopulate
        /// them with the information gotten from Firebase.
        /// </summary>
        public async void ReadDataFromFirebase()
        {
            //Open connection with Firebase
            ConnectToFirebase();

            String TreatmentNode = "marko" + "Treatments" + "/";
            String CustomerNode = "marko" + "Customers" + "/";
            String TreatmentPlanNode = "marko" + "TreatmentPlans" + "/";
            String TreatmentPlanTreatmentNode = "marko" + "TreatmentPlanTreatments" + "/";
            String PaymentNode = "marko" + "Payments" + "/";

            //Establish SQLite connection and populate treatment table
            using (SqliteConnection db =
                            new SqliteConnection("Filename=dentalManagerDB.db"))
                        {
                            //open sqlite Connection
                            db.Open();
                            SqliteCommand selectCommand = new SqliteCommand
                             //Delete everything in the treatment table
                                ("DELETE from treatment", db);
                            SqliteDataReader query = selectCommand.ExecuteReader();

                            //Read from Firebase and populate the local datbase/SQLite treatment table
                            var results = await firebase.Child(TreatmentNode).OnceAsync<TreatmentData>();
                            foreach (var details in results)
                            {
                                SqliteCommand insertCommand = new SqliteCommand();
                                insertCommand.Connection = db;

                                // Use parameterized query
                                insertCommand.CommandText = "INSERT INTO treatment (treatmentName,price) VALUES (@TreatmentName ,@Price);";
                                insertCommand.Parameters.AddWithValue("@TreatmentName", details.Object.name);
                                insertCommand.Parameters.AddWithValue("@Price", details.Object.price);

                                insertCommand.ExecuteReader();
                            }

                            db.Close();
                        }


            //Establish SQLite connection and populate customers table
            using (SqliteConnection db =
                new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                //open sqlite Connection
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand
                    //Delete everything in the customers table
                    ("DELETE from customers", db);
                SqliteDataReader query = selectCommand.ExecuteReader();

                //Read from Firebase and populate the local datbase/SQLite customers table

                var rlts = await firebase.Child(CustomerNode).OnceAsync<CustomerData>();
                foreach (var details in rlts)
                {
                    SqliteCommand insertCommand = new SqliteCommand();

                    insertCommand.Connection = db;

                    // Use parameterized query
                    insertCommand.CommandText = "INSERT INTO customers VALUES (@Id ,@FirstName ,@Surname,@DOB ,@Street ,@city ,@Province ,@Country ,@Postocode ,@MobileNum ,@FixNum ,@Email,@Comments);";
                    insertCommand.Parameters.AddWithValue("@Id", details.Object.id);
                    insertCommand.Parameters.AddWithValue("@FirstName", details.Object.firstName);
                    insertCommand.Parameters.AddWithValue("@Surname", details.Object.surname);
                    insertCommand.Parameters.AddWithValue("@DOB", details.Object.DOB);
                    insertCommand.Parameters.AddWithValue("@Street", details.Object.street);
                    insertCommand.Parameters.AddWithValue("@city", details.Object.city);
                    insertCommand.Parameters.AddWithValue("@Province", details.Object.province);
                    insertCommand.Parameters.AddWithValue("@Country", details.Object.country);
                    insertCommand.Parameters.AddWithValue("@Postocode", details.Object.postcode);
                    insertCommand.Parameters.AddWithValue("@MobileNum", details.Object.mobileNum);
                    insertCommand.Parameters.AddWithValue("@FixNum", details.Object.fixNum);
                    insertCommand.Parameters.AddWithValue("@Email", details.Object.email);
                    insertCommand.Parameters.AddWithValue("@Comments", details.Object.comments);

                    insertCommand.ExecuteReader();
                }

                db.Close();
            }

            //Establish SQLite connection and populate Treatment Plan table
            using (SqliteConnection db =
                            new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                //open sqlite Connection
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand
                    //Delete everything in the Treatment Plan table
                    ("DELETE from treatmentPlan", db);
                SqliteDataReader query = selectCommand.ExecuteReader();

                //Read from Firebase and populate the local datbase/SQLite treatmentPlan table
                var results = await firebase.Child(TreatmentPlanNode).OnceAsync<TreatmentPlanData>();
                foreach (var details in results)
                {

                    SqliteCommand insertCommand = new SqliteCommand();
                    insertCommand.Connection = db;

                    // Use parameterized query
                    insertCommand.CommandText = "INSERT INTO treatmentPlan (customerID,state,creationDate,treatmentPlanCompleteDate) VALUES (@CustomerID,@State,@CreationDate,@TreatmentPlanCompleteDate);";
                    insertCommand.Parameters.AddWithValue("@CustomerID", details.Object.customerID);
                    insertCommand.Parameters.AddWithValue("@State", details.Object.state);
                    insertCommand.Parameters.AddWithValue("@CreationDate", details.Object.creationDate);
                    insertCommand.Parameters.AddWithValue("@TreatmentPlanCompleteDate", details.Object.treatmentPlanCompleteDate);

                    insertCommand.ExecuteReader();
                }

                db.Close();
            }

            //Establish SQLite connection and populate treatment plan treatment table
            using (SqliteConnection db =
                            new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                //open sqlite Connection
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand
                    //Delete everything in the treatment plan treatment table
                    ("DELETE from treatmentPlanTreatments", db);
                SqliteDataReader query = selectCommand.ExecuteReader();

                //Read from Firebase and populate the local datbase/SQLite treatmentPlanTreatments table
                var results = await firebase.Child(TreatmentPlanTreatmentNode).OnceAsync<TreatmentPlanTreatmentsData>();
                foreach (var details in results)
                {
                    SqliteCommand insertCommand = new SqliteCommand();
                    insertCommand.Connection = db;

                    // Use parameterized query
                    insertCommand.CommandText = "INSERT INTO treatmentPlanTreatments (TreatmentPlanID,TreatmentID,Price,TreatmentCompleteDate) VALUES (@TreatmentPlanID,@TreatmentID,@Price,@TreatmentCompleteDate);";

                    insertCommand.Parameters.AddWithValue("@TreatmentPlanID", details.Object.treatmentPlanID);
                    insertCommand.Parameters.AddWithValue("@TreatmentID", details.Object.treatmentID);
                    insertCommand.Parameters.AddWithValue("@Price", details.Object.price);
                    insertCommand.Parameters.AddWithValue("@TreatmentCompleteDate", details.Object.treatmentCompleteDate);

                    insertCommand.ExecuteReader();

                }

                db.Close();
            }


            //Establish SQLite connection and populate payment table
            using (SqliteConnection db =
                            new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                //open sqlite Connection
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand
                    //Delete everything in the payment table
                    ("DELETE from payments", db);
                SqliteDataReader query = selectCommand.ExecuteReader();

                //Read from Firebase and populate the local datbase/SQLite payments table
                var results = await firebase.Child(PaymentNode).OnceAsync<PaymentsData>();
                foreach (var details in results)
                {

                    SqliteCommand insertCommand = new SqliteCommand();
                    insertCommand.Connection = db;

                    // Use parameterized query
                    insertCommand.CommandText = "INSERT INTO payments (treatmentPlanID,customerID,amount,treatmentCompleteDate) VALUES (@TreatmentPlanID,@CustomerID,@Amount,@TreatmentCompleteDate);";
                    insertCommand.Parameters.AddWithValue("@TreatmentPlanID", details.Object.treatmentPlanID);
                    insertCommand.Parameters.AddWithValue("@CustomerID", details.Object.customerID);
                    insertCommand.Parameters.AddWithValue("@Amount", details.Object.amount);
                    insertCommand.Parameters.AddWithValue("@TreatmentCompleteDate", details.Object.treatmentCompleteDate);

                    insertCommand.ExecuteReader();

                }

                db.Close();
            }

            // await firebase.Child("markTreatments/").DeleteAsync();

        }
    }
}
