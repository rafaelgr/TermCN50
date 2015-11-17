using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;

namespace TermCN50Lib
{
    public class TPunto
    {
        private int _puntoId;
        private string _nombre;
        private int _edificioId;
        private string _tag;
        private string _cota;
        private string _cubiculo;
        private string _observaciones;

        public int puntoId
        {
            get { return _puntoId; }
            set { _puntoId = value; }
        }

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

        public string tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        public string cota
        {
            get { return _cota; }
            set { _cota = value; }

        }
        public string cubiculo
        {
            get { return _cubiculo; }
            set { _cubiculo = value; }
        }

        public string observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; }
        }


    }
    public static partial class CntFCN50
    {
        public static TPunto GetPuntoFromDr(SqlCeDataReader dr)
        {
            TPunto p = new TPunto();
            p.puntoId = dr.GetInt32(0);
            p.nombre = dr.GetString(1);
            p.edificioId = dr.GetInt32(2);
            p.tag = dr.GetString(3);
            p.cota = dr.GetString(4);
            p.cubiculo = dr.GetString(5);
            p.observaciones = dr.GetString(6);
            return p;
        }

        public static TPunto GetTPunto(int id, SqlCeConnection conn)
        {
            TPunto p = null;
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = String.Format("SELECT * FROM puntos WHERE puntoId = {0}", id);
                using (SqlCeDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        p = GetPuntoFromDr(dr);

                    }
                    if (!dr.IsClosed)
                        dr.Close();
                }
            }
            return p;
        }

        public static void SetPunto(TPunto p, SqlCeConnection conn)
        {
            if (p == null) return;
            // comprobamos si existe el registro
            TPunto punto = GetTPunto(p.puntoId, conn);
            string sql = "";
            if (punto != null)
            {
                sql = @"UPDATE puntos SET nombre = '{1}', edificioId = {2}, tag = '{3}', cota = '{4}', cubiculo = '{5}', observaciones = '{6}'
                        WHERE puntoId = {0}";
            }
            else
            {
                sql = @"INSERT INTO puntos (puntoId, nombre, edificioId, tag, cota, cubiculo, observaciones)
                        VALUES({0},'{1}', {2}, '{3}','{4}','{5}','{6}')";
            }
            sql = String.Format(sql, p.puntoId, p.nombre, p.edificioId, p.tag, p.cota, p.cubiculo, p.observaciones);
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                int nrec = cmd.ExecuteNonQuery();
            }
        }

        public static void DeletePuntos(SqlCeConnection conn)
        {
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "DELETE FROM puntos";
                int nrec = cmd.ExecuteNonQuery();
            }
        }
    }
}
