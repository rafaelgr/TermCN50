using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlServerCe;
using Newtonsoft.Json;

namespace TermCN50Lib
{
    public class TCN50
    {
        public async Task<object> Invoke(dynamic input)
        {
            //string db = (string)input.db;
            //string pass = (string)input.password;
            //TAdministrador adm = JsonConvert.DeserializeObject<TAdministrador>((string)input.value);
            Console.WriteLine("INPUT: " + input);
            Input inp = JsonConvert.DeserializeObject<Input>(input);

            return await Llamada2(inp.db, inp.password, inp.value); ;
        }
        public async Task<string> LLamadoAsync()
        {
            return "Me has llamado";
        }
        public async Task<string> Llamada2(string db, string pass, string value)
        {
            Console.WriteLine("Db: " + db);
            Console.WriteLine("Pass: " + pass);
            Console.WriteLine("Adm: " + value);
            TAdministrador adm = JsonConvert.DeserializeObject<TAdministrador>(value);
            //TAdministrador adm = new TAdministrador()
            //{
            //    administradorId = 1,
            //    nombre = "Pepe",
            //    login = "pep",
            //    password = "pep",
            //    email = "pep@gmail.com",
            //    nivel = 0
            //};
            //string db = "C:\\Proyectos\\FalckCN50Sol\\FalckCN50Console\\DB\\terminal.sdf";
            //string pass = "";
            //string res = "OK";
            //if (adm == null)
            //{
            //    return "ERROR: No se ha paso un objeto";
            //}
            //else
            //{
            //    //try
            //    //{
            //        //SqlCeConnection conn = CntFCN50.TOpen(db, pass);
            //        //CntFCN50.SetAdministrador(adm, conn);
            //        //CntFCN50.TClose(conn);
            //        //return "OK";
            //    //}
            //    //catch (Exception ex)
            //    //{
            //    //    return String.Format("ERROR: {0}", ex.Message);
            //    //}
            //}
            return "DB: " + db + " PASS: " + pass + " LOGIN:" + adm.login;
        }
    }
}
