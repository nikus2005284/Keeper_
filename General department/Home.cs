using ClassLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace General_department
{
	public partial class Home : Form
	{
		public Home()
		{
			InitializeComponent();
			btnExit.Text = "Выход (" + Program.employees.name + ")";
			refresh();
		}

		public List<GroupUsers> GroupUsers = new List<GroupUsers>();
		public List<Individ> Individs = new List<Individ>();

		public async void refresh()
		{
			HttpClient client = new HttpClient();

			Individs = await client.GetFromJsonAsync<List<Individ>>($"https://localhost:7108/api/Api/GetIndivids");
			GroupUsers = await client.GetFromJsonAsync<List<GroupUsers>>($"https://localhost:7108/api/Api/GetGroups");
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			Program.employees = null;
			Auth auth = new Auth();
			auth.Show();
			this.Hide();
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBox1.Text == "Индивидуальные")
			{
				dataGridView1.DataSource = Individs;
			}
			else if (comboBox1.Text == "Групповые")
			{
				dataGridView1.DataSource = GroupUsers;
			}
		}
	}
}
