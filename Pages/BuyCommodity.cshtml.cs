using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PreTaobao.Pages
{
    public class BuyCommodityModel : PageModel
    {
        public Commodity commodity;
        public string message;
        [BindProperty]
        public int num_buy { get; set; }
        int cid;
        public void OnGet()
        {

            cid = int.Parse(Request.QueryString.ToString().Remove(0,5));
            ODBC oDBC = new ODBC();
            commodity = oDBC.GetCommodity(cid);

        }

        public void OnPost()
        {

            string user=null;
            foreach (var cookie in Request.Cookies)
            {
                if (@cookie.Key.Equals("user"))
                {
                    user = @cookie.Value;
                }
            }

            if(user==null)
            {
                message = "请先登录";
                return;
            }
            ODBC oDBC = new ODBC();
            User ut = oDBC.get_user(user);
            cid = int.Parse(Request.QueryString.ToString().Remove(0, 5));
            commodity = oDBC.GetCommodity(cid);


            if(ut.money<commodity.price*num_buy)
            {
                message = "你的钱不够啦";
                return;
            }
            oDBC.buy_com(ut.uid, commodity.cid, num_buy);
            oDBC.update_money(ut.uid, -commodity.price * num_buy);
            message = "购买成功";
        }
    }
}
