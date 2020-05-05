using System;
using System.Data;

namespace EmployeeRegistration
{
    class Program
    {
        /// <summary>
        /// Driver method of the program.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Employee Registration.");
            bool flag = true;
            while (flag)
            {
                Console.WriteLine();
                Console.WriteLine("1. Add Employee\n2. Show Employees\n3. Delete Employee\n4. Exit");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        AddEmployee();
                        break;
                    case 2:
                        PrintEmployee(DaoImplementation.ShowEmployee());
                        break;
                    case 3:
                        SelectEmployeeForDeletion();
                        break;                        
                    case 4:
                        Console.WriteLine("Exit");
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Invalid");
                        break;
                }
            }
            
        }

        /// <summary>
        /// Select employee for deletion, this is achieved
        /// by getting the empId and calling the DeleteEmployee() method.
        /// </summary>
        private static void SelectEmployeeForDeletion()
        {
            int empId;
            PrintEmployee(DaoImplementation.ShowEmployee());
            Console.Write("Enter Employee Id: ");
            empId = Convert.ToInt32(Console.ReadLine());
            if (!DaoImplementation.DeleteEmployee(empId))
            {
                Console.WriteLine("Employee does not exists.");
            }
        }

        /// <summary>
        /// Print employee on the console.
        /// </summary>
        /// <param name="dataSet">DataSet holds the data from the database which is displayed here.</param>
        private static void PrintEmployee(DataSet dataSet)
        {
            foreach (DataTable table in dataSet.Tables)
            {
                Console.WriteLine();
                foreach (DataRow row in table.Rows)
                {
                    Console.WriteLine("------------------------------------");
                    Console.WriteLine($"\nEmp Id: {row["empId"]}\nFull Name: {row["fullName"]}\nGender: {row["gender"]}\nEmail Id: {row["emailId"]}\nContact Number: {row["contactNumber"]}" +
                        $"\nOrganisation: {row["organisation"]}\nVehicle Name: {row["vehicleName"]}\nVehicle Number: {row["vehicleNumber"]}\nVehicle Type: {row["vehicleType"]}\n" +
                        $"Plan Type: {row["planType"]}\nAmount: {row["amount"]}");
                }
                
            }
        }

        /// <summary>
        /// Add an employee, vehicle and pass info.
        /// </summary>
        private static void AddEmployee()
        {
            Employee employee = GetEmployeeObject();
            Vehicle vehicle = GetVehicleObject(employee.EmpId);
            Pass pass = GetPassObject(vehicle.VehicleId, vehicle.VehicleType);

            if (employee.InsertEmployee() && vehicle.InsertVehicle() && pass.InsertPass())
            {
                Console.WriteLine("Insertion Successful");
            }
            else
            {
                Console.WriteLine("Something went wrong.");
            }
        }

        /// <summary>
        /// Take input from the user regarding Pass.
        /// </summary>
        /// <param name="vehicleId">Attribute of Pass class and can be get from vehicle class.</param>
        /// <param name="vehicleType">Attribute of Pass class and can be get from vehicle class.</param>
        /// <returns>It returns Pass object.</returns>
        private static Pass GetPassObject(int vehicleId, string vehicleType)
        {
            Pass pass = new Pass();

            pass.VehicleId = vehicleId;
            pass.VehicleType = vehicleType;
            Console.Write("Enter Plan type(Daily, Monthly, Yearly): ");
            pass.PlanType = Console.ReadLine();            
            if(pass.PlanType.ToLower() == "daily")
            {
                pass.Amount = 15;
            }
            else if (pass.PlanType.ToLower() == "monthly")
            {
                pass.Amount = 400;
            }
            else
            {
                pass.Amount = 4500;
            }

            return pass;
        }

        /// <summary>
        /// Take input from the user regarding Vehicle.
        /// </summary>
        /// <param name="empId">Attribute of Vehicle class and can be get from employee class.</param>
        /// <returns>It return Vehicle object.</returns>
        private static Vehicle GetVehicleObject(int empId)
        {
            Vehicle vehicle = new Vehicle();

            vehicle.EmpId = empId;
            Console.Write("Enter Vehicle Id: ");
            vehicle.VehicleId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Vehicle Name: ");
            vehicle.VehicleName = Console.ReadLine();
            Console.Write("Enter Vehicle Number: ");
            vehicle.VehicleNumber = Console.ReadLine();
            Console.Write("Enter Vehicle Type: ");
            vehicle.VehicleType = Console.ReadLine();

            return vehicle;
        }

        /// <summary>
        /// Take input from the user regarding Employee.
        /// </summary>
        /// <returns>It return employee object.</returns>
        private static Employee GetEmployeeObject()
        {
            Employee employee = new Employee();

            Console.Write("Enter Employee Id: ");
            employee.EmpId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Full Name: ");
            employee.FullName = Console.ReadLine();
            Console.Write("Enter Gender: ");
            employee.Gender = Console.ReadLine();
            Console.Write("Enter Email Id: ");
            employee.EmailId = Console.ReadLine();
            Console.Write("Enter Password: ");
            employee.Password = Console.ReadLine();
            Console.Write("Enter Contact Number: ");
            employee.ContactNumber = Console.ReadLine();
            Console.Write("Enter Organisation: ");
            employee.Organisation = Console.ReadLine();

            return employee;
        }
    }
}
