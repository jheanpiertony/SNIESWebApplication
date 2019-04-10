namespace SNIESWebApplication.ModelViews
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class UserView
    {
        public string UserID { get; set; }

        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }

        public RolView Role { get; set; }

        public List<RolView> Roles { get; set; }
    }
}