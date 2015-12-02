using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlServerCe;
using Newtonsoft.Json;

namespace TermCN50Lib
{
    public class CN50General
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
            // llamada a las funciones correspondientes según el comando pasado
            switch (cmd)
            {
                case "DeleteTables":
                    return await DeleteTables(db, pass);
                    break;
                default:
                    return String.Format("ERROR: Comando desconocido [{0}]", cmd);
                    break;
            }

        }


        public async Task<string> DeleteTables(string db, string pass)
        {
            try
            {
                SqlCeConnection conn = CntFCN50.TOpen(db, pass);
                string sql = @"delete from administradores;
                                    delete from descargas;
                                    delete from descargas_lineas;
                                    delete from edificios;
                                    delete from grupos;
                                    delete from incidencias;
                                    delete from puntos;
                                    delete from rondas;
                                    delete from rondaspuntos;
                                    delete from terminales;
                                    delete from vigilantes;";
                using (SqlCeCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    int nrec = cmd.ExecuteNonQuery();
                }
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
