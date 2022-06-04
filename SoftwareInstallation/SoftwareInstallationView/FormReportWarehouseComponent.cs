using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SoftwareInstallationContracts.BindingModels;
using SoftwareInstallationContracts.BusinessLogicsContracts;

namespace SoftwareInstallationView
{
    public partial class FormReportWarehouseComponent : Form
    {
        private readonly IReportLogic _logic;
        public FormReportWarehouseComponent(IReportLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void FormReportWarehouseComponent_Load(object sender, EventArgs e)
        {
            try
            {
                var dict = _logic.GetWarehouseComponent();
                if (dict != null)
                {
                    dataGridView1.Rows.Clear();
                    foreach (var elem in dict)
                    {
                        dataGridView1.Rows.Add(new object[] { elem.WarehouseName, "", "" });
                        foreach (var listElem in elem.Components)
                        {
                            dataGridView1.Rows.Add(new object[] { "", listElem.Item1, listElem.Item2 });
                        }
                        dataGridView1.Rows.Add(new object[] { "Итого", "", elem.TotalCount });
                        dataGridView1.Rows.Add(Array.Empty<object>());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSaveToExcel_Click(object sender, EventArgs e)
        {
            using var dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _logic.SaveWarehouseComponentToExcelFile(new ReportBindingModel
                    {
                        FileName = dialog.FileName
                    });
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


    }
}
