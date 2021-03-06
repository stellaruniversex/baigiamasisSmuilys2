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
        public string GetVardas() { return Vardas; }
        public string GetPavarde() { return Pavarde; }
        public string GetGyvVieta() { return GyvVieta; }
        public Int64 GetTelefonas() { return Telefonas; }
    }
}
