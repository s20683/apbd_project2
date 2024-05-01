namespace LegacyApp
{
    public class Client
    {
        public Client(int clientId, string name, string address, string email, string type )
        {
            this.ClientId = clientId;
            this.Name = name;
            this.Address = address;
            this.Email = email;
            this.Type = type;
        }
        public string Name { get; private set; }
        public int ClientId { get; private set; }
        public string Email { get; private set; }
        public string Address { get; private set; }
        public string Type { get; private set; }
    }
}