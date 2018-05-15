using BuildSchool.PasswordValidationTool.Abstracts;
using BuildSchool.PasswordValidationTool.Implementation.HashingProviders;
using BuildSchool.PasswordValidationTool.Implementation.PasswordRules;
using BuildSchool.PasswordValidationTool.Implementation.SaltStrategies;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSchool.PasswordValidationTool.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            // register dependency.
            var container = new Container();
            container.Register<IHashingProvider, SHA512HashingProvider>();
            container.Register<IPasswordRule, MediumComplexityPasswordRule>();
            container.Register<ISaltStrategy, DefaultSaltStrategy>();
            container.Register<IPasswordValidationService, PasswordValidationService>();

            // resolve object and get instance.
            var service = 
                container.GetInstance<IPasswordValidationService>();

            //var pwd = service.GeneratePassword();
            var userInputPwd = "x2n84tNe";
            var storedPwd = "x3n84tNe";
            var salt = "abcde";

            // emulate stored password.
            byte[] storedPwdData = 
                Encoding.UTF8.GetBytes(storedPwd);
            byte[] saltData = 
                Encoding.UTF8.GetBytes(salt);
            byte[] storedPwdHashed = 
                service.HashPassword(storedPwdData, saltData);

            // validate user input.
            byte[] userPwdData = 
                Encoding.UTF8.GetBytes(userInputPwd);

            if (service.ValidatePassword(
                storedPwdHashed, userPwdData, saltData))
            {
                Console.WriteLine("Correct.");
            }
            else
            {
                Console.WriteLine("Incorrect pwd.");
            }


            Console.ReadLine();
        }
    }
}
