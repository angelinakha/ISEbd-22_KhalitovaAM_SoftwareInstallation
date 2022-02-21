using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SoftwareInstallationContracts.BusinessLogicsContracts;
using SoftwareInstallationContracts.ViewModels;
using SoftwareInstallationContracts.BindingModels;


namespace SoftwareInstallationView
{
    public partial class FormWarehouseReplenishment : Form
    {

        private readonly IWarehouseLogic _logicWareh;

        private readonly IComponentLogic _logicComp;
        public FormWarehouseReplenishment(IWarehouseLogic logicWareh, IComponentLogic logicComp)
        {
            InitializeComponent();
            _logicWareh = logicWareh;
            _logicComp = logicComp;
        }
        private void FormWarehouseReplenishment_Load(object sender, EventArgs e)
        {
            try
            {
                List<WarehouseViewModel> listWarehouse = _logicWareh.Read(null);
                if (listWarehouse != null)
                {
                    comboBox_warehouseName.DisplayMember = "WarehouseName";
                    comboBox_warehouseName.ValueMember = "Id";
                    comboBox_warehouseName.DataSource = listWarehouse;
                    comboBox_warehouseName.SelectedItem = null;
                }

                List<ComponentViewModel> listComponent = _logicComp.Read(null);
                if (listComponent != null)
                {
                    comboBoxComponent.DisplayMember = "ComponentName";
                    comboBoxComponent.ValueMember = "Id";
                    comboBoxComponent.DataSource = listComponent;
                    comboBoxComponent.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле количество", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxComponent.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            if (comboBox_warehouseName.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            try
            {
                List<ComponentViewModel> listComponent = _logicComp.Read(null);
                _logicWareh.Replenishment(new WarehouseReplenishmentBindingModel
                {
                    WarehouseId = Convert.ToInt32(comboBox_warehouseName.SelectedValue),
                    ComponentId = listComponent[comboBoxComponent.SelectedIndex].Id,
                    Count = Convert.ToInt32(textBoxCount.Text)
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
