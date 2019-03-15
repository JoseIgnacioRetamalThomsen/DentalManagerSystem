using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary
{
    public class DataAccessService
    {
        FireBaseDAO fireDAO = new FireBaseDAO();

        string userID = "email";

        public void AddNewTreatment(Treatment treatment)
        {
            // add to local database

           long number = DAO.AddNewTreatment(treatment.Name, treatment.Price);

            treatment.ID = (int)number;
            fireDAO.AddNewTreatment(treatment);

            // add tpo firebase
        }
    }
}
