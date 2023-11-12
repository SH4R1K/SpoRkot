using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpoRkotLibrary.Data
{
    /// <summary>
    /// Класс для создания строки подключения
    /// </summary>
    public static class ConnectionManager
    {
        /// <summary>
        /// Строка подключения
        /// </summary>
        public static string ConnectionString { get => $"Data Source={Server};Initial Catalog={DataBase};User ID={Login};Password={Password};Trust Server Certificate=True";}

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public static string Login { get; set; }

        /// <summary>
        /// Пароля
        /// </summary>
        public static string Password { get; set; }

        /// <summary>
        /// Имя сервера
        /// </summary>
        public static string Server { get; set; }

        /// <summary>
        /// Имя базы данных
        /// </summary>
        public static string DataBase { get; set; }

    }
}
