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


        public void AddNewTreatment(Treatment treatment)
        {

           long number = DAO.AddNewTreatment(treatment.Name, treatment.Price);

            treatment.ID = (int)number;
            firebaseDAO.AddNewTreatment(treatment);
        }

        public void UpdateTreatment(int iD, string text, decimal v)
        {
             firebaseDAO.UpdateTreatment(new Treatment(iD, text, v));
             DAO.UpdateTreatment(new Treatment(iD, text, v));
        }

        //Add new Custermer to Firebase is in NewCustomer.xaml.cs
        public void NewCustomerDetailsFb(string idInput, string inputName, string inputSurename, string DOB, string streetInput, string cityInput, string provinceInput, string countryInput, string postCodeInput, string mobilNumInput, string homeNumInput, string emaillInput, string commentsInput)
        {

            firebaseDAO.AddNewCustomer(idInput, inputName, inputSurename, DOB, streetInput, cityInput, provinceInput, countryInput, postCodeInput, mobilNumInput, homeNumInput, emaillInput, commentsInput);
        }

        //Add new Custermer to SQLite is in NewCustomer.xaml.cs
        public bool NewCustomerDetailsSQLite(string idInput, string inputName, string inputSurename, string DOB, string streetInput, string cityInput, string provinceInput, string countryInput, string postCodeInput, string mobilNumInput, string homeNumInput, string emaillInput, string commentsInput)
        {
            bool temp = false;
            temp = DAO.AddNewCustomer(idInput, inputName, inputSurename, DOB, streetInput, cityInput, provinceInput, countryInput, postCodeInput, mobilNumInput, homeNumInput, emaillInput, commentsInput);
            return temp;
        }

        //Edit Customer is in EditCustomerDetails.xaml.cs
        public void UpdateCustomerDetails( string idInput, string inputName, string inputSurename, string DOB, string streetInput, string cityInput, string provinceInput, string countryInput, string postCodeInput, string mobilNumInput, string homeNumInput, string emaillInput, string commentsInput)
        {

            firebaseDAO.UpdateCustomer(idInput,inputName,inputSurename,DOB,streetInput,cityInput,provinceInput,countryInput,postCodeInput,mobilNumInput,homeNumInput,emaillInput,commentsInput);

            DAO.UpdateCustomer(idInput, inputName, inputSurename, DOB, streetInput, cityInput, provinceInput, countryInput, postCodeInput, mobilNumInput, homeNumInput, emaillInput, commentsInput);
        }

        //Update Treatment Plan(UpdateTreatmentPlanState) table is in TreatmentPlanViewModel
        public void UpdateTreatmentPlanState(TreatmentPlaneState state, int PlanID)
        {
            DAO.UpdateTreatmentPlanState(state, PlanID);
            firebaseDAO.UpdateTreatmentPlanState(state, PlanID);
        }

        //Add new  Treatment Plan(AddNewTreatmentPlan) table is in NewTreatmentPlanViewModel.cs
        //Add new TreatmentPlanTreatments(AddNewTreatmentPlanTreatments) is in NewTreatmentPlanViewModel.cs
        //public void AddNewTreatmentPlan(string customerID, int state, string creationDate, string treatmentPlanCompleteDate)
        //{
        //    int id = (int)DAO.AddNewTreatmentPlan(customerID, state, creationDate, treatmentPlanCompleteDate);

        //    //Add new Treatmentplan to firebase.
        //    firebaseDAO.AddNewTreatmentPlan(id, customerID, state, creationDate, treatmentPlanCompleteDate);

        //    foreach (TreatmentOnPlan top in treatmentsOnPlan)
        //    {

        //        top.TreatmentPlanID = id;
        //        DAO.AddNewTreatmentPlanTreatments(top);
        //        firebaseDAO.AddNewTreatmentPlanTreatments(top);

        //    }
        //}


        //Update TreatmentPlanTreatments(UpdateTreatmentOnPlan) is in TreatmentPlanView
        public void UpdateTreatmentOnPlan(TreatmentOnPlan top)
        {
            DAO.UpdateTreatmentOnPlan(top);
            firebaseDAO.UpdateTreatmentOnPlan(top);

        }

        //Add new Payment(AddNewpayment) is in NewPaymentView
        public void AddNewpayment(int treatmentPlanID, string customerID, decimal amount, string treatmentCompleteDate)
        {
            DAO.AddNewpayment(treatmentPlanID, customerID, amount, treatmentCompleteDate);
            firebaseDAO.AddNewpayment(treatmentPlanID, customerID, amount, treatmentCompleteDate);
        }

        public long AddNewTreatmentPlan(string customerID, int state, string creationDate, string treatmentPlanCompleteDate)
        {
            // add to sql
            int id = (int)DAO.AddNewTreatmentPlan(customerID, state, creationDate, treatmentPlanCompleteDate);

            //add to firebase
            firebaseDAO.AddNewTreatmentPlan(id, customerID, state, creationDate, treatmentPlanCompleteDate);

            return id;
        }

        public  void AddNewTreatmentPlanTreatments(TreatmentOnPlan t)
        {
            DAO.AddNewTreatmentPlanTreatments(t);
            firebaseDAO.AddNewTreatmentPlanTreatments(t);
        }
    }
}
