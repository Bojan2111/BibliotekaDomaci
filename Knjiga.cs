using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaDomaci
{
    internal class Knjiga
    {
        public int Id { get; set; }
        public string Naslov { get; set; }
        public string Autor { get; set; }
        public int GodinaIzdavanja { get; set; }
        public Clan KodClana { get; set; }
        public bool Izdata { get; set; }
        public Knjiga(int id, string naslov, string autor, int godinaIzdavanja)
        {
            Id = id;
            Naslov = naslov;
            Autor = autor;
            GodinaIzdavanja = godinaIzdavanja;
            Izdata = false;
        }
        public Knjiga(int id, string naslov, string autor, int godinaIzdavanja, Clan kodClana)
        {
            Id = id;
            Naslov = naslov;
            Autor = autor;
            GodinaIzdavanja = godinaIzdavanja;
            KodClana = kodClana;
            Izdata = KodClana != null;
        }
        public override string ToString()
        {
            string izdata = (KodClana != null) ? $", izdata je kod clana {KodClana.Ime} {KodClana.Prezime}" : " nije izdata";
            return $"Knjiga {Naslov}, autora {Autor}, izdata {GodinaIzdavanja}. godine pod ID {Id}{izdata}";
        }
    }
}
