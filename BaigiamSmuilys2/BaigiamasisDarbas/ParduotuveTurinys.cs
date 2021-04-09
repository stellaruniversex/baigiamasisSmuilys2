using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaigiamasisDarbas
{
    class ParduotuveTurinys
    {
        private string prekpavad, adresas;
        private int kiekis;
        public ParduotuveTurinys()
        {
            prekpavad = "";
            adresas = "";
            kiekis = 0;
            //gammetai = 0;
            //gammenuo = 0;
            //gamdiena = 0;
            //metai = 0;
            //menuo = 0;
            //diena = 0;
        }
        public ParduotuveTurinys(string prekpavad, string adresas, int kiekis)
        {
            this.prekpavad = prekpavad;
            this.adresas = adresas;
            this.kiekis = kiekis;
        }
        public void SetPrekPavad(string prp) { prekpavad = prp; }
        public void SetAdresas(string adr) { adresas = adr; }
        public void SetKiekis(int kiek) { kiekis = kiek; }
        public string GetPrekPavad() { return prekpavad; }
        public string GetAdresas() { return adresas; }
        public int GetKiekis() { return kiekis; }
    }
}
