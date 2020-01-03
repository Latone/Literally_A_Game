using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary2;
namespace WindowsFormsDelicate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public List<New_Leaf> st = new List<New_Leaf>();
        public List<New_Leaf_WithCor> Halls = new List<New_Leaf_WithCor>();
        public List<TextBox> tbList = new List<TextBox>();
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
        }

        #region OwnParams
        //public List<Leafs> leafs_List = new List<Leafs>();
        //public const int Start_Width = 300;
        //public const int Start_Height = 300;
        //public const int MIN_LEAF_Size = 20; //Чтобы делить
        //public bool divide = true; //Пока есть место - делить
        #endregion
        public int Start_Width = 500;
        public int Start_Height = 252;
        public int MIN_LEAF_Size = 120; //Чтобы делить
        public int MIN_ROOM_Size = 50; // MIN_LEAF_Size*0.5<=

        private void Button1_Click(object sender, EventArgs e)
        {
            st.Clear();
            Halls.Clear();

            if (Check_For_Exc_textboxes() == false)
                return;
            Start_Width = Convert.ToInt32(textBox1.Text);
            Start_Height = Convert.ToInt32(textBox2.Text);
            MIN_LEAF_Size = Convert.ToInt32(textBox3.Text);
            MIN_ROOM_Size = Convert.ToInt32(textBox4.Text);

            Form2 frm = new Form2();
            
                frm.frm1 = this;
                frm.ShowDialog();

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ToolTip tt1 = new ToolTip();
            tt1.AutoPopDelay = 10000; //10 сек
            tt1.SetToolTip(label3, "Меньше, чем первые 2 параметра");
            tt1.SetToolTip(label4, "75% от размера листа или меньше");

            textBox1.Text = Start_Width.ToString();
            textBox2.Text = Start_Height.ToString();
            textBox3.Text = MIN_LEAF_Size.ToString();
            textBox4.Text = MIN_ROOM_Size.ToString();
        }
        public bool Check_For_Exc_textboxes()
        {
            tbList.AddRange(new List<TextBox>
            {
              textBox1,textBox2,textBox3,textBox4
            });
            int i = 0;
            foreach (TextBox j in tbList)
            {
                try
                {
                   Convert.ToInt32(j.Text);
                }
                catch (FormatException e)
                {
                    MessageBox.Show(e.Message);
                    return false;
                }
                if(Convert.ToInt32(j.Text) < 1)
                {
                    MessageBox.Show("Параметры должны быть больше 10");
                    return false;
                }
                if (Convert.ToInt32(j.Text) > Screen.PrimaryScreen.Bounds.Width && i == 0 )
                {
                    MessageBox.Show("Ширина должна быть меньше, чем размер экрана");
                    return false;
                }
                else if (Convert.ToInt32(j.Text) > Screen.PrimaryScreen.Bounds.Height && i == 1)
                {
                    MessageBox.Show("Высота должна быть меньше, чем размер экрана");
                    return false;
                }
                else if (((Convert.ToInt32(j.Text) >= Convert.ToInt32(tbList[0].Text) ||
                Convert.ToInt32(j.Text) >= Convert.ToInt32(tbList[1].Text)) && i == 2 ) || (Convert.ToInt32(j.Text)<11 && i==2))
                {
                    MessageBox.Show("Размер листа должен быть меньше, чем ширина и высота заданных параметров. И больше 10");
                    return false;
                }
                 else if ((Convert.ToInt32(j.Text) > Convert.ToInt32(tbList[2].Text) * 0.75) && i == 3)
                {
                    MessageBox.Show("Максимально допустимый размер комнаты не превышает 75% от размера листа");
                    return false;
                }
                i++;
            }
            return true;
        }
    }
}
