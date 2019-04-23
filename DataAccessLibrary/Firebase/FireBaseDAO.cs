using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Data.Sqlite;
using Models;

namespace DataAccessLibrary
{


    public class FireBaseDAO
    {

        /// <summary>
        /// Firebase authetication parameters
        /// </summary>
        private const String databaseUrl = "https://dentalmanagersystem.firebaseio.com/";
        private const String databaseSecret = "f8YxsUaD2hAKXvywrf3XBJAMuIsjopDVba1v78np";
        private FirebaseClient firebase;


        /// <summary>
        /// Establish connection with Firebase
        /// </summary>
        public void ConnectToFirebase()
        {
            try
            {
                this.firebase = new FirebaseClient(

               databaseUrl,
               new FirebaseOptions
               {
                   AuthTokenAsyncFactory = () => Task.FromResult(databaseSecret)
               });
            }
            catch
            {
                Debug.WriteLine("Can't Conect to Firebase, check your Internet connection!");
            }
          
        }

        /// <summary>
        /// Create a counter record in Firebase. Is created once per user  when they create an accoount.
        /// </summary>
        /// <param name="counterNum"></param>
        public async void CreateCountRecordFB1(int counterNum)
        {

            ConnectToFirebase();

            string userName = DAO.GetUserID();
            String myUsername = userName;
            myUsername = myUsername.Replace(".", "-");
            String node = "CounterRecord" + "/";

            FbCnt fbCnt = new FbCnt
            {
              cnt= counterNum,
              username= myUsername,
            };

           await firebase.Child(node).PostAsync<FbCnt>(fbCnt);
        }

        /// <summary>
        /// Update Firebase count
        /// </summary>
        /// <param name="counterNum"></param>
        public async void UpdateCountRecordFB(int counterNum)
        {
            ConnectToFirebase();

            try
            {
                string userName = DAO.GetUserID();
                String myUsername = userName;
                myUsername = myUsername.Replace(".", "-");
                String node = "CounterRecord" + "/";

                FbCnt fbCnt = new FbCnt
                {
                    cnt = counterNum,
                    username = myUsername,
                };

                var results = await firebase.Child(node).OnceAsync<FbCnt>();
                foreach (var details in results)
                {
                    if (details.Object.username == myUsername)
                    {
                        //Delete the old row by key Id
                        await firebase.Child(node).Child(details.Key).DeleteAsync();
                    }
                }

                //Add a new row.
                await firebase.Child(node).PostAsync<FbCnt>(fbCnt);
            }
            catch
            {
                Debug.WriteLine("No Internet Connection");
            }
            
           
        }

        public async Task<int> GetUserCountFb(string email)
        {
            ConnectToFirebase();

            //string userName = DAO.GetUserID();
            String myUsername = email;
            myUsername = myUsername.Replace(".", "-");
            String node = "CounterRecord" + "/";
            int cntNum=0;

            FbCnt fbCnt = new FbCnt
            {
                username = myUsername,
            };

            var results = await firebase.Child(node).OnceAsync<FbCnt>();
            foreach (var details in results)
            {

                if (details.Object.username == myUsername)
                {
                    cntNum = details.Object.cnt;
                    break;
                }
            }

            return cntNum;
        }

        /// <summary>
        /// Add a new treatment to Firebae
        /// </summary>
        /// <param name="treatmentName"></param>
        /// <param name="price"></param>
        public async void AddNewTreatment(Treatment treatment)
        {

            ConnectToFirebase();

            string userName = DAO.GetUserID();
            String myUsername = userName;
            myUsername = myUsername.Replace(".", "-");
            String node = myUsername + "Treatments" + "/";

            TreatmentData treatmentData = new TreatmentData
            {
                iD = treatment.ID,
                name = treatment.Name,
                price = treatment.Price
            };

            await firebase.Child(node).PostAsync<TreatmentData>(treatmentData);

        }

        /// <summary>
        /// Update the Treatment  table by row id
        /// </summary>
        /// <param name="treatment"></param>
        public async void UpdateTreatment(Treatment treatment)
        {
            ConnectToFirebase();

            string userName = DAO.GetUserID();
            String myUsername = userName;
            myUsername = myUsername.Replace(".", "-");
            String node = myUsername + "Treatments" + "/";

            TreatmentData treatmentData = new TreatmentData
            {
                iD = treatment.ID,
                name = treatment.Name,
                price = treatment.Price
            };

            var results = await firebase.Child(node).OnceAsync<TreatmentData>();
            foreach (var details in results)
            {
                if (Convert.ToInt32(details.Object.iD) == treatment.ID)
                {
                    //Delete the old row by key Id
                    await firebase.Child(node).Child(details.Key).DeleteAsync();
                   //Create a new row  with the updated values
                   await firebase.Child(node).PostAsync<TreatmentData>(treatmentData);
                    break;
                }
               
            }
            
        }

        /// <summary>
        /// Delet Treatment from Firebase
        /// </summary>
        public async void DeleteTreatment()
        {
            try
            {
                ConnectToFirebase();
                string userName = DAO.GetUserID();
                String myUsername = userName;
                myUsername = myUsername.Replace(".", "-");
                String node = myUsername + "Treatments" + "/";

                await firebase.Child(node).DeleteAsync();
            }
            catch
            {
                Debug.WriteLine("Treatment not found!");
            }
        }

        /// <summary>
        /// Add a new customer to Firebase
        /// </summary>
        /// <param name="id"></param>
        /// <param name="firstName"></param>
        /// <param name="surname"></param>
        /// <param name="DOB"></param>
        /// <param name="street"></param>
        /// <param name="city"></param>
        /// <param name="province"></param>
        /// <param name="country"></param>
        /// <param name="postcode"></param>
        /// <param name="mobileNum"></param>
        /// <param name="fixNum"></param>
        /// <param name="email"></param>
        /// <param name="comments"></param>
        public async void AddNewCustomer(string id, string firstName, string surname, string DOB, string street, string city, string province, string country, string postcode, string mobileNum, string fixNum, string email, string comments)
        {

            bool FoundiD = false;
            ConnectToFirebase();

            string userName = DAO.GetUserID();
            String myUsername = userName;
            myUsername = myUsername.Replace(".", "-");
            String node = myUsername + "Customers" + "/";
            
            var customerData = new CustomerData
            {
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

            var results = await firebase.Child(node).OnceAsync<CustomerData>();
            foreach (var details in results)
            {
                if (id == details.Object.id)
                {
                    FoundiD = true;
                    break;
                }

            }

            if (FoundiD == true)
            {
                Debug.WriteLine("Id Already in the system");
            }
            else
            {
                await firebase.Child(node).PostAsync<CustomerData>(customerData);
            }

        }

        /// <summary>
        /// Update the customer row in Firebase
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="firstName"></param>
        /// <param name="surname"></param>
        /// <param name="DOB"></param>
        /// <param name="street"></param>
        /// <param name="city"></param>
        /// <param name="province"></param>
        /// <param name="country"></param>
        /// <param name="postcode"></param>
        /// <param name="mobileNum"></param>
        /// <param name="fixNum"></param>
        /// <param name="email"></param>
        /// <param name="comments"></param>
        public async void UpdateCustomer(string customerID, string firstName, string surname, string DOB, string street, string city, string province, string country, string postcode, string mobileNum, string fixNum, string email, string comments)
        {

            ConnectToFirebase();

            string userName = DAO.GetUserID();
            String myUsername = userName;
            myUsername = myUsername.Replace(".", "-");
            String node = myUsername + "Customers" + "/";
            var customerData = new CustomerData
            {
                id = customerID,
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

            var results = await firebase.Child(node).OnceAsync<CustomerData>();
            foreach (var details in results)
            {

                if (customerID == details.Object.id)
                {
                    //Delete the old row by key id
                    await firebase.Child(node).Child(details.Key).DeleteAsync();
                    //Add the new customer row
                    await firebase.Child(node).PostAsync<CustomerData>(customerData);
                    break;
                }
            }

        }

        /// <summary>
        /// Delet Customer
        /// </summary>
        public async void DeleteCustomer()
        {
            try
            {
                ConnectToFirebase();

                string userName = DAO.GetUserID();
                String myUsername = userName;
                myUsername = myUsername.Replace(".", "-");
                String node = myUsername + "Customers" + "/";

                await firebase.Child(node).DeleteAsync();
            }
            catch
            {
                Debug.WriteLine("Customers not found!");
            }
 
        }

        /// <summary>
        /// Add a new treatment plan to Firebase
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="state"></param>
        /// <param name="creationDate"></param>
        /// <param name="treatmentPlanCompleteDate"></param>
        public async void AddNewTreatmentPlan(int treatmentPlanID, string customerID, int state, string creationDate, string treatmentPlanCompleteDate)
        {
            ConnectToFirebase();

            string userName = DAO.GetUserID();
            String myUsername = userName;
            myUsername = myUsername.Replace(".", "-");
            String node = myUsername + "TreatmentPlans" + "/";
            var treatmentPlanData = new TreatmentPlanData
            {
                treatmentPlanID = treatmentPlanID,
                customerID = customerID,
                state = state,
                creationDate = creationDate,
                treatmentPlanCompleteDate = treatmentPlanCompleteDate
            };

            await firebase.Child(node).PostAsync<TreatmentPlanData>(treatmentPlanData);
        }


        /// <summary>
        ///  Write to Firebase when reading from SQLIte.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="iD"></param>
        public async void AddNewTreatmentPlanState(int treatmentPlanID, string customerID, TreatmentPlaneState state, string creationDate, string treatmentPlanCompleteDate)
        {
            ConnectToFirebase();

            int realState=0;
            if (state.ToString() == "Created")
            {
                realState = 0;
            }
            if (state.ToString()== "Accepted")
            {
                realState = 1;
            }
            if(state.ToString()== "Finish")
            {
                realState = 2;
            }

            string userName = DAO.GetUserID();
            String myUsername = userName;
            myUsername = myUsername.Replace(".", "-");
            String node = myUsername + "TreatmentPlans" + "/";
            var treatmentPlanData = new TreatmentPlanData
            {
                treatmentPlanID = treatmentPlanID,
                customerID = customerID,
                state = realState,
                creationDate = creationDate,
                treatmentPlanCompleteDate = treatmentPlanCompleteDate
            };

            await firebase.Child(node).PostAsync<TreatmentPlanData>(treatmentPlanData);
        }

        /// <summary>
        /// Delete Treatmentplan table in Firebase
        /// </summary>
        public async void DeletTreatmentPlanState()
        {
            try
            {
                ConnectToFirebase();

                string userName = DAO.GetUserID();
                String myUsername = userName;
                myUsername = myUsername.Replace(".", "-");
                String node = myUsername + "TreatmentPlans" + "/";

                await firebase.Child(node).DeleteAsync();
            }
            catch
            {
                Debug.WriteLine("TreatmentPlans not found!");
            }
            
        }

            /// <summary>
            /// Update Treatmentplan table in Firebase if state changes.
            /// </summary>
            /// <param name="state"></param>
            /// <param name="iD"></param>
            public async void UpdateTreatmentPlanState(TreatmentPlaneState state, int iD)
        {
            ConnectToFirebase();

            int realState = 0;
            if (state.ToString() == "Created")
            {
                realState = 0;
            }
            if (state.ToString() == "Accepted")
            {
                realState = 1;
            }
            if (state.ToString() == "Finish")
            {
                realState = 2;
            }


            int treatmentPlanID = 0;
            string customerID = "";
            string creationDate = "";
            string treatmentPlanCompleteDate = "";

            string userName = DAO.GetUserID();
            String myUsername = userName;
            myUsername = myUsername.Replace(".", "-");
            String node = myUsername + "TreatmentPlans" + "/";
            var treatmentPlanData = new TreatmentPlanData
            {
                treatmentPlanID = iD,
            };

            var results = await firebase.Child(node).OnceAsync<TreatmentPlanData>();
            foreach (var details in results)
            {


                if (iD == details.Object.treatmentPlanID)
                {
                    treatmentPlanID = details.Object.treatmentPlanID;
                    customerID = details.Object.customerID;
                    creationDate = details.Object.creationDate;
                    treatmentPlanCompleteDate = details.Object.treatmentPlanCompleteDate;
                    await firebase.Child(node).Child(details.Key).DeleteAsync();
                    AddNewTreatmentPlan(treatmentPlanID, customerID, realState, creationDate, treatmentPlanCompleteDate);
                    break;
                }
            }

        }

        /// <summary>
        /// Add a new TreatmentPlanTreatments to Firebase
        /// </summary>
        /// <param name="treatmentPlanID"></param>
        /// <param name="treatmentID"></param>
        /// <param name="price"></param>
        /// <param name="treatmentCompleteDate"></param>
        public async void AddNewTreatmentPlanTreatments(TreatmentOnPlan t, int id)
        {
            ConnectToFirebase();

            string userName = DAO.GetUserID();
            String myUsername = userName;
            myUsername = myUsername.Replace(".", "-");
            String node = myUsername + "TreatmentPlanTreatments" + "/";

            var treatmentPlanTreatmentsData = new TreatmentPlanTreatmentsData
            {
                treatmentPlanTreatmentsID = id.ToString(),
                treatmentPlanID = t.TreatmentPlanID,
                treatmentID = t.TreatmentID,
                price = t.Price,
                treatmentCompleteDate = t.CompletedDate.ToString(),
                tooth =t.Tooth,
                comment =t.Comment,
                isdone=t.IsDone

            };

            await firebase.Child(node).PostAsync<TreatmentPlanTreatmentsData>(treatmentPlanTreatmentsData);
        }

        /// <summary>
        /// Update the TreatmentOnPlan in the Firebase
        /// </summary>
        /// <param name="t"></param>
        public async void UpdateTreatmentOnPlan(TreatmentOnPlan t)
        {
            ConnectToFirebase();

            string userName = DAO.GetUserID();
            String myUsername = userName;
            myUsername = myUsername.Replace(".", "-");
            String node = myUsername + "TreatmentPlanTreatments" + "/";

            var treatmentPlanTreatmentsData = new TreatmentPlanTreatmentsData
            {
                treatmentPlanTreatmentsID = t.TreatmentPlanTreatmentsID.ToString(),
                treatmentPlanID = t.TreatmentPlanID,
                treatmentID = t.TreatmentID,
                price = t.Price,
                treatmentCompleteDate = t.CompletedDate.ToString(),
                tooth = t.Tooth,
                comment = t.Comment,
                isdone = t.IsDone
            };

            var results = await firebase.Child(node).OnceAsync<TreatmentPlanTreatmentsData>();
            foreach (var details in results)
            {
                if (t.TreatmentPlanTreatmentsID == details.Object.treatmentPlanID)
                {
                    //Delete the old row by key id
                    await firebase.Child(node).Child(details.Key).DeleteAsync();
                    //add the new row
                    await firebase.Child(node).PostAsync<TreatmentPlanTreatmentsData>(treatmentPlanTreatmentsData);
                    break;
                }
            }

        }

        /// <summary>
        /// Delete Treatmentplan table in Firebase
        /// </summary>
        public async void DeleteTreatmentPlanTreatments()
        {
            try
            {
                ConnectToFirebase();

                string userName = DAO.GetUserID();
                String myUsername = userName;
                myUsername = myUsername.Replace(".", "-");
                String node = myUsername + "TreatmentPlanTreatments" + "/";

                await firebase.Child(node).DeleteAsync();
            }
            catch
            {
                Debug.WriteLine("TreatmentPlanTreatments not found!");
            }
          
        }

        /// <summary>
        ///Add new Payment to Firebase
        /// </summary>
        /// <param name="treatmentPlanID"></param>
        /// <param name="customerID"></param>
        /// <param name="amount"></param>
        /// <param name="treatmentCompleteDate"></param>
        public async void AddNewpayment(int paymentID,int treatmentPlanID, string customerID, decimal amount, string treatmentCompleteDate)
        {
            ConnectToFirebase();

            string userName = DAO.GetUserID();
            String myUsername = userName;
            myUsername = myUsername.Replace(".", "-");
            String node = myUsername + "Payments" + "/";

             var paymentsData = new PaymentsData
             {
                 paymentID = paymentID,
                 treatmentPlanID = treatmentPlanID,
                 customerID = customerID,
                 amount = amount,
                 treatmentCompleteDate = treatmentCompleteDate
             };

             await firebase.Child(node).PostAsync<PaymentsData>(paymentsData);
        }

        /// <summary>
        ///Add new Payment to Firebase pass by object
        /// </summary>
        /// <param name="treatmentPlanID"></param>
        /// <param name="customerID"></param>
        /// <param name="amount"></param>
        /// <param name="treatmentCompleteDate"></param>
        public async void AddNewpaymentObject(Payments p)
        {
            ConnectToFirebase();

            string userName = DAO.GetUserID();
            String myUsername = userName;
            myUsername = myUsername.Replace(".", "-");
            String node = myUsername + "Payments" + "/";

            var paymentsData = new PaymentsData
            {
                paymentID = p.paymentsID,
                treatmentPlanID = p.treatmentPlanID,
                customerID = p.customerID,
                amount = Convert.ToDecimal(p.amount),
                treatmentCompleteDate = p.completedDate.ToString()
            };

            await firebase.Child(node).PostAsync<PaymentsData>(paymentsData);
        }

        /// <summary>
        /// Update payment table in Firebase.
        /// </summary>
        /// <param name="p"></param>
        public async void UpdatePaymentFB(Payments p)
        {
            ConnectToFirebase();

            string userName = DAO.GetUserID();
            String myUsername = userName;
            myUsername = myUsername.Replace(".", "-");
            String node = myUsername + "Payments" + "/";

            var paymentsData = new PaymentsData
            {
                paymentID = p.paymentsID,
                treatmentPlanID = p.treatmentPlanID,
                customerID = p.customerID,
                amount = Convert.ToDecimal(p.amount),
                treatmentCompleteDate = p.completedDate.ToString()
            };
            
            var results = await firebase.Child(node).OnceAsync<PaymentsData>();
            foreach (var details in results)
            {
                if (p.paymentsID == details.Object.paymentID)
                {
                    await firebase.Child(node).Child(details.Key).DeleteAsync();
                    //add the new row
                    await firebase.Child(node).PostAsync<PaymentsData>(paymentsData);
                    break;
                }
            }

        }

        /// <summary>
        /// Delete Payments table in Firebase
        /// </summary>
        public async void DeletePayments()
        {
            try
            {
                ConnectToFirebase();

                string userName = DAO.GetUserID();
                String myUsername = userName;
                myUsername = myUsername.Replace(".", "-");
                String node = myUsername + "Payments" + "/";

                await firebase.Child(node).DeleteAsync();
            }
            catch
            {
                Debug.WriteLine("Payments not found!");
            }

        }

        /// <summary>
        /// Read from Firebase, delete everything in all 5 tables and repopulate 
        /// them with the information gotten from Firebase.
        /// </summary>
        public async void ReadDataFromFirebase()
        {
            //Open connection with Firebase
            ConnectToFirebase();

            string userName = DAO.GetUserID();
            String myUsername = userName;
            myUsername = myUsername.Replace(".", "-");

            String TreatmentNode = myUsername + "Treatments" + "/";
            String CustomerNode = myUsername + "Customers" + "/";
            String TreatmentPlanNode = myUsername + "TreatmentPlans" + "/";
            String TreatmentPlanTreatmentNode = myUsername + "TreatmentPlanTreatments" + "/";
            String PaymentNode = myUsername + "Payments" + "/";
            String CountRecordNode = "CounterRecord" + "/";

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

                   try
                   {
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

                }
                    catch
                    {
                    Debug.WriteLine("Nothing found in Firebase");
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

                try
                {
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
                }
                catch
                {
                    Debug.WriteLine("Nothing found in customers in Firebase");
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

                try
                {
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

                }
                catch
                {
                    Debug.WriteLine("Nothing Found IN TreatmentPlan in Firebase");
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
                try
                {

                    //Read from Firebase and populate the local datbase/SQLite treatmentPlanTreatments table
                    var results = await firebase.Child(TreatmentPlanTreatmentNode).OnceAsync<TreatmentPlanTreatmentsData>();
                    foreach (var details in results)
                    {
                        SqliteCommand insertCommand = new SqliteCommand();
                        insertCommand.Connection = db;

                        // Use parameterized query
                        insertCommand.CommandText = "INSERT INTO treatmentPlanTreatments (TreatmentPlanID,TreatmentID,Price,TreatmentCompleteDate,tooth,comment,isdone) VALUES (@TreatmentPlanID,@TreatmentID,@Price,@TreatmentCompleteDate,@Tooth,@Comment,@Isdone);";

                        insertCommand.Parameters.AddWithValue("@TreatmentPlanID", details.Object.treatmentPlanID);
                        insertCommand.Parameters.AddWithValue("@TreatmentID", details.Object.treatmentID);
                        insertCommand.Parameters.AddWithValue("@Price", details.Object.price);
                        insertCommand.Parameters.AddWithValue("@TreatmentCompleteDate", details.Object.treatmentCompleteDate);
                        insertCommand.Parameters.AddWithValue("@Tooth", details.Object.tooth);
                        insertCommand.Parameters.AddWithValue("@Comment", details.Object.comment);
                        insertCommand.Parameters.AddWithValue("@Isdone", details.Object.isdone);

                        insertCommand.ExecuteReader();

                    }
                }
                catch
                {
                    Debug.WriteLine("Nothing Found in TreatmentPlanTreatments Firebase");
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

                try
                {
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
                }
                catch
                {
                    Debug.WriteLine("Nothing Found in Payments In Firebase");
                }

                    db.Close();
                }

            //Read from Firebase and update SQLite countRecord table
            //Establish connection with SQLite
            using (SqliteConnection db =
                                new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                //open sqlite Connection 
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand
                    //Delete everything in the payment table
                    ("DELETE from countRecord", db);
                SqliteDataReader query = selectCommand.ExecuteReader();

                try
                {
                  
                    //Read from Firebase and populate the local datbase/SQLite countRecord table
                    var results = await firebase.Child(CountRecordNode).OnceAsync<FbCnt>();
                    foreach (var details in results)
                    {
                        SqliteCommand insertCommand = new SqliteCommand();
                        insertCommand.Connection = db;

                        // Use parameterized query
                        insertCommand.CommandText = "INSERT INTO countRecord (counterNum,email) VALUES (@CounterNum,@Email);";
                        insertCommand.Parameters.AddWithValue("@CounterNum", details.Object.cnt);
                        insertCommand.Parameters.AddWithValue("@Email", details.Object.username);
     
                        insertCommand.ExecuteReader();

                    }
                }
                catch
                {
                    Debug.WriteLine("Nothing Found in countRecord In Firebase");
                }

                db.Close();
            }

            //await firebase.Child("g00351330@gmit-ieTreatmentPlans/").DeleteAsync();

        }


        public async void ReadDataFromSQLite()
        {
            //Open connection with Firebase
            ConnectToFirebase();

            string userName = DAO.GetUserID();
            String myUsername = userName;
            myUsername = myUsername.Replace(".", "-");

            String TreatmentNode = myUsername + "Treatments" + "/";
            String CustomerNode = myUsername + "Customers" + "/";
            String TreatmentPlanNode = myUsername + "TreatmentPlans" + "/";
            String TreatmentPlanTreatmentNode = myUsername + "TreatmentPlanTreatments" + "/";
            String PaymentNode = myUsername + "Payments" + "/";

            //Read from Customer Table SQLite and update Firebase Customer Table.
            using (SqliteConnection db =
                new SqliteConnection("Filename=dentalManagerDB.db"))
            {  
               //Delete customer table in Firebase
               DeleteCustomer();

                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from customers", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                //Populate customer table in Firebase.
                while (query.Read())
                {
                    query.GetString(0);
                    query.GetString(1);
                    query.GetString(2);
                    query.GetString(4);
                    query.GetString(5);
                    query.GetString(6);
                    query.GetString(7);
                    query.GetString(8);
                    query.GetString(9);
                    query.GetString(10);
                    query.GetString(11);
                    Convert.ToDateTime(query.GetString(3));
                    query.GetString(12);
                    AddNewCustomer(query.GetString(0), query.GetString(1), query.GetString(2), Convert.ToDateTime(query.GetString(3)).ToString(), query.GetString(4), query.GetString(5), query.GetString(6), query.GetString(7), query.GetString(8), query.GetString(9), query.GetString(10), query.GetString(11), query.GetString(12));
                }

                db.Close();
            }


            //Read from SQLite to update Firebase Treatment table in Firebase
            using (SqliteConnection db =
                new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                //Delete Treatment from Firebase
                 DeleteTreatment();

                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from treatment", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                //populate treatment table in Firebase
                while (query.Read())
                {
                    Treatment treatment = new Treatment(
                        query.GetInt32(0),
                        query.GetString(1),
                        query.GetDecimal(2)
                        );
                    AddNewTreatment(treatment);
                    if (query.GetInt32(0) == 1)
                    {
                        AddNewTreatment(treatment);
                    }
                }

                db.Close();
            }

            //Read from SQLite and update treatmentPlan in Firebase
            using (SqliteConnection db =
                 new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                //Delete table from Firebase
                DeletTreatmentPlanState();

                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from treatmentPlan", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                //Populate plan state table in Firebase.
                while (query.Read())
                {
                    int id = query.GetInt32(0);
                    string custumerID = query.GetString(1);
                    int state = query.GetInt32(2);
                    string creationDate = query.GetString(3);
                    TreatmentPlaneState tempSate = (TreatmentPlaneState)state;
                    string date2 = query.GetString(4);
                    DateTime datetime2 = Convert.ToDateTime("01/01/0001 00:00:00");
                    if (!date2.Equals("0"))
                    {
                        datetime2 = Convert.ToDateTime(date2);
                    }

                    TreatmentPlan treatmentPlan  = new TreatmentPlan(
                    custumerID,
                    tempSate,
                    Convert.ToDateTime(query.GetString(3)),
                    datetime2
                    );

                    AddNewTreatmentPlanState(id, custumerID, tempSate, creationDate, date2);
                }

                db.Close();
            }


            //Read from SQLite and update treatmentPlanTreatments in Firebase
            using (SqliteConnection db =
               new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                //Delete from DeleteTreatmentPlanTreatments in Firebase
                DeleteTreatmentPlanTreatments();

                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from treatmentPlanTreatments;", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    int TreatmentPlanTreatmentsID = query.GetInt32(0);
                    int TreatmentPlanID = query.GetInt32(1);
                    int TreatmentID = query.GetInt32(2);
                    decimal price = query.GetDecimal(3);
                    DateTime date = Convert.ToDateTime(query.GetString(4));
                    int toothNum = query.GetInt32(5);
                    string comments = query.GetString(6);
                    bool isDone = query.GetInt32(7) == 0 ? false : true;
                    string name ="test" ;

                    TreatmentOnPlan treatmentOnPlan = new TreatmentOnPlan(
                    TreatmentPlanTreatmentsID,
                    TreatmentPlanID,
                    TreatmentID,
                    price,
                     date,
                    toothNum,
                    comments,
                    isDone,
                    name
                    );

                    AddNewTreatmentPlanTreatments(treatmentOnPlan, treatmentOnPlan.TreatmentPlanTreatmentsID);
                }

                db.Close();
            }

            //Read from SQLite and update payments in Firebase
            using (SqliteConnection db =
                new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                //Delete payment from firebase
                DeletePayments();

                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from payments", db);
       
                SqliteDataReader query = selectCommand.ExecuteReader();

                //populate payment with info from SQLite
                while (query.Read())
                {
                    Payments payments = new Payments(
                    query.GetInt32(0),
                    query.GetInt32(1),
                    query.GetString(2),
                    query.GetFloat(3),
                    Convert.ToDateTime(query.GetString(4))
                    );
                    AddNewpaymentObject(payments);
                }

                db.Close();
            }


            //Read from SQLite and update countRecor in Firebase
            using (SqliteConnection db =
               new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from countRecord ", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    query.GetInt32(0);
                    int userCntNum = query.GetInt32(1);
                    query.GetString(2);

                    UpdateCountRecordFB(userCntNum);
                }

                db.Close();

            }

        }


    }
}
