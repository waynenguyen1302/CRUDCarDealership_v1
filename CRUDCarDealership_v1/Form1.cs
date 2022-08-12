using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDCarDealership_v1
{
    public partial class Form1 : Form
    {   
        Make make = new Make();
        Vehicle vehicle = new Vehicle();
        VehicleModel vehicleModel = new VehicleModel();
        VehicleType vehicleType = new VehicleType();
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        //Create a method to clear all the text boxes
        void Clear()
        {
            txtYear.Text = txtPrice.Text = txtCost.Text = txtSoldDate.Text = txtMake.Text = txtEngineSize.Text = txtNumberOfDoors.Text = txtColor.Text = txtVehicleType.Text = "";
            btnSave.Text = "Save";
            btnDelete.Enabled = false;
            make.MakeID = 0;
            vehicle.VehicleID = 0;
            vehicleModel.VehicleModelID = 0;
            vehicleType.VehicleTypeID = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            vehicle.Year = decimal.Parse(txtYear.Text.Trim());
            vehicle.Price = decimal.Parse(txtPrice.Text.Trim());
            vehicle.Cost = decimal.Parse(txtCost.Text.Trim());
            vehicle.SoldDate = DateTime.Parse(txtSoldDate.Text.Trim());

            //make.MakeID = int.Parse(txtMakeID.Text.Trim());
            make.Make1 = txtMake.Text.Trim();

            
            vehicleModel.EngineSize = decimal.Parse(txtEngineSize.Text.Trim());
            vehicleModel.NumberOfDoors = int.Parse(txtNumberOfDoors.Text.Trim());
            vehicleModel.Color = txtColor.Text.Trim();

            //vehicleType.VehicleTypeID = int.Parse(txtVehicleTypeID.Text.Trim());
            vehicleType.VehicleType1 = txtVehicleType.Text.Trim();


            using(EFDBEntities db = new EFDBEntities())
            {
                if (vehicle.VehicleID == 0)
                {
                    db.Vehicles.Add(vehicle);
                    db.Makes.Add(make);
                    db.VehicleModels.Add(vehicleModel);
                    db.VehicleTypes.Add(vehicleType);
                } else
                {
                    db.Entry(vehicle).State = EntityState.Modified;
                    db.Entry(vehicleModel).State = EntityState.Modified;
                    db.Entry(make).State = EntityState.Modified;
                    db.Entry(vehicleType).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
            Clear();
            PopulateDataGridView();
            MessageBox.Show("Vehicle Saved Successfully!");
        }

        private void Form1_Load(object sender, EventArgs e)
        {   
            Clear();
            PopulateDataGridView();
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //READ function, populate the grid view with data from our database
        void PopulateDataGridView()
        {
            dgvVehicle.AutoGenerateColumns = false;
            dgvVehicleModel.AutoGenerateColumns = false;
            using(EFDBEntities db = new EFDBEntities())
            {
                dgvVehicle.DataSource = db.Vehicles.ToList<Vehicle>();
                dgvVehicleModel.DataSource = db.VehicleModels.ToList<VehicleModel>();
                dgvVehicleType.DataSource = db.VehicleTypes.ToList<VehicleType>();
                dgvMake.DataSource = db.Makes.ToList<Make>();
            }
        }

        private void dgvVehicle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvVehicle_DoubleClick(object sender, EventArgs e)
        {
            if(dgvVehicle.CurrentRow.Index != -1)
            {
                vehicle.VehicleID = Convert.ToInt32(dgvVehicle.CurrentRow.Cells["vehicleIDDataGridViewTextBoxColumn"].Value);
                make.MakeID = Convert.ToInt32(dgvMake.CurrentRow.Cells["MakeID"].Value);
                vehicleModel.VehicleModelID = Convert.ToInt32(dgvVehicleModel.CurrentRow.Cells["vehicleModelIDDataGridViewTextBoxColumn"].Value);
                vehicleType.VehicleTypeID = Convert.ToInt32(dgvVehicleType.CurrentRow.Cells["VehicleTypeID"].Value);

                using (EFDBEntities db = new EFDBEntities())
                {   
                    vehicle = db.Vehicles.Where(x => x.VehicleID == vehicle.VehicleID).FirstOrDefault();
                    txtYear.Text = vehicle.Year.ToString();
                    txtPrice.Text = vehicle.Price.ToString();
                    txtCost.Text = vehicle.Cost.ToString();
                    txtSoldDate.Text = vehicle.SoldDate.ToString();

                    make = db.Makes.Where(x => x.MakeID == make.MakeID).FirstOrDefault();
                    txtMake.Text = make.Make1;

                    vehicleModel = db.VehicleModels.Where(x => x.VehicleModelID == vehicleModel.VehicleModelID).FirstOrDefault();
                    txtEngineSize.Text = vehicleModel.EngineSize.ToString();
                    txtNumberOfDoors.Text = vehicleModel.NumberOfDoors.ToString();
                    txtColor.Text = vehicleModel.Color.ToString();

                    vehicleType = db.VehicleTypes.Where(x => x.VehicleTypeID == vehicleType.VehicleTypeID).FirstOrDefault();
                    txtVehicleType.Text = vehicleType.VehicleType1.ToString();

                }
                btnSave.Text = "Update";
                btnDelete.Enabled = true;
            }
        }

        private void dgvVehicleType_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to delete this record?", "CRUD Assignment", MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                using(EFDBEntities db = new EFDBEntities())
                {
                    var entry = db.Entry(vehicle);
                    if(entry.State == EntityState.Detached)
                    {
                        db.Vehicles.Attach(vehicle);
                        db.Vehicles.Remove(vehicle);
                        db.Makes.Attach(make);
                        db.Makes.Remove(make);
                        db.VehicleModels.Attach(vehicleModel);
                        db.VehicleModels.Remove(vehicleModel);
                        db.VehicleTypes.Attach(vehicleType);
                        db.VehicleTypes.Remove(vehicleType);
                        db.SaveChanges();
                        PopulateDataGridView();
                        Clear();
                        MessageBox.Show("Delete successfully!");
                    }
                }
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
