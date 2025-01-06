using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Apartment_Management.Model
{
	public class Employee
	{
		public string EmployeeID { get; set; }

        [JsonProperty("empl_name")]
        public string EmployeeName { get; set; }

        [JsonProperty("empl_birthday")]
        public DateTime EmployeeBirth { get; set; }

        [JsonProperty("empl_sex")]
        public string EmployeeSex { get; set; }

        [JsonProperty("empl_role")]
        public string EmployeeRole { get; set; }

        [JsonProperty("empl_phone")]
        public string EmployeePhone { get; set; }

        [JsonProperty("empl_mail")]
        public string EmployeeEmail { get; set; }

        [JsonProperty("empl_date_start")]
        public DateTime Date_Start { get; set; }

        [JsonProperty("empl_password")]
        public string EmployeePassword { get; set; }

        [JsonProperty("empl_status")]

        public string EmployeeStatus { get; set; }

        [JsonProperty("create_at")]

        public DateTime Create_At { get; set; }
		public DateTime Update_At { get; set; }
		public string Create_By { get; set; }
		public string Update_By { get; set; }
		public DateTime Delete_At { get; set; }

		public Employee()
		{
		}

		public Employee(string employeeID, string employeeName, DateTime employeeBirth, string employeeSex, string employeeRole, string employeePhone, string employeeEmail, DateTime date_Start, string employeePassword,string status, DateTime create_At, DateTime update_At, string create_By, string update_By, DateTime delete_At)
		{
			EmployeeID = employeeID;
			EmployeeName = employeeName;
			EmployeeBirth = employeeBirth;
			EmployeeSex = employeeSex;
			EmployeeRole = employeeRole;
			EmployeePhone = employeePhone;
			EmployeeEmail = employeeEmail;
			Date_Start = date_Start;
			EmployeePassword = employeePassword;
			EmployeeStatus = status;
			Create_At = create_At;
			Update_At = update_At;
			Create_By = create_By;
			Update_By = update_By;
			Delete_At = delete_At;
		}
		public Employee(Employee employee)
		{
            EmployeeID = employee.EmployeeID;
            EmployeeName = employee.EmployeeName;
            EmployeeBirth = employee.EmployeeBirth;
            EmployeeSex = employee.EmployeeSex;
            EmployeeRole = employee.EmployeeRole;
            EmployeePhone = employee.EmployeePhone;
            EmployeeEmail = employee.EmployeeEmail;
            Date_Start = employee.Date_Start;
            EmployeePassword = employee.EmployeePassword;
            EmployeeStatus = employee.EmployeeStatus;
            Create_At = employee.Create_At;
            Update_At = employee.Update_At;
            Create_By = employee.Create_By;
            Update_By = employee.Update_By;
            Delete_At = employee.Delete_At;
        }
	}
}
