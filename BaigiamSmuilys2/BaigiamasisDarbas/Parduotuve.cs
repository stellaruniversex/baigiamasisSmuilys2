using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaigiamasisDarbas
{
    class Parduotuve
    {
        const int Cmax = 2048;
        //private Preke[] preke;
        private int Lentynos, Dydis, Imokos, Ismokos, n;
        private Int64 telefonas;
        private string Vieta;
        public Parduotuve()
        {
            n = 0;
            Lentynos = 0;
            Dydis = 0;
            Imokos = 0;
            Ismokos = 0;
            telefonas = 0;
            Vieta = "";
            //preke = new Preke[Cmax];
        }
        public Parduotuve(int Lentynos, int Dydis, int Imokos, int Ismokos, Int64 telefonas, string Vieta, Preke[] preke)
        {
            this.Lentynos = Lentynos;
            this.Dydis = Dydis;
            this.Imokos = Imokos;
            this.Ismokos = Ismokos;
            this.telefonas = telefonas;
            this.Vieta = Vieta;
            //this.preke = preke;
        }
        //public Preke Get(int i) { return preke[i]; }
        //public int Get() { return n; }
        //public void Set(Preke pr) { preke[n++] = pr; }
        public void SetLentynos(int l) { Lentynos = l; }
        public void SetDydis(int d) { Dydis = d; }
        public void SetImokos(int i) { Imokos = i; }
        public void SetIsmokos(int ii) { Ismokos = ii; }
        public void SetVieta(string v) { Vieta = v; }
        public int GetLentynos() { return Lentynos; }
        public int GetDydis() { return Dydis; }
        public int GetImokos() { return Imokos; }
        public int GetIsmokos() { return Ismokos; }
        public string GetVieta() { return Vieta; }
        public static bool operator <(Parduotuve pard1, Parduotuve pard2)
        {
            int p = String.Compare(pard1.Vieta, pard2.Vieta, StringComparison.CurrentCulture);
            int v = pard1.Dydis.CompareTo(pard2.Dydis);
            return (p > 0 || (p == 0 && v > 0));
        }

        public static bool operator >(Parduotuve pard1, Parduotuve pard2)
        {
            int p = String.Compare(pard1.Vieta, pard2.Vieta, StringComparison.CurrentCulture);
            int v = pard1.Dydis.CompareTo(pard2.Dydis);
            return (p < 0 || (p == 0 && v < 0));
        }
    }
}
