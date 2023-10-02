using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test2._10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamReader cteni = null;
            StreamWriter prepis = null;
            List<string> list = new List<string>();
            try
            {
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                cteni = new StreamReader("matematika.txt");
                double soucet = 0;
                int pocet = 0;
                double prumer = 0;
                while (!cteni.EndOfStream)
                {
                    string line = cteni.ReadLine();
                    listBox1.Items.Add(line);
                    string[] pole = line.Split(' ');
                    int cislo1 = Convert.ToInt32(pole[0]);
                    char znak = Convert.ToChar(pole[1]);
                    int cislo2 = Convert.ToInt32(pole[2]);
                    double vysledek = 0;
                    switch(znak)
                    {
                        case '-':
                            {
                                vysledek = cislo1 - cislo2;
                                pocet++;
                                break;
                            }
                        case '+':
                            {
                                vysledek = cislo1 + cislo2;
                                pocet++;
                                break;
                            }
                        case '*':
                            {
                                vysledek = cislo1 * cislo2;
                                pocet++;
                                break;
                            }
                        case '/':
                            {
                                if (cislo2 == 0) break;
                                checked { vysledek = (double)cislo1 /(double)cislo2; };
                                pocet++;
                                break;
                            }
                        default:
                            {
                                MessageBox.Show("Špatně zadaná operace");
                                break;
                            }
                    }
                    soucet += vysledek;
                    string priklad = pole[0] +" "+ pole[1]+" " + pole[2]+" " + pole[3]+" " + vysledek;
                    list.Add(priklad);
                }
                cteni.Close();

                prepis = new StreamWriter("matematika.txt", false);
                foreach (string s in list)
                {
                    prepis.WriteLine(s);
                }
                prepis.Close();

                cteni = new StreamReader("matematika.txt");
                while (!cteni.EndOfStream)
                {
                    listBox2.Items.Add(cteni.ReadLine());
                }
                prumer = soucet / pocet;
                cteni.Close();

                label1.Text = " prumer vysledku: " + prumer;

                FileStream tok = new FileStream("prumer.dat", FileMode.OpenOrCreate, FileAccess.Write);
                BinaryWriter zapisprum = new BinaryWriter(tok);
                zapisprum.Write(prumer);
                tok.Close();
            }
            catch(FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(OverflowException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(DivideByZeroException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(ArithmeticException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (cteni != null) cteni.Close();
                if(prepis != null) prepis.Close();
            }
        }
    }
}
