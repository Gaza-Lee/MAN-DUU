﻿
namespace MANDUU.Models
{
    public class EmailSettings
    {
        public string SmtpHost { get; set; } = "smtp.gmail.com";
        public int SmtpPort { get; set; } = 587;
        public string SmtpUsername { get; set; } = "";
        public string SmtpPassword { get; set; } = "";
        public string FromEmail { get; set; } = "noreply@manduu.com";
        public string FromName { get; set; } = "MANDUU Team";
    }
}