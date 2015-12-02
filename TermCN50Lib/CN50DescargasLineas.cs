using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlServerCe;
using Newtonsoft.Json;

namespace TermCN50Lib
{
    public class CN50DescargasLineas
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
            TTerminal t = null;
            if (value != "")
            {
                t = JsonConvert.DeserializeObject<TTerminal>(value);
            }
            // llamada a las funciones correspondientes según el comando pasado
            switch (cmd)
            {
                case "GetDescargasLineas":
                    return await GetDescargasLineas(db, pass);
                    break;
                case "DeleteDescargaLinea":
                    return await DeleteDescargaLinea(db, pass);
                    break;
                default:
                    return String.Format("ERROR: Comando desconocido [{0}]", cmd);
                    break;
            }

        }

        public async Task<string> SetTerminal(string db, string pass, TTerminal t)
        {
            if (t == null)
            {
                return "ERROR: No se ha paso un objeto";
            }
            else
            {
                try
                {
                    SqlCeConnection conn = CntFCN50.TOpen(db, pass);
                    CntFCN50.SetTerminal(t, conn);
                    CntFCN50.TClose(conn);
                    return "OK";
                }
                catch (Exception ex)
                {
                    return String.Format("ERROR: {0}", ex.Message);
                }
            }
        }

        public async Task<string> DeleteDescargaLinea(string db, string pass)
        {
            try
            {
                SqlCeConnection conn = CntFCN50.TOpen(db, pass);
                CntFCN50.DeleteDescargaLineas(conn);
                CntFCN50.TClose(conn);
                return "OK";
            }
            catch (Exception ex)
            {
                return String.Format("ERROR: {0}", ex.Message);
            }
        }

        public async Task<string> GetDescargasLineas(string db, string pass)
        {
            try
            {
                SqlCeConnection conn = CntFCN50.TOpen(db, pass);
                IList<TDescargaLinea> ldl = CntFCN50.GetTDescargasLineas(conn);
                CntFCN50.TClose(conn);
                // hay que serializar
                return JsonConvert.SerializeObject(ldl);
                //return String.Format("Num: {0}", ldl.Count);
            }
            catch (Exception ex)
            {
                return String.Format("ERROR: {0}", ex.Message);
            }
        }
    }
}
