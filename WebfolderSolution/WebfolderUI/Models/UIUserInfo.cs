using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebfolderUI.Models
{
    public class UIUserInfo
    {
        public string UserEmail { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }


        public string Phone { get; set; }

        public string QQ { get; set; }


        public string LoginName { get; internal set; }
    }
}