using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PreTaobao.Pages
{
    public class CommodityModel : PageModel
    {
        public List<Commodity> commodities;
        public void OnGet()
        {
            ODBC o = new ODBC();
            commodities = o.GetCommodities();
        }
    }
}
