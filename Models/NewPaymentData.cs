using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class NewPaymentData
    {
        public string CustomerId { get; set; }
        public int TreatmentId { get; set; }
        public bool IsTreatmentID { get; set; } = false;

        public NewPaymentData(string customerId, int treatmentId, bool isTreatmentID)
        {
            CustomerId = customerId;
            TreatmentId = treatmentId;
            IsTreatmentID = isTreatmentID;
        }
    }

}
