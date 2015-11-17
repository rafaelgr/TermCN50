using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;

namespace TermCN50Lib
{
    public class TTerminal
    {
        private int _terminalId;
        private string _numero;
        private string _nombre;
        private DateTime _fechaAlta;
        private DateTime? _fechaBaja;
        private string _observaciones;

        public string numero
        {
            get { return _numero; }
            set { _numero = value; }
        }

        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public DateTime fechaAlta
        {
            get { return _fechaAlta; }
            set { _fechaAlta = value; }
        }

        public DateTime? fechaBaja
        {
            get { return _fechaBaja; }
            set { _fechaBaja = value; }
        }

        public string observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; }
        }



        public int terminalId
        {
            get { return _terminalId; }
            set { _terminalId = value; }
        }
    }
    public static partial class CntFCN50
    {
        public static TTerminal GetTerminalFromDr(SqlCeDataReader dr)
        {
            TTerminal t = new TTerminal();
            t.terminalId = dr.GetInt32(0);
            t.numero = dr.GetString(1);
            t.nombre = dr.GetString(2);
            if (dr[3] != DBNull.Value)
               t.fechaAlta = dr.GetDateTime(3);
            if (dr[4] != DBNull.Value)
                t.fechaBaja = dr.GetDateTime(4);

            return t;
        }

        public static TTerminal GetTTerminal(int id, SqlCeConnection conn)
        {
            TTerminal t = null;
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = String.Format("SELECT * FROM terminales WHERE terminalId = {0}", id);
                using (SqlCeDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        t = GetTerminalFromDr(dr);

                    }
                    if (!dr.IsClosed)
                        dr.Close();
                }
            }
            return t;
        }

        public static void SetTerminal(TTerminal t, SqlCeConnection conn)
        {
            if (t == null) return;
            // comprobamos si existe el registro
            TTerminal terminal = GetTTerminal(t.terminalId, conn);
            string sql = "";
            if (terminal != null)
            {
                sql = @"UPDATE terminales SET numero = '{1}', nombre = '{2}', fechaAlta = '{3:yyyy-MM-dd}', fechaBaja = '{4:yyyy-MM-dd}', observaciones = '{5}'
                        WHERE terminalId = {0}";
            }
            else
            {
                sql = @"INSERT INTO terminales (terminalId, numero, nombre, fechaAlta, fechaBaja, observaciones)
                        VALUES({0},'{1}','{2}','{3:yyyy-MM-dd}', '{4:yyyy-MM-dd}', '{5}')";
            }
            sql = String.Format(sql, t.terminalId, t.numero, t.nombre, t.fechaAlta, t.fechaBaja, t.observaciones);
            Console.WriteLine("SQL: " + sql);
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                int nrec = cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteTerminales(SqlCeConnection conn)
        {
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "DELETE FROM terminales";
                int nrec = cmd.ExecuteNonQuery();
            }
        }
    }
}
