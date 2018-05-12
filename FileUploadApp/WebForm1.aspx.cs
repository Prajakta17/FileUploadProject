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
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Threading;
using System.Net;

namespace FileUploadApp
{

    public partial class WebForm1 : System.Web.UI.Page
    {
        FileInfo fileInfo;
        string MyConnectionString = "Server=localhost;Database=files_data;uid=root";

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void Button1_Click(object sender, EventArgs e)
        {
                Thread.Sleep(2000);
                fileInfo = new FileInfo(FileUpload1.FileName);
                byte[] FileContent = FileUpload1.FileBytes;
                bool exist = false;
                string name = fileInfo.Name;
                MySqlConnection connection = new MySqlConnection(MyConnectionString);
                try
                {
                    //check whether same file already exists(same name and content)
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
                            //Label1.Text = "Upload failed!!!";
                        }
                    }
                    dr.Close();
                   //check whether a file with same name exists
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
                                //Label1.Text = "Upload failed!!!";
                            }
                        }
                        dr2.Close();
                    }
                    //check whether a file with same content exists
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
                            //Label1.Text = "Upload failed!!!";
                        }
                        }
                        dr3.Close();
                    }
                    //upload the file
                    if (exist == false)
                    {
                    //connection.Open();
                        string ipAddr = getIPAdrr();
                        MySqlCommand cmd = connection.CreateCommand();
                        cmd.CommandText = "INSERT INTO files_data(file_name,upload_IP,physical_file)VALUES(@name,@ipaddr,@Content)";
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@ipaddr", ipAddr);
                        cmd.Parameters.Add("@Content", MySqlDbType.VarBinary).Value = FileContent;
                        cmd.ExecuteNonQuery();
                        Response.Write("<script>alert('File is uploaded successfully');</script>");
                        //Label1.Text = "Upload Success!!!";
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

        private string getIPAdrr()
        {
            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            string ipaddr = "";
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ipaddr = Convert.ToString(IP);
                }
            }
            return ipaddr;
        }
    }
}