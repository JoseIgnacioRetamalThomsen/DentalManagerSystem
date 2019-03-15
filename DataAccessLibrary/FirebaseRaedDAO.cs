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
        private const String databaseUrl = "https://unlockpincode-d448d.firebaseio.com/";
        private const String databaseSecret = "gOjxFlBGP1v8vufauNkO9VqnH5PiEwltVATNdUey";
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

            string tableName = "mark" + "Treatments" + "/";

            var results = await firebase.Child(tableName).OnceAsync<TreatmentData>();
            foreach (var t in results)
            {
             
                   System.Diagnostics.Debug.WriteLine("Test "+ t.Object.iD);
            }


        }
    }
}
