using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;

namespace TermCN50Lib
{
    public class TEdificio
    {
        private int _edificioId;
        private string _nombre;

        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public int edificioId
        {
            get { return _edificioId; }
            set { _edificioId = value; }
        }
    }
    public static partial class CntFCN50
    {
        public static TEdificio GetEdificioFromDr(SqlCeDataReader dr)
        {
            TEdificio edif = new TEdificio();
            edif.edificioId = dr.GetInt32(0);
            edif.nombre = dr.GetString(1);
            return edif;
        }

        public static TEdificio GetTEdificio(int id, SqlCeConnection conn)
        {
            TEdificio edif = null;
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = String.Format("SELECT * FROM edificios WHERE edificioId = {0}", id);
                using (SqlCeDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        edif = GetEdificioFromDr(dr);

                    }
                    if (!dr.IsClosed)
                        dr.Close();
                }
            }
            return edif;
        }

        public static void SetEdificio(TEdificio edif, SqlCeConnection conn)
        {
            if (edif == null) return;
            // comprobamos si existe el registro
            TEdificio edificio = GetTEdificio(edif.edificioId, conn);
            string sql = "";
            if (edificio != null)
            {
                sql = @"UPDATE edificios SET nombre = '{1}'
                        WHERE edificioId = {0}";
            }
            else
            {
                sql = @"INSERT INTO edificios (edificioId, nombre)
                        VALUES({0},'{1}')";
            }
            sql = String.Format(sql, edif.edificioId, edif.nombre);
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                int nrec = cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteEdificios(SqlCeConnection conn)
        {
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "DELETE FROM edificios";
                int nrec = cmd.ExecuteNonQuery();
            }
        }
    }
}
