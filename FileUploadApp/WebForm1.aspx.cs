using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;

namespace FileUploadApp
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string MyConnectionString = "Server=localhost;Database=files_data;uid=root";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            FileInfo fileInfo = new FileInfo(FileUpload1.FileName);
            byte[] FileContent = FileUpload1.FileBytes;
            bool exist = false;
            string name = fileInfo.Name;
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
            try
            {
                connection.Open();
                MySqlCommand cmd1 = connection.CreateCommand();
                cmd1.CommandText = "SELECT * from files_data where file_name = @name AND physical_file = @physicalFile";
                cmd1.Parameters.AddWithValue("@name", name);
                cmd1.Parameters.AddWithValue("@physicalFile", FileContent);
                MySqlDataReader dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                        if (dr.HasRows == true)
                        {
                            exist = true;
                            Response.Write("<script>alert('File already exists');</script>");
                        }
                 }
                dr.Close();
                
                if (exist == false)
                {
                    MySqlCommand cmd2 = connection.CreateCommand();
                    cmd2.CommandText = "SELECT * from files_data where file_name = @name2";
                    cmd2.Parameters.AddWithValue("@name2", name);
                    MySqlDataReader dr2 = cmd2.ExecuteReader();
                    while (dr2.Read())
                    {
                        if (dr2.HasRows == true)
                        {
                            exist = true;
                            Response.Write("<script>alert('A file with similar name already exists! Please rename the file');</script>");
                        }
                    }
                    dr2.Close();
                }
                if (exist == false)
                {
                    MySqlCommand cmd3 = connection.CreateCommand();
                    cmd3.CommandText = "SELECT * from files_data where physical_file = @physicalFile3";
                    cmd3.Parameters.AddWithValue("@physicalFile3", FileContent);
                    MySqlDataReader dr3 = cmd3.ExecuteReader();
                    while (dr3.Read())
                    {
                        if (dr3.HasRows == true)
                        { 
                            exist = true;
                            Response.Write("<script>alert('File already exists with a different name. Do you want to overwrite it?');</script>");
                        }
                    }
                    dr3.Close();
                }
                if (exist==false)
                {
                    //connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "INSERT INTO files_data(file_name,physical_file)VALUES(@name,@Content)";
                    cmd.Parameters.AddWithValue("@name", name);
                    //cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.Add("@Content", MySqlDbType.VarBinary).Value = FileContent;
                    cmd.ExecuteNonQuery();
                    Response.Write("<script>alert('File is uploaded successfully');</script>");
                }
                
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    
                    connection.Close();

                    // LoadData();
                }
            }
        }
    }
}