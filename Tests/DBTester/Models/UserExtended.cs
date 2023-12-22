namespace DataProducer
{
    public class UserExtended
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }


        public UserExtended(string name, string surname, string email, string login, string password, string phone)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Login = login;
            Password = password;
            Phone = phone;
        }
    }
}
