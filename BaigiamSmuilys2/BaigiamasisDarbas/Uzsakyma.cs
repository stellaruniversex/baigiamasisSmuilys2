using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaigiamasisDarbas
{
    class Uzsakyma
    {
        private string prekespavad, pardpavad, sandpavad;
        private int kiekis, metai, menuo, diena;
        public Uzsakyma()
        {
            prekespavad = "";
            pardpavad = "";
            sandpavad = "";
            kiekis = 0;
            metai = 0;
            menuo = 0;
            diena = 0;
        }
        public Uzsakyma(string prekespavad, string pardpavad, string sandpavad, int kiekis, int metai, int menuo, int diena)
        {
            this.prekespavad = prekespavad;
            this.pardpavad = pardpavad;
            this.sandpavad = sandpavad;
            this.kiekis = kiekis;
            this.metai = metai;
            this.menuo = menuo;
            this.diena = diena;
        }
        public void SetPrekesPavad(string prp) { prekespavad = prp; }
        public void SetPardPavad(string parp) { pardpavad = parp; }
        public void SetSandPavad(string sanp) { sandpavad = sanp; }
        public void SetKiekis(int k) { kiekis = k; }
        public void SetMetai(int m) { metai = m; }
        public void SetMenuo(int n) { menuo = n; }
        public void SetDiena(int d) { diena = d; }
        public string GetPrekesPavad() { return prekespavad; }
        public string GetPardPavad() { return pardpavad; }
        public string GetSandPavad() { return sandpavad; }
        public int GetKiekis() { return kiekis; }
        public int GetMetai() { return metai; }
        public int GetMenuo() { return menuo; }
        public int GetDiena() { return diena; }
    }
}
