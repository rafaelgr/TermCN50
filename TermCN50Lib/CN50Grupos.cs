using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlServerCe;
using Newtonsoft.Json;

namespace TermCN50Lib
{
    public class CN50Grupos
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
            TGrupo grp = null;
            if (value != "")
            {
                grp = JsonConvert.DeserializeObject<TGrupo>(value);
            }
            // llamada a las funciones correspondientes según el comando pasado
            switch (cmd)
            {
                case "SetGrupo":
                    return await SetGrupo(db, pass, grp);
                    break;
                case "DeleteGrupos":
                    return await DeleteGrupos(db, pass);
                    break;
                default:
                    return String.Format("ERROR: Comando desconocido [{0}]", cmd);
                    break;
            }

        }

        public async Task<string> SetGrupo(string db, string pass, TGrupo grp)
        {
            if (grp == null)
            {
                return "ERROR: No se ha paso un objeto";
            }
            else
            {
                try
                {
                    SqlCeConnection conn = CntFCN50.TOpen(db, pass);
                    CntFCN50.SetGrupo(grp, conn);
                    CntFCN50.TClose(conn);
                    return "OK";
                }
                catch (Exception ex)
                {
                    return String.Format("ERROR: {0}", ex.Message);
                }
            }
        }

        public async Task<string> DeleteGrupos(string db, string pass)
        {
            try
            {
                SqlCeConnection conn = CntFCN50.TOpen(db, pass);
                CntFCN50.DeleteGrupos(conn);
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
