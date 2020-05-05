using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Serialization
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This method is invoked when the user click on Serialization button present in UI.
        /// </summary>
        /// <param name="sender">Contains a reference to the control/object that raised the event.</param>
        /// <param name="e">It contains the event data.</param>
        private void Serialization_Click(object sender, EventArgs e)
        {
            //Here,Employee class object 'emp' data is filled using fields property.
            Employee emp = new Employee
            {
                Name = txtName.Text,
                Phone = txtNumber.Text,
                DoB = dateOfBirth.Value,
                Department = txtDepartment.Text,
                Salary = Convert.ToInt32(txtSalary.Text)
            };
            
            //If else blocks are used in order to check if the user selected value in the drop down option.
            if(dropDown.Text == "Binary")
            {
                BinarySerialization(emp);
            }
            else if(dropDown.Text == "XML")
            {
                XMLSerialization(emp);
            }
            else if (dropDown.Text == "JSON")
            {
                JSONSerialization(emp);
            }
        }

        /// <summary>
        /// Binary serialization is performed on object of Employee class.
        /// </summary>
        /// <param name="emp">Employee class object on which binary serialization is going to performed.</param>
        private void BinarySerialization(Employee emp)
        {
            // FileStream provides a Stream for a file, supporting 
            //both synchronous and asynchronous read and write operations. 
            //A stream is a flow of data from a source into a destination.
            FileStream fileOut = new FileStream("employee", FileMode.Create, FileAccess.Write, FileShare.None);

            //Here serialization is performed and in case of error updation will be done on screen.
            try
            {
                //Binary Formatter is used to serialize the object.
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                using (fileOut)
                {
                    binaryFormatter.Serialize(fileOut, emp);
                    update.Text = "Object Serialized";
                    update.ForeColor = Color.Blue;
                }
            }
            catch
            {
                update.Text = "Error occured.";
                update.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// Xml serialization is performed on object of Employee class.
        /// </summary>
        /// <param name="emp">Employee class object on which Xml serialization is going to performed.</param>
        private void XMLSerialization(Employee emp)
        {
            try
            {
                //XmlSerializer is used to serialize in xml file.
                XmlSerializer serializer = new XmlSerializer(typeof(Employee));
                
                using (FileStream file = File.Create("XmlEmployee.xml"))
                {
                    serializer.Serialize(file, emp);
                    update.Text = "Object Serialized";
                    update.ForeColor = Color.Blue;
                }
            }
            catch
            {
                update.Text = "Error occured.";
                update.ForeColor = Color.Red;
            }

        }

        /// <summary>
        /// Json serialization is performed on object of Employee class.
        /// </summary>
        /// <param name="emp">Employee class object on which Json serialization is going to performed.</param>
        private void JSONSerialization(Employee emp)
        {
            try
            {
                //JsonSerializer is used to serialize in Json format.
                JsonSerializer serializer = new JsonSerializer();

                //Here StreamWriter is used alonf with JsonWriter to write in a file.
                using (StreamWriter sw = new StreamWriter("json.txt"))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, emp);
                    update.Text = "Object Serialized";
                    update.ForeColor = Color.Blue;
                }
            }
            catch
            {
                update.Text = "Error occured.";
                update.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// It is invoked when the user click on Deserialization button present in UI.
        /// </summary>
        /// <param name="sender">Contains a reference to the control/object that raised the event.</param>
        /// <param name="e">It contains the event data.</param>
        private void Deserialization_Click(object sender, EventArgs e)
        {
            Employee emp = null;
            //If else blocks are used in order to check if the user selected value in the drop down option.
            if (dropDown.Text == "Binary")
            {
                emp = BinaryDeserialization();
            }
            else if (dropDown.Text == "XML")
            {
                emp = XMLDeserialization();
            }
            else if (dropDown.Text == "JSON")
            {
                emp = (Employee)JSONDeserialization();
            }

            //All the fields of UI are set here, in accordance to Employee object field.
            if(emp != null)
            {
                txtName.Text = emp.Name;
                txtNumber.Text = emp.Phone;
                dateOfBirth.Value = emp.DoB;
                txtDepartment.Text = emp.Department;
                txtSalary.Text = emp.Salary.ToString();
            }
        }

        /// <summary>
        /// Deserialization on Binary File.
        /// </summary>
        /// <returns>Employee object.</returns>
        private Employee BinaryDeserialization()
        {
            Employee emp = null;            
            FileStream fileStream = new FileStream("employee", FileMode.Open, FileAccess.Read, FileShare.None);

            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                using (fileStream)
                {                   
                    emp = (Employee)binaryFormatter.Deserialize(fileStream);
                    update.Text = "Object Deserialized";
                    update.ForeColor = Color.Blue;
                    return emp;
                }
            }
            catch
            {
                update.Text = "Error Occured.";
                return null;
            }
        }

        /// <summary>
        /// Deserialization on Xml File.
        /// </summary>
        /// <returns>Employee object.</returns>
        private Employee XMLDeserialization() 
        {
            Employee emp = new Employee();
            XmlSerializer serializer = new XmlSerializer(typeof(Employee));
            try
            {
                using(FileStream file = File.OpenRead("XmlEmployee.xml"))
                {
                    emp = (Employee)serializer.Deserialize(file);
                    update.Text = "Object Deserialized";
                    update.ForeColor = Color.Blue;
                    return emp;
                }
            }
            catch
            {
                update.Text = "Error Occured.";
                return null;
            }
        }

        /// <summary>
        /// Deserialization on Json File.
        /// </summary>
        /// <returns>Object which is type casted in Employee.</returns>
        private Object JSONDeserialization()
        {
            JObject obj = null;
            JsonSerializer serializer = new JsonSerializer();
            try
            {
                using (StreamReader streamReader = new StreamReader("json.txt"))
                using (JsonTextReader reader = new JsonTextReader(streamReader))
                {
                    obj = (JObject)serializer.Deserialize(reader);
                    update.Text = "Object Deserialized";
                    update.ForeColor = Color.Blue;
                    return obj.ToObject(typeof(Employee));
                }
            }
            catch
            {
                update.Text = "Error Occured.";
                return null;
            }
        }

        /// <summary>
        /// It is an error provider.
        /// Put validation constraint on Salary field, in case user enters non numeric values.
        /// </summary>
        /// <param name="sender">Contains a reference to the control/object that raised the event.</param>
        /// <param name="e">true if the event should be canceled, false otherwise.</param>
        private void ErrorProviderSalary(object sender, CancelEventArgs e)
        {
            try
            {
                Convert.ToInt32(txtSalary.Text);
                e.Cancel = false;
            }
            catch
            {
                errorProvider.SetError(txtSalary, "This must be an integer value.");
                e.Cancel = true;
            }
            
        }

        /// <summary>
        /// It is Error provider.
        /// Validation constraint on Drop down menu, in case user does not select any option. 
        /// </summary>
        /// <param name="sender">Contains a reference to the control/object that raised the event.</param>
        /// <param name="e">It is true if the event should be canceled, false otherwise.</param>
        private void CheckSerializationDropDown(object sender, CancelEventArgs e)
        {
            if (dropDown.Text == "Select Serialization" || dropDown.Text == "")
            {
                errorProvider1.SetError(dropDown, "Please select one option.");
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }
    }
}
