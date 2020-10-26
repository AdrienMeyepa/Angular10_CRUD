using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace WebApplication1.Controllers
{
    public class CarController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"select CarID,Car_Brand,Car_Type,convert(varchar(10),Date_Of_Purchased,120) as Date_Of_Purchased,PhotoFileName from dbo.Car";

            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["carsAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);

        }

        public string Post(cars car)
        {
            try
            {
                string query = @"insert into dbo.Car values ( '"+ car.Car_Brand + @"',
                                                              '"+ car.Car_Type + @"',
                                                              '"+ car.Date_Of_Purchased + @"',
                                                              '"+ car.PhotoFileName + @"'

                                                             )";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["carsAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Added Successfully! ";
            }

            catch (Exception)
            {
                return "Fail to Add";
            }
        }

        public string Put(cars car)
        {
            try
            {
                string query = @"update dbo.Car set 
                                Car_Brand='" + car.Car_Brand + @"', 
                                Car_Type='" + car.Car_Type + @"',
                                Company'" + car.Company +@"',
                                Date_Of_Purchase='" + car.Date_Of_Purchased + @"',
                                PhotoFileName='" + car.PhotoFileName + @"'
                                where CarID =" + car.CarID + @"";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["carsAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Updated Successfully! ";
            }

            catch (Exception)
            {
                return "Fail to Update";
            }
        }

        public string Delete(int id)
        {
            try
            {
                string query = @"delete from dbo.Car 
                                 where CarID =" + id + @"";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["carsAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Deleted Successfully! ";
            }

            catch (Exception)
            {
                return "Fail to Delete";
            }

        }


        [Route("api/Car/GetAllCompanyName")]
        [HttpGet]

        public HttpResponseMessage GetAllCompanyName()
        {

            string query = @"select CompanyName from dbo.Company";

            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["carsAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("api/Car/SaveFile")]
        
        public string SaveFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = HttpContext.Current.Server.MapPath("~/Photo/" + filename);

                postedFile.SaveAs(physicalPath);

                return filename;
            }

            catch (Exception)
            {

                return "anonymous.png";
            }
        }

    }
}
