using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace DataBasePractice
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().CreateTable();
        }
        public void CreateTable()
        {
            SqlConnection con = null;
            try
            {
                // Creating Connection  
                con = new SqlConnection("data source=.; database=student; integrated security=SSPI");

                // writing sql query  
                //SqlCommand cm = new SqlCommand("create table student(id int not null,name varchar(100), email varchar(50), join_date date)", con);  

                //Inserting Data
                //SqlCommand cm = new SqlCommand("insert into student (id, name, email, join_date)values('101', 'Scott Allen', 'scott@example.com', '2/6/2017')", con);  

                //Update Data
                //SqlCommand cm = new SqlCommand("update student set id = 104 where name = 'Scott Allen'", con);

                //Retrieving Data
                //SqlCommand cm = new SqlCommand("Select * from student", con);

                //Deleting Record
                //SqlCommand cm = new SqlCommand("delete from student where id = '101'", con);

                //DataSet Creation
                /*SqlDataAdapter sda = new SqlDataAdapter("select * from student", con);
                DataSet ds = new DataSet();
                sda.Fill(ds);*/

                // Opening Connection  
                //con.Open();

                // Executing the SQL query  
                //cm.ExecuteNonQuery();

                //Reading Dataset
                /*var xml = ds.GetXml();
                foreach(DataTable table in ds.Tables)
                {
                    foreach(DataRow row in table.Rows)
                    {
                        Console.WriteLine(row["id"]);
                    }
                    Console.WriteLine("One Table Finished.");
                }*/

                //DataTable
                /*DataTable dt = new DataTable();
                sda.Fill(dt);                
                foreach (DataRow item in dt.Rows)
                {
                    Console.WriteLine(item["id"]);
                }*/


                /*//Reading data from table
                SqlDataReader sdr = cm.ExecuteReader();
                // Iterating Data  
                while (sdr.Read())
                {
                    Console.WriteLine(sdr["id"] + " " + sdr["name"] + " " + sdr["email"]); // Displaying Record  
                }*/

            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong." + e);
            }
            // Closing the connection  
            finally
            {
                con.Close();
            }
            
        }
    }
}  
  