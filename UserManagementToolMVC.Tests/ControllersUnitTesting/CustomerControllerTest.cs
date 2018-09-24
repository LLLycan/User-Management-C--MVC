using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserManagementToolMVC.Controllers;
using UserManagementToolMVC.DBContext;
using Moq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Linq.Expressions;
using UserManagementToolMVC.Models;
using System.Web.Routing;
using System.Web;
using System.Security.Principal;
using UserManagementToolMVC.Tests.Models;

namespace UserManagementToolMVC.Tests.ControllersUnitTesting
{
    /// <summary>
    /// Mock Unit Testing for Customer Controller
    /// </summary>
    [TestClass]
    public class CustomerControllerTest
    {
        // Ininialize Custmer example
        tblCustomer GetCustomer()
        {
            return GetCustomer(1, "Janet", "Gates");
        }

        tblCustomer GetCustomer(int id, string fName, string lName)
        {
            return new tblCustomer
            {
                CustomerId = id,
                FirstName = fName,
                LastName = lName,
                Email = "janet1@adventure-works.com",
                DOB = DateTime.Now,
                CustCode = fName + lName + DateTime.Now.ToString()

            };
        }
        // Ininialize Mock Controller
        private static CustomerController GetCustomerController(ICustomerRepository repository)
        {
            CustomerController controller = new CustomerController(repository);

            controller.ControllerContext = new ControllerContext()
            {
                Controller = controller,
                RequestContext = new RequestContext(new MockHttpContext(), new RouteData())
            };
            return controller;
        }


        private class MockHttpContext : HttpContextBase
        {
            private readonly IPrincipal _user = new GenericPrincipal(
                     new GenericIdentity("someUser"), null);

            public override IPrincipal User
            {
                get
                {
                    return _user;
                }
                set
                {
                    base.User = value;
                }
            }
        }

        [TestMethod]
        public void Index_View_Name_Should_Empty()
        {
            // Arrange
            var controller = GetCustomerController(new InMemoryCustomerRepository());
            // Act
            var result = controller.Index() as ViewResult;
            // Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void Create_View_Name_Should_Correct()
        {
            // Arrange
            var controller = new CustomerController();

            //Act
            var result = controller.Create() as ViewResult;

            // Assert
            Assert.AreEqual("Create", result.ViewName);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create_View_Name_Different_If_Model_State_Is_Not_Valid()
        {
            // Arrange
            CustomerController controller = GetCustomerController(new InMemoryCustomerRepository());
            controller.ModelState.AddModelError("", "mock error message");
            tblCustomer model = GetCustomer(1, "", "");

            // Act
            var result = (ViewResult)controller.Create(model);

            // Assert
            Assert.AreNotEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void Index_Get_All_Validate_Customer_Into_Repository()
        {
            // Arrange
            InMemoryCustomerRepository repository = new InMemoryCustomerRepository();
            CustomerController controller = GetCustomerController(repository);
            tblCustomer customer = GetCustomer(1, "", "");

            // Act  
            controller.Create(customer);

            // Assert
            IEnumerable<tblCustomer> customers = repository.GetAllCustomers();
            Assert.IsFalse(customers.Contains(customer));
        }
    }
}
