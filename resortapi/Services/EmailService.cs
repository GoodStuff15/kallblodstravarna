using MailKit.Net.Smtp;
using MimeKit;
using resortapi.Services;
using resortdtos;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration config, ILogger<EmailService> logger)
    {
        _config = config;
        _logger = logger;
    }

    public async Task SendBookingConfirmationEmailAsync(BookingDetailsDto booking, byte[] pdfBytes)
    {
        if (booking == null) throw new ArgumentNullException(nameof(booking));
        if (pdfBytes == null) throw new ArgumentNullException(nameof(pdfBytes));

        // Validate configuration
        var fromEmail = _config["Email:From"];
        var smtpHost = _config["Email:SmtpHost"];
        var smtpPort = _config["Email:SmtpPort"];
        var username = _config["Email:Username"];
        var password = _config["Email:Password"];
        // Logga
        _logger.LogInformation($"Email:From: {fromEmail}");
        _logger.LogInformation($"Email:SmtpHost: {smtpHost}");
        _logger.LogInformation($"Email:SmtpPort: {smtpPort}");
        _logger.LogInformation($"Email:Username: {username}");
        _logger.LogInformation($"Email:Password: {(string.IsNullOrEmpty(password) ? "null" : "set")}");

        if (string.IsNullOrWhiteSpace(fromEmail) || string.IsNullOrWhiteSpace(smtpHost) ||
            string.IsNullOrWhiteSpace(smtpPort) || string.IsNullOrWhiteSpace(username) ||
            string.IsNullOrWhiteSpace(password))
        {
            _logger.LogError("Missing or invalid email configuration settings.");
            throw new InvalidOperationException("Email configuration is incomplete.");
        }

        if (booking.Customer == null)
        {
            _logger.LogWarning("Ingen kund tillgänglig. Skickar inte e-post.");
            return;
        }
        if (string.IsNullOrWhiteSpace(booking.Customer.Email))
        {
            _logger.LogWarning("Ingen e-postadress tillgänglig för kunden. Skickar inte e-post.");
            return;
        }

        try
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(fromEmail));
            message.To.Add(MailboxAddress.Parse(booking.Customer.Email));
            message.Subject = "Bokningsbekräftelse – Kallblodstravarna Resort";

            var body = new BodyBuilder
            {
                TextBody = $"Hej {booking.Customer.FirstName ?? "Kund"},\n\nTack för din bokning! Se bifogad PDF."
            };

            body.Attachments.Add("Bokningsbekräftelse.pdf", pdfBytes, new ContentType("application", "pdf"));
            message.Body = body.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(smtpHost, int.Parse(smtpPort), MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(username, password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);

            _logger.LogInformation($"Bokningsbekräftelse skickad till {booking.Customer.Email}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fel vid e-postutskick.");
            throw; // Re-throw to propagate the error for debugging
        }
    }
}
