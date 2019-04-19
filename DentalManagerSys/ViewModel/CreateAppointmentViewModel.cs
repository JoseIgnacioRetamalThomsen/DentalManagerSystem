using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary;
using Models;

namespace DentalManagerSys.ViewModel
{
    public class CreateAppointmentViewModel
    {
        /// <summary>
        /// Customer who made payment
        /// </summary>
        public Customer Customer { get; set; } = new Customer();

        public string CustomerName
        {
            get
            {
                return Customer.name + " " + Customer.surname;
            }
        }

        public int CustomerID { get; set; }

        public void SetCustomer(string id)
        {
            Customer = DAO.GetCustomerByID(id);
        }
    }
}
