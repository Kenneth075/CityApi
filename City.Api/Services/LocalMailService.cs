﻿namespace City.Api.Services
{
    public class LocalMailService : ILocalMailService
    {
        private readonly string _mailTo = string.Empty;
        private readonly string _mailFrom = string.Empty;

        public LocalMailService(IConfiguration configuration)
        {
            _mailTo = configuration["mailSettings : mailToAddress"];
            _mailFrom = configuration["mailSettings : mailFromAddress"];
        }

        public void Send(string subject, string message)
        {
            //Send mail output to console window.

            Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}" + $"With {nameof(LocalMailService)}.");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"message: {message}");
        }
    }
}
