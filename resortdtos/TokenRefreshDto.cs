namespace resortdtos
{
    public class TokenRefreshDto
    {
        public int UserId { get; set; }
        public required string RefreshToken { get; set; }
    }
}
