using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaBot.Models
{
    public partial class History
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Channel { get; set; }
        public System.DateTime Created { get; set; }
        public string Message { get; set; }
    }
}