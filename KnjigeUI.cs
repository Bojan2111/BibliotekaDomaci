using KonzolniMeni;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaDomaci
{
    internal class KnjigeUI
    {
        public static List<Knjiga> knjige = new List<Knjiga>();
        public static void KnjigeMeni()
        {
            Meni knjigeMeni = new Meni();
            knjigeMeni.DodajOpciju(Ispis, "Ispis svih kniga biblioteke");
            knjigeMeni.DodajOpciju(Unos, "Unos nove knjige");
            knjigeMeni.DodajOpciju(Brisanje, "Brisanje knige");
            knjigeMeni.Pokreni();
        }
        public static void UpisivanjeUListu()
        {
            SqlConnection conn = new SqlConnection("Server =.\\SQLEXPRESS; Database = Biblioteka; Integrated Security=true;");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from Knjiga", conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            
            while (rdr.Read())
            {
                string kodClanaStr = rdr["kod_clana"].ToString();
                Clan kodClana = null;
                Knjiga tempKnjiga = null;
                bool knjigaPostoji = false;
                if (rdr["kod_clana"].ToString() != "")
                {
                    foreach (Clan c in ClanoviUI.clanovi)
                    {
                        if (int.Parse(kodClanaStr) == c.Id)
                        {
                            kodClana = c;
                            tempKnjiga = new Knjiga(int.Parse(rdr[0].ToString()), rdr[1].ToString(), rdr[2].ToString(), int.Parse(rdr[3].ToString()), kodClana);
                            break;
                        }
                        
                    }
                }
                else
                {
                    tempKnjiga = new Knjiga(int.Parse(rdr[0].ToString()), rdr[1].ToString(), rdr[2].ToString(), int.Parse(rdr[3].ToString()));
                }
                foreach (Knjiga k in knjige)
                {
                    if (k.Id == tempKnjiga.Id)
                    {
                        knjigaPostoji = true;
                        break;
                    }
                }
                if (!knjigaPostoji)
                {
                    knjige.Add(tempKnjiga);
                }
            }
            conn.Close();
        }

        private static void Brisanje()
        {
            SqlConnection conn = new SqlConnection("Server =.\\SQLEXPRESS; Database = Biblioteka; Integrated Security=true;");
            conn.Open();
            Console.WriteLine("Unesite ID broj knjige koju zelite obrisati:");
            int idBrisanje = int.Parse(Console.ReadLine());
            
            foreach (Knjiga cl in knjige)
            {
                if (cl.Id == idBrisanje)
                {
                    knjige.Remove(cl);
                    break;
                }
            }
            string brisanje = $"delete from Knjiga where id={idBrisanje}";
            SqlCommand cmd = new SqlCommand(brisanje, conn);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Knjiga uspesno obrisana.");
            conn.Close();
        }

        private static void Unos()
        {
            SqlConnection conn = new SqlConnection("Server =.\\SQLEXPRESS; Database = Biblioteka; Integrated Security=true;");
            conn.Open();
            Console.WriteLine("Unesite naslov knjige:");
            string noviNaslov = Console.ReadLine();
            Console.WriteLine("Unesite ime i prezime autora:");
            string noviAutor = Console.ReadLine();
            Console.WriteLine("Unesite godinu izdavanja knjige:");
            int novaGodina = int.Parse(Console.ReadLine());
            Console.WriteLine("Knjiga uspesno dodata\nUkoliko knjigu zelite izdati clanu, pristupite toj opciji kroz meni 'Rad sa clanovima'.");
            string insertString = "INSERT INTO Knjiga " +
                "(naslov, autor, godina_izdavanja, kod_clana) " +
                "VALUES (@naslov, @autor, @godina, NULL);";
            SqlCommand cmd22 = new SqlCommand(insertString, conn);
            cmd22.Parameters.AddWithValue("@naslov", noviNaslov);
            cmd22.Parameters.AddWithValue("@autor", noviAutor);
            cmd22.Parameters.AddWithValue("@godina", novaGodina);
            cmd22.ExecuteNonQuery();
            conn.Close();
            UpisivanjeUListu();
        }

        private static void Ispis()
        {
            foreach (Knjiga k in knjige)
            {
                Console.WriteLine(k);
            }
        }
    }
}
