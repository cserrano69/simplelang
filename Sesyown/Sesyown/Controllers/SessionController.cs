using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sesyown.Models;
using Sesyown.App_Code;

namespace Sesyown.Controllers
{
    public class SessionController : Controller
    {
        // GET: Session
        public ActionResult Index()
        {
            return View();
        }

        public List<SelectListItem> GetMF()
        {
            List<SelectListItem> MF = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"SELECT Id, MF FROM MFType ORDER BY MF";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        while (data.Read())
                        {
                            MF.Add(new SelectListItem
                            {
                                Text = data["MF"].ToString(),
                                Value = data["ID"].ToString()
                            });
                        }
                    }
                }
            }
            return MF;
        }
        //add

        public ActionResult Add()
        {
            Person person = new Person();
            person.SelectMF = GetMF();
            return View(person);

        }

        [HttpPost]
        public ActionResult Add(Person person)
        {
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"INSERT INTO Person VALUES (@Name, @MutualFundType, @Amount, @NAVPS, @SalesLoad, @NetAmount, @TotalShares)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Name", person.Name);
                    cmd.Parameters.AddWithValue("@MutualFundType", person.MutualFundType);
                    cmd.Parameters.AddWithValue("@Amount", person.Amount);
                    cmd.Parameters.AddWithValue("@NAVPS", person.NAVPS);
                    cmd.Parameters.AddWithValue("@SalesLoad", person.SalesLoad);
                    cmd.Parameters.AddWithValue("@NetAmount", person.NetAmount);
                    cmd.Parameters.AddWithValue("@TotalShares", person.TotalShares);
                    cmd.ExecuteNonQuery();
                }
            }

            Session["Name"] = person.Name;
            Session["MutualFundType"] = person.MutualFundType;
            Session["Amount"] = person.Amount;
            Session["NAVPS"] = person.NAVPS;
            Session["SalesLoad"] = person.SalesLoad;
            Session["NetAmount"] = person.NetAmount;
            Session["TotalShares"] = person.TotalShares;

            return RedirectToAction("Index");
        }
    }
}