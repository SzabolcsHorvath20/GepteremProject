using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GepteremProject
{
    class Gepterem
    {
        int[,] ertekeles;
        int helyDb;
        string nev;
        int sorDb;

        public Gepterem(int[,] ertekeles, int helyDb, string nev, int sorDb)
        {
            this.Ertekeles = ertekeles;
            this.HelyDb = helyDb;
            this.Nev = nev;
            this.SorDb = sorDb;
        }

        public int[,] Ertekeles { get => ertekeles; set => ertekeles = value; }
        public int HelyDb { get => helyDb; set => helyDb = value; }
        public string Nev { get => nev; set => nev = value; }
        public int SorDb { get => sorDb; set => sorDb = value; }

        public double atlag()
        {
            double atlag = 0;
            double counter = 0;
            double mennyiseg = 0;
            for (int i = 0; i < Ertekeles.GetLength(0); i++)
            {
                for (int j = 0; j < Ertekeles.GetLength(1); j++)
                {
                    if (Ertekeles[i,j] != 0)
                    {
                        counter++;
                        mennyiseg += Ertekeles[i, j];
                    }
                }
            }
            atlag = mennyiseg / counter;
            
            return Math.Round(atlag, 1);
        }
    }
}
