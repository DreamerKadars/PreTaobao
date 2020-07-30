using System;
namespace PreTaobao.Pages
{
    public class Commodity
    {
        public int cid { get; set; }
        public int price { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        static public string pre_loc = "wwwroot/Image/";
        static public string suf_loc = ".png";
        public Commodity()
        {

        }
    }
}
