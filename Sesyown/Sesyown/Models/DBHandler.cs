using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Sesyown.Models
{
    public class DBHandler
    {
        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["SessionAgen"].ToString();
            con = new SqlConnection(constring);
        }

        public bool InsertItem(Person person)
        {
            connection();
            string query = "INSERT INTO Person VALUES('" + person.ID + "','" + person.Name + "','" + person.MutualFundType + "','" + person.Amount + "','" + person.NAVPS + "','" + person.SalesLoad + "','" + person.NetAmount + "','" + person.TotalShares + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
    }
}