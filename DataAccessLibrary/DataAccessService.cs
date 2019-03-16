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


        //Edit Treatment is in EditTreatmentViewModel

        //Add new Custermer is in NewCustomer.xaml.cs 
        //Edit Customer is in EditCustomerDetails.xaml.cs

        //Add new  Treatment Plan(AddNewTreatmentPlan) table is in NewTreatmentPlanViewModel.cs
        //Update Treatment Plan(UpdateTreatmentPlanState) table is in TreatmentPlanViewModel

        //Add new TreatmentPlanTreatments(AddNewTreatmentPlanTreatments) is in NewTreatmentPlanViewModel.cs
        //Update TreatmentPlanTreatments(UpdateTreatmentOnPlan) is in TreatmentPlanView

        //Add new Payment(AddNewpayment) is in NewPaymentView
    }
}
