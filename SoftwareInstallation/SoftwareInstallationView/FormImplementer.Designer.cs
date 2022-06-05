
namespace SoftwareInstallationView
{
    partial class FormImplementer
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxPauseTime = new System.Windows.Forms.TextBox();
            this.textBoxWorkTime = new System.Windows.Forms.TextBox();
            this.textBoxFIO = new System.Windows.Forms.TextBox();
            this.labelPauseTime = new System.Windows.Forms.Label();
            this.labelWorkTime = new System.Windows.Forms.Label();
            this.labelFIO = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(238, 161);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 38);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(77, 161);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(100, 38);
            this.buttonSave.TabIndex = 15;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxPauseTime
            // 
            this.textBoxPauseTime.Location = new System.Drawing.Point(163, 106);
            this.textBoxPauseTime.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxPauseTime.Name = "textBoxPauseTime";
            this.textBoxPauseTime.Size = new System.Drawing.Size(203, 23);
            this.textBoxPauseTime.TabIndex = 14;
            // 
            // textBoxWorkTime
            // 
            this.textBoxWorkTime.Location = new System.Drawing.Point(163, 64);
            this.textBoxWorkTime.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxWorkTime.Name = "textBoxWorkTime";
            this.textBoxWorkTime.Size = new System.Drawing.Size(203, 23);
            this.textBoxWorkTime.TabIndex = 13;
            // 
            // textBoxFIO
            // 
            this.textBoxFIO.Location = new System.Drawing.Point(163, 24);
            this.textBoxFIO.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxFIO.Name = "textBoxFIO";
            this.textBoxFIO.Size = new System.Drawing.Size(203, 23);
            this.textBoxFIO.TabIndex = 12;
            // 
            // labelPauseTime
            // 
            this.labelPauseTime.AutoSize = true;
            this.labelPauseTime.Location = new System.Drawing.Point(25, 109);
            this.labelPauseTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPauseTime.Name = "labelPauseTime";
            this.labelPauseTime.Size = new System.Drawing.Size(108, 15);
            this.labelPauseTime.TabIndex = 11;
            this.labelPauseTime.Text = "Время перерыва : ";
            // 
            // labelWorkTime
            // 
            this.labelWorkTime.AutoSize = true;
            this.labelWorkTime.Location = new System.Drawing.Point(25, 67);
            this.labelWorkTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelWorkTime.Name = "labelWorkTime";
            this.labelWorkTime.Size = new System.Drawing.Size(95, 15);
            this.labelWorkTime.TabIndex = 10;
            this.labelWorkTime.Text = "Время на заказ: ";
            // 
            // labelFIO
            // 
            this.labelFIO.AutoSize = true;
            this.labelFIO.Location = new System.Drawing.Point(88, 24);
            this.labelFIO.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelFIO.Name = "labelFIO";
            this.labelFIO.Size = new System.Drawing.Size(40, 15);
            this.labelFIO.TabIndex = 9;
            this.labelFIO.Text = "ФИО: ";
            // 
            // FormImplementer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 230);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxPauseTime);
            this.Controls.Add(this.textBoxWorkTime);
            this.Controls.Add(this.textBoxFIO);
            this.Controls.Add(this.labelPauseTime);
            this.Controls.Add(this.labelWorkTime);
            this.Controls.Add(this.labelFIO);
            this.Name = "FormImplementer";
            this.Text = "Исполнитель";
            this.Load += new System.EventHandler(this.FormImplementer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxPauseTime;
        private System.Windows.Forms.TextBox textBoxWorkTime;
        private System.Windows.Forms.TextBox textBoxFIO;
        private System.Windows.Forms.Label labelPauseTime;
        private System.Windows.Forms.Label labelWorkTime;
        private System.Windows.Forms.Label labelFIO;
    }
}