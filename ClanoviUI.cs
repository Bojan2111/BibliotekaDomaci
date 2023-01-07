using KonzolniMeni;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaDomaci
{
    internal class ClanoviUI
    {
        public static List<Clan> clanovi = new List<Clan>();
        public static void ClanoviMeni()
        {
            // TODO: Lista knjiga koje su kod jednog clana, IznajmljivanjeKnjige(), VracanjeKnjige(), ili nova relaciona tabela
            Meni clanoviMeni = new Meni();
            clanoviMeni.DodajOpciju(Ispis, "Ispis svih clanova biblioteke");
            clanoviMeni.DodajOpciju(Unos, "Unos novog clana");
            clanoviMeni.DodajOpciju(Brisanje, "Brisanje clana");
            clanoviMeni.DodajOpciju(IzdavanjeKnjige, "Izdavanje knjige");
            clanoviMeni.DodajOpciju(VracanjeKnjige, "Vracanje knjige");
            clanoviMeni.Pokreni();
        }
        public static void IzdavanjeKnjige()
        {
            bool knjigaIznajmljena = true;
            int idKnjige = -1;
            Console.WriteLine("Unesite ID broj clana kome zelite izdati knjigu:");
            int idClana = int.Parse(Console.ReadLine());
            Clan tempClan = null;
            foreach (Clan cl in clanovi)
            {
                if (cl.Id == idClana)
                {
                    tempClan = cl;
                }
            }

            while (knjigaIznajmljena)
            {
                Console.WriteLine("Unesite ID broj knjige koju zelite izdati (0 za izlaz):");
                int tempIdKnjige = int.Parse(Console.ReadLine());
                if (tempIdKnjige == 0)
                    break;
                foreach (Knjiga k in KnjigeUI.knjige)
                {
                    if (k.Id == tempIdKnjige && k.KodClana == null)
                    {
                        knjigaIznajmljena = false;
                        idKnjige = tempIdKnjige;
                        k.KodClana = tempClan;
                        break;
                    }
                }
                if (knjigaIznajmljena)
                    Console.WriteLine("Knjiga je vec iznajmljena.");
            }
            if (idKnjige > 0)
            {
                SqlConnection conn = new SqlConnection("Server =.\\SQLEXPRESS; Database = Biblioteka; Integrated Security=true;");
                conn.Open();
                string iznajmljivanje = $"update Knjiga set kod_clana={idClana} where id={idKnjige}";
                SqlCommand cmd = new SqlCommand(iznajmljivanje, conn);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Knjiga uspesno izdata.");
                conn.Close();
            }
        }
        public static void VracanjeKnjige()
        {
            bool knjigaIznajmljena = false;
            int idKnjige = -1;

            while (!knjigaIznajmljena)
            {
                Console.WriteLine("Unesite ID broj knjige koju zelite vratiti (0 za izlaz):");
                int tempIdKnjige = int.Parse(Console.ReadLine());
                if (tempIdKnjige == 0)
                    break;
                foreach (Knjiga k in KnjigeUI.knjige)
                {
                    if (k.Id == tempIdKnjige && k.KodClana != null)
                    {
                        knjigaIznajmljena = true;
                        idKnjige = tempIdKnjige;
                        k.KodClana = null;
                        break;
                    }
                }
                if (!knjigaIznajmljena)
                    Console.WriteLine("Knjiga uopste nije iznajmljena.");
            }
            if (idKnjige > 0)
            {
                SqlConnection conn = new SqlConnection("Server =.\\SQLEXPRESS; Database = Biblioteka; Integrated Security=true;");
                conn.Open();
                string iznajmljivanje = $"update Knjiga set kod_clana=NULL where id={idKnjige}";
                SqlCommand cmd = new SqlCommand(iznajmljivanje, conn);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Knjiga uspesno vracena.");
                conn.Close();
            }
        }
        public static void UpisivanjeUListu()
        {
            SqlConnection conn = new SqlConnection("Server =.\\SQLEXPRESS; Database = Biblioteka; Integrated Security=true;");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from Clan", conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                int id = int.Parse(rdr[0].ToString());
                string ime = rdr[1].ToString();
                string prezime = rdr[2].ToString();
                Clan tempClan = new Clan(id, ime, prezime);
                bool clanPostoji= false;

                foreach (Clan cl in clanovi)
                {
                    if (cl.Id == tempClan.Id)
                    {
                        clanPostoji = true;
                        break;
                    }
                }
                if (!clanPostoji)
                {
                    clanovi.Add(tempClan);
                }
            }
            rdr.Close();
            conn.Close();

        }
        public static void Brisanje()
        {
            SqlConnection conn = new SqlConnection("Server =.\\SQLEXPRESS; Database = Biblioteka; Integrated Security=true;");
            conn.Open();
            Console.WriteLine("Unesite ID clana kojeg zelite obrisati");
            int idBrisanje = int.Parse(Console.ReadLine());
            foreach (Clan cl in clanovi)
            {
                if (cl.Id == idBrisanje)
                {
                    clanovi.Remove(cl);
                    break;
                }
            }
            string brisanje = $"delete from Clan where id={idBrisanje}";
            SqlCommand cmd = new SqlCommand(brisanje, conn);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Clan uspesno obrisan.");
            conn.Close();
        }

        private static void Unos()
        {
            SqlConnection conn = new SqlConnection("Server =.\\SQLEXPRESS; Database = Biblioteka; Integrated Security=true;");
            conn.Open();
            Console.WriteLine("Unesite ime clana:");
            string novoIme = Console.ReadLine();
            Console.WriteLine("Unesite prezime clana:");
            string novoPrezime = Console.ReadLine();
            Console.WriteLine("Novi clan uspesno dodat.");
            string insertString = "INSERT INTO Clan " +
                "(ime, prezime) " +
                "VALUES (@ime, @prezime);";
            SqlCommand cmd = new SqlCommand(insertString, conn);
            cmd.Parameters.AddWithValue("@ime", novoIme);
            cmd.Parameters.AddWithValue("@prezime", novoPrezime);
            cmd.ExecuteNonQuery();
            conn.Close();
            UpisivanjeUListu();
        }

        private static void Ispis()
        {
            foreach (Clan cl in clanovi)
            {
                Console.WriteLine(cl);
            }
        }
    }
}
