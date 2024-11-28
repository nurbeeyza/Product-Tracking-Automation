using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_Tracking_Automation
{
    public static class DBConnections//bağlantıları burdan yönet
    {
        private static string dbConnectionString = "Data Source=DESKTOP-JOC073U\\BEYZANUR;Initial Catalog=Urun_stok_Otomasyon;User ID=sa;Password=20012600;";
        //Encapsulation kullanım mantığı nedir, amaç nedir?
        //Inheritance
        //Polimorphism
        //static class-fonksiyon

        public static string DbConnectionString { get => dbConnectionString; set => dbConnectionString = value; }
    }
}
