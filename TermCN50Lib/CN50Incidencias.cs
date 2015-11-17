using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlServerCe;
using Newtonsoft.Json;

namespace TermCN50Lib
{
    public class CN50Incidencias
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
            TIncidencia inci = null;
            if (value != "")
            {
                inci = JsonConvert.DeserializeObject<TIncidencia>(value);
            }
            // llamada a las funciones correspondientes según el comando pasado
            switch (cmd)
            {
                case "SetIncidencia":
                    return await SetIncidencia(db, pass, inci);
                    break;
                case "DeleteIncidencias":
                    return await DeleteIncidencias(db, pass);
                    break;
                default:
                    return String.Format("ERROR: Comando desconocido [{0}]", cmd);
                    break;
            }

        }

        public async Task<string> SetIncidencia(string db, string pass, TIncidencia inci)
        {
            if (inci == null)
            {
                return "ERROR: No se ha paso un objeto";
            }
            else
            {
                try
                {
                    SqlCeConnection conn = CntFCN50.TOpen(db, pass);
                    CntFCN50.SetIncidencia(inci, conn);
                    CntFCN50.TClose(conn);
                    return "OK";
                }
                catch (Exception ex)
                {
                    return String.Format("ERROR: {0}", ex.Message);
                }
            }
        }

        public async Task<string> DeleteIncidencias(string db, string pass)
        {
            try
            {
                SqlCeConnection conn = CntFCN50.TOpen(db, pass);
                CntFCN50.DeleteIncidencias(conn);
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
