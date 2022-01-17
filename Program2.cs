using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlTypes;
using MySql.Data.MySqlClient;

namespace kodeopgaveFiskeristyrelse
{
    class ParseToDB
    {
        public void parse(String[] lines)
        {
            MySqlConnection conn = new MySqlConnection("server=127.0.0.1;uid=root;pwd=admin;database=kodeopgavefiskeristyrelse");
            conn.Open();
            foreach (String line in lines.Skip(1))
            {
                String[] elements = line.Split(',');
                String Sloeret_SkibID = elements[0],
                    Skib_Nationalitet = elements[1],
                    Redskab = elements[3],
                    Art = elements[4],
                    Fiske_Zone = elements[5],
                    Fng_Um_Eng = elements[10],
                    Fng_Um_Egenskab = elements[11];
                DateTime Afsejling_Datomedtid = DateTime.Parse(elements[7]),
                    Ankomst_Datomedtid = DateTime.Parse(elements[8]);
                int Afs_Havn = Int32.Parse(elements[6]),
                    Fangst = Int32.Parse(elements[2]);

                using (MySqlCommand queryComm = new MySqlCommand("INSERT into fisk (art,redskab) VALUES (@art, @redskab)"))
                {
                    queryComm.Parameters.Add("@art", MySqlDbType.Int32).Value = Art;
                    queryComm.Parameters.Add("@redskab", MySqlDbType.VarChar, 45).Value = Redskab;
                    conn.Open();

                    int ID = (int)queryComm.ExecuteScalar();

                    using (MySqlCommand queryComm2 = new MySqlCommand("INSERT into skib (name,nationality,fangst,afsejling,afsejling_dato_med_tid,ankomst_dato_med_tid,fng_um_eng,fang_um_egenskab) VALUES (@name,@nationality,@fangst,@afsejling,@afsejling_dato_med_tid,@ankomst_dato_med_tid,@fng_um_eng,@fang_um_egenskab)"))
                    {
                        queryComm.Parameters.Add("@name", MySqlDbType.VarChar, 45).Value = Sloeret_SkibID;
                        queryComm.Parameters.Add("@nationality", MySqlDbType.VarChar, 45).Value = Skib_Nationalitet;
                        queryComm.Parameters.Add("@fangst", MySqlDbType.Int32).Value = ID;
                        queryComm.Parameters.Add("@afsejling", MySqlDbType.VarChar, 45).Value = Afs_Havn;
                        queryComm.Parameters.Add("@afsejling_dato_med_tid", MySqlDbType.VarChar, 45).Value = Afsejling_Datomedtid;
                        queryComm.Parameters.Add("@ankomst_dato_med_tid", MySqlDbType.VarChar, 45).Value = Ankomst_Datomedtid;
                        queryComm.Parameters.Add("@fng_um_eng", MySqlDbType.VarChar, 45).Value = Fng_Um_Eng;
                        queryComm.Parameters.Add("@fang_um_egenskab", MySqlDbType.VarChar, 45).Value = Fng_Um_Egenskab;

                        queryComm2.ExecuteNonQuery();

                        conn.Close();
                    }
                }
            }
        }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            init();
        }

        private static void init()
        {
            String filePath = Path.GetFullPath(Directory.GetCurrentDirectory() + @"..\..\..\../src/Data_til_rapportering_af_fiskeri.csv");
            var lines = File.ReadAllLines(filePath);
            ParseToDB ptdb = new ParseToDB();
            ptdb.parse(lines);

        }
    }
}