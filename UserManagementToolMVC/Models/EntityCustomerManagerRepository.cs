using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserManagementToolMVC.DBContext;

namespace UserManagementToolMVC.Models
{
    /// <summary>
    /// Customer Repository manager class
    /// </summary>
 
    public class EntityCustomerManagerRepository
    {
        private CustomerEntities _db = new CustomerEntities();

        public tblCustomer GetCustomerByID(int id)
        {
            return _db.tblCustomers.FirstOrDefault(d => d.CustomerId == id);
        }

        public IEnumerable<tblCustomer> GetAllCustomers()
        {
            return _db.tblCustomers.ToList();
        }

        public void CreateNewCustomer(tblCustomer customer)
        {
            _db.tblCustomers.Add(customer);
            _db.SaveChanges();
        }

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        public void DeleteCustomer(int id)
        {
            var customerToDel = GetCustomerByID(id);
            _db.tblCustomers.Remove(customerToDel);
            _db.SaveChanges();
        }
    }
}