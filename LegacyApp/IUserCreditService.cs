namespace LegacyApp;

public interface IUserCreditService
{
    int GetCreditLimit(string lastName);
}