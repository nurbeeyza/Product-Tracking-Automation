using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_Tracking_Automation
{
    public class Class1
    {
        public static void test(string x, string y)
        {
            //sqlconn
            //sqlcommand => "SELECT Name, Surname FROM x WHERE Name LIKE @asd OR Surname LIKE @dfg "
            //Addwit ("@asd","%" + x + "%" )
            //Addwit ("@dfg","%" + y + "%" )
        }
    }

    public class C2
    {
        public void c()
        {
            Class1.test("li", "aş");
        }
    }

}
