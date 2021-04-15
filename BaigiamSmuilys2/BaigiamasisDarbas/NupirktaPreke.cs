using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaigiamasisDarbas
{
    class NupirktaPreke
    {
        private string prekestipas, parduotuve, vardas, pavarde;
        private int kiekis, metai, menuo, diena;
        public NupirktaPreke()
        {
            prekestipas = "";
            parduotuve = "";
            vardas = "";
            pavarde = "";
            kiekis = 0;
            metai = 0;
            menuo = 0;
            diena = 0;
        }
        public NupirktaPreke(string prekestipas, string parduotuve, string vardas, string pavarde, int kiekis, int metai, int menuo, int diena)
        {
            this.prekestipas = prekestipas;
            this.parduotuve = parduotuve;
            this.vardas = vardas;
            this.pavarde = pavarde;
            this.kiekis = kiekis;
            this.metai = metai;
            this.menuo = menuo;
            this.diena = diena;
        }
        public void SetPrekesTipas(string prt) { prekestipas = prt; }
        public void SetParduotuve(string pr) { parduotuve = pr; }
        public void SetVardas(string v) { vardas = v; }
        public void SetPavarde(string p) { pavarde = p; }
        public void SetKiekis(int k) { kiekis = k; }
        public void SetMetai(int m) { kiekis = m; }
        public void SetMenuo(int mn) { kiekis = mn; }
        public void SetDiena(int d) { kiekis = d; }
        public string GetPrekesTipas() { return prekestipas; }
        public string GetParduotuve() { return parduotuve; }
        public string GetVardas() { return vardas; }
        public string GetPavarde() { return pavarde; }
        public int GetKiekis() { return kiekis; }
        public int GetMetai() { return metai; }
        public int GetMenuo() { return menuo; }
        public int GetDiena() { return diena; }
    }
}
