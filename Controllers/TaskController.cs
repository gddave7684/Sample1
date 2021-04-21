﻿using System;
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

        // GET: Task 
        public ActionResult one()
        {
            String cs = ConfigurationManager.ConnectionStrings["DBRPS"].ConnectionString;
            String sql = "SELECT * FROM dbo.Switches";
           
            List<dataTable> model = new List<dataTable>();
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var tblData = new dataTable();

                    tblData.Name = (string)rdr["Name"];
                    tblData.Description = (string)rdr["Description"];
                    tblData.Status = (bool)rdr["Status"];
                    
                    model.Add(tblData);
                }

            }

            return View(model);
        }
    }
}