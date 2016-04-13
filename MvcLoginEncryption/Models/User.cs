using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcLoginEncryption.Models
{
    public class User
    {
        //This will be primary key column with auto increment  
        [Key]
        public int RegistrationId
        {
            get;
            set;
        }
        public string FirstName
        {
            get;
            set;
        }
        public string LastName
        {
            get;
            set;
        }
        public string UserName
        {
            get;
            set;
        }
        public string EmailId
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
        public string Gender
        {
            get;
            set;
        }
        public string VCode
        {
            get;
            set;
        }
        public DateTime CreateDate
        {
            get;
            set;
        }
        public DateTime ModifyDate
        {
            get;
            set;
        }
        public bool Status
        {
            get;
            set;
        }
    }
}