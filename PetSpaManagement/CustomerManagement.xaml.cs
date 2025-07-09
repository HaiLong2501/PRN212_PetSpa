using PetSpaManagement.BLL.Customers;
using PetSpaManagement.BLL.Pets;
using PetSpaManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PetSpaManagement
{
    /// <summary>
    /// Interaction logic for CustomerManagement.xaml
    /// </summary>
    public partial class CustomerManagement : Window
    {
        private CustomerController customerService = new CustomerController();
        private PetController petService = new PetController();
        public CustomerManagement()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataCustomerGrid();
            RenderCmbData();
        }

        private void dgCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCustomer = dgCustomers.SelectedItem as Customer;

            if (selectedCustomer == null)
            {
                dgPets.ItemsSource = null;
                return;
            }

            txtCustomerId.Text = selectedCustomer.CustomerId.ToString();

            txtCustomerName.Text = selectedCustomer.FullName;

            txtPhone.Text = selectedCustomer.Phone;

            txtEmail.Text = selectedCustomer.Email;

            txtAddress.Text = selectedCustomer.Address;

            LoadPetBySelectCustomer(selectedCustomer);

            RenderCmbData();
        }

        private void RenderCmbData()
        {
            cmbGender.ItemsSource = new List<string> { "Đực", "Cái" };
            cmbSpecies.ItemsSource = new List<string> { "Chó", "Mèo", "Thỏ", "Hamster" };
        }

        private void LoadPetBySelectCustomer(Customer selectedCustomer)
        {
            var pets = petService.GetPetsByCustomerId(selectedCustomer.CustomerId);

            dgPets.ItemsSource = pets;
        }

        private void LoadDataCustomerGrid()
        {
            var customers = customerService.GetAllCustomers();
            dgCustomers.ItemsSource = customers;
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = string.Empty;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

            string searchText = txtSearch.Text.Trim().ToLower();

            dgCustomers.SelectedItem = null;
            dgPets.SelectedItem = null;
            btnUpdatePet.Visibility = Visibility.Collapsed;
            btnUpdateCustomer.Visibility = Visibility.Collapsed;
            ClearPetFields();
            ClearCustomerFields();

            if (string.IsNullOrEmpty(searchText))
            {
                LoadDataCustomerGrid();
                return;
            }
            
            var allCustomers = customerService.GetAllCustomers();
            var filteredCustomers = allCustomers.Where(c => c.Phone.Equals(searchText)).ToList();
            if (filteredCustomers.Count > 0)
            {
                dgCustomers.ItemsSource = filteredCustomers;
            }
            else
            {
                dgCustomers.ItemsSource = null;
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Customer selectedCustomer = dgCustomers.SelectedItem as Customer;


            btnUpdateCustomer.Visibility = Visibility.Visible;

            LoadPetBySelectCustomer(selectedCustomer);

            LoadDataCustomerEdit(selectedCustomer);


        }

        private void LoadDataCustomerEdit(Customer selectedCustomer)
        {
            selectedCustomer.FullName = txtCustomerName.Text.Trim();
            selectedCustomer.Phone = txtPhone.Text.Trim();
            selectedCustomer.Email = txtEmail.Text.Trim();
            selectedCustomer.Address = txtAddress.Text.Trim();
        }

        private void dgPets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Pet selectedPet = dgPets.SelectedItem as Pet;

            if (selectedPet == null)
            {
                // Khi không có thú cưng nào được chọn, hoặc khi dgPets.ItemsSource bị thay đổi
                // và không có mục nào được chọn lại, bạn cần xóa các trường thông tin pet.
                txtPetId.Text = string.Empty;
                txtPetName.Text = string.Empty;
                cmbSpecies.SelectedItem = null; // Xóa lựa chọn của ComboBox
                txtBreed.Text = string.Empty;
                cmbGender.SelectedItem = null; // Xóa lựa chọn của ComboBox
                return; // Thoát khỏi phương thức để tránh lỗi NullReferenceException
            }
            LoadPetEditForm(selectedPet);
            
        }

        private void LoadPetEditForm(Pet selectedPet)
        {
            txtPetId.Text = selectedPet.PetId.ToString();
            txtPetName.Text = selectedPet.PetName;
            cmbSpecies.SelectedItem = selectedPet.Species;
            txtBreed.Text = selectedPet.Breed;
            cmbGender.SelectedItem = selectedPet.Gender == "Male" ? "Đực" : "Cái";
            switch (selectedPet.Species)
            {
                case "Dog":
                    cmbSpecies.SelectedItem = "Chó";
                    break;
                case "Cat":
                    cmbSpecies.SelectedItem = "Mèo";
                    break;
                case "Rabbit":
                    cmbSpecies.SelectedItem = "Thỏ";
                    break;

                case "Hamster":
                    cmbSpecies.SelectedItem = "Hamster";
                    break;
            }
        }

        private void txtSearch_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdatePet_Click(object sender, RoutedEventArgs e)
        {
            Pet pet = dgPets.SelectedItem as Pet;
            Customer customer = dgCustomers.SelectedItem as Customer;
            if (pet == null)
            {
                MessageBox.Show("Vui lòng chọn thú cưng trước khi cập nhật.");
                return;
            }
            pet.PetName = txtPetName.Text.Trim();
            switch (cmbSpecies.SelectedItem.ToString())
            {
                case "Chó":
                    pet.Species = "Dog";
                    break;
                case "Mèo":
                    pet.Species = "Cat";
                    break;
                case "Thỏ":
                    pet.Species = "Rabbit";
                    break;
                case "Hamster":
                    pet.Species = "Hamster";
                    break;
                default:
                    MessageBox.Show("Vui lòng chọn loài thú hợp lệ.");
                    return;
            }
            pet.Breed = txtBreed.Text.Trim();
            pet.Gender = cmbGender.SelectedItem.ToString() == "Đực" ? "Male" : "Female";
            petService.UpdatePet(pet);
            ClearPetFields();
            LoadPetBySelectCustomer(customer);
            dgPets.SelectedItem = null;
        }

        private void ClearCustomerFields()
        {
            txtCustomerId.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAddress.Text = string.Empty;
        }

        private void ClearPetFields()
        {
            txtPetId.Text = string.Empty;
            txtPetName.Text = string.Empty;
            cmbSpecies.SelectedItem = null;
            txtBreed.Text = string.Empty;
            cmbGender.SelectedItem = null;
            dgPets.SelectedItem = null; // Đảm bảo không có thú cưng nào được chọn trong DataGrid
        }

        private void btnUpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            Customer selectedCustomer = dgCustomers.SelectedItem as Customer;

            if (selectedCustomer == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng trước khi cập nhật thú cưng.");
                return;
            }

            Customer updatedCustomer = new Customer
            {
                CustomerId = selectedCustomer.CustomerId,
                FullName = txtCustomerName.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Address = txtAddress.Text.Trim()
            };
            customerService.UpdateCustomer(updatedCustomer);
            ClearPetFields();
            ClearCustomerFields();
            dgCustomers.SelectedItem = null;
            LoadDataCustomerGrid();
        }

        private void btnEditPet_Click(object sender, RoutedEventArgs e)
        {
            Pet selectedPet = dgPets.SelectedItem as Pet;
            LoadPetEditForm(selectedPet);
            btnUpdatePet.Visibility = Visibility.Visible;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            
        }
    }
}
