using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaigiamasisDarbas
{
    class Pirkejas
    {
        private string Vardas, Pavarde, GyvVieta;
        private Int64 Telefonas;
        public Pirkejas()
        {
            Vardas = "";
            Pavarde = "";
            GyvVieta = "";
            Telefonas = 0;
        }
        public Pirkejas(string Vardas, string Pavarde, string GyvVieta, Int64 Telefonas)
        {
            this.Vardas = Vardas;
            this.Pavarde = Pavarde;
            this.GyvVieta = GyvVieta;
            this.Telefonas = Telefonas;
        }
        public void SetVardas(string vrd) { Vardas = vrd; }
        public void SetPavarde(string pvr) { Pavarde = pvr; }
        public void SetGyvVieta(string gyv) { GyvVieta = gyv; }
        public void SetTelefonas(Int64 tel) { Telefonas = tel; }
        public string GetVardas() { return Vardas; }
        public string GetPavarde() { return Pavarde; }
        public string GetGyvVieta() { return GyvVieta; }
        public Int64 GetTelefonas() { return Telefonas; }

        public static bool operator <(Pirkejas pirk1, Pirkejas pirk2)
        {
            int p = pirk1.Vardas.CompareTo(pirk2.Vardas);
            int v = String.Compare(pirk1.Pavarde, pirk2.Pavarde, StringComparison.CurrentCulture);
            return (p > 0 || (p == 0 && v > 0));
        }

        public static bool operator >(Pirkejas pirk1, Pirkejas pirk2)
        {
            int p = pirk1.Vardas.CompareTo(pirk2.Vardas);
            int v = String.Compare(pirk1.Pavarde, pirk2.Pavarde, StringComparison.CurrentCulture);
            return (p < 0 || (p == 0 && v < 0));
        }
    }
}
