using System;
using KonzolniMeni; // Klasa meni i opcija iz zadatka za kreiranje menija su stavljeni u eksterni dll i uvezeni ovde kao reference.
using System.Data.Sql;
using System.Data.SqlClient;

/*
 * U folderu je ukljucen i sql fajl sa komandama za kreiranje nove baze podataka i tabela
 * potrebnih za ovaj zadatak. Takodje se u istom query-u izvrsava upis podataka u tabele.
 * 
 * Eksterna klasna biblioteka KonzolniMeni.dll je ukljucena preko references u ovaj solution
 * radi lakseg formiranja menija. Hvala vam na zadatku i primeru kako koristiti delegate!
 * KonzolniMeni sadrzi klase Meni i Opcija. Meni sadrzi metode DodajOpciju(<imeFunkcije>, "zeljena opcija u meniju")
 * Na kraju se mora pozvati Pokreni() metoda iz klase Meni, kako bi se meni ispisivao na konzoli.
 */

namespace BibliotekaDomaci
{
    internal class Program
    {
        
        static void Main()
        {
            // Ucitavanje podataka iz SQL baze u liste objekata
            ClanoviUI.UpisivanjeUListu();
            KnjigeUI.UpisivanjeUListu();

            // Ispis glavnog menija
            Meni glavniMeni = new Meni();
            glavniMeni.DodajOpciju(ClanoviUI.ClanoviMeni, "Rad sa clanovima biblioteke");
            glavniMeni.DodajOpciju(KnjigeUI.KnjigeMeni, "Rad sa knjigama u biblioteci");
            glavniMeni.Pokreni();

        }
        
    }
}
