using System;

namespace LegacyApp
{
    public class UserService
    {
        private IClientRepository _clientRepository;
        private IUserCreditService _userCreditService;
        
        public UserService(IClientRepository clientRepository, IUserCreditService userCreditService)
        {
            this._clientRepository = clientRepository;
            this._userCreditService = userCreditService;
        }

        public UserService():this(new ClientRepository(), new UserCreditService())
        {
            
        }

        private bool ValidUserData(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                return false;
            }
            if (!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }

            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (age < 21)
            {
                return false;
            }

            return true;
        }

        private bool CalculateUserLimit(int clientId, DateTime dateOfBirth, string email, string firstName, string lastName)
        {
            var client = _clientRepository.GetById(clientId);

            var user = new User(client, dateOfBirth, email, firstName, lastName);

            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else if (client.Type == "ImportantClient")
            {
                
                int creditLimit = _userCreditService.GetCreditLimit(user.LastName);
                creditLimit = creditLimit * 2;
                user.CreditLimit = creditLimit;
                
            }
            else
            {
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName);
                    user.CreditLimit = creditLimit;
                }
            }
            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }
            UserDataAccess.AddUser(user);

            return true;
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (!ValidUserData(firstName, lastName, email, dateOfBirth))
            {
                return false;
            }

            if (!CalculateUserLimit(clientId, dateOfBirth, email, firstName, lastName))
            {
                return false;
            }
            return true;
        }
    }
}
