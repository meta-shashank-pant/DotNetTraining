using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EmployeeRegistration
{
    /// <summary>
    /// This class interact with the database.
    /// </summary>
    public static class DaoImplementation
    {
        static private SqlConnection con;

        /// <summary>
        /// Extension method for inserting an employee data to the database.
        /// </summary>
        /// <param name="emp">Object holds the values of the employee.</param>
        /// <returns>True if successfull, false otherwise.</returns>
        public static bool InsertEmployee(this Employee emp)
        {
            using(con = new SqlConnection("data source=.; database=Employee; integrated security=SSPI"))
            {
                using(SqlCommand command = new SqlCommand("insert into " +
                    "employee(empId, fullName, gender, emailId, empPassword, contactNumber, organisation) " +
                    $"values({emp.EmpId},'{emp.FullName}', '{emp.Gender}' , '{emp.EmailId}', '{emp.Password}', '{emp.ContactNumber}', '{emp.Organisation}')", con))
                {
                    // Opening Connection  
                    con.Open();

                    try
                    {
                        // Executing the SQL query  
                        command.ExecuteNonQuery();
                        con.Close();
                        return true;
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                        con.Close();
                        return false;
                    }
                }
            }
            
        }

        /// <summary>
        /// Extension method for inserting the vehicle data to the database.
        /// </summary>
        /// <param name="vehicle">Object holds the values of the vehicle.</param>
        /// <returns>True if successfull, false otherwise.</returns>
        public static bool InsertVehicle(this Vehicle vehicle)
        {
            using (con = new SqlConnection("data source=.; database=Employee; integrated security=SSPI"))
            {
                using (SqlCommand command = new SqlCommand("insert into " +
                    "vehicle(vehicleId, empId, vehicleName, vehicleNumber, vehicleType) " +
                    $"values({vehicle.VehicleId},'{vehicle.EmpId}', '{vehicle.VehicleName}' , '{vehicle.VehicleNumber}', '{vehicle.VehicleType}')", con))
                {
                    // Opening Connection  
                    con.Open();

                    try
                    {
                        // Executing the SQL query  
                        command.ExecuteNonQuery();
                        con.Close();
                        return true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        con.Close();
                        return false;
                    }
                }
            }

        }

        /// <summary>
        /// Extension method for inserting the pass info to the database.
        /// </summary>
        /// <param name="pass">Object holds the values of the pass.</param>
        /// <returns>True if successfull, false otherwise.</returns>
        public static bool InsertPass(this Pass pass)
        {
            using (con = new SqlConnection("data source=.; database=Employee; integrated security=SSPI"))
            {
                using (SqlCommand command = new SqlCommand("insert into " +
                    "pass(vehicleId, vehicleType, planType, amount) " +
                    $"values({pass.VehicleId},'{pass.VehicleType}', '{pass.PlanType}' , '{pass.Amount}')", con))
                {
                    // Opening Connection  
                    con.Open();

                    try
                    {
                        // Executing the SQL query  
                        command.ExecuteNonQuery();
                        con.Close();
                        return true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        con.Close();
                        return false;
                    }
                }
            }

        }

        /// <summary>
        /// Delete the complete record of employee,
        /// including vehicle detail and pass info.
        /// </summary>
        /// <param name="empId">Employee with the empId will get removed. If empId is wrong than data remains same.</param>
        /// <returns>True if emp exists and false otherwise.</returns>
        public static bool DeleteEmployee(int empId)
        {
            using (con = new SqlConnection("data source=.; database=Employee; integrated security=SSPI"))
            {
                SqlCommand cmd = new SqlCommand($"select vehicleId from vehicle where empId = {empId}", con);
                con.Open();
                cmd.ExecuteNonQuery();
                var vehicleId = cmd?.ExecuteScalar(); 
                
                if(vehicleId != null)
                {
                    vehicleId = (Int32)vehicleId;
                    cmd = new SqlCommand($"delete from employee where empId = {empId}; " +
                    $"delete from vehicle where empId = {empId}; " +
                    $"delete from pass where vehicleId = {vehicleId}", con);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    return false;
                }
                con.Close();

            }
            return true;
        }

        /// <summary>
        /// Retreive data from the database.
        /// </summary>
        /// <returns>Dataset which have all the required data.</returns>
        public static DataSet ShowEmployee()
        {
            using (con = new SqlConnection("data source=.; database=Employee; integrated security=SSPI"))
            {
                SqlDataAdapter sda = new SqlDataAdapter("select e.empId, e.fullName, e.gender, e.emailId, e.contactNumber," +
                                        " e.organisation, v.vehicleName, v.vehicleNumber, v.vehicleType, p.planType, p.amount " +
                                        "from employee as e join vehicle as v on e.empId = v.empId " +
                                        "join pass as p on p.vehicleId = v.vehicleId", con);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
            }         
        }

        /// <summary>
        /// Get Vehicle Id from the database incremented by 1.
        /// </summary>
        /// <returns>vehicle id for the next entry.</returns>
        public static int GetVehicleId()
        {
            using (con = new SqlConnection("data source=.; database=Employee; integrated security=SSPI"))
            {
                SqlCommand cmd = new SqlCommand($"select vehicleId from vehicle where vehicleId = (select max(vehicleId) from vehicle)", con);
                con.Open();
                int vehicleId;
                try
                {
                    cmd.ExecuteNonQuery();
                    vehicleId = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
                }
                catch (Exception e)
                {
                    vehicleId = 1;
                    Console.WriteLine(e.Message);
                }
                con.Close();
                return vehicleId;
            }
        }
    }
}
