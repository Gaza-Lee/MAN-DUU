using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MANDUU.RegexValidation
{
    public static class InputValidation
    {
            public static bool IsValidEmail(string email)
            {
                if (string.IsNullOrWhiteSpace(email)) return false;

                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase);
            }

            public static bool IsValidPhoneNumber(string phone)
            {
                if (string.IsNullOrWhiteSpace(phone)) return false;

                // Ghana-specific (0xxxxxxxxx) or international format
                return Regex.IsMatch(phone, @"^0\d{9}$") || Regex.IsMatch(phone, @"^\+?[1-9]\d{9,14}$");
            }

            public static bool IsValidPassword(string password)
            {
                if (string.IsNullOrWhiteSpace(password)) return false;

                // At least 8 chars, 1 upper, 1 digit, 1 special char
                return Regex.IsMatch(password,
                    @"^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$");
            }

            public static bool IsValidName(string name)
            {
                return !string.IsNullOrWhiteSpace(name) && Regex.IsMatch(name, @"^[A-Za-z\s'-]{2,}$");
            }
        }
    }
