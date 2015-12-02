using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;

namespace TermCN50Lib
{
    public class TDescargaLinea
    {
        private int _descargaLineaId;

        public int descargaLineaId
        {
            get { return _descargaLineaId; }
            set { _descargaLineaId = value; }
        }
        private int _descargaId;

        public int descargaId
        {
            get { return _descargaId; }
            set { _descargaId = value; }
        }
        private string _tag;

        public string tag
        {
            get { return _tag; }
            set { _tag = value; }
        }
        private DateTime _fechaHora;

        public DateTime fechaHora
        {
            get { return _fechaHora; }
            set { _fechaHora = value; }
        }
        private string _tipo;

        public string tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }
        private int _tipoId;

        public int tipoId
        {
            get { return _tipoId; }
            set { _tipoId = value; }
        }
        private string _nombre;

        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        private int _incidenciaId;

        public int incidenciaId
        {
            get { return _incidenciaId; }
            set { _incidenciaId = value; }
        }
        private string _observaciones;

        public string observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; }
        }
    }

    public static partial class CntFCN50
    {
        public static TDescargaLinea GetDescargaLineaFromDr(SqlCeDataReader dr)
        {
            TDescargaLinea dl = new TDescargaLinea();
            dl.descargaLineaId = dr.GetInt32(0);
            dl.descargaId = dr.GetInt32(1);
            dl.tag = dr.GetString(2);
            dl.fechaHora = dr.GetDateTime(3);
            dl.tipo = dr.GetString(4);
            dl.tipoId = dr.GetInt32(5);
            dl.nombre = dr.GetString(6);
            dl.incidenciaId = dr.GetInt32(7);
            dl.observaciones = dr.GetString(8);
            return dl;
        }

        public static TDescargaLinea GetTDescargaLinea(int id, SqlCeConnection conn)
        {
            TDescargaLinea dl = null;
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = String.Format("SELECT * FROM descargas_lineas WHERE descargaLineaId = {0}", id);
                using (SqlCeDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        dl = GetDescargaLineaFromDr(dr);

                    }
                    if (!dr.IsClosed)
                        dr.Close();
                }
            }
            return dl;
        }


        public static IList<TDescargaLinea> GetTDescargasLineas(SqlCeConnection conn)
        {
            IList<TDescargaLinea> ldl = new List<TDescargaLinea>();
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = String.Format("SELECT * FROM descargas_lineas");
                using (SqlCeDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        TDescargaLinea dl = GetDescargaLineaFromDr(dr);
                        ldl.Add(dl);

                    }
                    if (!dr.IsClosed)
                        dr.Close();
                }
            }
            return ldl;
        }

        public static void DeleteDescargaLineas(SqlCeConnection conn)
        {
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "DELETE FROM descargas_lineas";
                int nrec = cmd.ExecuteNonQuery();
            }
        }
    }
}
