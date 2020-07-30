using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using MySql.Data.MySqlClient;
using System.Web;
using Microsoft.Extensions.Configuration;
namespace PreTaobao.Pages
{
    public class ODBC
    {
        static public string sqlstr;
        public MySqlConnection conn = null;
        public ODBC()
        {
            connect();
        }
        public bool connect()
        {
            try
            {
                conn = new MySqlConnection(sqlstr);
                conn.Open();
                Console.WriteLine("true");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("false");
                return false;
            }

        }
        ~ODBC()
        {
            close();
        }

        public void close()
        {
            conn.Close();
        }
        
        public User get_user(string user,string pwd)
        {
            User user1=new User();
            user1.uid=-1;
            user1.user = user;
            user1.pwd = pwd;
            string sql = "SELECT * FROM Login where User=@para1 and pwd = @para2";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("para1", user);
            cmd.Parameters.AddWithValue("para2", pwd);

            MySqlDataReader reader = cmd.ExecuteReader();//执行ExecuteReader()返回一个MySqlDataReader对象
            while (reader.Read())//初始索引是-1，执行读取下一行数据，返回值是bool
            {
                user1.uid = reader.GetInt32("uid");
            }
            if (user1.uid == -1)
                return user1;

            conn.Close();
            conn.Open();
            sql = "SELECT * FROM Information where uid = @para1";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("para1", user1.uid);
            reader = cmd.ExecuteReader();

            while (reader.Read())//初始索引是-1，执行读取下一行数据，返回值是bool
            {
                user1.name = reader.GetString("name");
                user1.money = reader.GetInt32("money");
                user1.num_cards = reader.GetInt32("num_cards");
            }
            return user1; 
        }
        public User get_user(string user)
        {
            User user1 = new User();
            user1.uid = -1;
            user1.user = user;
            
            string sql = "SELECT * FROM Login where User=@para1";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("para1", user);
            MySqlDataReader reader = cmd.ExecuteReader();//执行ExecuteReader()返回一个MySqlDataReader对象

            while (reader.Read())//初始索引是-1，执行读取下一行数据，返回值是bool
            {
                user1.uid = reader.GetInt32("uid");
                user1.pwd = reader.GetString("pwd");
            }
            if (user1.uid == -1)
                return user1;

            conn.Close();
            conn.Open();
            sql = "SELECT * FROM Information where uid=@para1";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("para1", user1.uid);
            reader = cmd.ExecuteReader();
            while (reader.Read())//初始索引是-1，执行读取下一行数据，返回值是bool
            {
                user1.name = reader.GetString("name");
                user1.money = reader.GetInt32("money");
                user1.num_cards = reader.GetInt32("num_cards");
            }
            return user1;
        }
        public User get_user(int uid)
        {
            User user1 = new User();
            user1.uid = uid;

            string sql = "SELECT * FROM Login where uid=@para1";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("para1", uid);
            MySqlDataReader reader = cmd.ExecuteReader();//执行ExecuteReader()返回一个MySqlDataReader对象
            
            if(reader.Read())//初始索引是-1，执行读取下一行数据，返回值是bool
            {
                user1.user = reader.GetString("user");
                user1.pwd = reader.GetString("pwd");
            }
            else {
                user1.uid = -1;
                return user1;
            }
            conn.Close();
            conn.Open();
            sql = "SELECT * FROM Information where uid=@para1";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("para1", user1.uid);
            reader = cmd.ExecuteReader();
            while (reader.Read())//初始索引是-1，执行读取下一行数据，返回值是bool
            {
                user1.name = reader.GetString("name");
                user1.money = reader.GetInt32("money");
                user1.num_cards = reader.GetInt32("num_cards");
            }
            return user1;
        }
        public int get_user_num()
        {
            string sql = "SELECT COUNT(*) as num FROM Login";
            conn.Close();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            return reader.GetInt32("num");
        }

        public void ins_user(User user)
        {

            string sql = "INSERT INTO Login Value(@para1,@para2,@para3)";
            conn.Close();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("para1", user.uid);
            cmd.Parameters.AddWithValue("para2", user.user);
            cmd.Parameters.AddWithValue("para3", user.pwd);
            cmd.ExecuteReader();

            conn.Close();
            conn.Open();
            sql = "INSERT INTO Information Value(@para1,@para2,@para3,@para4)";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("para1", user.uid);
            cmd.Parameters.AddWithValue("para2", user.name);
            cmd.Parameters.AddWithValue("para3", user.money);
            cmd.Parameters.AddWithValue("para4", user.num_cards);
            cmd.ExecuteReader();
        }
        public int get_com_num()//拿到商品的数量
        {

            string sql = "SELECT COUNT(*) as num FROM Commodity";
            conn.Close();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader= cmd.ExecuteReader();
            reader.Read();
            return reader.GetInt32("num");

        }
        public void ins_com(Commodity ct)
        {
            string sql = "INSERT INTO Commodity Value(@para1,@para2,@para3,@para4)";
            conn.Close();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("para1", ct.cid);
            cmd.Parameters.AddWithValue("para2", ct.price);
            cmd.Parameters.AddWithValue("para3", ct.name);
            cmd.Parameters.AddWithValue("para4", ct.image);
            cmd.ExecuteReader();
        }
        public List<Commodity> GetCommodities()
        {
            List<Commodity> commodities=new List<Commodity>();

            conn.Close();
            conn.Open();
            string sql = "SELECT * FROM Commodity";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            MySqlDataReader reader = cmd.ExecuteReader();//执行ExecuteReader()返回一个MySqlDataReader对象
            
            while (reader.Read())//初始索引是-1，执行读取下一行数据，返回值是bool
            {
                Commodity commodity = new Commodity();
                commodity.cid= reader.GetInt32("cid");
                commodity.price = reader.GetInt32("price");
                commodity.name = reader.GetString("name");
                commodity.image = reader.GetString("image");
                commodities.Add(commodity);
            }
            return commodities;
        }
        public Commodity GetCommodity(int cid)
        {

            conn.Close();
            conn.Open();
            string sql = "SELECT * FROM Commodity where cid=@para1";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("para1", cid);
            MySqlDataReader reader = cmd.ExecuteReader();//执行ExecuteReader()返回一个MySqlDataReader对象

            reader.Read();//初始索引是-1，执行读取下一行数据，返回值是bool
            
            Commodity commodity = new Commodity();
            commodity.cid = reader.GetInt32("cid");
            commodity.price = reader.GetInt32("price");
            commodity.name = reader.GetString("name");
            commodity.image = reader.GetString("image");
            return commodity;
            
        }
        public void buy_com(int uid,int cid,int num)
        {
            conn.Close();
            conn.Open();
            string sql = "SELECT * FROM UserCard where uid=@para1 and cid=@para2";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("para1", uid);
            cmd.Parameters.AddWithValue("para2", cid);
            MySqlDataReader reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                int num_now = reader.GetInt16("num");
                conn.Close();
                conn.Open();
                sql = "UPDATE UserCard SET num=@para1 where uid=@para2 and cid=@para3";
                cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("para1", num_now+num);
                cmd.Parameters.AddWithValue("para2", uid);
                cmd.Parameters.AddWithValue("para3", cid);
                reader = cmd.ExecuteReader();
            }
            else
            {
                conn.Close();
                conn.Open();
                sql = "INSERT INTO UserCard Value(@para1,@para2,@para3)";
                cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("para1", uid);
                cmd.Parameters.AddWithValue("para2", cid);
                cmd.Parameters.AddWithValue("para3", num);
                reader = cmd.ExecuteReader();
            }
        }
        public void update_money(int uid,int money)
        {
            conn.Close();
            conn.Open();
            string sql = "SELECT * FROM Information where uid=@para1";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("para1", uid);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                int money_now = reader.GetInt16("money");
                conn.Close();
                conn.Open();

                sql = "UPDATE Information SET money=@para1 where uid=@para2";
                cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("para1", money_now + money);
                cmd.Parameters.AddWithValue("para2", uid);
                reader = cmd.ExecuteReader();
            }
        }
        public List<KeyValuePair<int, int>> get_Cards(int uid)
        {
            List<KeyValuePair<int, int>> C_List = new List<KeyValuePair<int, int>>();
            conn.Close();
            conn.Open();
            string sql = "SELECT * FROM UserCard where uid=@para1";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("para1", uid);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())//初始索引是-1，执行读取下一行数据，返回值是bool
            {
                KeyValuePair<int, int> KP = new KeyValuePair<int, int>(reader.GetInt32("cid"), reader.GetInt32("num"));
                C_List.Add(KP);
            }
            return C_List;
        }

    }
}
