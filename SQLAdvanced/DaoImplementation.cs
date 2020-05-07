using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SQLAdvanced
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
            using (con = new SqlConnection("data source=.; database=Employee; integrated security=SSPI"))
            {
                SqlCommand command = new SqlCommand("AddEmployeeToDB", con);
                command.CommandType = CommandType.StoredProcedure;

                // Adding input parameter and setting the property.
                SqlParameter empId = new SqlParameter("@empId", emp.EmpId);
                SqlParameter name = new SqlParameter("@fullName", emp.FullName);
                SqlParameter gender = new SqlParameter("@gender", emp.Gender);
                SqlParameter email = new SqlParameter("@emailId", emp.EmailId);
                SqlParameter password = new SqlParameter("@empPassword", emp.Password);
                SqlParameter number = new SqlParameter("@contactNumber", emp.ContactNumber);
                SqlParameter organisation = new SqlParameter("@organisation", emp.Organisation);

                // Adding the parameter to the Parameters collection.
                command.Parameters.Add(name);
                command.Parameters.Add(gender);
                command.Parameters.Add(email);
                command.Parameters.Add(password);
                command.Parameters.Add(number);
                command.Parameters.Add(organisation);
                command.Parameters.Add(empId);


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

        /// <summary>
        /// Extension method for inserting the vehicle data to the database.
        /// </summary>
        /// <param name="vehicle">Object holds the values of the vehicle.</param>
        /// <returns>True if successfull, false otherwise.</returns>
        public static bool InsertVehicle(this Vehicle vehicle)
        {
            using (con = new SqlConnection("data source=.; database=Employee; integrated security=SSPI"))
            {
                SqlCommand command = new SqlCommand("AddVehicleToDB", con);
                command.CommandType = CommandType.StoredProcedure;

                // Adding input parameter and setting the property.
                SqlParameter empId = new SqlParameter("@empId", vehicle.EmpId);
                SqlParameter vehicleName = new SqlParameter("@vehicleName", vehicle.VehicleName);                               
                SqlParameter vehicleType = new SqlParameter("@vehicleType", vehicle.VehicleType);
                SqlParameter vehicleNumber = new SqlParameter("@vehicleNumber", vehicle.VehicleNumber);
                SqlParameter vehicleId = new SqlParameter("@vehicleId", vehicle.VehicleId);

                // Adding the parameter to the Parameters collection.
                command.Parameters.Add(empId);
                command.Parameters.Add(vehicleName);
                command.Parameters.Add(vehicleNumber);
                command.Parameters.Add(vehicleType);
                command.Parameters.Add(vehicleId);
                
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

        /// <summary>
        /// Extension method for inserting the pass info to the database.
        /// </summary>
        /// <param name="pass">Object holds the values of the pass.</param>
        /// <returns>True if successfull, false otherwise.</returns>
        public static bool InsertPass(this Pass pass)
        {
            using (con = new SqlConnection("data source=.; database=Employee; integrated security=SSPI"))
            {
                SqlCommand command = new SqlCommand("AddPassToDB", con);
                command.CommandType = CommandType.StoredProcedure;

                // Adding input parameter and setting the property.                
                SqlParameter planType = new SqlParameter("@planType", pass.PlanType);
                SqlParameter vehicleType = new SqlParameter("@vehicleType", pass.VehicleType);
                SqlParameter amount = new SqlParameter("@amount", pass.Amount);
                SqlParameter vehicleId = new SqlParameter("@vehicleId", pass.VehicleId);

                // Adding the parameter to the Parameters collection.
                command.Parameters.Add(vehicleId);
                command.Parameters.Add(vehicleType);
                command.Parameters.Add(planType);
                command.Parameters.Add(amount);
                                
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

        /// <summary>
        /// Delete the complete record of employee,
        /// including vehicle detail and pass info.
        /// </summary>
        /// <param name="empId">Employee with the empId will get removed. If empId is wrong than data remains same.</param>
        /// <returns>True if no errors and false otherwise.</returns>
        public static bool DeleteEmployee(int empId)
        {
            using (con = new SqlConnection("data source=.; database=Employee; integrated security=SSPI"))
            {
                SqlCommand command = new SqlCommand("DeleteEmployeeFromDB", con);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@empId", empId);

                command.Parameters.Add(param);

                try
                {
                    con.Open();
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

        /// <summary>
        /// Retreive data from the database.
        /// </summary>
        /// <returns>Dataset which have all the required data.</returns>
        public static DataSet ShowEmployee()
        {
            using (con = new SqlConnection("data source=.; database=Employee; integrated security=SSPI"))
            {
                using (SqlCommand cmd = new SqlCommand("DisplayDataFromDB", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        //Create a new DataSet.
                        DataSet dsEmployee = new DataSet();
                        dsEmployee.Tables.Add("Employees");

                        //Load DataReader into the DataTable.
                        dsEmployee.Tables[0].Load(sdr);

                        con.Close();

                        return dsEmployee;
                    }                    
                }
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
                SqlCommand cmd = new SqlCommand("GetVehicleId", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                int vehicleId;
                try
                {
                    cmd.ExecuteNonQuery();
                    vehicleId = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
                }catch(Exception e)
                {
                    vehicleId = 1;
                    Console.WriteLine(e.Message);
                }
                con.Close();
                return vehicleId;
            }
        }

        /// <summary>
        /// Get Employee object from Database for the particular employee id.
        /// </summary>
        /// <param name="empId">Employee id</param>
        /// <returns>Employee object</returns>
        public static Employee GetEmployeeFromDb(int empId)
        {
            using (con = new SqlConnection("data source=.; database=Employee; integrated security=SSPI"))
            {
                using (SqlCommand cmd = new SqlCommand("GetEmployee", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@empId", empId));
                    con.Open();
                    Employee emp = new Employee();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            emp.EmpId = Convert.ToInt32(sdr["empId"]);
                            emp.EmailId = sdr["emailId"].ToString();
                            emp.ContactNumber = sdr["contactNumber"].ToString();
                            emp.FullName = sdr["fullName"].ToString();
                            emp.Organisation = sdr["organisation"].ToString();
                            emp.Password = sdr["empPassword"].ToString();
                            emp.Gender = sdr["gender"].ToString();
                        }
                    }

                    con.Close();
                    return emp;
                }
            }
        }

        /// <summary>
        /// Get the vehicle data from the database and store it on Vehicle object.
        /// </summary>
        /// <param name="empId">Employee did</param>
        /// <returns>Vehicle object</returns>
        public static Vehicle GetVehicleFromDb(int empId)
        {
            using (con = new SqlConnection("data source=.; database=Employee; integrated security=SSPI"))
            {
                using (SqlCommand cmd = new SqlCommand("GetVehicle", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@empId", empId));
                    con.Open();
                    Vehicle vehicle = new Vehicle();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            vehicle.EmpId = Convert.ToInt32(sdr["empId"]);
                            vehicle.VehicleId = Convert.ToInt32(sdr["vehicleId"]);
                            vehicle.VehicleName = sdr["vehicleName"].ToString();
                            vehicle.VehicleNumber = sdr["vehicleNumber"].ToString();
                            vehicle.VehicleType = sdr["vehicleType"].ToString();
                        }
                    }

                    con.Close();
                    return vehicle;
                }
            }
        }

        /// <summary>
        /// Get the pass information from the database and store it on Pass object.
        /// </summary>
        /// <param name="vehicleId">Vehicle id</param>
        /// <returns>Pass object</returns>
        public static Pass GetPassFromDb(int vehicleId)
        {
            using (con = new SqlConnection("data source=.; database=Employee; integrated security=SSPI"))
            {
                using (SqlCommand cmd = new SqlCommand("GetPass", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@vehicleId", vehicleId));
                    con.Open();
                    Pass pass = new Pass();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            pass.VehicleId = Convert.ToInt32(sdr["vehicleId"]);
                            pass.Amount = Convert.ToInt32(sdr["amount"]);
                            pass.VehicleType = sdr["vehicleType"].ToString();
                            pass.PlanType = sdr["planType"].ToString();
                        }
                    }

                    con.Close();
                    return pass;
                }
            }
        }
    }
}
