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
 

    public  class FireBaseDAO
    {

        /// <summary>
        /// Firebase authetication parameters
        /// </summary>
        private const String databaseUrl = "https://unlockpincode-d448d.firebaseio.com/";
        private const String databaseSecret = "gOjxFlBGP1v8vufauNkO9VqnH5PiEwltVATNdUey";
        private FirebaseClient firebase;


        /// <summary>
        /// Establish connection with Firebase
        /// </summary>
        public  void ConnectToFirebase()
        {
            this.firebase = new FirebaseClient(

                 databaseUrl,
                 new FirebaseOptions
                 {
                     AuthTokenAsyncFactory = () => Task.FromResult(databaseSecret)
                 });
        }


        /// <summary>
        /// Add a new treatment to Firebae
        /// </summary>
        /// <param name="treatmentName"></param>
        /// <param name="price"></param>
            public async void AddNewTreatment(Treatment treatment)
        {
            
            ConnectToFirebase();

            String node = "marko" + "Treatments" + "/";

            TreatmentData treatmentData = new TreatmentData
            {
                iD= treatment.iD,
                name = treatment.name,
                price = treatment.price
            };

            //Create a new row  with the updated values
            await firebase.Child(node).Child(treatment.iD.ToString).PostAsync<TreatmentData>(treatmentData);
        }

        /// <summary>
        /// Update the Treatment  table by row id
        /// </summary>
        /// <param name="treatment"></param>
        public async void UpdateTreatment(Treatment treatment)
        {
            ConnectToFirebase();

            String node = "marko" + "Treatments" + "/";

         TreatmentData treatmentData = new TreatmentData
             {
                 iD = treatment.iD,
                 name = treatment.name,
                 price = treatment.price
             };

         //Delete the old row by Id
          await firebase.Child(node).Child(treatment.iD.ToString).DeleteAsync();

          //Create a new row  with the updated values
         await firebase.Child(node).Child(treatment.iD.ToString).PostAsync<TreatmentData>(treatmentData);
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

            String node = "marko" + "Customers" + "/";
            var customerData = new CustomerData
            {
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

            var results = await firebase.Child(node).Child(id+"/").OnceAsync<CustomerData>();
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
                await firebase.Child(node).Child(id.ToString).PostAsync<CustomerData>(customerData);
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

            String node = "marko" + "Customers" + "/";
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

                //Delete the old row by Id
                await firebase.Child(node).Child(customerID).DeleteAsync();

                //Add the new customer row
               await firebase.Child(node).Child(customerID).PostAsync<CustomerData>(customerData);
        }


        /// <summary>
        /// Add a new treatment plan to Firebase
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="state"></param>
        /// <param name="creationDate"></param>
        /// <param name="treatmentPlanCompleteDate"></param>
        public async void AddNewTreatmentPlan(string customerID, int state, string creationDate, string treatmentPlanCompleteDate)
        {
            ConnectToFirebase();

            String node = "marko" + "TreatmentPlans" + "/";
            var treatmentPlanData = new TreatmentPlanData
            {
              customerID= customerID,
              state= state,
              creationDate = creationDate,
              treatmentPlanCompleteDate= treatmentPlanCompleteDate
            };

            await firebase.Child(node).Child(customerID.ToString).PostAsync<TreatmentPlanData>(treatmentPlanData);
        }


        /// <summary>
        /// Add a new TreatmentPlanTreatments to Firebase
        /// </summary>
        /// <param name="treatmentPlanID"></param>
        /// <param name="treatmentID"></param>
        /// <param name="price"></param>
        /// <param name="treatmentCompleteDate"></param>
        public async void AddNewTreatmentPlanTreatments(TreatmentOnPlan t)
        {
            ConnectToFirebase();

            String node = "marko" + "TreatmentPlanTreatments" + "/";

            var treatmentPlanTreatmentsData = new TreatmentPlanTreatmentsData
            {
                treatmentPlanTreatmentsID = t.TreatmentPlanTreatmentsID.ToString(),
                treatmentPlanID = t.TreatmentPlanID,
                treatmentID = t.TreatmentID,
                price = t.Price,
                treatmentCompleteDate = t.CompletedDate.ToString()
            };

            await firebase.Child(node).Child(t.TreatmentPlanTreatmentsID.ToString).PostAsync<TreatmentPlanTreatmentsData>(treatmentPlanTreatmentsData);
        }

        /// <summary>
        /// Update the TreatmentOnPlan in the Firebase
        /// </summary>
        /// <param name="t"></param>
        public async void UpdateTreatmentOnPlan(TreatmentOnPlan t)
        {
            ConnectToFirebase();

            String node = "marko" + "TreatmentPlanTreatments" + "/";

            var treatmentPlanTreatmentsData = new TreatmentPlanTreatmentsData
            {
                treatmentPlanTreatmentsID = t.TreatmentPlanTreatmentsID.ToString(),
                treatmentPlanID = t.TreatmentPlanID,
                treatmentID = t.TreatmentID,
                price = t.Price,
                treatmentCompleteDate = t.CompletedDate.ToString()
            };


            //Delete the old row by Id
             await firebase.Child(node).Child(t.TreatmentPlanTreatmentsID.ToString()).DeleteAsync();

            //add the new row
            await firebase.Child(node).Child(t.TreatmentPlanTreatmentsID.ToString).PostAsync<TreatmentPlanTreatmentsData>(treatmentPlanTreatmentsData);
        }

        /// <summary>
        ///Add new Payment to Firebase
        /// </summary>
        /// <param name="treatmentPlanID"></param>
        /// <param name="customerID"></param>
        /// <param name="amount"></param>
        /// <param name="treatmentCompleteDate"></param>
        public async void AddNewpayment(int treatmentPlanID, string customerID, decimal amount, string treatmentCompleteDate)
        {
            ConnectToFirebase();

            String node = "marko" + "Payments" + "/";

            var paymentsData = new PaymentsData
            {
                treatmentPlanID = treatmentPlanID,
                customerID = customerID,
                amount = amount,
                treatmentCompleteDate = treatmentCompleteDate
            };

            await firebase.Child(node).PostAsync<PaymentsData>(paymentsData);
        }

    }
}
