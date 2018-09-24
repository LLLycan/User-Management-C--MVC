using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserManagementToolMVC.MessageHelper
{
    public class Message
    {
        /// <summary>
        /// const string for CRUD showing message.
        /// </summary>
        public const string CreateSucceed = "New customer created successfully.";
        public const string CreateFailed = "New customer created failed, please try again.";
        public const string UpdateSucceed = "Customer updated successfully.";
        public const string UpdateFailed = "Custmer updated failed, please try again.";
        public const string DeleteSucceed = "Customer deleted successfully.";
        public const string DeleteFailed = "Customer deleted failed, please try again.";
        public const string LoadingSucceed = "Customers loading successfully.";
        public const string LoadingFailed = "Customers loading failed, please try again.";
        public const string CustomerLoadingFailed = "Cannot find this customer, please try again.";

    }
}