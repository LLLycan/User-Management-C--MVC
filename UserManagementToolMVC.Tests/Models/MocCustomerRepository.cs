using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementToolMVC.DBContext;

namespace UserManagementToolMVC.Tests.Models
{
    class InMemoryCustomerRepository : UserManagementToolMVC.Models.ICustomerRepository
    {
        private List<tblCustomer> _db = new List<tblCustomer>();

        public Exception ExceptionToThrow { get; set; }

        public void SaveChanges(tblCustomer customerToUpdate)
        {

            foreach (tblCustomer customer in _db)
            {
                if (customer.CustomerId == customerToUpdate.CustomerId)
                {
                    _db.Remove(customer);
                    _db.Add(customerToUpdate);
                    break;
                }
            }
        }

        public void Add(tblCustomer customerToAdd)
        {
            _db.Add(customerToAdd);
        }

        public tblCustomer GetCustomerByID(int id)
        {
            return _db.FirstOrDefault(d => d.CustomerId == id);
        }

        public void CreateNewCustomer(tblCustomer customerToCreate)
        {
            if (ExceptionToThrow != null)
                throw ExceptionToThrow;

            _db.Add(customerToCreate);
        }

        public int SaveChanges()
        {
            return 1;
        }

        public IEnumerable<tblCustomer> GetAllCustomers()
        {
            return _db.ToList();
        }


        public void DeleteCustomer(int id)
        {
            _db.Remove(GetCustomerByID(id));
        }

        public void AddNew(tblCustomer customer)
        {
            throw new NotImplementedException();
        }

        public void Del(int id)
        {
            throw new NotImplementedException();
        }

        public tblCustomer Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tblCustomer> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
