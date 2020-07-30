using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace PreTaobao.Pages
{
    public class RegisterModel : PageModel
    {
        public string message_re;
        [BindProperty]
        public string user { get; set; }

        [BindProperty]
        public string pwd { get; set; }
        [BindProperty]
        public string pwd_again { get; set; }
        int uid;
        [BindProperty]
        public string name { get; set; }
        int money=0,num_cards=0;

        public void OnGet()
        {

        }
        public void OnPost()
        {
            //验证是否重复
            User ut;
            ODBC odbc = new ODBC();
            ut= odbc.get_user(user);
            if(ut.uid!=-1)
            {
                message_re = "账号重复";
                return;
            }

            int flag = 0;
            for(int i=0;i<user.Length;i++)
            {
                if (user[i] >= '0' && user[i] <= '9')
                { }
                else
                    flag = 1;
            }
            if(flag==1)
            {
                message_re = "包含数字之外的字符";
                return;
            }

            if(!pwd.Equals(pwd_again))
            {
                message_re = "两次输入的内容不符";
                return;
            }

            message_re = "注册成功！";
            uid = odbc.get_user_num()+1;
            ut = new User(uid, user, pwd, name, money, num_cards);
            odbc.ins_user(ut);

        }

    }
}
