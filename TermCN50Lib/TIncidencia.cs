using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;

namespace TermCN50Lib
{
    public class TIncidencia
    {
        private int _incidenciaId;
        private string _nombre;

        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public int incidenciaId
        {
            get { return _incidenciaId; }
            set { _incidenciaId = value; }
        }
    }
    public static partial class CntFCN50
    {
        public static TIncidencia GetIncidenciaFromDr(SqlCeDataReader dr)
        {
            TIncidencia inci = new TIncidencia();
            inci.incidenciaId = dr.GetInt32(0);
            inci.nombre = dr.GetString(1);
            return inci;
        }

        public static TIncidencia GetTIncidencia(int id, SqlCeConnection conn)
        {
            TIncidencia inci = null;
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = String.Format("SELECT * FROM incidencias WHERE incidenciaId = {0}", id);
                using (SqlCeDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        inci = GetIncidenciaFromDr(dr);

                    }
                    if (!dr.IsClosed)
                        dr.Close();
                }
            }
            return inci;
        }

        public static void SetIncidencia(TIncidencia inci, SqlCeConnection conn)
        {
            if (inci == null) return;
            // comprobamos si existe el registro
            TIncidencia incidencia = GetTIncidencia(inci.incidenciaId, conn);
            string sql = "";
            if (incidencia != null)
            {
                sql = @"UPDATE incidencias SET nombre = '{1}'
                        WHERE incidenciaId = {0}";
            }
            else
            {
                sql = @"INSERT INTO incidencias (incidenciaId, nombre)
                        VALUES({0},'{1}')";
            }
            sql = String.Format(sql, inci.incidenciaId, inci.nombre);
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                int nrec = cmd.ExecuteNonQuery();
            }
        }
        
        public static void DeleteIncidencias(SqlCeConnection conn)
        {
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "DELETE FROM incidencias";
                int nrec = cmd.ExecuteNonQuery();
            }
        }
    }
}
