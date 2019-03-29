using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using System.Text;

namespace DataAccessLibrary
{
    public class FirebaseRaedDAO
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
            this.firebase = new FirebaseClient(

                 databaseUrl,
                 new FirebaseOptions
                 {
                     AuthTokenAsyncFactory = () => Task.FromResult(databaseSecret)
                 });
        }


        public async void ReadTreatmentTable()
        {

            ConnectToFirebase();

            string userName = DAO.GetUserID();
            String myUsername = userName;
            myUsername = myUsername.Replace(".", "-");

            string tableName = myUsername + "Treatments" + "/";

            var results = await firebase.Child(tableName).OnceAsync<TreatmentData>();
            foreach (var t in results)
            {
             
               
            }


        }
    }
}
