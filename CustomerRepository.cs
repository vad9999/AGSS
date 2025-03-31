using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGSS.Entities;

namespace AGSS
{
    public static class CustomerRepository
    {
        public static int GetCustomerID(string login, string password, GravitySurveyOnDeleteNoAction context)
        {
            return context.Customers.FirstOrDefault(c => c.Login == login && c.Password == password).CustomerId;
        }
    }
}
