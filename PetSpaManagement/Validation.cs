using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PetSpaManagement
{
    public static class Validation
    {
        public static bool IsValidVietnamesePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            // Remove spaces and special characters
            string cleanedPhone = phoneNumber.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");

            // Vietnamese phone number patterns
            // Mobile: 03x, 05x, 07x, 08x, 09x (10 digits)
            // Landline: 02x (10-11 digits)
            var mobilePattern = @"^(03|05|07|08|09)[0-9]{8}$";
            var landlinePattern = @"^(02)[0-9]{8,9}$";

            return System.Text.RegularExpressions.Regex.IsMatch(cleanedPhone, mobilePattern) ||
                   System.Text.RegularExpressions.Regex.IsMatch(cleanedPhone, landlinePattern);
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
