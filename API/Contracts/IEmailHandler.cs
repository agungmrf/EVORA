namespace API.Contracts;

public interface IEmailHandler
{
    void Send(string subject, string body, string toEmail); // Untuk mengirim email.    
    void SendForgotPasswordEmail(string fullName, int otp, string email);
}