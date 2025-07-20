using PetSpaManagement.BLL.Bookings;
using PetSpaManagement.BLL.Customers;
using PetSpaManagement.BLL.Pets;
using PetSpaManagement.BLL.Services;
using PetSpaManagement.BLL.Users;
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
    /// Interaction logic for BookingManagement.xaml
    /// </summary>
    public partial class BookingManagement : Window
    {
        private CustomerController customerService = new CustomerController();
        private PetController petService = new PetController();
        private UserController userService = new UserController();
        private BookingController bookingService = new BookingController();
        private ServiceController serviceController = new ServiceController();
        public BookingManagement()
        {
            InitializeComponent();
            LoadService();
            LoadBookings();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {


        }

        private void LoadService()
        {
            var services = serviceController.GetAllService();
            serviceList.ItemsSource = services;
            serviceList.DisplayMemberPath = "ServiceName";
            serviceList.SelectedValuePath = "ServiceId";
        }

        private void btnLookup_Click(object sender, RoutedEventArgs e)
        {
            string phoneCheck = txtPhoneLookup.Text.Trim();
            if (!Validation.IsValidVietnamesePhoneNumber(phoneCheck))
            {
                MessageBox.Show("Số điện thoại không đúng định dạng. Vui lòng nhập lại");
                return;
            }
            if (string.IsNullOrEmpty(phoneCheck))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại để tra cứu khách hàng.");
                return;
            }
            // Giả sử có phương thức để tra cứu khách hàng theo số điện thoại
            Customer customer = customerService.LookupCustomerByPhone(phoneCheck);

            if (customer != null)
            {
                // Hiển thị thông tin khách hàng
                txtCustomerName.Text = customer.FullName;
                txtPhone.Text = customer.Phone;
                txtEmail.Text = customer.Email;
                txtAddress.Text = customer.Address;

                //clear pets
                LoadPets();
                LoadService();
            }
            else
            {
                btnSaveCustomer.Visibility = Visibility.Visible;
                txtPhone.Text = phoneCheck;
            }
        }

        private void LoadBookings()
        {
            var bookings = bookingService.GetAllBookings().ToList();
            dgBookings.ItemsSource = bookings;
        }
        private void LoadPets()
        {
            Customer customer = customerService.LookupCustomerByPhone(txtPhone.Text);
            var pets = petService.GetAllPets().Where(p => p.CustomerId == customer.CustomerId).ToList();
            cmbPets.ItemsSource = pets;
            cmbPets.DisplayMemberPath = "PetName";
            cmbPets.SelectedValuePath = "PetId";
        }
        //private CheckBox CreatePetCheckBox(Pet pet)
        //{
        //    // 1. Tạo CheckBox chính
        //    CheckBox chkBox = new CheckBox();
        //    chkBox.Name = "chkPet" + pet.PetId; // Gán Name duy nhất (tùy chọn nhưng nên làm)
        //    chkBox.Margin = new Thickness(0, 3, 0, 0);
        //    chkBox.FontSize = 12;
        //    chkBox.Tag = pet.PetId ; // Có thể lưu trữ ID của Pet tại đây để dễ truy xuất

        //    // 2. Tạo Grid cho Content của CheckBox
        //    Grid contentGrid = new Grid();

        //    // 3. Tạo StackPanel bên trong Grid
        //    StackPanel contentStackPanel = new StackPanel();
        //    contentStackPanel.Orientation = Orientation.Horizontal;
        //    Grid.SetColumn(contentStackPanel, 0); // Đặt StackPanel vào cột 0 của Grid (nếu có)

        //    // 4. Tạo TextBlock bên trong StackPanel
        //    TextBlock petNameTextBlock = new TextBlock();
        //    petNameTextBlock.Name = "existedPetName"; // Name này chỉ có giá trị cục bộ trong CheckBox này
        //    petNameTextBlock.Margin = new Thickness(0, 0, 10, 0);
        //    petNameTextBlock.Text = $"{pet.PetName} | {pet.Species} {pet.Breed}"; // Ghép tên và loài
        //    petNameTextBlock.FontWeight = FontWeights.SemiBold; // Sử dụng FontWeights
        //    petNameTextBlock.FontSize = 12;

        //    // 5. Thêm TextBlock vào StackPanel
        //    contentStackPanel.Children.Add(petNameTextBlock);

        //    // 6. Thêm StackPanel vào Grid
        //    contentGrid.Children.Add(contentStackPanel);

        //    // 7. Gán Grid làm Content cho CheckBox
        //    chkBox.Content = contentGrid;

        //    return chkBox;
        //}

        private void cmbExistingPets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void rbPetSelection_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddService_Click(object sender, RoutedEventArgs e)
        {

        }

        


        private void btnCreateBooking_Click(object sender, RoutedEventArgs e)
        {
            Booking booking = new Booking();
            booking.ReceptionistId = 2;
            booking.CareStaffId = (int)cmbCareStaff.SelectedValue;
            booking.BookingDate = DateOnly.FromDateTime(dpBookingDate.SelectedDate.Value);
            booking.BookingTime = TimeOnly.Parse(((ComboBoxItem)cmbBookingTime.SelectedItem).Content.ToString());
            booking.Notes = txtNotes.Text;
            booking.PetId = (int)cmbPets.SelectedValue;
            var services = serviceList.SelectedItems;
            decimal totalPrice = 0;

            foreach (var item in services)
            {
                Service serivce = (Service)item;
                totalPrice += (decimal)serivce.Price;
                BookingDetail detail = new BookingDetail();
                detail.ServiceId = serivce.ServiceId;
                detail.Status = "Doing";
                booking.BookingDetails.Add(detail);
            }
            booking.TotalAmount = totalPrice;
            bookingService.AddBooking(booking);
        }



        private void btnClear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dpFilterDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnViewAll_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSaveCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate dữ liệu đầu vào - kiểm tra các ô trống
                List<string> missingFields = new List<string>();

                if (string.IsNullOrWhiteSpace(txtCustomerName.Text))
                {
                    missingFields.Add("Tên khách hàng");
                }

                if (string.IsNullOrWhiteSpace(txtPhone.Text))
                {
                    missingFields.Add("Số điện thoại");
                }

                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    missingFields.Add("Email");
                }

                if (string.IsNullOrWhiteSpace(txtAddress.Text))
                {
                    missingFields.Add("Địa chỉ");
                }

                // Nếu có trường nào bị thiếu, hiển thị thông báo tổng hợp
                if (missingFields.Any())
                {
                    string message = "Vui lòng nhập đầy đủ thông tin:\n" + string.Join("\n", missingFields.Select(f => "• " + f));
                    MessageBox.Show(message, "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);

                    // Focus vào trường đầu tiên bị thiếu
                    if (missingFields.Contains("Tên khách hàng"))
                        txtCustomerName.Focus();
                    else if (missingFields.Contains("Số điện thoại"))
                        txtPhone.Focus();
                    else if (missingFields.Contains("Email"))
                        txtEmail.Focus();
                    else if (missingFields.Contains("Địa chỉ"))
                        txtAddress.Focus();

                    return;
                }

                // Validate định dạng số điện thoại
                if (!Validation.IsValidVietnamesePhoneNumber(txtPhone.Text.Trim()))
                {
                    MessageBox.Show("Số điện thoại không đúng định dạng. Vui lòng nhập lại.", "Định dạng không hợp lệ", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtPhone.Focus();
                    return;
                }

                // Validate định dạng email
                if (!Validation.IsValidEmail(txtEmail.Text.Trim()))
                {
                    MessageBox.Show("Email không đúng định dạng. Vui lòng nhập lại.", "Định dạng không hợp lệ", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtEmail.Focus();
                    return;
                }

                // Tạo đối tượng Customer mới
                Customer newCustomer = new Customer
                {
                    FullName = txtCustomerName.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Address = txtAddress.Text.Trim()
                };

                // Kiểm tra xem khách hàng đã tồn tại chưa (theo số điện thoại)
                Customer existingCustomerByPhone = customerService.LookupCustomerByPhone(newCustomer.Phone);
                if (existingCustomerByPhone != null)
                {
                    MessageBox.Show("Khách hàng với số điện thoại này đã tồn tại trong hệ thống.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Kiểm tra trùng email
                Customer existingCustomerByEmail = customerService.checkEmail(newCustomer.Email);
                if (existingCustomerByEmail != null)
                {
                    MessageBoxResult result = MessageBox.Show(
                        $"Email '{newCustomer.Email}' đã được sử dụng",
                        "Email đã tồn tại",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);

                        txtEmail.Focus();
                        return;
                    
                }

                // Lưu khách hàng mới
                customerService.AddCustomer(newCustomer);

                MessageBox.Show("Thêm khách hàng mới thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                btnSaveCustomer.Visibility = Visibility.Collapsed;

                // Tự động load lại thông tin khách hàng vừa tạo
                Customer savedCustomer = customerService.LookupCustomerByPhone(newCustomer.Phone);
                if (savedCustomer != null)
                {
                    LoadPets();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void btnSaveNewPet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate thông tin khách hàng trước
                if (string.IsNullOrWhiteSpace(txtPhone.Text))
                {
                    MessageBox.Show("Vui lòng tra cứu hoặc tạo khách hàng trước khi thêm thú cưng.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Lấy thông tin khách hàng hiện tại
                Customer currentCustomer = customerService.LookupCustomerByPhone(txtPhone.Text.Trim());
                if (currentCustomer == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin khách hàng. Vui lòng lưu thông tin khách hàng trước.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Validate dữ liệu thú cưng - kiểm tra các ô trống
                List<string> missingPetFields = new List<string>();

                if (string.IsNullOrWhiteSpace(txtNewPetName.Text))
                {
                    missingPetFields.Add("Tên thú cưng");
                }

                if (cmbNewPetSpecies.SelectedItem == null)
                {
                    missingPetFields.Add("Loài thú cưng");
                }

                if (string.IsNullOrWhiteSpace(txtNewPetBreed.Text))
                {
                    missingPetFields.Add("Giống thú cưng");
                }

                if (cmbNewPetGender.SelectedItem == null)
                {
                    missingPetFields.Add("Giới tính thú cưng");
                }

                // Nếu có trường nào bị thiếu, hiển thị thông báo tổng hợp
                if (missingPetFields.Any())
                {
                    string message = "Vui lòng nhập đầy đủ thông tin thú cưng:\n" + string.Join("\n", missingPetFields.Select(f => "• " + f));
                    MessageBox.Show(message, "Thiếu thông tin thú cưng", MessageBoxButton.OK, MessageBoxImage.Warning);

                    // Focus vào trường đầu tiên bị thiếu
                    if (missingPetFields.Contains("Tên thú cưng"))
                        txtNewPetName.Focus();
                    else if (missingPetFields.Contains("Loài thú cưng"))
                        cmbNewPetSpecies.Focus();
                    else if (missingPetFields.Contains("Giống thú cưng"))
                        txtNewPetBreed.Focus();
                    else if (missingPetFields.Contains("Giới tính thú cưng"))
                        cmbNewPetGender.Focus();

                    return;
                }

                // Tạo đối tượng Pet mới
                Pet newPet = new Pet
                {
                    CustomerId = currentCustomer.CustomerId,
                    PetName = txtNewPetName.Text.Trim(),
                    Species = ((ComboBoxItem)cmbNewPetSpecies.SelectedItem).Content.ToString(),
                    Breed = txtNewPetBreed.Text.Trim(),
                    Gender = ((ComboBoxItem)cmbNewPetGender.SelectedItem).Content.ToString()
                };

                // Lưu thú cưng mới
                petService.AddPet(newPet);

                MessageBox.Show("Thêm thú cưng mới thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

                // Clear form thú cưng mới
                ClearNewPetForm();

                // Ẩn form thêm thú cưng mới
                borderNewPetForm.Visibility = Visibility.Collapsed;

                // Refresh danh sách thú cưng
                LoadPets();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearNewPetForm()
        {
            txtNewPetName.Clear();
            cmbNewPetSpecies.SelectedIndex = -1;
            txtNewPetBreed.Clear();
            cmbNewPetGender.SelectedIndex = -1;
            dpNewPetBirthday.SelectedDate = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //var careStaff = userService.GetUserByRole(3);
            //cmbCareStaff.ItemsSource = careStaff;
            //cmbCareStaff.DisplayMemberPath = "FullName";
            //cmbCareStaff.SelectedValuePath = "UserId";
        }

        private void btnAddNewPet_Click(object sender, RoutedEventArgs e)
        {
            borderNewPetForm.Visibility = Visibility.Visible;
        }

        private void btnCancelNewPet_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dpBookingDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadAvailableCareStaff();
        }

        private void LoadAvailableCareStaff()
        {
            // Đảm bảo rằng cả ngày và giờ đều đã được chọn
            if (dpBookingDate.SelectedDate.HasValue && cmbBookingTime.SelectedItem != null)
            {
                // Lấy ngày đã chọn từ DatePicker
                DateOnly selectedDate = DateOnly.FromDateTime(dpBookingDate.SelectedDate.Value);

                // Lấy giờ đã chọn từ ComboBox
                string timeString = ((ComboBoxItem)cmbBookingTime.SelectedItem).Content.ToString();
                TimeOnly selectedTime;

                // Cố gắng phân tích chuỗi giờ thành TimeOnly
                if (TimeOnly.TryParseExact(timeString, "HH:mm", out selectedTime))
                {
                    // Gọi hàm trong UserController để lấy danh sách nhân viên rảnh rỗi
                    List<User> availableCareStaff = userService.GetAvailableCareStaff(selectedDate, selectedTime);

                    // Gán danh sách này cho ItemsSource của ComboBox cmbCareStaff
                    cmbCareStaff.ItemsSource = availableCareStaff;
                    cmbCareStaff.DisplayMemberPath = "FullName"; // Hiển thị tên đầy đủ
                    cmbCareStaff.SelectedValuePath = "UserId";   // Giá trị thực sự là UserId

                    if (!availableCareStaff.Any())
                    {
                        MessageBox.Show("Không có nhân viên chăm sóc nào rảnh vào thời gian này. Vui lòng chọn thời gian khác.");
                    }
                    else
                    {
                        cmbCareStaff.SelectedIndex = 0; // Tùy chọn: chọn nhân viên đầu tiên trong danh sách
                    }
                }
                else
                {
                    MessageBox.Show("Định dạng giờ không hợp lệ.");
                    cmbCareStaff.ItemsSource = null;
                }
            }
            else
            {
                // Nếu ngày hoặc giờ chưa được chọn, xóa danh sách nhân viên
                cmbCareStaff.ItemsSource = null;
            }
        }

        private void cmbBookingTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadAvailableCareStaff();
        }
    }
}
