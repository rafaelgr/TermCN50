using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;

namespace TermCN50Lib
{
    public class TRonda
    {
        private int _rondaId;
        private string _nombre;
        private string _tag;
        private string _tagf;

        public int rondaId
        {
            get { return _rondaId; }
            set { _rondaId = value; }
        }

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

    }
    public static partial class CntFCN50
    {
        public static TRonda GetRondaFromDr(SqlCeDataReader dr)
        {
            TRonda r = new TRonda();
            r.rondaId = dr.GetInt32(0);
            r.nombre = dr.GetString(1);
            r.tag = dr.GetString(2);
            r.tagf = dr.GetString(3);
            return r;
        }

        public static TRonda GetTRonda(int id, SqlCeConnection conn)
        {
            TRonda r = null;
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = String.Format("SELECT * FROM rondas WHERE rondaId = {0}", id);
                using (SqlCeDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        r = GetRondaFromDr(dr);

                    }
                    if (!dr.IsClosed)
                        dr.Close();
                }
            }
            return r;
        }

        public static void SetRonda(TRonda r, SqlCeConnection conn)
        {
            if (r == null) return;
            // comprobamos si existe el registro
            TRonda ronda = GetTRonda(r.rondaId, conn);
            string sql = "";
            if (ronda != null)
            {
                sql = @"UPDATE rondas SET nombre = '{1}', tag = '{2}', tagf = '{3}'
                        WHERE rondaId = {0}";
            }
            else
            {
                sql = @"INSERT INTO rondas (rondaId, nombre, tag, tagf)
                        VALUES({0},'{1}', '{2}', '{3}')";
            }
            sql = String.Format(sql, r.rondaId, r.nombre, r.tag, r.tagf);
            Console.WriteLine("SQL: " + sql);
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                int nrec = cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteRondas(SqlCeConnection conn)
        {
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "DELETE FROM rondas";
                int nrec = cmd.ExecuteNonQuery();
            }
        }
    }
}
