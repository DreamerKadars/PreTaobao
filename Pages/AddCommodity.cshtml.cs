using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PreTaobao.Pages
{
    public class AddCommodityModel : PageModel
    {
        [BindProperty]
        public Commodity commodity { get; set; } = new Commodity();
        [BindProperty]
        public long Len { get; set; }
        public string name;
        [BindProperty]
        public IFormFile file { get; set; }
        public string message;
        public void OnGet()
        {

        }
        public void OnPost()
        {


            if (file == null)
            {
                message = "请输入图片";
                return;
            }
            else if (commodity.name == null)
            {
                message = "请输入名字";
                return;
            }
            else if (commodity.price <= 0)
            {
                message = "价格不合法";
                return;
            }


            ODBC odbc = new ODBC();
            commodity.cid = odbc.get_com_num() + 1;
            commodity.image = Commodity.pre_loc + commodity.cid.ToString() + Commodity.suf_loc;
            FileStream filet = new FileStream(commodity.image, FileMode.Create);
            file.CopyTo(filet);
            filet.Close();
            odbc.ins_com(commodity);
            Console.WriteLine("123123");

            try
            {
                Send.send(commodity.image, Send.Fip);
                message = "添加成功！";
            }
            catch (Exception ex)
            {
                message = "添加失败！";
                Console.WriteLine(ex.Message);
            }
        }

    }


}

    

