namespace API.DTOs
{
    //data for user that will be sent to the client
    public class UserDto
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public string Image { get; set; }
    }
}