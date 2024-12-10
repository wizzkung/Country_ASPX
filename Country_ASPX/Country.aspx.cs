using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlTypes;
using ClosedXML.Excel;
using System.IO;

namespace Country_ASPX
{
    public partial class Country : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCountryDropdown();
            }

        }
        private void BindCountryDropdown()
        {
            using (SqlConnection db = new SqlConnection(ConfigurationManager.AppSettings["db"]))
            {
                
                SqlDataAdapter da = new SqlDataAdapter("SELECT DISTINCT Id, Name FROM Country", db);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlCountry.DataSource = dt;
                ddlCountry.DataBind();

                ddlCountry.Items.Insert(0, new ListItem("Выберите страну", "0"));
            }
        }


        void Get()
        {
            DataTable dt = new DataTable();
            using (SqlConnection db = new SqlConnection(ConfigurationManager.AppSettings["db"]))
            {
                db.Open();
                using (SqlCommand cmd = new SqlCommand("pGetAll", db))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    dt.Load(cmd.ExecuteReader());
                }
                db.Close();
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();


        }

        void GetByNameCity()
        {
            DataTable dt = new DataTable();
            string name = TextBox1.Text;
            using (SqlConnection db = new SqlConnection(ConfigurationManager.AppSettings["db"]))
            {
                db.Open();
                using (SqlCommand cmd = new SqlCommand("pGetCity", db))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", name);  
                    dt.Load(cmd.ExecuteReader());
                }
                db.Close();
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        void GetByNameCountry()
        {
            DataTable dt = new DataTable();
            string name = TextBox2.Text;
            using (SqlConnection db = new SqlConnection(ConfigurationManager.AppSettings["db"]))
            {
                db.Open();
                using (SqlCommand cmd = new SqlCommand("pGetCountry", db))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", name);
                    dt.Load(cmd.ExecuteReader());
                }
                db.Close();
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        void AddCity()
        {
            DataTable dt = new DataTable();
            int countryId = int.Parse(ddlCountry.SelectedValue);
            using (SqlConnection db = new SqlConnection(ConfigurationManager.AppSettings["db"]))
            {
                db.Open();
                using (SqlCommand cmd = new SqlCommand("pAddCity", db))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", tbCityName.Text);
                    cmd.Parameters.AddWithValue("@population", tbPopulation.Text);
                    cmd.Parameters.AddWithValue("@country_id", countryId);
                    cmd.ExecuteNonQuery();
                }
                Get();
                db.Close();
            }
           
        }

        void DeleteCity()
        {
            string n = tbdelupd.Text;
            using (SqlConnection db = new SqlConnection(ConfigurationManager.AppSettings["db"]))
            {
                db.Open();
                using (SqlCommand cmd = new SqlCommand("pDeleteCity", db))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", n);
                    cmd.ExecuteNonQuery();
                }
                Get();
                db.Close();
            }
        }

        void UpdateCity()
        {
                int cityId = int.Parse(tbid.Text); 
                string cityName = tbCityName.Text; 
                int cityPopulation = int.Parse(tbPopulation.Text);

                using (SqlConnection db = new SqlConnection(ConfigurationManager.AppSettings["db"]))
                {
                    db.Open();
                    using (SqlCommand cmd = new SqlCommand("pUpdateCity", db))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", cityId);
                        cmd.Parameters.AddWithValue("@name", cityName);
                        cmd.Parameters.AddWithValue("@Population", cityPopulation);

                        cmd.ExecuteNonQuery();
                    }
                    db.Close();
                }

          
                Get();
            }



        void ExportToCSV()
        {
            DataTable dt = GetCityData();

            string csvContent = string.Empty;

            foreach (DataColumn column in dt.Columns)
            {
                csvContent += column.ColumnName + ",";
            }
            csvContent = csvContent.TrimEnd(',') + Environment.NewLine;

          
            foreach (DataRow row in dt.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    csvContent += item.ToString() + ",";
                }
                csvContent = csvContent.TrimEnd(',') + Environment.NewLine;
            }

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=CityData.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(csvContent);
            Response.Flush();
            Response.End();
        }

        DataTable GetCityData()
        {
            DataTable dt = new DataTable();
            using (SqlConnection db = new SqlConnection(ConfigurationManager.AppSettings["db"]))
            {
                db.Open();
                using (SqlCommand cmd = new SqlCommand("pGetAll", db))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }
        void ExportToExcel()
        {
            DataTable dt = GetCityData();

            using (XLWorkbook workbook = new XLWorkbook())
            {
                
                workbook.Worksheets.Add(dt, "CityData");

                using (MemoryStream stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    byte[] byteArray = stream.ToArray();
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=CityData.xlsx");
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.BinaryWrite(byteArray);
                    Response.Flush();
                    Response.End();
                }
            }
        }


        protected void btAll_Click(object sender, EventArgs e)
        {
            Get();
        }

        protected void btSearchName_Click(object sender, EventArgs e)
        {
            GetByNameCity();
        }

        protected void btSearchCountry_Click(object sender, EventArgs e)
        {
            GetByNameCountry();
        }

        protected void btDelete_Click(object sender, EventArgs e)
        {
            DeleteCity();
        }

        protected void CityAdd_Click(object sender, EventArgs e)
        {
            AddCity();
        }

        protected void btUpdate_Click(object sender, EventArgs e)
        {
            UpdateCity();
        }

        protected void btCSV_Click(object sender, EventArgs e)
        {
            ExportToCSV();
        }

        protected void btEXCEL_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }
    }
}