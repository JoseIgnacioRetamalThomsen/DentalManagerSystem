﻿namespace DataAccessLibrary
{
    internal class PaymentsData
    {
        public int paymentID { get; set; }
        public int treatmentPlanID { get; set; }
        public string customerID { get; set; }
        public decimal amount { get; set; }
        public string treatmentCompleteDate { get; set; }
    }
}