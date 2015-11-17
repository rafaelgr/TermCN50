using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TermCN50Lib;

namespace TermCN50Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Antes de llamar");
            string db = "C:\\data\\terminal2.sdf";
            string adm = @"{'administradorId':'2', 'nombre':'Juan','login':'login','password':'pass','email':'cojo@gh.com','nivel':'0'}";
            string pass = "";
            string cmd = "SetAdministrador";
            Input input = new Input()
            {
                command = cmd,
                db = db,
                password = pass,
                value = adm
            };
            //CN50Administradores cn50Admin = new CN50Administradores();
            //Task<object> o = cn50Admin.Invoke(input);
            TCN50 tcn50 = new TCN50();
            Task<object> o = tcn50.Invoke(input);
            Console.WriteLine("Despues de llamar");
            Console.WriteLine("Resultado: {0}", o.Result);
            Console.ReadLine();
        }
    }
}
