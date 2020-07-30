using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PreTaobao.Pages
{
    public class MyInforModel : PageModel
    {

        public User user { get; set; }
        public void OnGet()
        {
            string ut;
            Request.Cookies.TryGetValue("user",out ut);
            ODBC odbc = new ODBC();
            user = odbc.get_user(ut);
        }
        public void OnPost()
        {
            ODBC oDBC = new ODBC();
            string ut;
            Request.Cookies.TryGetValue("user", out ut);
            user = oDBC.get_user(ut);
            oDBC.update_money(user.uid, 5);
            Response.Redirect(MyWeb.Pre_Web+"/MyInfor");
        }
    }
}
