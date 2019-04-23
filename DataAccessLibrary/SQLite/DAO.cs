using System;
using System.Collections.Generic;
using System.Text;
using Models;
using Microsoft.Data.Sqlite;
using System.Diagnostics;

namespace DataAccessLibrary
{
    public class DAO
    {

        /// <summary>
        /// Create a database and tables
        /// </summary>
        public static void InitializeDatabase()
        {

            using (SqliteConnection db = new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                String customers = "CREATE TABLE IF NOT EXISTS customers(  customerID varchar(20)NOT NULL, firstName varchar(30)NOT NULL,surname varchar(30)NOT NULL,DOB varchar(12),street varchar(45),city varchar(30),province varchar(30),country varchar(15),postcode varchar(15),mobileNum varchar(15),fixNum varchar(15),email varchar(45),comments varchar(500),PRIMARY KEY (customerID))";
                SqliteCommand createCustomers = new SqliteCommand(customers, db);
                createCustomers.ExecuteReader();

                String treatment = "CREATE TABLE IF NOT EXISTS treatment(treatmentID INTEGER, treatmentName varchar(50) NOT NULL,price float,PRIMARY KEY (treatmentID))";
                SqliteCommand createTreatment = new SqliteCommand(treatment, db);
                createTreatment.ExecuteReader();

                String treatmentPlan = "CREATE TABLE IF NOT EXISTS treatmentPlan( treatmentPlanID INTEGER,customerID varchar(20)NOT NULL, state INTEGER NOT NULL,creationDate DATETIME DEFAULT CURRENT_TIMESTAMP,treatmentPlanCompleteDate DATETIME,PRIMARY KEY (treatmentPlanID),FOREIGN KEY (customerID) REFERENCES customers(customerID))";
                SqliteCommand createTreatmentPlan = new SqliteCommand(treatmentPlan, db);
                createTreatmentPlan.ExecuteReader();


                String treatmentPlanTreatments = "CREATE TABLE IF NOT EXISTS treatmentPlanTreatments( treatmentPlanTreatmentsID INTEGER,treatmentPlanID int,treatmentID int, price float,treatmentCompleteDate DATETIME,tooth INT,comment VARCHAR(2000),isdone INT,PRIMARY KEY (treatmentPlanTreatmentsID),FOREIGN KEY (treatmentPlanID) REFERENCES treatmentPlan(treatmentPlanID),FOREIGN KEY (treatmentID) REFERENCES treatment(treatmentID))";
                SqliteCommand createTreatmentPlanTreatments = new SqliteCommand(treatmentPlanTreatments, db);
                createTreatmentPlanTreatments.ExecuteReader();

                String payments = "CREATE TABLE IF NOT EXISTS payments(paymentsID INTEGER,treatmentPlanID int,customerID varchar(20)NOT NULL,amount float,treatmentCompleteDate DATETIME,PRIMARY KEY (paymentsID),FOREIGN KEY (treatmentPlanID) REFERENCES treatmentPlan(treatmentPlanID),FOREIGN KEY (customerID) REFERENCES customers(customerID))";
                SqliteCommand createPayments = new SqliteCommand(payments, db);
                createPayments.ExecuteReader();

                //users tables
                String users = "CREATE TABLE IF NOT EXISTS users(userID INTEGER,name varchar(32)NOT NULL,email varchar(32),password varchar(16)NOT NULL,PRIMARY KEY (userID) )";
                SqliteCommand createUsers = new SqliteCommand(users, db);
                createUsers.ExecuteReader();

                /*String address = "CREATE TABLE IF NOT EXISTS address(addressID INTEGER,userID INTEGER NOT NULL,street varchar(32),city varchar(20),province" +
                    " varchar(20),country varchar(20), postcode varchar(20),PRIMARY KEY (addressID), FOREIGN KEY (userID) REFERENCES users(userID)  )";
                SqliteCommand createAddress = new SqliteCommand(address, db);
                createAddress.ExecuteReader();*/

                string appointments = "CREATE TABLE IF NOT EXISTS appointment(id INTEGER,patientID VARCHAR(20) NOT NULL,date DATETIME,status INTEGER NOT NULL,PRIMARY KEY (id), FOREIGN KEY (patientID) REFERENCES customers(customerID))";
                SqliteCommand createAppointments = new SqliteCommand(appointments, db);
                createAppointments.ExecuteReader();

                String countRecord = "CREATE TABLE IF NOT EXISTS countRecord(countID INTEGER,counterNum INTEGER NOT NULL,email varchar(32)NOT NULL, PRIMARY KEY (countID),FOREIGN KEY (email) REFERENCES users(email))";
                SqliteCommand createCountRecord = new SqliteCommand(countRecord, db);
                createCountRecord.ExecuteReader();

                String adminDetails = "CREATE TABLE IF NOT EXISTS adminDetails(firstName varchar(30),surname varchar(30),street varchar(45),city varchar(30),province varchar(30),country varchar(15),postcode varchar(15),mobileNum varchar(15),fixNum varchar(15),email varchar(45) NOT NULL,PRIMARY KEY (email))";
                SqliteCommand createAdminDetails = new SqliteCommand(adminDetails, db);
                createAdminDetails.ExecuteReader();

            }

        }

        #region User Data Methods

        public static void AddNewUser(User user)
        {
            using (SqliteConnection db =
              new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();

                insertCommand.Connection = db;

                insertCommand.CommandText = "INSERT INTO users (name,password,email) VALUES (@Name ,@Password, @Email);";
                insertCommand.Parameters.AddWithValue("@Name", user.Name);
                insertCommand.Parameters.AddWithValue("@Password", user.Password);
                insertCommand.Parameters.AddWithValue("@Email", user.Email);

                insertCommand.ExecuteNonQuery();

                db.Close();
            }
        }
        #endregion


        public static User GetUser()
        {
            User user = null;
            using (SqliteConnection db =
               new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                try
                {
                    SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from users", db);

                    SqliteDataReader query = selectCommand.ExecuteReader();

                    while (query.Read())
                    {
                        int id = query.GetInt32(0);
                        user = new User(
                            query.GetString(1),
                            "no pass",//query.GetString(3),
                            query.GetString(2)

                            );
                    }

                    db.Close();
                }
                catch
                {
                    Debug.WriteLine("User name not found!");
                }

            }
            return user;
        }



        //public static void AddMockData()
        //{
        //    DAO.AddNewCustomer("12234543-k", "Marco Antonio", "Perez Gonzales", "03/03/1978 00:00:00", "Los leons 29", "Valpariso", "Valparaiso", "Chile", "gh567", "0983442233", "02122222", "marco@email.com", "Sin alergias");
        //    DAO.AddNewTreatment("Composite Compuesto", 24000);
        //    DAO.AddNewTreatment("Incrustacion", 30000);
        //    AddNewTreatmentPlan("12234543-k", (int)TreatmentPlaneState.Created, DateTime.Now.ToString(), "0");
        //    //AddNewTreatmentPlanTreatments(1, 1, 24000, "0");
        //    //AddNewTreatmentPlanTreatments(1, 2, 30000, "0");
        //    //AddNewTreatmentPlanTreatments(1, 2, 25000, "0");


        //}

        /// <summary>
        /// Create the first column of the count table(to be used once).
        /// </summary>
        /// <param name="countID"></param>
        /// <param name="counterNum"></param>
        public void NewUserCount(string email, int counterNum)
        {

            using (SqliteConnection db =
               new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO countRecord VALUES (@ID,@CounterNum,@Email);";
                insertCommand.Parameters.AddWithValue("@ID", 0);
                insertCommand.Parameters.AddWithValue("@CounterNum", counterNum);
                insertCommand.Parameters.AddWithValue("@Email", email);

                insertCommand.ExecuteNonQuery();

                db.Close();
            }
        }

        /// <summary>
        /// Update counter
        /// </summary>
        /// <param name="counterNum"></param>
        public void UpdateCountRecordSQlite(int counterNum)
        {
            string userName = DAO.GetUserID();
            String myUsername = userName;
            myUsername = myUsername.Replace(".", "-");

            using (SqliteConnection db =
               new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "UPDATE countRecord SET counterNum =@CounterNum where email=@Email;";
                insertCommand.Parameters.AddWithValue("@CounterNum", counterNum);
                insertCommand.Parameters.AddWithValue("@Email", myUsername);

                insertCommand.ExecuteNonQuery();

                db.Close();
            }
        }

        public void NewAdminDetails(string firstName, string surname,string street, string city, string province, string country, string postcode, string mobileNum, string fixNum, string email)
        {

            using (SqliteConnection db =
               new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO adminDetails VALUES (@FirstName ,@Surname, @Street ,@city ,@Province ,@Country ,@Postocode ,@MobileNum ,@FixNum ,@Email);";
                insertCommand.Parameters.AddWithValue("@FirstName", firstName);
                insertCommand.Parameters.AddWithValue("@Surname", surname);
                insertCommand.Parameters.AddWithValue("@Street", street);
                insertCommand.Parameters.AddWithValue("@city", city);
                insertCommand.Parameters.AddWithValue("@Province", province);
                insertCommand.Parameters.AddWithValue("@Country", country);
                insertCommand.Parameters.AddWithValue("@Postocode", postcode);
                insertCommand.Parameters.AddWithValue("@MobileNum", mobileNum);
                insertCommand.Parameters.AddWithValue("@FixNum", fixNum);
                insertCommand.Parameters.AddWithValue("@Email", email);

                insertCommand.ExecuteNonQuery();

                db.Close();
            }
        }

        public bool CheckAdmidDetailsExist(string email)
        {

            Boolean FoundEmail = false;

            using (SqliteConnection db =
               new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                     ("SELECT * from adminDetails where email=@Email", db);
                selectCommand.Parameters.AddWithValue("@Email", email);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    if (query.GetString(9).Equals(email))
                    {
                        FoundEmail=true;
                    }
                    query.GetString(9);
                }

                db.Close();
            }
            return FoundEmail;
        }

        public void UpdateAdminDetails(string firstName, string surname, string street, string city, string province, string country, string postcode, string mobileNum, string fixNum, string email)
        {
            using (SqliteConnection db =
               new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "UPDATE adminDetails SET firstName =@FirstName, surname =@Surname, street =@Street, city =@City,province =@Province,country =@Country, postcode =@Postcode, mobileNum =@MobileNum,fixNum =@FixNum where email=@Email;";
                insertCommand.Parameters.AddWithValue("@FirstName", firstName);
                insertCommand.Parameters.AddWithValue("@Surname", surname);
                insertCommand.Parameters.AddWithValue("@Street", street);
                insertCommand.Parameters.AddWithValue("@City", city);
                insertCommand.Parameters.AddWithValue("@Province", province);
                insertCommand.Parameters.AddWithValue("@Country", country);
                insertCommand.Parameters.AddWithValue("@Postcode", postcode);
                insertCommand.Parameters.AddWithValue("@MobileNum", mobileNum);
                insertCommand.Parameters.AddWithValue("@FixNum", fixNum);
                insertCommand.Parameters.AddWithValue("@Email", email);

                insertCommand.ExecuteNonQuery();

                db.Close();
            }
        }

        public AdminDetails GetAdminDetails(string email)
        {
            AdminDetails adminDetails = null;

            using (SqliteConnection db =
                new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from adminDetails where email=@Email", db);
                selectCommand.Parameters.AddWithValue("@Email", email);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    adminDetails=new AdminDetails(
                    query.GetString(0),
                    query.GetString(1),
                    query.GetString(2),
                    query.GetString(3),
                    query.GetString(4),
                    query.GetString(5),
                    query.GetString(6),
                    query.GetString(7),
                    query.GetString(8),
                    query.GetString(9)
                    );
                }

                db.Close();
            }

            return adminDetails;
        }

        public static int GetUserCountSqlite(string email)
        {

            int userCntNum = 0;
            CounterData cd = null;

            String myEmail = email;
            myEmail = myEmail.Replace(".", "-");

            using (SqliteConnection db =
               new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                     ("SELECT * from countRecord where email=@MyEmail", db);
                selectCommand.Parameters.AddWithValue("@MyEmail", myEmail);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    cd = new CounterData(
                    query.GetInt32(0),
                    userCntNum = query.GetInt32(1),
                    query.GetString(2)
                     );
                }

                db.Close();
            }
            return userCntNum;
        }



        public void UpdateTreatmentPlanState(TreatmentPlaneState state, int iD)
        {


            using (SqliteConnection db =
               new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "UPDATE treatmentPlan SET state =@State where treatmentPlanID=@ID;";
                insertCommand.Parameters.AddWithValue("@State", (int)state);
                insertCommand.Parameters.AddWithValue("@ID", iD);

                insertCommand.ExecuteNonQuery();

                db.Close();
            }
        }

        /// <summary>
        /// Add new customer with all parameters
        /// </summary>
        /// <param name="id"></param>
        /// <param name="firstName"></param>
        /// <param name="surname"></param>
        /// <param name="dOB"></param>
        /// <param name="street"></param>
        /// <param name="city"></param>
        /// <param name="province"></param>
        /// <param name="country"></param>
        /// <param name="postcode"></param>
        /// <param name="mobileNum"></param>
        /// <param name="fixNum"></param>
        /// <param name="email"></param>
        public bool AddNewCustomer(string id, string firstName, string surname, string dOB, string street, string city, string province, string country, string postcode, string mobileNum, string fixNum, string email, string comments)
        {
            try
            {

                using (SqliteConnection db =
                    new SqliteConnection("Filename=dentalManagerDB.db"))
                {
                    db.Open();

                    SqliteCommand insertCommand = new SqliteCommand();

                    insertCommand.Connection = db;

                    // Use parameterized query 
                    insertCommand.CommandText = "INSERT INTO customers VALUES (@Id ,@FirstName ,@Surname,@DOB ,@Street ,@city ,@Province ,@Country ,@Postocode ,@MobileNum ,@FixNum ,@Email,@Comments);";
                    insertCommand.Parameters.AddWithValue("@Id", id);
                    insertCommand.Parameters.AddWithValue("@FirstName", firstName);
                    insertCommand.Parameters.AddWithValue("@Surname", surname);
                    insertCommand.Parameters.AddWithValue("@DOB", dOB);
                    insertCommand.Parameters.AddWithValue("@Street", street);
                    insertCommand.Parameters.AddWithValue("@city", city);
                    insertCommand.Parameters.AddWithValue("@Province", province);
                    insertCommand.Parameters.AddWithValue("@Country", country);
                    insertCommand.Parameters.AddWithValue("@Postocode", postcode);
                    insertCommand.Parameters.AddWithValue("@MobileNum", mobileNum);
                    insertCommand.Parameters.AddWithValue("@FixNum", fixNum);
                    insertCommand.Parameters.AddWithValue("@Email", email);
                    insertCommand.Parameters.AddWithValue("@Comments", comments);

                    insertCommand.ExecuteReader();


                    db.Close();
                }
            }
            catch (SqliteException e)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Insert a new treatment
        /// </summary>
        /// <param name="treatmentName"></param>
        /// <param name="price"></param>
        public long AddNewTreatment(string treatmentName, Decimal price)
        {
            long id = 0;

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

                //insertCommand.Connection.Close();
                //insertCommand.Connection = db;
                SqliteCommand insertCommand1 = new SqliteCommand();
                insertCommand1.Connection = db;

                string sql = @"select last_insert_rowid()";
                insertCommand1.CommandText = sql;
                id = (long)insertCommand1.ExecuteScalar();
                db.Close();
            }

            return id;
        }

        /// <summary>
        /// Get all treatment form the database
        /// </summary>
        /// <returns></returns>
        public static List<Treatment> GetAllTreatment()
        {
            List<Treatment> treatments = new List<Treatment>();

            using (SqliteConnection db =
                new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from treatment", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    treatments.Add(new Treatment(
                    query.GetInt32(0),
                    query.GetString(1),
                    query.GetDecimal(2)
                    ));
                }

                db.Close();
            }

            return treatments;

        }

        /// <summary>
        /// Get a treatement by treatement id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public static Treatment GetTreatmentByID(String id)
        {

            Treatment treatment = null;

            using (SqliteConnection db =
                new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from treatment where treatmentID=@treatmentID", db);
                selectCommand.Parameters.AddWithValue("@TreatmentID", id);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {

                    treatment = new Treatment(
                    query.GetInt32(0),
                    query.GetString(1),
                    query.GetDecimal(2)


                 );
                  
                }

                db.Close();
            }

            return treatment;
        }

        /// <summary>
        /// Insert a new treatment plan
        /// </summary>
        /// <param name="treatmentPlanID"></param>
        /// <param name="customerID"></param>
        /// <param name="state"></param>
        /// <param name="creationDate"></param>
        /// <param name="treatmentPlanCompleteDate"></param>
        public long AddNewTreatmentPlan(string customerID, int state, string creationDate, string treatmentPlanCompleteDate)
        {
            long id = 0;
            using (SqliteConnection db =
                new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query
                insertCommand.CommandText = "INSERT INTO treatmentPlan (customerID,state,creationDate,treatmentPlanCompleteDate) VALUES (@CustomerID,@State,@CreationDate,@TreatmentPlanCompleteDate);";
                insertCommand.Parameters.AddWithValue("@CustomerID", customerID);
                insertCommand.Parameters.AddWithValue("@State", state);
                insertCommand.Parameters.AddWithValue("@CreationDate", creationDate);
                insertCommand.Parameters.AddWithValue("@TreatmentPlanCompleteDate", treatmentPlanCompleteDate);

                insertCommand.ExecuteReader();

                //insertCommand.Connection.Close();
                //insertCommand.Connection = db;
                SqliteCommand insertCommand1 = new SqliteCommand();
                insertCommand1.Connection = db;

                string sql = @"select last_insert_rowid()";
                insertCommand1.CommandText = sql;
                id = (long)insertCommand1.ExecuteScalar();


                db.Close();
            }
            return id;
        }

        /// <summary>
        /// Get all treatement plans
        /// </summary>
        /// <returns></returns>
        public static List<TreatmentPlan> GetAllTreatmentPlans()
        {
            List<TreatmentPlan> treatmentPlans = new List<TreatmentPlan>();

            using (SqliteConnection db =
                new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from treatmentPlan", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    string first = query.GetString(1);
                    int state = query.GetInt32(2);
                    TreatmentPlaneState temp = (TreatmentPlaneState)state;
                    string date2 = query.GetString(4);
                    DateTime datetime2 = Convert.ToDateTime("01/01/0001 00:00:00");
                    if (!date2.Equals("0"))
                    {
                        datetime2 = Convert.ToDateTime(date2);
                    }

                    treatmentPlans.Add(new TreatmentPlan(
                    first,
                    temp,
                    Convert.ToDateTime(query.GetString(3)),
                    datetime2
                    ));
                }

                db.Close();
            }
            return treatmentPlans;

        }

        /// <summary>
        /// Get Get all treatement plans by Customer ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<TreatmentPlan> GetAllTreatmentPlansByID(string id)
        {
            List<TreatmentPlan> treatmentPlans = new List<TreatmentPlan>();
            int IdForCustomer = 1;
            using (SqliteConnection db =
                new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from treatmentPlan where customerID=@CustomerID", db);
                selectCommand.Parameters.AddWithValue("@CustomerID", id);

                SqliteDataReader query = selectCommand.ExecuteReader();


                while (query.Read())
                {
                    int treatementPlanID = query.GetInt32(0);
                    string customerId = query.GetString(1);
                    int state = query.GetInt32(2);
                    TreatmentPlaneState temp = (TreatmentPlaneState)state;

                    string date2 = query.GetString(4);
                    DateTime datetime2 = Convert.ToDateTime("01/01/0001 00:00:00");
                    if (!date2.Equals("0"))
                    {
                        datetime2 = Convert.ToDateTime(date2);
                    }

                    treatmentPlans.Add(new TreatmentPlan(
                    treatementPlanID,
                    customerId,
                    temp,
                    Convert.ToDateTime(query.GetString(3)),
                    datetime2,
                    IdForCustomer++
                    ));
                }

                db.Close();
            }
            return treatmentPlans;

        }


        /// <summary>
        /// Get Sum price of all treatment plans by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static decimal GetSumTreatmentPlanByID(int id)
        {

            decimal sumPrice = 0;

            using (SqliteConnection db =
                new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT sum(price) from treatmentPlanTreatments where treatmentPlanID=@TreatmentPlanID", db);
                selectCommand.Parameters.AddWithValue("@TreatmentPlanID", id);

                sumPrice = selectCommand.ExecuteNonQuery();


                db.Close();
            }

            return sumPrice;
        }


        /// <summary>
        /// Insert a new treatment plan treatments
        /// </summary>
        /// <param name="treatmentPlanTreatmentsID"></param>
        /// <param name="treatmentPlanID"></param>
        /// <param name="treatmentID"></param>
        /// <param name="price"></param>
        /// <param name="treatmentCompleteDate"></param>
        public void AddNewTreatmentPlanTreatments(int treatmentPlanID, int treatmentID, Decimal price, string treatmentCompleteDate, int tooth, string comment, bool isDone)
        {

            int _isDone = (isDone) ? 1 : 0;
            using (SqliteConnection db =
                new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query
                insertCommand.CommandText = "INSERT INTO treatmentPlanTreatments (TreatmentPlanID,TreatmentID,Price,TreatmentCompleteDate, tooth, comment, isdone) VALUES (@TreatmentPlanID,@TreatmentID,@Price,@TreatmentCompleteDate,@Tooth,@Comment,@IsDone);";

                insertCommand.Parameters.AddWithValue("@TreatmentPlanID", treatmentPlanID);
                insertCommand.Parameters.AddWithValue("@TreatmentID", treatmentID);
                insertCommand.Parameters.AddWithValue("@Price", price);
                insertCommand.Parameters.AddWithValue("@TreatmentCompleteDate", treatmentCompleteDate);
                insertCommand.Parameters.AddWithValue("@Tooth", tooth);
                insertCommand.Parameters.AddWithValue("@Comment", comment);
                insertCommand.Parameters.AddWithValue("@IsDone", _isDone);

                insertCommand.ExecuteReader();

                db.Close();
            }
        }
        public long AddNewTreatmentPlanTreatments(TreatmentOnPlan t)
        {
            long id = 0;

            int _isDone = (t.IsDone) ? 1 : 0;
            using (SqliteConnection db =
                new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query
                insertCommand.CommandText = "INSERT INTO treatmentPlanTreatments (TreatmentPlanID,TreatmentID,Price,TreatmentCompleteDate, tooth, comment, isdone) VALUES (@TreatmentPlanID,@TreatmentID,@Price,@TreatmentCompleteDate,@Tooth,@Comment,@IsDone);";

                insertCommand.Parameters.AddWithValue("@TreatmentPlanID", t.TreatmentPlanID);
                insertCommand.Parameters.AddWithValue("@TreatmentID", t.TreatmentID);
                insertCommand.Parameters.AddWithValue("@Price", t.Price);
                insertCommand.Parameters.AddWithValue("@TreatmentCompleteDate", t.CompletedDate);
                insertCommand.Parameters.AddWithValue("@Tooth", t.Tooth);
                insertCommand.Parameters.AddWithValue("@Comment", t.Comment);
                insertCommand.Parameters.AddWithValue("@IsDone", _isDone);

                insertCommand.ExecuteReader();

                SqliteCommand insertCommand1 = new SqliteCommand();
                insertCommand1.Connection = db;

                string sql = @"select last_insert_rowid()";
                insertCommand1.CommandText = sql;
                id = (long)insertCommand1.ExecuteScalar();

                db.Close();
            }

            return id;
        }


        /// <summary>
        /// Insert a new payment
        /// </summary>
        /// <param name="treatmentPlanID"></param>
        /// <param name="customerID"></param>
        /// <param name="amount"></param>
        /// <param name="treatmentCompleteDate"></param>
        public long AddNewpayment(int treatmentPlanID, string customerID, decimal amount, string treatmentCompleteDate)
        {
            long id = 0;

            using (SqliteConnection db =
                new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query
                insertCommand.CommandText = "INSERT INTO payments (treatmentPlanID,customerID,amount,treatmentCompleteDate) VALUES (@TreatmentPlanID,@CustomerID,@Amount,@TreatmentCompleteDate);";
                insertCommand.Parameters.AddWithValue("@TreatmentPlanID", treatmentPlanID);
                insertCommand.Parameters.AddWithValue("@CustomerID", customerID);
                insertCommand.Parameters.AddWithValue("@Amount", amount);
                insertCommand.Parameters.AddWithValue("@TreatmentCompleteDate", treatmentCompleteDate);

                insertCommand.ExecuteReader();

                SqliteCommand insertCommand1 = new SqliteCommand();
                insertCommand1.Connection = db;

                string sql = @"select last_insert_rowid()";
                insertCommand1.CommandText = sql;
                id = (long)insertCommand1.ExecuteScalar();

                db.Close();
            }

            return id;
        }

        /// <summary>
        /// Get payment by customer Id
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public static Payments GetPaymenyByCustomerID(String CustomerID)
        {

            Payments payment = null;

            using (SqliteConnection db =
                new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from payments where customerID=@customerID", db);
                selectCommand.Parameters.AddWithValue("@customerID", CustomerID);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {

                    payment = new Payments(
                        query.GetInt32(0),
                        query.GetInt32(1),
                        query.GetString(2),
                        query.GetFloat(3),
                        Convert.ToDateTime(query.GetString(4))

                     );

                }

                db.Close();
            }

            return payment;
        }

        /// <summary>
        /// Get a list of payments by customer id
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public static List<Payments> GetAllPaymenyByCustomerID(string CustomerID)
        {

            List<Payments> payment = new List<Payments>();

            using (SqliteConnection db =
                new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from payments where customerID=@customerID", db);
                selectCommand.Parameters.AddWithValue("@customerID", CustomerID);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {

                    payment.Add(new Payments(
                    query.GetInt32(0),
                    query.GetInt32(1),
                    query.GetString(2),
                    query.GetFloat(3),
                    Convert.ToDateTime(query.GetString(4))
                   ));

                }

                db.Close();
            }

            return payment;
        }

        /// <summary>
        /// Get the sum of payment for a particular period
        /// </summary>
        /// <param name="selectedDate"></param>
        /// <returns></returns>
        public static decimal GetSumPaymentByDate(string selectedDate)
        {

            decimal sumPayments = 0;

            using (SqliteConnection db =
                 new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("select *, strftime('%d-%m-%Y', treatmentCompleteDate) as date from payments where date<=@SelectedDate", db);
                selectCommand.Parameters.AddWithValue("@SelectedDate", selectedDate);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    sumPayments += (decimal)query.GetFloat(3);
                }

                db.Close();
            }
            return sumPayments;
        }

        public static void AddData(string inputText)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteSample.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO MyTable VALUES (NULL, @Entry);";
                insertCommand.Parameters.AddWithValue("@Entry", inputText);

                insertCommand.ExecuteReader();

                db.Close();
            }

        }

        /// <summary>
        /// Get all customers details
        /// </summary>
        /// <returns></returns>
        public static List<Customer> GetAllCustomer()
        {
            List<Customer> customers = new List<Customer>();

            using (SqliteConnection db =
                new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from customers", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    customers.Add(new Customer(
                    query.GetString(0),
                    query.GetString(1),
                    query.GetString(2),
                    query.GetString(4),
                    query.GetString(5),
                    query.GetString(6),
                    query.GetString(7),
                    query.GetString(8),
                    query.GetString(9),
                    query.GetString(10),
                    query.GetString(11),
                    Convert.ToDateTime(query.GetString(3)),
                    query.GetString(12)
                    ));
                }

                db.Close();
            }

            return customers;
        }



        public static List<Customer> DeleteCustomerByID(String CustomerID)
        {

            List<Customer> customers = new List<Customer>();

            using (SqliteConnection db =
                new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("DELETE from customers where customerID=@customerID", db);

                selectCommand.Parameters.AddWithValue("@customerID", CustomerID);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    customers.Add(new Customer(
                    query.GetString(0),
                    query.GetString(1),
                    query.GetString(2),
                    query.GetString(3),
                    query.GetString(4),
                    query.GetString(5),
                    query.GetString(6),
                    query.GetString(7),
                    query.GetString(8),
                    query.GetString(9),
                    query.GetString(10),
                    new DateTime(),
                    query.GetString(11)
                    ));
                }

                db.Close();
            }

            return customers;
        }

        /// <summary>
        /// Get all customers by Surname
        /// </summary>
        /// <param name="Surname"></param>
        /// <returns></returns>
        public static List<Customer> GetAllCustomerBySurname(String Surname)
        {

            List<Customer> customers = new List<Customer>();

            using (SqliteConnection db =
                new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from customers where surname=@surname", db);
                selectCommand.Parameters.AddWithValue("@surname", Surname);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    customers.Add(new Customer(
                    query.GetString(0),
                    query.GetString(1),
                    query.GetString(2),
                    query.GetString(3),
                    query.GetString(4),
                    query.GetString(5),
                    query.GetString(6),
                    query.GetString(7),
                    query.GetString(8),
                    query.GetString(9),
                    query.GetString(10),
                    new DateTime(),
                    query.GetString(11)
                    ));
                }

                db.Close();
            }

            return customers;
        }
        /// <summary>
        /// Get Customer by Id
        /// </summary>
        /// <param name="Surname"></param>
        /// <returns></returns>
        public static Customer GetCustomerByID(String CustomerID)
        {

            Customer customer = null;

            using (SqliteConnection db =
                new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from customers where customerID=@customerID", db);
                selectCommand.Parameters.AddWithValue("@customerID", CustomerID);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {

                    customer = new Customer(
                        query.GetString(0),
                        query.GetString(1),
                        query.GetString(2),
                        query.GetString(4),
                        query.GetString(5),
                        query.GetString(6),
                        query.GetString(7),
                        query.GetString(8),
                        query.GetString(9),
                        query.GetString(10),
                        query.GetString(11),
                        Convert.ToDateTime(query.GetString(3)),
                        query.GetString(12)

                     );
               
                }

                db.Close();
            }

            return customer;
        }

        /// <summary>
        /// Update or edit customer.
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="firstName"></param>
        /// <param name="surname"></param>
        /// <param name="dOB"></param>
        /// <param name="street"></param>
        /// <param name="city"></param>
        /// <param name="province"></param>
        /// <param name="country"></param>
        /// <param name="postcode"></param>
        /// <param name="mobileNum"></param>
        /// <param name="fixNum"></param>
        /// <param name="email"></param>
        /// <param name="comments"></param>
        public void UpdateCustomer(string customerID, string firstName, string surname, string dOB, string street, string city, string province, string country, string postcode, string mobileNum, string fixNum, string email, string comments)
        {
            using (SqliteConnection db =
               new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "UPDATE customers SET firstName =@FirstName, surname =@Surname, dOB =@DOB, street =@Street, city =@City,province =@Province,country =@Country, postcode =@Postcode, mobileNum =@MobileNum,fixNum =@FixNum, email =@Email, comments =@Comments   where customerID=@CustomerID;";
                insertCommand.Parameters.AddWithValue("@CustomerID", customerID);
                insertCommand.Parameters.AddWithValue("@FirstName", firstName);
                insertCommand.Parameters.AddWithValue("@Surname", surname);
                insertCommand.Parameters.AddWithValue("@DOB", dOB);
                insertCommand.Parameters.AddWithValue("@Street", street);
                insertCommand.Parameters.AddWithValue("@City", city);
                insertCommand.Parameters.AddWithValue("@Province", province);
                insertCommand.Parameters.AddWithValue("@Country", country);
                insertCommand.Parameters.AddWithValue("@Postcode", postcode);
                insertCommand.Parameters.AddWithValue("@MobileNum", mobileNum);
                insertCommand.Parameters.AddWithValue("@FixNum", fixNum);
                insertCommand.Parameters.AddWithValue("@Email", email);
                insertCommand.Parameters.AddWithValue("@Comments", comments);

                insertCommand.ExecuteNonQuery();

                db.Close();
            }
        }

        public static List<String> GetData()
        {
            List<String> entries = new List<string>();

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteSample.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Text_Entry from MyTable", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    entries.Add(query.GetString(0));
                }

                db.Close();
            }

            return entries;
        }


        public void UpdateTreatment(Treatment treatment)
        {
            using (SqliteConnection db =
               new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "UPDATE treatment set treatmentName = @Name, price = @Price where treatmentID = @Id;";
                insertCommand.Parameters.AddWithValue("@Name", treatment.Name);
                insertCommand.Parameters.AddWithValue("@Price", treatment._Price);
                insertCommand.Parameters.AddWithValue("@Id", treatment.ID);

                insertCommand.ExecuteNonQuery();

                db.Close();
            }
        }


        /// <summary>
        /// Get all treatment by treatement ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<TreatmentOnPlan> GetTreatmentOnPlansByID(int id)
        {
            List<TreatmentOnPlan> treatmentList = new List<TreatmentOnPlan>();

            using (SqliteConnection db =
                new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                //SqliteCommand selectCommand = new SqliteCommand
                //    ("SELECT * from treatmentPlanTreatments where treatmentPlanID=@TreatmentPlanID", db);
                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from treatmentPlanTreatments  Inner Join treatment ON treatmentPlanTreatments.treatmentID=treatment.treatmentID  where treatmentPlanID=@TreatmentPlanID;", db);
                selectCommand.Parameters.AddWithValue("@TreatmentPlanID", id);

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
                    string name = query.GetString(9);


                    treatmentList.Add(new TreatmentOnPlan(
                    TreatmentPlanTreatmentsID,
                    TreatmentPlanID,
                    TreatmentID,
                    price,
                     date,
                    toothNum,
                    comments,
                    isDone,
                    name
                    ));
                }

                db.Close();
            }

            return treatmentList;

        }


        /// <summary>
        /// Get user ID
        /// </summary>
        /// <returns></returns>
        public static string GetUserID()
        {

            string UserID = "";
            User user = null;

            using (SqliteConnection db =
                new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from users", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {

                    user = new User(
                    query.GetString(0),
                    query.GetString(1),
                    UserID = query.GetString(2)

                 );

                }

                db.Close();
            }

            return UserID;
        }


        /// <summary>
        /// Update treatmentPlanTreatments
        /// </summary>
        /// <param name="treatmentPlanTreatmentsID"></param>
        /// <param name="treatmentPlanID"></param>
        /// <param name="treatmentID"></param>
        /// <param name="price"></param>
        /// <param name="completedDate"></param>
        public void UpdateTreatmentOnPlan(TreatmentOnPlan t)
        {
            int isDone = (t.IsDone) ? 1 : 0;
            using (SqliteConnection db =
               new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "UPDATE treatmentPlanTreatments SET treatmentPlanID =@TreatmentPlanID, treatmentID =@TreatmentID, price =@Price, treatmentCompleteDate =@CompletedDate,tooth = @Tooth,comment = @Comment, isdone =@IsDone  where treatmentPlanTreatmentsID=@TreatmentPlanTreatmentsID;";
                insertCommand.Parameters.AddWithValue("@TreatmentPlanTreatmentsID", t.TreatmentPlanTreatmentsID);
                insertCommand.Parameters.AddWithValue("@TreatmentPlanID", t.TreatmentPlanID);
                insertCommand.Parameters.AddWithValue("@TreatmentID", t.TreatmentID);
                insertCommand.Parameters.AddWithValue("@Price", t.Price);
                insertCommand.Parameters.AddWithValue("@CompletedDate", t.CompletedDate);
                insertCommand.Parameters.AddWithValue("@Tooth", t.Tooth);
                insertCommand.Parameters.AddWithValue("@Comment", t.Comment);
                insertCommand.Parameters.AddWithValue("@IsDone", isDone);

                insertCommand.ExecuteNonQuery();
                
                db.Close();
            }
        }

        public long AddAppointment(Appointment appointment)
        {
            long id;
            using (SqliteConnection db =
              new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query id INTEGER,patientID INTEGER NOT NULL,date DATETIME,status INTEGER NOT NULL
                insertCommand.CommandText = "INSERT INTO appointment (patientId,date,status) VALUES " +
                    "(@PatientId,@Date,@Status);";
                insertCommand.Parameters.AddWithValue("@PatientId", appointment.PatientID);
                insertCommand.Parameters.AddWithValue("@Date", appointment.Date);
                insertCommand.Parameters.AddWithValue("@Status", appointment.Status);


                insertCommand.ExecuteReader();

                SqliteCommand insertCommand1 = new SqliteCommand();
                insertCommand1.Connection = db;

                string sql = @"select last_insert_rowid()";
                insertCommand1.CommandText = sql;
                id = (long)insertCommand1.ExecuteScalar();

                db.Close();
            }
            return id;
        }

        public static List<Appointment> GetAppointmetsWeek(DateTime startingDay)
        {
            List<Appointment> aps = new List<Appointment>();
            DateTime endDate = startingDay.AddDays(7);

            using (SqliteConnection db =
                new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from Appointment where Date>=@StartingDay AND Date<@EndDate", db);
                selectCommand.Parameters.AddWithValue("@StartingDay", startingDay);
                selectCommand.Parameters.AddWithValue("@EndDate", endDate);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    aps.Add(new Appointment(
                        query.GetInt32(0),
                        query.GetString(1),
                        query.GetDateTime(2),
                        ((AppointmentStatus)query.GetInt32(3))
                        
                        ));
                   

                }

                db.Close();
            }

            return aps;
        }

        public static Appointment GetAppointmentByID(int id)
        {
            Appointment appointment = null;

            using (SqliteConnection db =
                new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from Appointment where id=@appointmentID", db);
                selectCommand.Parameters.AddWithValue("@appointmentID", id);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {

                    appointment = new Appointment(
                        query.GetInt32(0),
                        query.GetString(1),
                        query.GetDateTime(2),
                        (AppointmentStatus)query.GetInt32(3)


                     );
                   
                }

                db.Close();
            }

            return appointment;
        }

        public static void UpdateAppointment(Appointment appointment)
        {
          
            using (SqliteConnection db =
               new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "UPDATE Appointment SET patientId=@PatientId ,date=@Date ,status=@Status where id=@ID;";
                insertCommand.Parameters.AddWithValue("@ID", appointment.ID);
                insertCommand.Parameters.AddWithValue("@PatientId", appointment.PatientID);
                insertCommand.Parameters.AddWithValue("@Date", appointment.Date);
                insertCommand.Parameters.AddWithValue("@Status", appointment.Status);

                insertCommand.ExecuteNonQuery();



                db.Close();
            }
        }

        public static decimal GetTotalMonth(DateTime date)
        {
          
            decimal result;
            using (SqliteConnection db =
                new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query
                insertCommand.CommandText = "select sum(amount) from payments WHERE CAST(strftime('%m',treatmentCompleteDate) as INTEGER)= @Month";
                insertCommand.Parameters.AddWithValue("@Month", date.Month);


                SqliteDataReader query = insertCommand.ExecuteReader();

                query.Read();
                result = query.GetDecimal(0) ;


                db.Close();
            }

            return result;

        }
    }
}