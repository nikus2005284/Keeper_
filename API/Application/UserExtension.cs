using ClassLibrary;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using API.Models;

namespace API.Application
{
    public static class UserExtension
    {
        /*public static bool IsPasswordConfirmation(this User user)
        {
            return (user.password == user.passwordConfirmation) ? true : false;
        }*/


        public static string HashPassword(this User user, string password)
        {
            // Реализация хеширования пароля с использованием MD5
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(password);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Конвертируем байты обратно в строку
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
