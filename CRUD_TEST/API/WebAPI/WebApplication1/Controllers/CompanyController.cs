using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Caching;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CompanyController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"select CompanyID,CompanyName from dbo.Company";

            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["carsAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query,con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);

        }

        public string Post (company com)
        {
            try
            {
                string query = @"insert into dbo.company values ('"+com.CompanyName+@"')";

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

        public string Put(company com)
        {
            try
            {
                string query = @"update dbo.company set CompanyName='" + com.CompanyName + @"' where CompanyID ="+com.CompanyID+@"";

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
                string query = @"delete from dbo.company 
                                 where CompanyID =" + id + @"";

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
    }

}
