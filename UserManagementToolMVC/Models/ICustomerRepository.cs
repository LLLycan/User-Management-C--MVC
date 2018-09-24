using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserManagementToolMVC.DBContext;

namespace UserManagementToolMVC.Models
{
    /// <summary>
    /// Interface for customer repository
    /// </summary>
    public interface ICustomerRepository
    {
        void CreateNewCustomer(tblCustomer customer);

        void DeleteCustomer(int id);

        tblCustomer GetCustomerByID(int id);

        IEnumerable<tblCustomer> GetAllCustomers();

        int SaveChanges();
    }
}