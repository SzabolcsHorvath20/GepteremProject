using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GepteremProject
{
    public partial class Form1 : Form
    {
        PetrikLajosSzg petrik = new PetrikLajosSzg("petrikgepek.txt");
        private int aktivgepterem = 0;
        private int oldalméret;
        PictureBox[,] gepek;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            kirajzolas();
        }

        private void kirajzolas()
        {
            panel1.Controls.Clear();
            oldalméret = (int)Math.Min(Math.Floor((double)(panel1.Height / petrik.Geptermek[aktivgepterem].SorDb)),
                    Math.Floor((double)(panel1.Width / petrik.Geptermek[aktivgepterem].HelyDb)));
            gepek = new PictureBox[petrik.Geptermek[aktivgepterem].SorDb, petrik.Geptermek[aktivgepterem].HelyDb];
            Text = petrik.Geptermek[aktivgepterem].Nev + "(" + petrik.Geptermek[aktivgepterem].atlag()+")";
            string[] split = petrik.Geptermek[aktivgepterem].Nev.Split(' ');
            pictureBox1.BackgroundImage = Image.FromFile("Kepek/" + split[0] + ".jpg");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            for (int i = 0; i < petrik.Geptermek[aktivgepterem].Ertekeles.GetLength(0); i++)
            {
                for (int j = 0; j < petrik.Geptermek[aktivgepterem].Ertekeles.GetLength(1); j++)
                {
                    gepek[i, j] = new PictureBox();
                    gepek[i, j].Location = new System.Drawing.Point(j*oldalméret, i*oldalméret);
                    gepek[i, j].Size = new System.Drawing.Size(oldalméret - 2, oldalméret - 2);
                    gepek[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                    gepek[i, j].Tag = i.ToString() + ";" + j.ToString();
                    switch (petrik.Geptermek[aktivgepterem].Ertekeles[i,j])
                    {
                        case 0:
                            gepek[i, j].BackgroundImage = Image.FromFile("Kepek/Pont0.jpg");
                            break;
                        case 1:
                            gepek[i, j].BackgroundImage = Image.FromFile("Kepek/Pont1.jpg");
                            break;
                        case 2:
                            gepek[i, j].BackgroundImage = Image.FromFile("Kepek/Pont2.jpg");
                            break;
                        case 3:
                            gepek[i, j].BackgroundImage = Image.FromFile("Kepek/Pont3.jpg");
                            break;
                    }
                    gepek[i, j].Click += this.PictureClick;
                    panel1.Controls.Add(gepek[i, j]);
                }
            }
        }

        private void PictureClick(object sender, EventArgs e)
        {
            PictureBox picture = sender as PictureBox;
            string[] split = picture.Tag.ToString().Split(';');
            switch (petrik.Geptermek[aktivgepterem].Ertekeles[Convert.ToInt32(split[0]), Convert.ToInt32(split[1])])
            {
                case 0:
                    MessageBox.Show("Az adott helyen nem ült senki!","Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    break;
                case 1:
                    petrik.Geptermek[aktivgepterem].Ertekeles[Convert.ToInt32(split[0]), Convert.ToInt32(split[1])] = 2;
                    break;
                case 2:
                    petrik.Geptermek[aktivgepterem].Ertekeles[Convert.ToInt32(split[0]), Convert.ToInt32(split[1])] = 3;
                    break;
                case 3:
                    petrik.Geptermek[aktivgepterem].Ertekeles[Convert.ToInt32(split[0]), Convert.ToInt32(split[1])] = 1;
                    break;
            }
            kirajzolas();
        }

        private void btnBal_Click(object sender, EventArgs e)
        {
            aktivgepterem--;
            if (aktivgepterem < 0)
            {
                aktivgepterem = 3;
            }
            kirajzolas();
        }

        private void btnJobb_Click(object sender, EventArgs e)
        {
            aktivgepterem++;
            if (aktivgepterem == petrik.Geptermek.Count)
            {
                aktivgepterem = 0;
            }
            kirajzolas();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                File.Copy("petrikgepek.txt", "petrikgepek.bak", true);
                StreamWriter ir = new StreamWriter("petrikgepek.txt");
                foreach (var item in petrik.Geptermek)
                {
                    ir.WriteLine(item.Nev);
                    ir.WriteLine(item.SorDb + ";" + item.HelyDb);
                    for (int i = 0; i < item.Ertekeles.GetLength(0); i++)
                    {
                        for (int j = 0; j < item.Ertekeles.GetLength(1); j++)
                        {
                            ir.Write(item.Ertekeles[i,j]+";");
                        }
                        ir.Write("\n");
                    }
                    ir.Write("\n");
                }
                ir.Close();
            }
            catch (IOException a)
            {
                MessageBox.Show(a.Message, "Hiba",MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
    }
}
