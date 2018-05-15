using BuildSchool.PasswordValidationTool.Abstracts;

namespace BuildSchool.PasswordValidationTool.Client
{
    public class PasswordValidationService : IPasswordValidationService
    {
        private IHashingProvider _hashProvider;
        private ISaltStrategy _saltStrategy;
        private IPasswordRule _passwordRule;

        public PasswordValidationService(
            IHashingProvider hashingProvider,
            ISaltStrategy saltStrategy,
            IPasswordRule passwordRule)
        {
            _hashProvider = hashingProvider;
            _saltStrategy = saltStrategy;
            _passwordRule = passwordRule;
        }

        public bool ValidatePassword(byte[] pwd, byte[] pwdCheck, byte[] salt)
        {
            var formattedPwdCheck = _saltStrategy.Format(pwdCheck, salt);
            var hashedPwd = _hashProvider.ComputeHash(formattedPwdCheck);

            if (pwd.Length != hashedPwd.Length)
                return false;

            for (var i=0; i<pwd.Length; i++)
            {
                if (pwd[i] != hashedPwd[i])
                    return false;
            }

            return true;
        }

        public string GeneratePassword()
        {
            return _passwordRule.Generate();
        }

        public byte[] HashPassword(byte[] pwd, byte[] salt)
        {
            var formattedPwd = _saltStrategy.Format(pwd, salt);
            return _hashProvider.ComputeHash(formattedPwd);
        }
    }
}
