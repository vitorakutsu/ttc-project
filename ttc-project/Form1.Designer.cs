namespace ttc_project
{
    partial class Application
    {
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();

            SuspendLayout();

            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(20, 20);
            this.pictureBox1.Size = new System.Drawing.Size(200, 150);
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(240, 20);
            this.pictureBox2.Size = new System.Drawing.Size(200, 150);
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(20, 200);
            this.button1.Size = new System.Drawing.Size(90, 30);
            this.button1.Text = "Button 1";

            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(120, 200);
            this.button2.Size = new System.Drawing.Size(90, 30);
            this.button2.Text = "Button 2";

            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(20, 240);
            this.button3.Size = new System.Drawing.Size(90, 30);
            this.button3.Text = "Button 3";

            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(120, 240);
            this.button4.Size = new System.Drawing.Size(90, 30);
            this.button4.Text = "Button 4";

            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(20, 280);
            this.button5.Size = new System.Drawing.Size(90, 30);
            this.button5.Text = "Button 5";

            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(120, 280);
            this.button6.Size = new System.Drawing.Size(90, 30);
            this.button6.Text = "Button 6";

            // 
            // Application
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button6);
            this.Text = "Application";
            this.ResumeLayout(false);
        }
    }
}
