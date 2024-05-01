using System;

namespace LegacyApp
{
    public class User
    {
        public User(Client client, DateTime dateOfBirth, string email, string firstName, string lastName)
        {
            Client = client;
            DateOfBirth = dateOfBirth;
            EmailAddress = email;
            FirstName = firstName;
            LastName = lastName;
        }
        public object Client { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string EmailAddress { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public bool HasCreditLimit { get; internal set; }
        public int CreditLimit { get; internal set; }
    }
}