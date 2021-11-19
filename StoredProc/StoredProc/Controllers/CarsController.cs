using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using StoredProc.Data;
using StoredProc.Models;

namespace StoredProc.Controllers
{
    public class CarsController : Controller
    {
        public StoredProcDbContext _context;
        public IConfiguration _config { get; }

        public CarsController
            (
            StoredProcDbContext context,
            IConfiguration config
            )
        {
            _context = context;
            _config = config;

        }

        [HttpGet]
        public IActionResult Index()
        {
            string connectionStr = _config.GetConnectionString("DefaultConnection");

            using (SqlConnection con = new SqlConnection(connectionStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "dbo.spSearchCars";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                List<Car> model = new List<Car>();
                while (sdr.Read())
                {
                    var details = new Car();
                    details.id = Convert.ToInt32(sdr["id"]);
                    details.model_year = Convert.ToInt32(sdr["model_year"]);
                    details.model = sdr["model"].ToString();
                    details.manufacturer = sdr["manufacturer"].ToString();
                    details.VIN = sdr["VIN"].ToString();

                    model.Add(details);
                }
                return View(model);
            }
        }

        /// <summary>
        /// SearchPageWithoutDynamicSQL
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Index(int id, int model_year, string car_model, string manufacturer, string VIN)   //(string firstName, string lastName, string gender, int salary)
        {
            string connectionStr = _config.GetConnectionString("DefaultConnection");

            using (SqlConnection con = new SqlConnection(connectionStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "dbo.spSearchCars";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (id != 0)
                {
                    SqlParameter param_fn = new SqlParameter("@id", id);
                    cmd.Parameters.Add(param_fn);
                }
                if (model_year != 0)
                {
                    SqlParameter param_ln = new SqlParameter("@model_year", model_year);
                    cmd.Parameters.Add(param_ln);
                }
                if (car_model != null)
                {
                    SqlParameter param_g = new SqlParameter("@car_model", car_model);
                    cmd.Parameters.Add(param_g);
                }
                if (manufacturer != null)
                {
                    SqlParameter param_s = new SqlParameter("@manufacturer", manufacturer);
                    cmd.Parameters.Add(param_s);
                }
                if (manufacturer != null)
                {
                    SqlParameter param_s = new SqlParameter("@VIN", VIN);
                    cmd.Parameters.Add(param_s);
                }
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                List<Car> model = new List<Car>();
                while (sdr.Read())
                {
                    var details = new Car();
                    details.id = Convert.ToInt32(sdr["id"]);
                    details.model_year = Convert.ToInt32(sdr["model_year"]);
                    details.model = sdr["model"].ToString();
                    details.manufacturer = sdr["manufacturer"].ToString();
                    details.VIN = sdr["VIN"].ToString();

                    model.Add(details);
                }
                return View(model);
            }
        }
    }
}
