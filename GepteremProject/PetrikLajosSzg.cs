using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GepteremProject
{
    class PetrikLajosSzg
    {
        private List<Gepterem> geptermek = new List<Gepterem>();
        public PetrikLajosSzg(string filenev)
        {
            try
            {
                
                StreamReader olvas = new StreamReader(filenev);
                while (!olvas.EndOfStream)
                {
                    string nev = olvas.ReadLine();
                    if (nev != "")
                    {
                        string[] dimenziok = olvas.ReadLine().Split(';');
                        int[,] ertekeles = new int[Convert.ToInt32(dimenziok[0]), Convert.ToInt32(dimenziok[1])];
                        for (int i = 0; i < ertekeles.GetLength(0); i++)
                        {
                            for (int j = 0; j < ertekeles.GetLength(1); j++)
                            {
                                ertekeles[i, j] = 0;
                            }
                        }
                        for (int i = 0; i < Convert.ToInt32(dimenziok[0]); i++)
                        {
                            string[] sor = olvas.ReadLine().Split(';');
                            for (int j = 0; j < Convert.ToInt32(dimenziok[1]); j++)
                            {
                                ertekeles[i, j] = Convert.ToInt32(sor[j]);
                            }
                        }
                        geptermek.Add(new Gepterem(ertekeles, Convert.ToInt32(dimenziok[1]), nev, Convert.ToInt32(dimenziok[0])));
                    }
                }
                olvas.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hiba!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        internal List<Gepterem> Geptermek { get => geptermek; set => geptermek = value; }
    }
}
