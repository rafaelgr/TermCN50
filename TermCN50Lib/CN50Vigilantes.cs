using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlServerCe;
using Newtonsoft.Json;

namespace TermCN50Lib
{
    public class CN50Vigilantes
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
            TVigilante vig = null;
            if (value != "")
            {
                vig = JsonConvert.DeserializeObject<TVigilante>(value);
            }
            // llamada a las funciones correspondientes según el comando pasado
            switch (cmd)
            {
                case "SetVigilante":
                    return await SetVigilante(db, pass, vig);
                    break;
                case "DeleteVigilantes":
                    return await DeleteVigilantes(db, pass);
                    break;
                default:
                    return String.Format("ERROR: Comando desconocido [{0}]", cmd);
                    break;
            }

        }

        public async Task<string> SetVigilante(string db, string pass, TVigilante vig)
        {
            if (vig == null)
            {
                return "ERROR: No se ha paso un objeto";
            }
            else
            {
                try
                {
                    SqlCeConnection conn = CntFCN50.TOpen(db, pass);
                    CntFCN50.SetVigilante(vig, conn);
                    CntFCN50.TClose(conn);
                    return "OK";
                }
                catch (Exception ex)
                {
                    return String.Format("ERROR: {0}", ex.Message);
                }
            }
        }

        public async Task<string> DeleteVigilantes(string db, string pass)
        {
            try
            {
                SqlCeConnection conn = CntFCN50.TOpen(db, pass);
                CntFCN50.DeleteVigilantes(conn);
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
