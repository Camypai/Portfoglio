using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Portfoglio.Models
{
    public static class Crypto
    {
        /// <summary>
        /// Проверка и шифрация входящей строки. При отсутствии или пустой строке вернёт null
        /// </summary>
        /// <param name="s">Строка, которую необходимо зашифровать</param>
        /// <returns></returns>
        public static string GetHashString(string s)
        {
            return string.IsNullOrEmpty(s) ? null : _GetHashString(s);
        }

        private static string _GetHashString(string s)
        {
            //переводим строку в байт-массим  
            var bytes = Encoding.Unicode.GetBytes(s);

            //создаем объект для получения средст шифрования  
            var CSP = 
                new MD5CryptoServiceProvider();

            //вычисляем хеш-представление в байтах  
            var byteHash = CSP.ComputeHash(bytes);

            //формируем одну цельную строку из массива  

            return byteHash.Aggregate(string.Empty, (current, b) => current + string.Format("{0:x2}", b));
        }
    }
}