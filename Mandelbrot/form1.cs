using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Mandelbrot
{
    public partial class Form1 : Form
    {
        // ** VARIABLER ** //
        double x_min, x_max, y_min, y_max, bailout;
        int i, i_max, zoom;

        public Form1()
        {
            InitializeComponent();

            x_min = Convert.ToDouble(textBox1.Text);
            x_max = Convert.ToDouble(textBox2.Text);
            y_min = Convert.ToDouble(textBox3.Text);
            y_max = Convert.ToDouble(textBox4.Text);
            i_max = Convert.ToInt32(textBox5.Text);
            bailout = Convert.ToDouble(textBox6.Text);
            zoom = Convert.ToInt32(textBox7.Text);

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)          // Rita upp mandelbrotmängden på skärmen
        {
            DrawMandelbrot();
        }

        double TransX(double x, double x_min, double x_max)                // Omvandla en pixel på platsen x i x-led till en punkt a
        {                                                               // i det komplexa talplanet
            /* 
             * Det komplexa talplanet utgörs av x_min < x < x_max och y_min < y_max.
             */
            return (x / (900.0 / (x_max - x_min))) + x_min;
        }

        double TransY(double y, double y_min, double y_max)                // Omvandla en pixel på platsen y i y-led till en punkt b
        {                                                               // i det komplexa talplanet
            return y_max - (y / (600.0 / (y_max - y_min)));
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox4.Enabled = !textBox4.Enabled;
        }

        int iterColor(int i, int i_max)                                // Skapa färgpalett beroende på hur snabbt [a, b] flyr origo
        {
            return (int)Math.Round(((double)i / (double)i_max) * 225.0, 0);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (checkBox2.Checked)
            {
                label6.Text = "x: " + Convert.ToString(TransX(e.X, Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text)));
                label7.Text = "y: " + Convert.ToString(TransY(e.Y, Convert.ToDouble(textBox3.Text), Convert.ToDouble(textBox4.Text)));
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            label6.Visible = !label6.Visible;
            label7.Visible = !label7.Visible;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                i_max = Convert.ToInt32(textBox5.Text);

                if (Convert.ToInt32(textBox5.Text) > 255)          // Om antalet iterationer överstiger 255 -> påpeka
                {
                    textBox5.BackColor = Color.Yellow;
                }
                else
                {
                    textBox5.BackColor = Color.White;
                }
            }
            catch
            {
                textBox5.BackColor = Color.LightPink;
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            try
            {
                bailout = Convert.ToDouble(textBox6.Text);

                if (Convert.ToInt32(textBox6.Text) > 2)            // Om bailout är större än 2 -> påpeka
                {
                    textBox6.BackColor = Color.Yellow;
                }
                else
                {
                    textBox6.BackColor = Color.White;
                }
            }
            catch
            {
                textBox6.BackColor = Color.LightPink;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Antalet iterationer innan [x, y] anses tillhöra mandelbrotmängden.");
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Avbryt iterationen när [x, y] flyr utanför bailout-radien.");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                x_min = Convert.ToDouble(textBox1.Text);

                if (checkBox1.Checked)                                  // Räkna automatiskt ut y_max
                {
                    y_max = y_min + (x_max - x_min) * (2.0 / 3.0);
                    textBox4.Text = Convert.ToString(y_max);
                }

                if (x_min >= x_max)
                {
                    textBox1.BackColor = Color.LightPink;
                }
                else
                {
                    textBox1.BackColor = Color.White;
                    textBox2.BackColor = Color.White;
                }
            }
            catch
            {
                textBox1.BackColor = Color.LightPink;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                y_min = Convert.ToDouble(textBox3.Text);

                if (checkBox1.Checked)                                  // Räkna automatiskt ut y_max
                {
                    y_max = y_min + (x_max - x_min) * (2.0 / 3.0);
                    textBox4.Text = Convert.ToString(y_max);
                }

                if (y_min >= y_max)
                {
                    textBox3.BackColor = Color.LightPink;
                }
                else
                {
                    textBox3.BackColor = Color.White;
                    textBox4.BackColor = Color.White;
                }
            }
            catch
            {
                textBox3.BackColor = Color.LightPink;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                x_max = Convert.ToDouble(textBox2.Text);

                if (checkBox1.Checked)                                  // Räkna automatiskt ut y_max
                {
                    y_max = y_min + (x_max - x_min) * (2.0 / 3.0);
                    textBox4.Text = Convert.ToString(y_max);
                }

                if (x_min >= x_max)
                {
                    textBox2.BackColor = Color.LightPink;
                }
                else
                {
                    textBox2.BackColor = Color.White;
                    textBox1.BackColor = Color.White;
                }
            }
            catch
            {
                textBox2.BackColor = Color.LightPink;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                y_max = Convert.ToDouble(textBox4.Text);

                if (y_max <= y_min)
                {
                    textBox4.BackColor = Color.LightPink;
                }
                else
                {
                    textBox4.BackColor = Color.White;
                    textBox3.BackColor = Color.White;
                }
            }
            catch
            {
                textBox4.BackColor = Color.LightPink;
            }
        }

        void DrawMandelbrot()
        {
            try
            {
                progressBar1.Visible = true;

                int colorOption = comboBox1.SelectedIndex;              // Hämta renderings- och färginställningar
                int mandelbrotOption = comboBox2.SelectedIndex;
                int renderOption = 1;

                if (radioButton1.Checked) renderOption = 1;
                else if (radioButton2.Checked) renderOption = 2;
                else if (radioButton3.Checked) renderOption = 3;
                else if (radioButton4.Checked) renderOption = 4;

                Bitmap bitmap = new Bitmap(900, 600);                   // Skapa ritbord
                pictureBox1.Image = bitmap;

                for (int y = 0; y < 600; y++)                           // Loopa igenom pixel för pixel
                {
                    for (int x = 0; x < 900; x++)
                    {                               
                        double re = 0;                                  // Skapa komplext tal z
                        double im = 0;

                        double re_old = 0;                              // Skapa kopia av z
                        double im_old = 0;

                        double a = TransX(x, x_min, x_max);             // Gör om en koordinat [x, y] på ritbordet till en punkt [a, b]
                        double b = TransY(y, y_min, y_max);             // i det komplexa talplanet. Denna punkt bildar ett nytt
                                                                        // komplext tal c.
                        for (i = 1; Math.Sqrt(Math.Pow(re, 2) + Math.Pow(im, 2)) < bailout && i <= i_max; i++)
                        {                                               // Iterera i det komplexa talplanet medan absolutbeloppet för
                                                                        // z är mindre än bailout och antalet iterationer i <= i_max
                            re_old = Math.Pow(re, 2) - Math.Pow(im, 2); // Sätt z till z^2 + c
                            im_old = 2.0 * re * im;

                            re = a + re_old;
                            im = b + im_old;
                        }

                        //*************************************************************************************** coloring
                        if (Math.Pow(re, 2) + Math.Pow(im, 2) < Math.Pow(bailout, 2))
                        {                                               // Om absolutbeloppet av z är mindre än bailout tillhör
                                                                        // punkten [x, y] mandelbrotmängden. 
                            if (mandelbrotOption == 0)                  // Fyll pixeln med vald färg
                                bitmap.SetPixel(x, y, Color.White);
                            else if (mandelbrotOption == 1)
                                bitmap.SetPixel(x, y, Color.Black);
                            else if (mandelbrotOption == 2)
                            {
                                if (colorOption == 0)
                                    bitmap.SetPixel(x, y, Color.FromArgb(iterColor(i, i_max), iterColor(i, i_max), iterColor(i, i_max)));
                                else if (colorOption == 1)
                                    bitmap.SetPixel(x, y, Color.FromArgb(iterColor(i, i_max), iterColor(i, i_max), 0));
                                else if (colorOption == 2)
                                    bitmap.SetPixel(x, y, Color.FromArgb(iterColor(i, i_max), 0, 0));
                                else if (colorOption == 3)
                                    bitmap.SetPixel(x, y, Color.FromArgb(0, iterColor(i, i_max), 0));
                                else if (colorOption == 4)
                                    bitmap.SetPixel(x, y, Color.FromArgb(0, 0, iterColor(i, i_max)));
                            }
                        }
                        /////////////////////////////////////////////////////////////////////////////////////////
                        else                                            // Tona
                        {
                            if (renderOption == 1)                      // iter
                            {
                                if (colorOption == 0)
                                    bitmap.SetPixel(x, y, Color.FromArgb(iterColor(i, i_max), iterColor(i, i_max), iterColor(i, i_max)));
                                else if (colorOption == 1)
                                    bitmap.SetPixel(x, y, Color.FromArgb(iterColor(i, i_max), iterColor(i, i_max), 0));
                                else if (colorOption == 2)
                                    bitmap.SetPixel(x, y, Color.FromArgb(iterColor(i, i_max), 0, 0));
                                else if (colorOption == 3)
                                    bitmap.SetPixel(x, y, Color.FromArgb(0, iterColor(i, i_max), 0));
                                else if (colorOption == 4)
                                    bitmap.SetPixel(x, y, Color.FromArgb(0, 0, iterColor(i, i_max)));     
                            }
                            else if (renderOption == 2)                 // abs
                            {
                                if (colorOption == 0)
                                    bitmap.SetPixel(x, y, Color.FromArgb(absColor(re, im), absColor(re, im), absColor(re, im)));
                                else if (colorOption == 1)
                                    bitmap.SetPixel(x, y, Color.FromArgb(absColor(re, im), absColor(re, im), 0));
                                else if (colorOption == 2)
                                    bitmap.SetPixel(x, y, Color.FromArgb(absColor(re, im), 0, 0));
                                else if (colorOption == 3)
                                    bitmap.SetPixel(x, y, Color.FromArgb(0, absColor(re, im), 0));
                                else if (colorOption == 4)
                                    bitmap.SetPixel(x, y, Color.FromArgb(0, 0, absColor(re, im)));
                            }
                            else if (renderOption == 3)                 // cos
                            {
                                if (colorOption == 0)
                                    bitmap.SetPixel(x, y, Color.FromArgb(cosColor(i), cosColor(i), cosColor(i)));
                                else if (colorOption == 1)
                                    bitmap.SetPixel(x, y, Color.FromArgb(cosColor(i), cosColor(i), 0));
                                else if (colorOption == 2)
                                    bitmap.SetPixel(x, y, Color.FromArgb(cosColor(i), 0, 0));
                                else if (colorOption == 3)
                                    bitmap.SetPixel(x, y, Color.FromArgb(0, cosColor(i), 0));
                                else if (colorOption == 4)
                                    bitmap.SetPixel(x, y, Color.FromArgb(0, 0, cosColor(i)));
                            }
                            else if (renderOption == 4)                 // iter+abs+cos
                            {
                                bitmap.SetPixel(x, y, Color.FromArgb(absColor(re, im), iterColor(i, i_max), cosColor(i)));
                            }
                        }
                        //*************************************************************************************** coloring end
                    }
                    progressBar1.PerformStep();                         // Ändra status på progressBar  
                }

                progressBar1.Visible = false;
                progressBar1.Value = 0;                                 // Nollställ progressBar

                label9.Text = "Zoom: " + Convert.ToString(Convert.ToDouble(3.0 / (x_max - x_min))) + " X";
            }
            catch
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            i_max = 50;
            bailout = 2;
            x_min = -2;
            x_max = 1;
            y_min = -1;
            y_max = 1;

            textBox1.Text = Convert.ToString(x_min);
            textBox2.Text = Convert.ToString(x_max);
            textBox3.Text = Convert.ToString(y_min);
            textBox4.Text = Convert.ToString(y_max);
            textBox5.Text = Convert.ToString(i_max);
            textBox6.Text = Convert.ToString(bailout);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                switch(e.Button)
                {
                    case MouseButtons.Left:                               // Zooma in 
                        double deltaX = x_max - x_min;
                        double deltaY = y_max - y_min;

                        double X = TransX(e.X, x_min, x_max);
                        double Y = TransY(e.Y, y_min, y_max);

                        textBox1.Text = Convert.ToString(X - (deltaX / (2.0 * (double)zoom)));
                        textBox2.Text = Convert.ToString(X + (deltaX / (2.0 * (double)zoom)));
                        textBox3.Text = Convert.ToString(Y - (deltaY / (2.0 * (double)zoom)));
                        textBox4.Text = Convert.ToString(Y + (deltaY / (2.0 * (double)zoom)));
                        break;
                        //////////////////////////////////////////////////////////////////
                    case MouseButtons.Right:                              // Zooma ut
                        deltaX = x_max - x_min;
                        deltaY = y_max - y_min;

                        X = TransX(e.X, x_min, x_max);
                        Y = TransY(e.Y, y_min, y_max);

                        textBox1.Text = Convert.ToString(X - (deltaX / (2.0 * (1.0 / (double)zoom))));
                        textBox2.Text = Convert.ToString(X + (deltaX / (2.0 * (1.0 / (double)zoom))));
                        textBox3.Text = Convert.ToString(Y - (deltaY / (2.0 * (1.0 / (double)zoom))));
                        textBox4.Text = Convert.ToString(Y + (deltaY / (2.0 * (1.0 / (double)zoom))));
                        break;
                }

                DrawMandelbrot();
            }
            catch
            {

            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            try
            {
                zoom = Convert.ToInt32(textBox7.Text);
                textBox7.BackColor = Color.White;

                trackBar1.Value = Convert.ToInt32(textBox7.Text);
            }
            catch
            {
                textBox7.BackColor = Color.LightPink;
            }
        }

        int cosColor(int i)
        {
            return (int)Math.Abs((Math.Cos(i) * 255));
        }

        int absColor(double re, double im)
        {
            int color = Convert.ToInt32(Math.Sqrt(((Math.Pow(re, 2) + Math.Pow(im, 2)) - 3.0) * 1750.0));
            if (color <= 255)
            {
                return color;
            }
            else
            {
                return 255;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Anger med vilken faktor koordinatsystemets höjd och bredd kommer att skalas ner för varje klick.");
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            label9.Visible = !label9.Visible;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            zoom = trackBar1.Value;
            textBox7.Text = Convert.ToString(zoom);

            textBox7.Text = Convert.ToString(trackBar1.Value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = !panel1.Visible;

            if (panel1.Visible == true)
            {
                this.Width = 1125;
                button2.Text = "<<";
            }
            else
            {
                this.Width = 955;
                button2.Text = ">>";
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = !comboBox1.Enabled;

            textBox6.Text = Convert.ToString(2);
            textBox6.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process.Start("SnippingTool.exe");
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox6.Text = Convert.ToString(2);
            textBox6.Enabled = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox6.Enabled = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBox6.Enabled = true;
        }
    }
}
