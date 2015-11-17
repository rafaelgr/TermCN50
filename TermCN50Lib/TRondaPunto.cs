using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;

namespace TermCN50Lib
{
    public class TRondaPunto
    {
        private int _rondaPuntoId;
        private int _rondaId;
        private int _orden;
        private int _puntoId;

        public int rondaPuntoId
        {
            get { return _rondaPuntoId; }
            set { _rondaPuntoId = value; }
        }

        public int rondaId
        {
            get { return _rondaId; }
            set { _rondaId = value; }
        }

        public int orden
        {
            get { return _orden; }
            set { _orden = value; }
        }

        public int puntoId
        {
            get { return _puntoId; }
            set { _puntoId = value; }
        }


    }
    public static partial class CntFCN50
    {
        public static TRondaPunto GetRondaPuntoFromDr(SqlCeDataReader dr)
        {
            TRondaPunto rp = new TRondaPunto();
            rp.rondaPuntoId = dr.GetInt32(0);
            rp.rondaId = dr.GetInt32(1);
            rp.orden = dr.GetInt32(2);
            rp.puntoId = dr.GetInt32(3);
            return rp;
        }

        public static TRondaPunto GetTRondaPunto(int id, SqlCeConnection conn)
        {
            TRondaPunto rp = null;
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = String.Format("SELECT * FROM rondaspuntos WHERE rondaPuntoId = {0}", id);
                using (SqlCeDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        rp = GetRondaPuntoFromDr(dr);

                    }
                    if (!dr.IsClosed)
                        dr.Close();
                }
            }
            return rp;
        }

        public static void SetRondaPunto(TRondaPunto rp, SqlCeConnection conn)
        {
            if (rp == null) return;
            // comprobamos si existe el registro
            TRondaPunto rondaPunto = GetTRondaPunto(rp.rondaPuntoId, conn);
            string sql = "";
            if (rondaPunto != null)
            {
                sql = @"UPDATE rondaspuntos SET rondaId = {1}, orden = {2}, puntoId = {3}
                        WHERE rondaPuntoId = {0}";
            }
            else
            {
                sql = @"INSERT INTO rondaspuntos (rondaPuntoId, rondaId, orden, puntoId)
                        VALUES({0}, {1}, {2}, {3})";
            }
            sql = String.Format(sql, rp.rondaPuntoId, rp.rondaId, rp.orden, rp.puntoId);
            Console.WriteLine("SQL: " + sql);
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                int nrec = cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteRondaPunto(TRondaPunto rp, SqlCeConnection conn)
        {
            if (rp == null) return;
            // comprobamos si existe el registro
            TRondaPunto rondaPunto = GetTRondaPunto(rp.rondaPuntoId, conn);
            string sql = "";
            if (rondaPunto != null)
            {
                sql = @"UPDATE rondaspuntos SET rondaId = {1}, orden = {2}, puntoId = {3}
                        WHERE rondaPuntoId = {0}";
            }
            else
            {
                sql = @"INSERT INTO rondaspuntos (rondaPuntoId, rondaId, orden, puntoId)
                        VALUES({0}, {1}, {2}, {3})";
            }
            sql = String.Format(sql, rp.rondaPuntoId, rp.rondaId, rp.orden, rp.puntoId);
            Console.WriteLine("SQL: " + sql);
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                int nrec = cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteRondasPuntos(SqlCeConnection conn)
        {
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "DELETE FROM rondaspuntos";
                int nrec = cmd.ExecuteNonQuery();
            }
        }
    }
}
