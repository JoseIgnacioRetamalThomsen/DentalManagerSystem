using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

using System.Collections.Generic;


namespace DataAccessLibrary
{
    public static class DAO
    {
        /// <summary>
        /// Create a database and tables
        /// </summary>
        public static void InitializeDatabase()
        {

            using (SqliteConnection db = new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                String customers = "CREATE TABLE IF NOT EXISTS customers(  customerID varchar(20)NOT NULL, firstName varchar(30)NOT NULL,surname varchar(30)NOT NULL,DOB varchar(12),street varchar(45),city varchar(30),province varchar(30),country varchar(15),postcode varchar(15),mobileNum varchar(15),fixNum varchar(15),email varchar(45),PRIMARY KEY (customerID))";
                SqliteCommand createCustomers = new SqliteCommand(customers, db);
                createCustomers.ExecuteReader();

                String treatment = "CREATE TABLE IF NOT EXISTS treatment(treatmentID INTEGER, treatmentName varchar(50) NOT NULL,price float,PRIMARY KEY (treatmentID))";
                SqliteCommand createTreatment = new SqliteCommand(treatment, db);
                createTreatment.ExecuteReader();

                String treatmentPlan = "CREATE TABLE IF NOT EXISTS treatmentPlan( treatmentPlanID INTEGER,customerID varchar(20)NOT NULL, state varchar(30)NOT NULL,creationDate DATETIME DEFAULT CURRENT_TIMESTAMP,treatmentPlanCompleteDate DATETIME,PRIMARY KEY (treatmentPlanID),FOREIGN KEY (customerID) REFERENCES customers(customerID))";
                SqliteCommand createTreatmentPlan = new SqliteCommand(treatmentPlan, db);
                createTreatmentPlan.ExecuteReader();


                String treatmentPlanTreatments = "CREATE TABLE IF NOT EXISTS treatmentPlanTreatments( treatmentPlanTreatmentsID INTEGER,treatmentPlanID int,treatmentID int, price float,treatmentCompleteDate DATETIME,PRIMARY KEY (treatmentPlanTreatmentsID),FOREIGN KEY (treatmentPlanID) REFERENCES treatmentPlan(treatmentPlanID),FOREIGN KEY (treatmentID) REFERENCES treatment(treatmentID))";
                SqliteCommand createTreatmentPlanTreatments = new SqliteCommand(treatmentPlanTreatments, db);
                createTreatmentPlanTreatments.ExecuteReader();

                String payments = "CREATE TABLE IF NOT EXISTS payments(paymentsID INTEGER,treatmentPlanID int,customerID varchar(20)NOT NULL,amount float,treatmentCompleteDate DATETIME,PRIMARY KEY (paymentsID),FOREIGN KEY (treatmentPlanID) REFERENCES treatmentPlan(treatmentPlanID),FOREIGN KEY (customerID) REFERENCES customers(customerID))";
                SqliteCommand createPayments = new SqliteCommand(payments, db);
                createPayments.ExecuteReader();
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
        public static bool AddNewCustomer(string id, string firstName, string surname, string dOB, string street, string city, string province, string country, string postcode, string mobileNum, string fixNum, string email)
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
                    insertCommand.CommandText = "INSERT INTO customers VALUES (@Id ,@FirstName ,@Surname,@DOB ,@Street ,@city ,@Province ,@Country ,@Postocode ,@MobileNum ,@FixNum ,@Email);";
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
        public static void AddNewTreatment(string treatmentName, Decimal price)
        {
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

        /// <summary>
        /// Insert a new treatment plan
        /// </summary>
        /// <param name="treatmentPlanID"></param>
        /// <param name="customerID"></param>
        /// <param name="state"></param>
        /// <param name="creationDate"></param>
        /// <param name="treatmentPlanCompleteDate"></param>
        public static void AddNewTreatmentPlan( string customerID, string state, string creationDate, string treatmentPlanCompleteDate)
        {
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

                db.Close();
            }
        }

        /// <summary>
        /// Insert a new treatment plan treatments
        /// </summary>
        /// <param name="treatmentPlanTreatmentsID"></param>
        /// <param name="treatmentPlanID"></param>
        /// <param name="treatmentID"></param>
        /// <param name="price"></param>
        /// <param name="treatmentCompleteDate"></param>
        public static void AddNewTreatmentPlanTreatments(int treatmentPlanID, int treatmentID, Decimal price, string treatmentCompleteDate)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query
                insertCommand.CommandText = "INSERT INTO treatmentPlanTreatments (TreatmentPlanID,TreatmentID,Price,TreatmentCompleteDate) VALUES (@TreatmentPlanID,@TreatmentID,@Price,@TreatmentCompleteDate);";

                insertCommand.Parameters.AddWithValue("@TreatmentPlanID", treatmentPlanID);
                insertCommand.Parameters.AddWithValue("@TreatmentID", treatmentID);
                insertCommand.Parameters.AddWithValue("@Price", price);
                insertCommand.Parameters.AddWithValue("@TreatmentCompleteDate", treatmentCompleteDate);

                insertCommand.ExecuteReader();

                db.Close();
            }
        }

        /// <summary>
        /// Insert a new payment
        /// </summary>
        /// <param name="treatmentPlanID"></param>
        /// <param name="customerID"></param>
        /// <param name="amount"></param>
        /// <param name="treatmentCompleteDate"></param>
        public static void AddNewpayment(int treatmentPlanID, string customerID, float amount, string treatmentCompleteDate)
        {
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

                db.Close();
            }
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
    }
}
