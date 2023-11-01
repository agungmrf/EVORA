using System.Net.Mail;
using API.Contracts;

namespace API.Utilities.Handlers;

public class EmailHandler : IEmailHandler
{
    private readonly string _fromEmailAddress;
    private readonly int _port;
    private readonly string _server;

    public EmailHandler(string server, int port,
        string fromEmailAddress) // Parameter yang dibutuhkan untuk mengirim email
    {
        _server = server; // Server email
        _port = port; // Port email
        _fromEmailAddress = fromEmailAddress; // Email pengirim
    }

    // Method untuk mengirim email
    public void Send(string subject, string body, string toEmail)
    {
        var message = new MailMessage() // Membuat objek MailMessage
        {
            From = new MailAddress(_fromEmailAddress), // Email pengirim
            Subject = subject, // Subject email
            Body = body, // Body email
            IsBodyHtml = true // Body email berupa HTML
        };

        message.To.Add(new MailAddress(toEmail)); // Untuk mngirim email ke email tujuan

        using var smtpClient = new SmtpClient(_server, _port); // Membuat objek SmtpClient
        smtpClient.Send(message); // Mengirim email
    }
    
    public void SendForgotPasswordEmail(string fullName, int otp, string toEmail)
    {
        string subject = "Lupa Password - Verifikasi Kode OTP";
        string emailBody = $@"
                <div style='text-align: center;'>
                    Hi, <strong>{fullName}</strong>!<br/><br/>
                    Untuk melanjutkan, harap masukkan One-Time Password (OTP) Anda sebelum berakhir dalam 3 menit<br/><br/>
                    <strong style='background-color: #FFFF00;'>{otp}</strong><br/><br/>
                    Jangan membagikan OTP dengan siapapun, termasuk pihak manapun dari Evora.<br/><br/>
                    Harap dicatat bahwa OTP hanya berlaku sekali pakai.<br/><br/>
                    Jika Anda merasa tidak melakukan transaksi ini, silakan hubungi<br/><br/>
                    <strong>admin@evora.com</strong><br/><br/>
                    <br/><br/>Terima Kasih
                </div>
            ";

        Send(subject, emailBody, toEmail);
    }
}