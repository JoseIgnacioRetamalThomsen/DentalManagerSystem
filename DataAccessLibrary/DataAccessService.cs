using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace DataAccessLibrary
{
    public class DataAccessService
    {
        FireBaseDAO firebaseDAO = new FireBaseDAO();
        private DAO sqlite = new DAO();


        public void AddNewTreatment(Treatment treatment)
        {

           long number = sqlite.AddNewTreatment(treatment.Name, treatment.Price);


            treatment.ID = (int)number;
            firebaseDAO.AddNewTreatment(treatment);
        }

        public void UpdateTreatment(int iD, string text, decimal v)
        {
            sqlite.UpdateTreatment(new Treatment(iD, text, v));

            firebaseDAO.UpdateTreatment(new Treatment(iD, text, v));

        }

        public bool NewCustomerDetails(string idInput, string inputName, string inputSurename, string DOB, string streetInput, string cityInput, string provinceInput, string countryInput, string postCodeInput, string mobilNumInput, string homeNumInput, string emaillInput, string commentsInput)
        {
            bool temp = false;
            temp = sqlite.AddNewCustomer(idInput, inputName, inputSurename, DOB, streetInput, cityInput, provinceInput, countryInput, postCodeInput, mobilNumInput, homeNumInput, emaillInput, commentsInput);

            firebaseDAO.AddNewCustomer(idInput, inputName, inputSurename, DOB, streetInput, cityInput, provinceInput, countryInput, postCodeInput, mobilNumInput, homeNumInput, emaillInput, commentsInput);



            return temp;
        }


        //Edit Customer is in EditCustomerDetails.xaml.cs
        public void UpdateCustomerDetails( string idInput, string inputName, string inputSurename, string DOB, string streetInput, string cityInput, string provinceInput, string countryInput, string postCodeInput, string mobilNumInput, string homeNumInput, string emaillInput, string commentsInput)
        {
            sqlite.UpdateCustomer(idInput, inputName, inputSurename, DOB, streetInput, cityInput, provinceInput, countryInput, postCodeInput, mobilNumInput, homeNumInput, emaillInput, commentsInput);

            firebaseDAO.UpdateCustomer(idInput,inputName,inputSurename,DOB,streetInput,cityInput,provinceInput,countryInput,postCodeInput,mobilNumInput,homeNumInput,emaillInput,commentsInput);


        }

        //Update Treatment Plan(UpdateTreatmentPlanState) table is in TreatmentPlanViewModel
        public void UpdateTreatmentPlanState(TreatmentPlaneState state, int PlanID)
        {
            sqlite.UpdateTreatmentPlanState(state, PlanID);

            firebaseDAO.UpdateTreatmentPlanState(state, PlanID);
        }




        //Update TreatmentPlanTreatments(UpdateTreatmentOnPlan) is in TreatmentPlanView
        public void UpdateTreatmentOnPlan(TreatmentOnPlan top)
        {
            sqlite.UpdateTreatmentOnPlan(top);

            firebaseDAO.UpdateTreatmentOnPlan(top);

        }

        //Add new Payment(AddNewpayment) is in NewPaymentView
        public void AddNewpayment(int treatmentPlanID, string customerID, decimal amount, string treatmentCompleteDate)
        {
            sqlite.AddNewpayment(treatmentPlanID, customerID, amount, treatmentCompleteDate);
            firebaseDAO.AddNewpayment(treatmentPlanID, customerID, amount, treatmentCompleteDate);
        }

        public long AddNewTreatmentPlan(string customerID, int state, string creationDate, string treatmentPlanCompleteDate)
        {
            // add to sql
            int id = (int)sqlite.AddNewTreatmentPlan(customerID, state, creationDate, treatmentPlanCompleteDate);

            //add to firebase
            firebaseDAO.AddNewTreatmentPlan(id, customerID, state, creationDate, treatmentPlanCompleteDate);

            return id;
        }

        public  void AddNewTreatmentPlanTreatments(TreatmentOnPlan t)
        {
            sqlite.AddNewTreatmentPlanTreatments(t);

            firebaseDAO.AddNewTreatmentPlanTreatments(t);
        }
    }
}
