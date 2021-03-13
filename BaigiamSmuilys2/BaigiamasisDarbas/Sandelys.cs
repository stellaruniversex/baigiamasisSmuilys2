using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaigiamasisDarbas
{
    class Preke
    {
        private string Gamintojas, Pavadinimas;
        private double Kaina;
        private int Metai, Menuo, Diena;
        public Preke()
        {
            Pavadinimas = "";
            Gamintojas = "";
            Kaina = 0;
            Metai = 0;
            Menuo = 0;
            Diena = 0;
        }
        public Preke(string Pavadinimas, string Gamintojas, double Kaina, int Metai, int Menuo, int Diena)
        {
            this.Pavadinimas = Pavadinimas;
            this.Gamintojas = Gamintojas;
            this.Kaina = Kaina;
            this.Metai = Metai;
            this.Menuo = Menuo;
            this.Diena = Diena;
        }
        public string GetPavadinimas() { return Pavadinimas; }
        public string GetGamintojas() { return Gamintojas; }
        public double GetKaina() { return Kaina; }
        public int GetMetai() { return Metai; }
        public int GetMenuo() { return Menuo; }
        public int GetDiena() { return Diena; }

        public static bool operator <(Preke prek1, Preke prek2)
        {
            int p = prek1.Pavadinimas.CompareTo(prek2.Pavadinimas);
            int v = String.Compare(prek1.Gamintojas, prek2.Gamintojas, StringComparison.CurrentCulture);
            return (p > 0 || (p == 0 && v > 0));
        }

        public static bool operator >(Preke prek1, Preke prek2)
        {
            int p = prek1.Pavadinimas.CompareTo(prek2.Pavadinimas);
            int v = String.Compare(prek1.Gamintojas, prek2.Gamintojas, StringComparison.CurrentCulture);
            return (p < 0 || (p == 0 && v < 0));
        }
    }
    class Sandelys
    {
        const int Cmax = 2048;
        private int n, plotas;
        string adresas;

        public Sandelys()
        {
            n = 0; // prekiu kiekis - 1
            plotas = 0; // plotas
            adresas = "";
        }

        public Sandelys(int plotas, string adresas)
        {
            this.plotas = plotas;
            this.adresas = adresas;
        }

        public int Get() { return n; }
    }
    //class Sandelys // senas sandelys, su prekes klase
    //{
    //    const int Cmax = 2048;
    //    private Preke[] preke;
    //    private int n, plotas;
    //
    //    public Sandelys()
    //    {
    //        n = 0; // prekiu kiekis - 1
    //        plotas = 0; // ilgis
    //        preke = new Preke[Cmax];
    //    }
    //
    //    public Sandelys(int plotas, Preke[] preke)
    //    {
    //        this.plotas = plotas;
    //        this.preke = preke;
    //    }
    //
    //    public int Get() { return n; }
    //}
}
