using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlServerCe;
using Newtonsoft.Json;

namespace TermCN50Lib
{
    public class CN50Administradores
    {
        public async Task<object> Invoke(dynamic input)
        {
            // los parámetros vienen parametrizados
            Input i = JsonConvert.DeserializeObject<Input>(input);
            // leer los parámetros de la llamada
            string cmd = i.command;
            string db = i.db;
            string pass = i.password;
            string value = i.value;
            // si viene algo en el valor
            TAdministrador adm = null;
            if (value != "")
            {
                adm = JsonConvert.DeserializeObject<TAdministrador>(value);
            }
            Console.WriteLine("adm " + adm);
            // llamada a las funciones correspondientes según el comando pasado
            switch (cmd)
            {
                case "SetAdministrador":
                    return await SetAdministrador(db, pass, adm);
                    break;
                case "DeleteAdministradores":
                    return await DeleteAdministradores(db, pass);
                    break;
                default:
                    return String.Format("ERROR: Comando desconocido [{0}]", cmd);
                    break;
            }

        }

        public async Task<string> SetAdministrador(string db, string pass, TAdministrador adm)
        {
            string res = "OK";
            if (adm == null)
            {
                return "ERROR: No se ha paso un objeto";
            }
            else
            {
                try
                {
                    SqlCeConnection conn = CntFCN50.TOpen(db, pass);
                    CntFCN50.SetAdministrador(adm, conn);
                    CntFCN50.TClose(conn);
                    return "OK";
                }
                catch (Exception ex)
                {
                    return String.Format("ERROR: {0}", ex.Message);
                }
            }
        }

        public async Task<string> DeleteAdministradores(string db, string pass)
        {
            try
            {
                SqlCeConnection conn = CntFCN50.TOpen(db, pass);
                CntFCN50.DeleteAdministradores(conn);
                CntFCN50.TClose(conn);
                return "OK";
            }
            catch (Exception ex)
            {
                return String.Format("ERROR: {0}", ex.Message);
            }
        }
    }
}
