using System;
namespace PreTaobao.Pages
{
    public class User
    {
        public int uid { get; set; }

        public string user { get; set; }
        public string pwd { get; set; }
        public string name { get; set; }
        public int money { get; set; }
        public int num_cards { get; set; }
        public User()
        { }

        public User(int _uid,string _user,string _pwd,string _name,int _money,int _num_cards)
        {
            uid=_uid;
            user = _user;
            pwd=_pwd;
            name = _name;
            money = _money;
            num_cards = _num_cards;
        }
    }
}
