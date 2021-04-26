using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleTask1.Models;

namespace SampleTask1.Controllers
{
    public class TaskController : Controller
    {
        String ConnectionString = ConfigurationManager.ConnectionStrings["DBRPS"].ConnectionString;
        // GET: Task 
        public ActionResult One()
        {
            String sql = "SELECT * FROM dbo.Switches";

            List<SwitchDataTable> model = new List<SwitchDataTable>();
            
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var tblData = new SwitchDataTable
                    {
                        Id = (int)rdr["Id"],
                        Name = (string)rdr["Name"],
                        Description = (string)rdr["Description"],
                        Status = (bool)rdr["Status"]
                    };

                    model.Add(tblData);
                }

            }

            return View("One", model);
        } // one

        [AjaxOnly]
        public ActionResult PostNewData(string inName, string inDescription, string inStatus)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                String query = "INSERT INTO dbo.Switches (Name, Description, Status) VALUES (@Name, @Description, @Status)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    int status = (inStatus == "on") ? 1 : 0;

                    command.Parameters.AddWithValue("@Name", inName);
                    command.Parameters.AddWithValue("@Description", inDescription);
                    command.Parameters.AddWithValue("@Status", status);

                    connection.Open(); 
                    int result = command.ExecuteNonQuery();

                    // Check Error
                    if (result < 0)
                        Console.WriteLine("Error inserting data into Database!");

                    connection.Close();
                }
            }
            return Json(new { status = "ok" }, JsonRequestBehavior.AllowGet);
        }//PostNewData

        [AjaxOnly]
        public ActionResult SwitchUpdate(bool updateStatus, string refName)
        {
            int newStatus = (updateStatus == true) ? 1 : 0;
            
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                String query = "UPDATE dbo.Switches SET Status = @updateStatus WHERE Name = @refName";
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@updateStatus", newStatus);
                    command.Parameters.AddWithValue("@refName", refName);
                    connection.Open();

                    int result = command.ExecuteNonQuery();
                    // Check Error
                    if (result < 0)
                        Console.WriteLine("Error updating data into Database!");
                    connection.Close();
                }
            }
            return Json(new { status = "ok" }, JsonRequestBehavior.AllowGet);
        }//SwitchUpdate

        [AjaxOnly]
        public ActionResult DeleteSwitch(string nameTodelete)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                String Query = "DELETE FROM dbo.Switches WHERE Name = @nameTodelete";
                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    SqlParameter nameTodeleteparam = command.Parameters.AddWithValue("@nameTodelete", nameTodelete);
                    if (nameTodelete == null)
                    {
                        nameTodeleteparam.Value = DBNull.Value;
                    }

                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    // Check Error
                    if (result < 0)
                        Console.WriteLine("Error deleting data into the Database!");

                    connection.Close();
                }
            }
            return Json(new { status = "ok" }, JsonRequestBehavior.AllowGet);
        }//SwitchDelete


        public ActionResult GetView(string viewName)
        {
            List<SwitchDataTable> model = new List<SwitchDataTable>();
            String sql = "SELECT * FROM dbo.Switches";
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var tblData = new SwitchDataTable
                    {
                        Id = (int)rdr["Id"],
                        Name = (string)rdr["Name"],
                        Description = (string)rdr["Description"],
                        Status = (bool)rdr["Status"]
                    };

                    model.Add(tblData);
                }

            }
            return PartialView(viewName, model);
        }
    }
}
