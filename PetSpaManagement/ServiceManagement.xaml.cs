using PetSpaManagement.BLL.Services;
using PetSpaManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for ServiceManagement.xaml
    /// </summary>
    public partial class ServiceManagement : Window
    {
        private ServiceController _service = new();
        public ServiceManagement()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
            
        }

        private void FillDataGrid()
        {
            dgServices.ItemsSource = null;
            dgServices.ItemsSource = _service.GetAllService();
            
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Service service = new Service
            {
                ServiceName = txtServiceName.Text.Trim(),
                Price = decimal.Parse(txtPrice.Text.Trim()),
                Description = txtDescription.Text.Trim()
            };

            if(_service.ExistedService(service.ServiceName))
            {
                MessageBox.Show("Service already exists. Please enter a different service name.");
                return;
            }

            _service.AddService(service);
            ClearFields();
            FillDataGrid();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Service service = dgServices.SelectedItem as Service;
            if (service != null)
            {
                service.ServiceName = txtServiceName.Text;
                service.Price = decimal.Parse(txtPrice.Text);
                service.Description = txtDescription.Text;
                if (_service.ExistedService(service.ServiceName))
                {
                    MessageBox.Show("Service already exists. Please enter a different service name.");
                    return;
                }
                _service.UpdateService(service);
                ClearFields();
                FillDataGrid();
            }
            else
            {
                MessageBox.Show("Please select a service to update.");
            }
        }

        private void ClearFields()
        {
            dgServices.SelectedItem = null;
            txtServiceId.Clear();
            txtServiceName.Clear();
            txtPrice.Clear();
            txtDescription.Clear();
            btnUpdate.Visibility = Visibility.Collapsed;
            btnAdd.Visibility = Visibility.Visible;
            btnDelete.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            btnUpdate.Visibility = Visibility.Visible;
            btnAdd.Visibility = Visibility.Collapsed;
            btnDelete.Visibility = Visibility.Collapsed;

            Service service = dgServices.SelectedItem as Service;

            if (service != null)
            {
                txtServiceId.Text = service.ServiceId.ToString();
                txtServiceName.Text = service.ServiceName;
                txtPrice.Text = service.Price.ToString();
                txtDescription.Text = service.Description;
            }
            else
            {
                MessageBox.Show("Please select a service to update.");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Service service = dgServices.SelectedItem as Service;

            if(service != null)
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete the service '{service.ServiceName}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(result == MessageBoxResult.Yes)
                {
                    _service.DeleteService(service);
                    ClearFields();
                    FillDataGrid();
                }
            }
        }

        private void btnChooseToDelete_Click(object sender, RoutedEventArgs e)
        {
            btnUpdate.Visibility = Visibility.Collapsed;
            btnAdd.Visibility = Visibility.Collapsed;
            btnDelete.Visibility = Visibility.Visible;

            Service service = dgServices.SelectedItem as Service;

            if (service != null)
            {
                txtServiceId.Text = service.ServiceId.ToString();
                txtServiceName.Text = service.ServiceName;
                txtPrice.Text = service.Price.ToString();
                txtDescription.Text = service.Description;
            }
            else
            {
                MessageBox.Show("Please select a service to delete.");
            }
        }
    }
}
