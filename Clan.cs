using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaDomaci
{
    internal class Clan
    {
        private static int maxKnjiga = 3;
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public List<Knjiga> KnjigeKodClana { get; set; }

        public Clan(int id, string ime, string prezime)
        {
            Id = id;
            Ime = ime;
            Prezime = prezime;
        }
        public override string ToString()
        {
            return $"Id: {Id}, ime i prezime clana: {Ime} {Prezime}";
        }
    }
}
