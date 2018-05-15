namespace BuildSchool.PasswordValidationTool.Client
{
    public interface IPasswordValidationService
    {
        byte[] HashPassword(byte[] pwd, byte[] salt);
        bool ValidatePassword(byte[] pwd, byte[] pwdToCheck, byte[] salt);
        string GeneratePassword();
    }
}
