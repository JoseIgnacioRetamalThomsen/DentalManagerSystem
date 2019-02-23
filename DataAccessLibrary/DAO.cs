﻿using System;
using System.Collections.Generic;
using System.Text;
using Models;
using Microsoft.Data.Sqlite;
using System.Diagnostics;

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

                String customers = "CREATE TABLE IF NOT EXISTS customers(  customerID varchar(20)NOT NULL, firstName varchar(30)NOT NULL,surname varchar(30)NOT NULL,DOB varchar(12),street varchar(45),city varchar(30),province varchar(30),country varchar(15),postcode varchar(15),mobileNum varchar(15),fixNum varchar(15),email varchar(45),comments varchar(500),PRIMARY KEY (customerID))";
                SqliteCommand createCustomers = new SqliteCommand(customers, db);
                createCustomers.ExecuteReader();

                String treatment = "CREATE TABLE IF NOT EXISTS treatment(treatmentID INTEGER, treatmentName varchar(50) NOT NULL,price float,PRIMARY KEY (treatmentID))";
                SqliteCommand createTreatment = new SqliteCommand(treatment, db);
                createTreatment.ExecuteReader();

                String treatmentPlan = "CREATE TABLE IF NOT EXISTS treatmentPlan( treatmentPlanID INTEGER,customerID varchar(20)NOT NULL, state INTEGER NOT NULL,creationDate DATETIME DEFAULT CURRENT_TIMESTAMP,treatmentPlanCompleteDate DATETIME,PRIMARY KEY (treatmentPlanID),FOREIGN KEY (customerID) REFERENCES customers(customerID))";
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
        public static bool AddNewCustomer(string id, string firstName, string surname, string dOB, string street, string city, string province, string country, string postcode, string mobileNum, string fixNum, string email, string comments)
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
                    query.GetString(0),
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
                    query.GetString(0),
                    query.GetString(1),
                    query.GetDecimal(2)


                 );
                    //treatment.Print();
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
        public static long AddNewTreatmentPlan(string customerID, int state, string creationDate, string treatmentPlanCompleteDate)
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
                    if(!date2.Equals("0"))
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
                    datetime2
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
                    customer.Print();
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
        public static void UpdateCustomer(string customerID, string firstName, string surname, string dOB, string street, string city, string province, string country, string postcode, string mobileNum, string fixNum, string email, string comments)
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
        public static void UpdateTreatment(Treatment treatment)
        {
            using (SqliteConnection db =
               new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "UPDATE treatment set treatmentName = @Name, price = @Price where treatmentID = @Id;";
                insertCommand.Parameters.AddWithValue("@Name", treatment.name);
                insertCommand.Parameters.AddWithValue("@Price", treatment.price);
                insertCommand.Parameters.AddWithValue("@Id", treatment.iD);

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

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from treatmentPlanTreatments where treatmentPlanID=@TreatmentPlanID", db);
                selectCommand.Parameters.AddWithValue("@TreatmentPlanID", id);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    int TreatmentPlanTreatmentsID = query.GetInt32(0);
                    int TreatmentPlanID = query.GetInt32(1);
                    int TreatmentID = query.GetInt32(2);
                    decimal price = query.GetDecimal(2);

                    treatmentList.Add(new TreatmentOnPlan(
                    TreatmentPlanTreatmentsID,
                    TreatmentPlanID,
                    TreatmentID,
                    price,
                    new DateTime()
                    ));
                }

                db.Close();
            }

            return treatmentList;
 
        }

        /// <summary>
        /// Update treatmentPlanTreatments
        /// </summary>
        /// <param name="treatmentPlanTreatmentsID"></param>
        /// <param name="treatmentPlanID"></param>
        /// <param name="treatmentID"></param>
        /// <param name="price"></param>
        /// <param name="completedDate"></param>
        public static void UpdateTreatmentOnPlan(int treatmentPlanTreatmentsID, int treatmentPlanID, int treatmentID, decimal price, DateTime completedDate)
        {
            using (SqliteConnection db =
               new SqliteConnection("Filename=dentalManagerDB.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "UPDATE treatmentPlanTreatments SET treatmentPlanID =@TreatmentPlanID, treatmentID =@TreatmentID, price =@Price, completedDate =@CompletedDate  where treatmentPlanTreatmentsID=@TreatmentPlanTreatmentsID;";
                insertCommand.Parameters.AddWithValue("@TreatmentPlanTreatmentsID", treatmentPlanTreatmentsID);
                insertCommand.Parameters.AddWithValue("@TreatmentPlanID", treatmentPlanID);
                insertCommand.Parameters.AddWithValue("@TreatmentID", treatmentID);
                insertCommand.Parameters.AddWithValue("@Price", price);
                insertCommand.Parameters.AddWithValue("@CompletedDate", completedDate);

                insertCommand.ExecuteNonQuery();

                db.Close();
            }
        }
    }
}
