﻿namespace DataAccessLibrary
{
    internal class TreatmentPlanTreatmentsData
    {
        public string treatmentPlanTreatmentsID { get; set; }
        public int treatmentPlanID { get; set; }
        public int treatmentID { get; set; }
        public decimal price { get; set; }
        public string treatmentCompleteDate { get; set; }
        public int tooth { get; set; }
        public string comment { get; set; }
        public bool isdone { get; set; }
    }
}