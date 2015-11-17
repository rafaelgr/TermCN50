using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;

namespace TermCN50Lib
{
    public class TVigilante
    {
        private int _vigilanteId;
        private string _nombre;
        private string _tag;
        private string _tagf;

        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public string tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        public string tagf
        {
            get { return _tagf; }
            set { _tagf = value; }
        }


        public int vigilanteId
        {
            get { return _vigilanteId; }
            set { _vigilanteId = value; }
        }
    }
    public static partial class CntFCN50
    {
        public static TVigilante GetVigilanteFromDr(SqlCeDataReader dr)
        {
            TVigilante vig = new TVigilante();
            vig.vigilanteId = dr.GetInt32(0);
            vig.nombre = dr.GetString(1);
            return vig;
        }

        public static TVigilante GetTVigilante(int id, SqlCeConnection conn)
        {
            TVigilante vig = null;
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = String.Format("SELECT * FROM vigilantes WHERE vigilanteId = {0}", id);
                using (SqlCeDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        vig = GetVigilanteFromDr(dr);

                    }
                    if (!dr.IsClosed)
                        dr.Close();
                }
            }
            return vig;
        }

        public static void SetVigilante(TVigilante vig, SqlCeConnection conn)
        {
            if (vig == null) return;
            // comprobamos si existe el registro
            TVigilante vigilante = GetTVigilante(vig.vigilanteId, conn);
            string sql = "";
            if (vigilante != null)
            {
                sql = @"UPDATE vigilantes SET nombre = '{1}', tag = '{2}', tagf = '{3}'
                        WHERE vigilanteId = {0}";
            }
            else
            {
                sql = @"INSERT INTO vigilantes (vigilanteId, nombre, tag, tagf)
                        VALUES({0},'{1}','{2}','{3}')";
            }
            sql = String.Format(sql, vig.vigilanteId, vig.nombre, vig.tag, vig.tagf);
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                int nrec = cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteVigilantes(SqlCeConnection conn)
        {
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "DELETE FROM vigilantes";
                int nrec = cmd.ExecuteNonQuery();
            }
        }
    }
}
