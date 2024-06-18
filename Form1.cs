using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp21
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();
        int balance = 0;
        int gamePrice = 5;
        int dailyExpense = 15; // Günlük kesin çıkarılması gereken para
        int finalizedBalance = 0;
        int cigPrice = 20;
        int gainRandom;
        int percentageGain;
        int givenSmokNum = 0;
        int[] numbers = new int[15] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
        int[] gain = new int[5] { 1, 5, 10, 20, 40 };
        int[] userNumber = new int[7];
        public Form1()
        {
            InitializeComponent();
        }
        public int randomGenerator(int[] givenN,int giveControl)
        {
            int sayi = rnd.Next(15);
            if (giveControl == 0) //Kullaniciya sigara vermeyecegim icin kullanicinin sayilarindan farkli bir sayi belirlemeliyim
            {
                int i = 0;
                foreach (int number in givenN)
                {
                    if (number == sayi)
                    {
                        i = 1;
                    }
                }
                if (i == 1)
                {
                    sayi = randomGenerator(givenN,0);
                }
            }
            else//kullaniciya sigara verecegim icin kullanicinin belirttigi sayilardan birini vermeliyim.
            {
                int i = 0;
                foreach (int number in givenN)
                {
                    if (sayi == number)
                    {
                        i = 1;
                    }
                }
                if (i != 1)
                {
                    sayi = randomGenerator(givenN, 1);
                }
            }
            return sayi; //recursive fonksiyon oldugu icin geri sayi donecek.
        }
        public void giveSmoke() // sigara verme fonksiyonu
        {
            gainRandom = rnd.Next(5);
            percentageGain = rnd.Next(3); // 3 farkli durum 0 1 2
            balance = gamePrice; 
            givenSmokNum++;
            int durationTime = 100;
            label4.Text = finalizedBalance.ToString() + " TL";
            label4.Update();
            //Sayilari almam lazim
            userNumber[0] = Convert.ToInt32(numericUpDown1.Value);
            userNumber[1] = Convert.ToInt32(numericUpDown2.Value);
            userNumber[2] = Convert.ToInt32(numericUpDown3.Value);
            userNumber[3] = Convert.ToInt32(numericUpDown4.Value);
            userNumber[4] = Convert.ToInt32(numericUpDown5.Value);
            userNumber[5] = Convert.ToInt32(numericUpDown6.Value);
            userNumber[6] = Convert.ToInt32(numericUpDown7.Value);
            //Simdi sigara verecegim icin kullanıcının girdigi degerleri vermeliyim.
            int randNumber = randomGenerator(userNumber,1);

            for (int i = 0; i < 3; i++) // sayi 3 kere bastan sona gitsin ucuncude dursun 
            {
                if (i == 2)//son tur yavaslayacak ve belirledigi sayida duracak.
                {
                    foreach (int num in numbers)
                    {
                        if (num == randNumber)
                        {
                            label1.Text = num.ToString();
                            label1.Update();
                            break;
                        }
                        else
                        {
                            label1.Text = num.ToString();
                            label1.Update();
                            Thread.Sleep(durationTime);
                        }

                    }
                }
                else
                {
                    foreach (int num in numbers) // sayilari donecek
                    {
                        label1.Text = num.ToString();
                        label1.Update();
                        Thread.Sleep(durationTime);
                    }
                    durationTime += 100;
                }
            }
            label6.Text = "Tebrikler Sigara Kazandınız!!";
            label6.Update();
            label5.Text = givenSmokNum.ToString() + " ADET";
            label5.Update();
        }
        public void dontGive() // sigara vermeme fonksiyonu
        {
            label1.Visible = true;   
            balance += gamePrice;
            int durationTime = 100;
            label4.Text = finalizedBalance.ToString() + " TL";
            label4.Update();
            //Sayilari almam lazim
            userNumber[0] = Convert.ToInt32(numericUpDown1.Value); 
            userNumber[1] = Convert.ToInt32(numericUpDown2.Value);
            userNumber[2] = Convert.ToInt32(numericUpDown3.Value);
            userNumber[3] = Convert.ToInt32(numericUpDown4.Value);
            userNumber[4] = Convert.ToInt32(numericUpDown5.Value);
            userNumber[5] = Convert.ToInt32(numericUpDown6.Value);
            userNumber[6] = Convert.ToInt32(numericUpDown7.Value);
            //Simdi sigara vermeyecegim icin bu degerlerden farkli bi deger olusturmam gerekli
            int randNumber = randomGenerator(userNumber,0);

            for (int i = 0; i < 3; i++)
            {
                if (i == 2)
                {
                    foreach (int num in numbers)
                    {
                        if (num == randNumber)
                        {
                            label1.Text = num.ToString();
                            label1.Update();
                            label6.Text = "Tekrar Deneyin!!";
                            label6.Update();
                            break;
                        }
                        else 
                        {
                            label1.Text = num.ToString();
                            label1.Update();
                            Thread.Sleep(durationTime);
                        }
                        
                    }
                }
                else
                {
                    foreach (int num in numbers)
                    {
                        label1.Text = num.ToString();
                        label1.Update();
                        Thread.Sleep(durationTime);
                    }
                    durationTime += 100;
                }
            } 
        }
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            int profit = 0; //kar belirliyorum
            for(int j = 0; j < Convert.ToInt32(comboBox1.SelectedItem); j++)
            {
                finalizedBalance += gamePrice; // makinenin icindeki toplam para
                profit = (finalizedBalance - (cigPrice * givenSmokNum)); //kâr
                if (profit < dailyExpense)//eger gunluk harcamadan dusukse kari cikarana kadar sigara verme
                {
                    percentageGain = 1;//kari cikarma yuzdelikli kisma git
                }

                if (percentageGain == 0) // %33 ihtimalle kasa o el kârlı kullanıcı %16 ihtimalle sigara kazanir.
                {
                    if (balance < (cigPrice + gain[gainRandom]))
                    {
                        dontGive();
                    }
                    else
                    {
                        int percent = rnd.Next(1); // iki durumlu 0 1 --> %50 ihtimal.
                        if(percent == 0)
                        {
                            giveSmoke();
                        }
                        else
                        {
                            dontGive();
                            percentageGain = rnd.Next(3);
                        }
                    }
                }
                else if (percentageGain == 1) // %33 ihtimalle kasa zarari komple cikarir, oyuncunun en talihsiz oldugu durum
                {
                    if (profit > ((cigPrice * givenSmokNum) + dailyExpense) + cigPrice + gamePrice)
                    {
                        giveSmoke();
                    }
                    else
                    {
                        dontGive();
                    }
                }
                else if (percentageGain == 2) // %8 ihtimalle kullanici sigara kazanir.
                {
                    int percent = rnd.Next(4); // dort durumlu 0 1 2 3 --> %50 kazanma ihtimali var.
                    if(percent == 0)
                    {
                        giveSmoke();
                    }
                    else
                    {
                        dontGive();
                        percentageGain = rnd.Next(3);
                    }
                }
                System.Console.WriteLine(gain[gainRandom].ToString());
                label8.Text = profit.ToString() + " TL";
                label8.Update();
                Thread.Sleep(1300); // tekrar deneyin yazisi gorunmesi icin
                label6.Text = "";
            }
            button1.Visible = true;  
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Visible = false;
            gainRandom = rnd.Next(5);
            percentageGain = rnd.Next(3); // 3 farkli durum 0 1 2
            comboBox1.SelectedIndex = 0;
            label18.Text = gamePrice.ToString() + " TL";
        }
    }
}
