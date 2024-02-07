namespace General_department
{
	partial class Auth
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
			label1 = new Label();
			Login = new TextBox();
			Password = new TextBox();
			label2 = new Label();
			label3 = new Label();
			checkBox1 = new CheckBox();
			button1 = new Button();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
			label1.Location = new Point(92, 35);
			label1.Name = "label1";
			label1.Size = new Size(142, 28);
			label1.TabIndex = 0;
			label1.Text = "Авторизация";
			// 
			// Login
			// 
			Login.Location = new Point(83, 107);
			Login.Name = "Login";
			Login.Size = new Size(183, 27);
			Login.TabIndex = 1;
			// 
			// Password
			// 
			Password.Location = new Point(83, 196);
			Password.Name = "Password";
			Password.Size = new Size(183, 27);
			Password.TabIndex = 2;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(25, 107);
			label2.Name = "label2";
			label2.Size = new Size(52, 20);
			label2.TabIndex = 3;
			label2.Text = "Логин";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(15, 199);
			label3.Name = "label3";
			label3.Size = new Size(62, 20);
			label3.TabIndex = 4;
			label3.Text = "Пароль";
			// 
			// checkBox1
			// 
			checkBox1.Location = new Point(282, 186);
			checkBox1.Name = "checkBox1";
			checkBox1.Size = new Size(99, 48);
			checkBox1.TabIndex = 5;
			checkBox1.Text = "Показать пароль";
			checkBox1.UseVisualStyleBackColor = true;
			checkBox1.CheckedChanged += checkBox1_CheckedChanged;
			// 
			// button1
			// 
			button1.Location = new Point(149, 279);
			button1.Name = "button1";
			button1.Size = new Size(94, 29);
			button1.TabIndex = 6;
			button1.Text = "Войти";
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click;
			// 
			// Auth
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(393, 358);
			Controls.Add(button1);
			Controls.Add(checkBox1);
			Controls.Add(label3);
			Controls.Add(label2);
			Controls.Add(Password);
			Controls.Add(Login);
			Controls.Add(label1);
			Name = "Auth";
			Text = "Auth";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label label1;
		private TextBox Login;
		private TextBox Password;
		private Label label2;
		private Label label3;
		private CheckBox checkBox1;
		private Button button1;
	}
}