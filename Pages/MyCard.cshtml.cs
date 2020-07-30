using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PreTaobao.Pages
{
    public class MyCardModel : PageModel
    {
        public string message="";
        User user;
        public List<KeyValuePair<int, int>> C_List = new List<KeyValuePair<int, int>>();
        public List<Commodity> Commodities=new List<Commodity>();
        public void OnGet()
        {
            ODBC oDBC = new ODBC();

            string ut;
            Request.Cookies.TryGetValue("user", out ut);
            user = oDBC.get_user(ut);
            C_List = oDBC.get_Cards(user.uid);
            foreach(var kp in C_List)
            {
                Commodities.Add(oDBC.GetCommodity(kp.Key));
            }

        }
    }
}
