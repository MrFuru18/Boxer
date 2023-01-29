using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLibrary.Model.ToCreate
{
    public class ToCreateEmployee
    {
        public string uid { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string permissions { get; set; }
    }
}
