using ClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Web;
using System.Net;

namespace General_department
{
	public partial class Auth : Form
	{
		public Auth()
		{
			InitializeComponent();
			Password.UseSystemPasswordChar = PasswordPropertyTextAttribute.No.Password;
		}

		private async void button1_Click(object sender, EventArgs e)
		{
			if (Login.Text.Length > 0 && Password.Text.Length > 0)
			{
				HttpClient client = new HttpClient();
				Employees employees = new Employees();
				employees.login_employee = Login.Text;
				employees.password_employee = Password.Text;
				using var response = await client.PostAsJsonAsync($"https://localhost:7108/api/Api/AuthEmployee", employees);
				// если объект на сервере найден, то есть статусный код равен 404
				if (response.StatusCode == HttpStatusCode.OK)
				{
					// считываем ответ
					employees = await response.Content.ReadFromJsonAsync<Employees>();
					if (employees != null)
					{
						Program.employees = employees;
						Home home = new Home();
						home.Show();
						this.Hide();
					}
				}
			}
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			if (!checkBox1.Checked)
			{
				Password.UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password;
			}
			else { Password.UseSystemPasswordChar = PasswordPropertyTextAttribute.No.Password; }
		}
	}
}
