namespace General_department
{
	partial class Home
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			btnExit = new Button();
			comboBox1 = new ComboBox();
			dataGridView1 = new DataGridView();
			((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
			SuspendLayout();
			// 
			// btnExit
			// 
			btnExit.Location = new Point(1134, 12);
			btnExit.Name = "btnExit";
			btnExit.Size = new Size(167, 29);
			btnExit.TabIndex = 0;
			btnExit.Text = "button1";
			btnExit.UseVisualStyleBackColor = true;
			btnExit.Click += btnExit_Click;
			// 
			// comboBox1
			// 
			comboBox1.FormattingEnabled = true;
			comboBox1.Items.AddRange(new object[] { "Индивидуальные", "Групповые" });
			comboBox1.Location = new Point(28, 13);
			comboBox1.Name = "comboBox1";
			comboBox1.Size = new Size(151, 28);
			comboBox1.TabIndex = 1;
			comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
			// 
			// dataGridView1
			// 
			dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridView1.Location = new Point(28, 75);
			dataGridView1.Name = "dataGridView1";
			dataGridView1.RowHeadersWidth = 51;
			dataGridView1.RowTemplate.Height = 29;
			dataGridView1.ScrollBars = ScrollBars.Vertical;
			dataGridView1.Size = new Size(1273, 635);
			dataGridView1.TabIndex = 2;
			// 
			// Home
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1313, 722);
			Controls.Add(dataGridView1);
			Controls.Add(comboBox1);
			Controls.Add(btnExit);
			Name = "Home";
			Text = "Home";
			((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private Button btnExit;
		private ComboBox comboBox1;
		private DataGridView dataGridView1;
	}
}