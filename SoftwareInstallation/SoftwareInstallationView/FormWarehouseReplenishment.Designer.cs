
namespace SoftwareInstallationView
{
    partial class FormWarehouseReplenishment
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
            this.textBoxCount = new System.Windows.Forms.TextBox();
            this.comboBoxComponent = new System.Windows.Forms.ComboBox();
            this.labelNum = new System.Windows.Forms.Label();
            this.labelComp = new System.Windows.Forms.Label();
            this.label_WarehouseName = new System.Windows.Forms.Label();
            this.comboBox_warehouseName = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(355, 136);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(111, 26);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(238, 136);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(111, 26);
            this.buttonSave.TabIndex = 10;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxCount
            // 
            this.textBoxCount.Location = new System.Drawing.Point(86, 94);
            this.textBoxCount.Name = "textBoxCount";
            this.textBoxCount.Size = new System.Drawing.Size(320, 23);
            this.textBoxCount.TabIndex = 9;
            // 
            // comboBoxComponent
            // 
            this.comboBoxComponent.FormattingEnabled = true;
            this.comboBoxComponent.Location = new System.Drawing.Point(86, 58);
            this.comboBoxComponent.Name = "comboBoxComponent";
            this.comboBoxComponent.Size = new System.Drawing.Size(320, 23);
            this.comboBoxComponent.TabIndex = 8;
            // 
            // labelNum
            // 
            this.labelNum.AutoSize = true;
            this.labelNum.Location = new System.Drawing.Point(5, 98);
            this.labelNum.Name = "labelNum";
            this.labelNum.Size = new System.Drawing.Size(75, 15);
            this.labelNum.TabIndex = 7;
            this.labelNum.Text = "Количество:";
            // 
            // labelComp
            // 
            this.labelComp.AutoSize = true;
            this.labelComp.Location = new System.Drawing.Point(5, 60);
            this.labelComp.Name = "labelComp";
            this.labelComp.Size = new System.Drawing.Size(72, 15);
            this.labelComp.TabIndex = 6;
            this.labelComp.Text = "Компонент:";
            // 
            // label_WarehouseName
            // 
            this.label_WarehouseName.AutoSize = true;
            this.label_WarehouseName.Location = new System.Drawing.Point(5, 19);
            this.label_WarehouseName.Name = "label_WarehouseName";
            this.label_WarehouseName.Size = new System.Drawing.Size(43, 15);
            this.label_WarehouseName.TabIndex = 12;
            this.label_WarehouseName.Text = "Склад:";
            // 
            // comboBox_warehouseName
            // 
            this.comboBox_warehouseName.FormattingEnabled = true;
            this.comboBox_warehouseName.Location = new System.Drawing.Point(54, 16);
            this.comboBox_warehouseName.Name = "comboBox_warehouseName";
            this.comboBox_warehouseName.Size = new System.Drawing.Size(352, 23);
            this.comboBox_warehouseName.TabIndex = 13;
            // 
            // FormWarehouseReplenishment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 181);
            this.Controls.Add(this.comboBox_warehouseName);
            this.Controls.Add(this.label_WarehouseName);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxCount);
            this.Controls.Add(this.comboBoxComponent);
            this.Controls.Add(this.labelNum);
            this.Controls.Add(this.labelComp);
            this.Name = "FormWarehouseReplenishment";
            this.Text = "Пополнение склада";
            this.Load += new System.EventHandler(this.FormWarehouseReplenishment_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxCount;
        private System.Windows.Forms.ComboBox comboBoxComponent;
        private System.Windows.Forms.Label labelNum;
        private System.Windows.Forms.Label labelComp;
        private System.Windows.Forms.Label label_WarehouseName;
        private System.Windows.Forms.ComboBox comboBox_warehouseName;
    }
}