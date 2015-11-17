using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;

namespace TermCN50Lib
{
    public class TGrupo
    {
        private int _grupoId;
        private string _nombre;

        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public int grupoId
        {
            get { return _grupoId; }
            set { _grupoId = value; }
        }
    }
    public static partial class CntFCN50
    {
        public static TGrupo GetGrupoFromDr(SqlCeDataReader dr)
        {
            TGrupo grp = new TGrupo();
            grp.grupoId = dr.GetInt32(0);
            grp.nombre = dr.GetString(1);
            return grp;
        }

        public static TGrupo GetTGrupo(int id, SqlCeConnection conn)
        {
            TGrupo grp = null;
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = String.Format("SELECT * FROM grupos WHERE grupoId = {0}", id);
                using (SqlCeDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        grp = GetGrupoFromDr(dr);

                    }
                    if (!dr.IsClosed)
                        dr.Close();
                }
            }
            return grp;
        }

        public static void SetGrupo(TGrupo grp, SqlCeConnection conn)
        {
            if (grp == null) return;
            // comprobamos si existe el registro
            TGrupo grupo = GetTGrupo(grp.grupoId, conn);
            string sql = "";
            if (grupo != null)
            {
                sql = @"UPDATE grupos SET nombre = '{1}'
                        WHERE grupoId = {0}";
            }
            else
            {
                sql = @"INSERT INTO grupos (grupoId, nombre)
                        VALUES({0},'{1}')";
            }
            sql = String.Format(sql, grp.grupoId, grp.nombre);
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                int nrec = cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteGrupos(SqlCeConnection conn)
        {
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "DELETE FROM grupos";
                int nrec = cmd.ExecuteNonQuery();
            }
        }
    }
}
