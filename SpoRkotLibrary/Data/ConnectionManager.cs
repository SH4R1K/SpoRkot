using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpoRkotLibrary.Data
{
    public static class ConnectionManager
    {
        public static string ConnectionString { get => $"Data Source={Server};Initial Catalog={DataBase};User ID={Login};Password={Password};Trust Server Certificate=True";}

        public static string Login { get; set; }
        public static string Password { get; set; }
        public static string Server { get; set; }
        public static string DataBase { get; set; }

    }
}
