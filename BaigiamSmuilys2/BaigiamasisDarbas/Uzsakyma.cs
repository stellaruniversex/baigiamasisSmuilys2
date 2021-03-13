using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaigiamasisDarbas
{
    class Uzsakyma
    {
        private string prekespavad, pardpavad;
        private int kiekis, metai, menuo, diena;
        public Uzsakyma()
        {
            prekespavad = "";
            pardpavad = "";
            kiekis = 0;
            metai = 0;
            menuo = 0;
            diena = 0;
        }
        public void SetPrekesPavad(string prp) { prekespavad = prp; }
        public void SetPardPavad(string parp) { pardpavad = parp; }
        public void SetKiekis(int k) { kiekis = k; }
        public void SetMetai(int m) { metai = m; }
        public void SetMenuo(int n) { menuo = n; }
        public void SetDiena(int d) { diena = d; }
        public string GetPrekesPavad() { return prekespavad; }
        public string GetPardPavad() { return pardpavad; }
        public int GetKiekis() { return kiekis; }
        public int GetMetai() { return metai; }
        public int GetMenuo() { return menuo; }
        public int GetDiena() { return diena; }
    }
}
