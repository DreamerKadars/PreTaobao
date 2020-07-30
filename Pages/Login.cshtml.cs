using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using MySql.Data.MySqlClient;
using System.Web;
using Microsoft.Extensions.Configuration;
namespace PreTaobao.Pages
{
    

    public class LoginModel : PageModel
    {


        public int uid { get; set; }
        [BindProperty]
        public string user { get; set; }
        [BindProperty]
        public string pwd { get; set; }
        
        public bool login()
        {



            ODBC odbc = new ODBC();

            User ut = odbc.get_user(user, pwd);

            if(ut.uid==-1)
            {
                return false;
            }
            else
            {
                Response.Cookies.Delete("name");
                Response.Cookies.Append("name", ut.name);
                Response.Cookies.Delete("user");
                Response.Cookies.Append("user", ut.user);
            }

            odbc.close();

            return true;

        }

        public void OnGet()
        {

        }
    }
}
