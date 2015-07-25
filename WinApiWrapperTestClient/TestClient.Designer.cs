namespace WinApiWrapperTestClient
{
    partial class TestClient
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
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.textbox1 = new System.Windows.Forms.TextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.mousePosition = new System.Windows.Forms.TextBox();
            this.clientPosition = new System.Windows.Forms.TextBox();
            this.button10 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(487, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Refresh";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(469, 173);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(12, 191);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(469, 199);
            this.listBox2.TabIndex = 2;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(487, 128);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Close";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(487, 70);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(97, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "SetToolWindow";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(487, 41);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(97, 23);
            this.button5.TabIndex = 6;
            this.button5.Text = "IsToolWindow";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(487, 99);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "SetParent";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(487, 157);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 8;
            this.button6.Text = "Change Title";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // textbox1
            // 
            this.textbox1.Location = new System.Drawing.Point(568, 160);
            this.textbox1.Name = "textbox1";
            this.textbox1.Size = new System.Drawing.Size(228, 20);
            this.textbox1.TabIndex = 9;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(598, 12);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 10;
            this.button7.Text = "TopMost";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(598, 41);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 11;
            this.button8.Text = "!TopMost";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(487, 191);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(124, 23);
            this.button9.TabIndex = 12;
            this.button9.Text = "Toggle Mouse Cursor";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // mousePosition
            // 
            this.mousePosition.Location = new System.Drawing.Point(756, 493);
            this.mousePosition.Name = "mousePosition";
            this.mousePosition.Size = new System.Drawing.Size(118, 20);
            this.mousePosition.TabIndex = 13;
            // 
            // clientPosition
            // 
            this.clientPosition.Location = new System.Drawing.Point(756, 522);
            this.clientPosition.Name = "clientPosition";
            this.clientPosition.Size = new System.Drawing.Size(118, 20);
            this.clientPosition.TabIndex = 14;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(487, 220);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(124, 23);
            this.button10.TabIndex = 15;
            this.button10.Text = "PerformClick";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // TestClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 554);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.clientPosition);
            this.Controls.Add(this.mousePosition);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textbox1);
            this.Name = "TestClient";
            this.Text = "WinApiWrapper Test Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TestClient_FormClosing);
            this.Load += new System.EventHandler(this.TestClient_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox textbox1;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.TextBox mousePosition;
        private System.Windows.Forms.TextBox clientPosition;
        private System.Windows.Forms.Button button10;
    }
}

