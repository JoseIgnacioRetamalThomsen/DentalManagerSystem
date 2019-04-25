using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace DataAccessLibrary
{
    /// <summary>
    /// Provide acces for data for sqlite and firebase batabases
    /// All writes are done to firebase and sqlite db
    /// Reads are only done from sqlite db 
    /// </summary>
    public class DataAccessService
    {
        FireBaseDAO firebaseDAO = new FireBaseDAO();
        public DAO sqlite = new DAO();

        //Keep count for Sychronization
        public static int DAOCount;
        public static int FBCount;

        public bool IsWorkOffline { get; set; } = false;

        public DataAccessService(bool isWorkOffline)
        {
            IsWorkOffline = isWorkOffline;
        }

        public void AddNewTreatment(Treatment treatment)
        {
            //local
            //Keep count for Sychronization
            long number = sqlite.AddNewTreatment(treatment.Name, treatment.Price);
            DAOCount++;
            sqlite.UpdateCountRecordSQlite(DAOCount);

            //firease
            if (!IsWorkOffline)
            {
                FBCount++;
                firebaseDAO.UpdateCountRecordFB(FBCount);
                treatment.ID = (int)number;
                firebaseDAO.AddNewTreatment(treatment);
            }

        }

        public void UpdateTreatment(int iD, string text, decimal v)
        {
            //Keep count for Sychronization
            DAOCount++;
            sqlite.UpdateCountRecordSQlite(DAOCount);
            sqlite.UpdateTreatment(new Treatment(iD, text, v));

            //firease
            if (!IsWorkOffline)
            {
                FBCount++;
                firebaseDAO.UpdateCountRecordFB(FBCount);
                firebaseDAO.UpdateTreatment(new Treatment(iD, text, v));
            }


        }

        public bool NewCustomerDetails(string idInput, string inputName, string inputSurename, string DOB, string streetInput, string cityInput, string provinceInput, string countryInput, string postCodeInput, string mobilNumInput, string homeNumInput, string emaillInput, string commentsInput)
        {
            //Keep count for Sychronization
            DAOCount++;
            sqlite.UpdateCountRecordSQlite(DAOCount);
            bool temp = false;
            temp = sqlite.AddNewCustomer(idInput, inputName, inputSurename, DOB, streetInput, cityInput, provinceInput, countryInput, postCodeInput, mobilNumInput, homeNumInput, emaillInput, commentsInput);

            //firease
            if (!IsWorkOffline)
            {
                FBCount++;
                firebaseDAO.UpdateCountRecordFB(FBCount);
                firebaseDAO.AddNewCustomer(idInput, inputName, inputSurename, DOB, streetInput, cityInput, provinceInput, countryInput, postCodeInput, mobilNumInput, homeNumInput, emaillInput, commentsInput);
            }


            return temp;
        }


        //Edit Customer is in EditCustomerDetails.xaml.cs
        public void UpdateCustomerDetails(string idInput, string inputName, string inputSurename, string DOB, string streetInput, string cityInput, string provinceInput, string countryInput, string postCodeInput, string mobilNumInput, string homeNumInput, string emaillInput, string commentsInput)
        {
            //Keep count for Sychronization
            DAOCount++;
            sqlite.UpdateCountRecordSQlite(DAOCount);
            sqlite.UpdateCustomer(idInput, inputName, inputSurename, DOB, streetInput, cityInput, provinceInput, countryInput, postCodeInput, mobilNumInput, homeNumInput, emaillInput, commentsInput);

            //firease
            if (!IsWorkOffline)
            {
                FBCount++;
                firebaseDAO.UpdateCountRecordFB(FBCount);
                firebaseDAO.UpdateCustomer(idInput, inputName, inputSurename, DOB, streetInput, cityInput, provinceInput, countryInput, postCodeInput, mobilNumInput, homeNumInput, emaillInput, commentsInput);
            }
        }

        //Update Treatment Plan(UpdateTreatmentPlanState) table is in TreatmentPlanViewModel
        public void UpdateTreatmentPlanState(TreatmentPlaneState state, int PlanID)
        {
            //Keep count for Sychronization
            DAOCount++;
            sqlite.UpdateCountRecordSQlite(DAOCount);
            sqlite.UpdateTreatmentPlanState(state, PlanID);

            //firease
            if (!IsWorkOffline)
            {
                FBCount++;
                firebaseDAO.UpdateCountRecordFB(FBCount);
                firebaseDAO.UpdateTreatmentPlanState(state, PlanID);
            }
        }




        //Update TreatmentPlanTreatments(UpdateTreatmentOnPlan) is in TreatmentPlanView
        public void UpdateTreatmentOnPlan(TreatmentOnPlan top)
        {
            //Keep count for Sychronization
            DAOCount++;

            sqlite.UpdateCountRecordSQlite(DAOCount);


            sqlite.UpdateTreatmentOnPlan(top);


            //firease
            if (!IsWorkOffline)
            {
                FBCount++;
                firebaseDAO.UpdateCountRecordFB(FBCount);
                firebaseDAO.UpdateTreatmentOnPlan(top);
            }
        }

        //Add new Payment(AddNewpayment) is in NewPaymentView
        public void AddNewpayment(int treatmentPlanID, string customerID, decimal amount, string treatmentCompleteDate)
        {
            //Keep count for Sychronization
            DAOCount++;

            sqlite.UpdateCountRecordSQlite(DAOCount);

            long paymentId = sqlite.AddNewpayment(treatmentPlanID, customerID, amount, treatmentCompleteDate);



            //firease
            if (!IsWorkOffline)
            {
                FBCount++;
                firebaseDAO.UpdateCountRecordFB(FBCount);
                int paymentID = (int)paymentId;
                firebaseDAO.AddNewpayment(paymentID, treatmentPlanID, customerID, amount, treatmentCompleteDate);

            }
        }

        public long AddNewTreatmentPlan(string customerID, int state, string creationDate, string treatmentPlanCompleteDate)
        {
            //Keep count for Sychronization
            DAOCount++;

            sqlite.UpdateCountRecordSQlite(DAOCount);


            // add to sql
            int id = (int)sqlite.AddNewTreatmentPlan(customerID, state, creationDate, treatmentPlanCompleteDate);



            //firease
            if (!IsWorkOffline)
            {
                FBCount++;
                firebaseDAO.UpdateCountRecordFB(FBCount);
                //add to firebase
                firebaseDAO.AddNewTreatmentPlan(id, customerID, state, creationDate, treatmentPlanCompleteDate);
            }

            return id;
        }

        public void AddNewTreatmentPlanTreatments(TreatmentOnPlan t)
        {
            //Keep count for Sychronization
            DAOCount++;
            FBCount++;
            sqlite.UpdateCountRecordSQlite(DAOCount);
            firebaseDAO.UpdateCountRecordFB(FBCount);

            int id = (int)sqlite.AddNewTreatmentPlanTreatments(t);

            firebaseDAO.AddNewTreatmentPlanTreatments(t, id);
        }

        public long AddNewAppointment(Appointment appointment)
        {
            return sqlite.AddAppointment(appointment);
        }
    }
}


