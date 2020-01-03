using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Threading;
using ClassLibrary2;

namespace WindowsFormsDelicate
{
    public partial class Form2 : Form
    {
        public Form1 frm1;
        public Form2()
        {
            if (fight)
                return;
            InitializeComponent();
            Fight_Started += Fight_Source;
            Turn_Hero += TurnHero;
            Turn_Enemy += TurnEnemy;
            UpdInfo += Update_INFO;
            KeyPreview = true;
        }
        

        public Pen Apen = new Pen(Color.Red);
        public SolidBrush Cpen = new SolidBrush(Color.White);
        public SolidBrush Cpen3 = new SolidBrush(Color.White);
        public Pen Bpen = new Pen(Color.Black);
        public Pen Cpen2 = new Pen(Color.Orange);
        public SolidBrush bruh = new SolidBrush(Color.Blue);
        public Pen bruh2 = new Pen(Color.Yellow);
        public Rectangle rekt, roomIns, HallsIn;

        public Bitmap bmp = new Bitmap(Environment.CurrentDirectory + "\\ImagesH\\olp.png");
        public CheckedListBox clb = new CheckedListBox();
        public Player_Binds player;
        public List<Enemies> enemys = new List<Enemies>();
        public List<Thread> thList = new List<Thread>();
        public List<Thread> thRunList = new List<Thread>();
        public param_s prms = new param_s();
        ToolTip tt1 = new ToolTip();
        private void Form2_Load(object sender, EventArgs e)
        {
            pictureBox2.Location = new Point(0, frm1.Start_Height +10);
            ToolTip tt1 = new ToolTip();
            tt1.AutoPopDelay = 10000; //10 сек
            tt1.SetToolTip(STOIKA_BUTTON, "Встаёте в стойку. +50% заблокировать след. атаку врага \n +7 урона +10% контратаковать.\n Но +30% Уклонение врага");
            info_label.Location = new Point(frm1.Start_Width + 10, 10);
            // checkedListBox1.Enabled = false;
            if (fight)
                return;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            pictureBox2.Image = Image.FromFile(Environment.CurrentDirectory + "\\ImagesH\\Battle_tendence.png");
            this.Size = new Size(frm1.Start_Width + 257, frm1.Start_Height + 292); //17 40
            pictureBox1.Height = frm1.Start_Height + 2;
            pictureBox1.Width = frm1.Start_Width + 2;
            checkedListBox1.Location = new Point(frm1.Start_Width + 8, 10);
            checkedListBox1.Items.AddRange(new object[] { "Hide leafs", "Hide halls", "Hide rooms", "Perspective Mode" });
            ReBuild();
            CreateNew();
            pictureBox1.Refresh();
            LoadEnemySightAndWay();
            pictureBox1.Select();
        }
        public CancellationTokenSource source;
        public void LoadEnemySightAndWay()
        {
            if (fight)
                return;
            // prms.bmp = new Bitmap(Environment.CurrentDirectory + "\\ImagesH\\olp.png");
            source = new CancellationTokenSource();
            prms.ct = source.Token;
            prms.pb = pictureBox1;
            prms.plar = player;
            prms.enaList = enemys;
            prms.thList = thRunList;
            prms.St_Width = frm1.Start_Width;
            prms.St_Height = frm1.Start_Height;
            prms.thAutoList = thList;
            Thread newTh = new Thread(delegate () { PathFinding.CheckForEntities(prms); });
            thRunList.Add(newTh);
            newTh.Start();
        }
        public bool fight = false;
        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (fight)
                return;
            if (e.KeyCode == Keys.Left)
            {
                if (GetPixelColor("left"))
                {
                    --player.recCollision.X;
                    --player.Cor_X;
                    --player.LNS[0].CorX;
                    --player.LNS[1].CorX;
                    pictureBox1.Refresh();
                }
            }
            if (e.KeyCode == Keys.Up)
            {
                if (GetPixelColor("up"))
                {
                    --player.recCollision.Y;
                    --player.Cor_Y;
                    --player.LNS[0].CorY;
                    --player.LNS[1].CorY;
                    pictureBox1.Refresh();
                }
            }
            if (e.KeyCode == Keys.Right)
            {
                if (GetPixelColor("right"))
                {
                    ++player.recCollision.X;
                    ++player.Cor_X;
                    ++player.LNS[0].CorX;
                    ++player.LNS[1].CorX;
                    pictureBox1.Refresh();
                }
            }
            if (e.KeyCode == Keys.Down)
            {
                if (GetPixelColor("down"))
                {
                    ++player.recCollision.Y;
                    ++player.Cor_Y;
                    ++player.LNS[0].CorY;
                    ++player.LNS[1].CorY;
                    pictureBox1.Refresh();
                }
            }
            if (e.KeyCode == Keys.F3) //New paint
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Prove = true;
                ReBuild();
                CreateNew();
                set = true;
                pictureBox1.Refresh();
                LoadEnemySightAndWay();
            }
        }
        public bool Prove = true;
        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (fight)
                return;
            bmp = new Bitmap(frm1.Start_Width + 20, frm1.Start_Height + 20);
            e.Graphics.FillRectangle(bruh, 0, 0, frm1.Start_Width + 20, frm1.Start_Height + 20);
            e.Graphics.DrawRectangle(Bpen, 0, 0, frm1.Start_Width + 20, frm1.Start_Height + 20);
            using (Graphics gfx = Graphics.FromImage(bmp))
            using (SolidBrush brush = new SolidBrush(Color.Blue))
            {
                gfx.FillRectangle(brush, 0, 0, frm1.Start_Width + 20, frm1.Start_Height + 20);
                gfx.Dispose();
                brush.Dispose();
            }
            foreach (New_Leaf_WithCor _p in frm1.Halls)
            {
                Graphics g = e.Graphics;
                for (int i = 0; i < 2; i++)
                {
                    if (_p.Rect[i].height == 0 && _p.Rect[i].width == 0)
                        continue;
                    HallsIn = new Rectangle(_p.Rect[i].x, _p.Rect[i].y, _p.Rect[i].width, _p.Rect[i].height);
                    if (checkedListBox1.GetItemCheckState(3) == CheckState.Unchecked)
                    {
                        e.Graphics.FillRectangle(Cpen3, HallsIn);
                        using (Graphics gfx = Graphics.FromImage(bmp))
                        using (SolidBrush brush = new SolidBrush(Color.White))
                        {
                            gfx.FillRectangle(brush, HallsIn);
                            gfx.Dispose();
                            brush.Dispose();
                        }
                    }
                    else
                    {
                        if (checkedListBox1.GetItemCheckState(1) == CheckState.Checked)
                            e.Graphics.FillRectangle(Cpen3, HallsIn);
                        else
                            e.Graphics.DrawRectangle(bruh2, HallsIn);

                    }
                }
            }
            foreach (New_Leaf _l in frm1.st)
            {
                if (_l.LeftChild == null && _l.RightChild == null)
                {
                    Graphics g = e.Graphics;
                    rekt = new Rectangle(_l.X, _l.Y, _l.Width, _l.Height);
                    g.DrawRectangle(Apen, rekt);
                    roomIns = new Rectangle(_l.Aroom.x, _l.Aroom.y, _l.Aroom.width, _l.Aroom.height);
                    if (checkedListBox1.GetItemCheckState(3) == CheckState.Unchecked)
                    {
                        g.FillRectangle(Cpen, roomIns);
                        using (Graphics gfx = Graphics.FromImage(bmp))
                        using (SolidBrush brush = new SolidBrush(Color.White))
                        {
                            gfx.FillRectangle(brush, roomIns);
                            gfx.Dispose();
                            brush.Dispose();
                        }
                    }
                    else
                    {
                        if (checkedListBox1.GetItemCheckState(2) == CheckState.Checked)
                            e.Graphics.FillRectangle(Cpen, roomIns);
                        else
                            g.DrawRectangle(Cpen2, roomIns);
                    }
                }
            }
            if (Prove)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                try
                {
                    bmp.Save(Environment.CurrentDirectory + "\\ImagesH\\olp.png");
                    bmp.Save(Environment.CurrentDirectory + "\\ImagesH\\olp1.png");
                }
                catch (Exception) { }
                Prove = false;
            }
            recPlayer = new Rectangle(player.Cor_X, player.Cor_Y, player.Linear_side, player.Linear_side);
            e.Graphics.FillRectangle(new SolidBrush(Color.Orange), recPlayer);
            if (enemyRectanglepos != null)
                enemyRectanglepos.Clear();
            if (enemys.Count == 0)
                WinScreen.Visible = true;
            foreach (Enemies en in enemys)
            {
                e.Graphics.DrawRectangle(new Pen(Color.Black), new Rectangle(en.recCollision.X, en.recCollision.Y, en.recCollision.Width, en.recCollision.Height));
                e.Graphics.DrawRectangle(new Pen(Color.Orange), new Rectangle(en.LNS[0].CorXsec, en.LNS[0].CorYsec, en.LNS[1].CorXsec - en.LNS[0].CorXsec, en.LNS[3].CorYsec - en.LNS[0].CorYsec));
                enemyRectanglepos.Add(new Rectangle(en.Cor_X, en.Cor_Y, en.Linear_side, en.Linear_side));
                e.Graphics.FillRectangle(new SolidBrush(Color.Black), enemyRectanglepos.ElementAt(enemyRectanglepos.Count - 1));
                if (set)
                {
                    Start_Enemies(en);
                    ++set_1;
                    if (set_1 == enemys.Count)
                    {
                        set = false;
                        set_1 = 0;
                    }
                }
            }
            if (!fight)
                for (int g = 0; g < enemyRectanglepos.Count; g++)
                {
                    if (enemyRectanglepos.ElementAt(g).IntersectsWith(recPlayer))
                    {
                        fight = true;
                        source.Cancel();
                        pictureBox2.Image = Image.FromFile(Environment.CurrentDirectory + "\\ImagesH\\FightScene.png");
                        foreach (Thread thr in thList)
                        {
                            thr.Suspend();
                        }
                        //
                        pictureBox2.Refresh();
                        checkedListBox1.Enabled = false;
                        Fight_Started.Invoke(this, new NumOfEnemy
                        {
                            i = (byte)g
                        });
                        //fight = false;
                        //enemyRectanglepos.RemoveAt(i);
                        //enemys.RemoveAt(i);
                        //foreach (Thread thr in thList)
                        //{
                        //    thr.Resume();
                        //}
                        //LoadEnemySightAndWay();
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                    }

                }
        }
        public Rectangle recPlayer = new Rectangle();
        public List<Rectangle> enemyRectanglepos = new List<Rectangle>();
        public Byte set_1 = 0;
        public bool set = true;
        private void CheckedListBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedItem == null)
                return;
            string CurItem = checkedListBox1.SelectedItem.ToString();
            switch (CurItem)
            {
                case "Hide leafs":
                    if (checkedListBox1.GetItemCheckState(0) == CheckState.Checked)
                        Apen.Color = Color.Transparent;
                    else
                        Apen.Color = Color.Red;
                    break;
                case "Hide halls":
                    if (checkedListBox1.GetItemCheckState(1) == CheckState.Checked)
                        Cpen3.Color = Color.Transparent;
                    else
                        Cpen3.Color = Color.White;
                    break;
                case "Hide rooms":
                    if (checkedListBox1.GetItemCheckState(2) == CheckState.Checked)
                        Cpen.Color = Color.Transparent;
                    else
                        Cpen.Color = Color.White;
                    break;
            }
            pictureBox1.Refresh();
        }
        List<param_s> paramList = new List<param_s>();
        public void Start_Enemies(Enemies en)
        {
            param_s parameters = new param_s();
            parameters.ena = en;
            parameters.bmp = new Bitmap(Environment.CurrentDirectory + "\\ImagesH\\olp.png");
            parameters.pb = pictureBox1;
            paramList.Add(parameters);
            Thread newTh = new Thread(new ParameterizedThreadStart(Enemies_Logic.Path));
            newTh.IsBackground = true;
            thList.Add(newTh);
            newTh.Start(parameters);
        }
        public void ReBuild()
        {
            enemyRectanglepos.Clear();
            if (prms.thAutoList != null)
                prms.thAutoList.Clear();
            foreach (Rectangle gl in enemyRectanglepos)
            {
                //gl.dis
            }
            foreach (Thread thr in thRunList)
            {
                thr.Abort();
            }
            foreach (Thread thr in thList)
            {
                thr.Abort();
            }
            foreach (param_s ps in paramList)
            {
                ps.bmp.Dispose();
            }
            Thread.Sleep(1200);
            if (prms.bmp != null)
                prms.bmp.Dispose();
            if (bmp != null)
                bmp.Dispose();
            enemys.Clear();
            frm1.st.Clear();
            frm1.Halls.Clear();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            ReBuild();
        }

        private void PictureBox2_Paint(object sender, PaintEventArgs e)
        {

        }

        public bool GetPixelColor(string rotation)
        {
            bool ret = true;
            int n = 0;
            using (Bitmap bmpd = new Bitmap(Environment.CurrentDirectory + "\\ImagesH\\olp.png"))
            {
                switch (rotation)
                {
                    case "up":
                        while (n < player.Linear_side)
                        {
                            if (bmpd.GetPixel(player.Cor_X + n, player.Cor_Y - 1) == Color.FromArgb(255, 0, 0, 255))
                            {
                                ret = false;
                                break;
                            }
                            n++;

                        }
                        break;
                    case "down":
                        while (n < player.Linear_side)
                        {
                            if (bmpd.GetPixel(player.Cor_X + n, player.Cor_Y + player.Linear_side) == Color.FromArgb(255, 0, 0, 255))
                            {
                                ret = false;
                                break;
                            }
                            n++;

                        }
                        break;
                    case "left":
                        while (n < player.Linear_side)
                        {
                            if (bmpd.GetPixel(player.Cor_X - 1, player.Cor_Y + n) == Color.FromArgb(255, 0, 0, 255))
                            {
                                ret = false;
                                break;
                            }
                            n++;

                        }
                        break;
                    case "right":
                        while (n < player.Linear_side)
                        {
                            if (bmpd.GetPixel(player.Cor_X + player.Linear_side, player.Cor_Y + n) == Color.FromArgb(255, 0, 0, 255))
                            {
                                ret = false;
                                break;
                            }
                            n++;

                        }
                        break;
                }
                bmpd.Dispose();
            }
            return ret;
        }
        public void CreateNew()
        {
            New_Leaf Base_Leaf = new New_Leaf(0, 0, frm1.Start_Width, frm1.Start_Height);
            Create_Leafs CL = new Create_Leafs();
            frm1.st.Add(Base_Leaf);
            int ArrayOfcrRooms = 0, j = 0, hod = 0;
            for (int i = 0; i < frm1.st.Count; i++)
            {
                CL.Cut_Leafs(frm1.st[i], frm1.MIN_LEAF_Size);
                if (frm1.st[i].LeftChild != null)
                {
                    frm1.st.Add(frm1.st[i].LeftChild);
                }
                if (frm1.st[i].RightChild != null)
                {
                    frm1.st.Add(frm1.st[i].RightChild);
                }
                if (frm1.st[i].LeftChild == null && frm1.st[i].RightChild == null)
                {
                    CL.Create_Room(frm1.st[i], frm1.MIN_ROOM_Size, ref player, ref enemys);
                    frm1.Halls.Add(new New_Leaf_WithCor(frm1.st[i].Aroom.x, frm1.st[i].Aroom.y, frm1.st[i].Aroom.width, frm1.st[i].Aroom.height, 0, 0));
                    ArrayOfcrRooms++;
                }
                if (frm1.Halls.Count > 1 && ArrayOfcrRooms > 0)
                {
                    CL.Create_Hall(ref frm1.Halls, ref j, ref hod, frm1.MIN_ROOM_Size);
                    ArrayOfcrRooms = 0;
                }
            }
        }
        public void Attack_Button_Click(object sender, EventArgs e)
        {
            Random rnd = RandomProvider.GetThreadRandom();
            if (rnd.NextDouble() <= enemy.BlockChance / 100)
            {
            }
            else
            {
                if (rnd.NextDouble() <= enemy.EvadeChance / 100)
                { }
                else
                {
                    if (rnd.NextDouble() <= hero.CriticalDMGChance)
                        enemy.HP -= hero.DMG * hero.CriticalDMG / 100;
                    else
                        enemy.HP -= hero.DMG;
                }
            }
            if (Enemy_is_blocking || enemy.BlockChance>=100)
            {
                Enemy_is_blocking = false;
                enemy.BlockChance -= 25;
            }
            Hero_Attacked = true;
        }
        public class Status
        {
            public int HP { get; set; }
            public int FULLHP { get; set; }
            public int DMG { get; set; }
            public int EvadeChance { get; set; }
            public int BlockChance { get; set; }
            public int CriticalDMGChance { get; set; }
            public int CriticalDMG { get; set; }
            public Status(int hp, int fullhp, int dmg, int evch, int bch, int crch, int cr)
            {
                HP = hp;
                DMG = dmg;
                EvadeChance = evch;
                BlockChance = bch;
                CriticalDMGChance = crch;
                CriticalDMG = cr;
                FULLHP = fullhp;
            }
        }
        public class NumOfEnemy
        {
            public byte i { get; set; }
        }

        public int stHP = 100,
            stBlockChance = 5, //Percentage
            stDMG = 3,
            stEvadeChance = 5, //Percentage
            CriticalDmgChance = 5, //Percentage
            CriticalDmg = 170; //Percentage

        public static int enstHP = 7,
            enstBlockChance = 0, //Percentage
            enstDMG = 3,
            enstEvadeChance = 2, //Percentage
            enCriticalDmgChance = 1, //Percentage
            enCriticalDmg = 145,//Percentage
            Dungeon_Number = 0;

        public bool Enemy_is_blocking = false;
        public bool IsHeroCreated = false;
        public static bool Hero_Attacked = false;
        public Hero hero;
        public Enemy enemy = new Enemy(enstHP, enstHP, enstDMG, enstEvadeChance, enstBlockChance, enCriticalDmgChance, enCriticalDmg);
        public class Hero : Status
        {
            public Hero(int hp, int fullhp, int dmg, int evch, int bch, int crch, int cr) : base(hp, fullhp, dmg, evch, bch, crch, cr)
            { }
        }
        public class Enemy : Status
        {
            public Enemy(int hp, int fullhp, int dmg, int evch, int bch, int crch, int cr) : base(hp, fullhp, dmg, evch, bch, crch, cr)
            { }
        }
        public void Fight_Source(object sender, NumOfEnemy arg)
        {
            Player_HP.Visible = true;
            Enemy_HP.Visible = true;
            Hero_Stats.Visible = true;
            Enemy_Stats.Visible = true;
            Attack_Button.Visible = true;
            STOIKA_BUTTON.Visible = true;
            hodiki.Visible = true;
            hodiki.ForeColor = Color.Green;
            Random rnd = RandomProvider.GetThreadRandom();
            if (!IsHeroCreated)
            {
                hero = new Hero(stHP, stHP, stDMG, stEvadeChance, stBlockChance, CriticalDmgChance, CriticalDmg);
                IsHeroCreated = true;
            }
            int enemyFullhp = (int)rnd.Next(enstHP + Dungeon_Number * 5, enstHP + Dungeon_Number * 7);
            enemy = new Enemy(enemyFullhp,
                enemyFullhp,
                rnd.Next(enstDMG + Dungeon_Number * 3, enstDMG + Dungeon_Number * 5),
                rnd.Next(enstEvadeChance + Dungeon_Number, enstEvadeChance + Dungeon_Number * 2),
                rnd.Next(enstBlockChance + Dungeon_Number, enstBlockChance + Dungeon_Number * 3),
                rnd.Next(enCriticalDmgChance + Dungeon_Number * 2, enCriticalDmgChance + Dungeon_Number * 3),
                rnd.Next(enCriticalDmg + Dungeon_Number * 5, enCriticalDmg + Dungeon_Number * 7));
            Application.DoEvents();
            UpdInfo.Invoke(this, new NumOfEnemy { i = arg.i });
        }
        public void Update_INFO(object sender, NumOfEnemy arg)
        {
           // th1 = new Thread(new ParameterizedThreadStart(Update_Info_thread));
           if(th1 == null)
                th1 = new Thread(new ParameterizedThreadStart(Update_Info_thread));
            //else
            if (th1.ThreadState == System.Threading.ThreadState.Aborted || th1.ThreadState == System.Threading.ThreadState.Running)
                th1 = new Thread(new ParameterizedThreadStart(Update_Info_thread));
            if (th1.ThreadState == System.Threading.ThreadState.Stopped)
                th1 = new Thread(new ParameterizedThreadStart(Update_Info_thread));
            //else
            try
            {
                th1.Start(arg.i);
            }
            catch (Exception) { }
            //if (th1.ThreadState == System.Threading.ThreadState.Stopped)
            //    th1.Resume();
        }

        private void Chck_box_CheckedChanged(object sender, EventArgs e)
        {
            if (chck_box.Checked)
                checkedListBox1.Enabled = true;
            else
                checkedListBox1.Enabled = false;
        }

        private void Form2_KeyUp(object sender, KeyEventArgs e)
        {

        }
        private bool stoika_opt = false;
        private void STOIKA_button_Click(object sender, EventArgs e) //+50% shield defence + 7 damage rate +30 % enemy evasion 
            //(10 процентов контратаки обеспечено)
        {
            hero.BlockChance += 50;
            hero.DMG += 7;
            enemy.EvadeChance += 30;
            stoika_opt = true;
            Hero_Attacked = true;
        }

        void Update_Info_thread(object obj)
        {
            byte g = (byte)obj;
            if (Player_HP.InvokeRequired)
                Player_HP.Invoke(new MethodInvoker(delegate { Player_HP.Text = string.Format("HP: {0}/{1}", hero.HP, hero.FULLHP); }));
            if (Enemy_HP.InvokeRequired)
                Enemy_HP.Invoke(new MethodInvoker(delegate { Enemy_HP.Text = string.Format("HP: {0}/{1}", enemy.HP, enemy.FULLHP); }));
            string Hero_text = string.Format(@"Full HP: {0}
Damage: {1}
Evade Chance: {2}
Block Chance: {3}
CriticalDMG %: {4}
CriticalDMG: {5}%", hero.HP, hero.DMG, hero.EvadeChance, hero.BlockChance, hero.CriticalDMGChance, hero.CriticalDMG);
            string Enemy_text = string.Format(@"Full HP: {0}
Damage: {1}
Evade Chance: {2}
Block Chance: {3}
CriticalDMG %: {4}
CriticalDMG: {5}%", enemy.HP, enemy.DMG, enemy.EvadeChance, enemy.BlockChance, enemy.CriticalDMGChance, enemy.CriticalDMG);
            if (Hero_Stats.InvokeRequired)
                Hero_Stats.Invoke(new MethodInvoker(delegate { Hero_Stats.Text = Hero_text; }));
            if (Enemy_Stats.InvokeRequired)
                Enemy_Stats.Invoke(new MethodInvoker(delegate { Enemy_Stats.Text = Enemy_text; }));
            if (hero.HP <= 0)
            {
                Thread t = Thread.CurrentThread;
                fight = false;
                enemyRectanglepos.RemoveAt(g);
                enemys.RemoveAt(g);
                foreach (Thread thr in thList)
                {
                    thr.Resume();
                }
                LoadEnemySightAndWay();
                if (pictureBox2.InvokeRequired)
                    pictureBox2.Invoke(new MethodInvoker(delegate { pictureBox2.Image = Image.FromFile(Environment.CurrentDirectory + "\\ImagesH\\Battle_tendence.png"); }));
                if (Attack_Button.InvokeRequired)
                    Attack_Button.Invoke(new MethodInvoker(delegate
                    {
                        Attack_Button.Enabled = false;
                        Attack_Button.Visible = false;
                    }));
                if (STOIKA_BUTTON.InvokeRequired)
                    STOIKA_BUTTON.Invoke(new MethodInvoker(delegate
                    {
                        STOIKA_BUTTON.Enabled = false;
                        STOIKA_BUTTON.Visible = false;
                    }));
                if (Player_HP.InvokeRequired)
                    Player_HP.Invoke(new MethodInvoker(delegate
                    {
                        Player_HP.Visible = false;
                    }));
                if (Enemy_HP.InvokeRequired)
                    Enemy_HP.Invoke(new MethodInvoker(delegate
                    {
                        Enemy_HP.Visible = false;
                    }));
                if (Hero_Stats.InvokeRequired)
                    Hero_Stats.Invoke(new MethodInvoker(delegate
                    {
                        Hero_Stats.Visible = false;
                    }));
                if (Enemy_Stats.InvokeRequired)
                    Enemy_Stats.Invoke(new MethodInvoker(delegate
                    {
                        Enemy_Stats.Visible = false;
                    }));
                if (hodiki.InvokeRequired)
                {
                    hodiki.Invoke(new MethodInvoker(delegate
                    {
                        hodiki.Visible = false;
                        hodiki.Text = "<--Ваш ход";
                        hodiki.ForeColor = Color.Green;
                    }));
                }
                t.Abort();
            }
            else if (enemy.HP <= 0)
            {
                if (pictureBox2.InvokeRequired)
                    pictureBox2.Invoke(new MethodInvoker(delegate { pictureBox2.Image = Image.FromFile(Environment.CurrentDirectory + "\\ImagesH\\Battle_tendence.png"); }));
                Thread t = Thread.CurrentThread;
                fight = false;
                enemyRectanglepos.RemoveAt(g);
                enemys.RemoveAt(g);
                foreach (Thread thr in thList)
                {
                    thr.Resume();
                }
                LoadEnemySightAndWay();
                if (hodiki.InvokeRequired)
                {
                    hodiki.Invoke(new MethodInvoker(delegate
                    {
                        hodiki.Visible = false;
                        hodiki.Text = "<--Ваш ход";
                        hodiki.ForeColor = Color.Green;
                    }));
                }
                if (Attack_Button.InvokeRequired)
                    Attack_Button.Invoke(new MethodInvoker(delegate
                    {
                        Attack_Button.Enabled = false;
                        Attack_Button.Visible = false;
                    }));
                if (STOIKA_BUTTON.InvokeRequired)
                    STOIKA_BUTTON.Invoke(new MethodInvoker(delegate
                    {
                        STOIKA_BUTTON.Enabled = false;
                        STOIKA_BUTTON.Visible = false;
                    }));
                if (Player_HP.InvokeRequired)
                    Player_HP.Invoke(new MethodInvoker(delegate
                    {
                        Player_HP.Visible = false;
                    }));
                if (Enemy_HP.InvokeRequired)
                    Enemy_HP.Invoke(new MethodInvoker(delegate
                    {
                        Enemy_HP.Visible = false;
                    }));
                if (Hero_Stats.InvokeRequired)
                    Hero_Stats.Invoke(new MethodInvoker(delegate
                    {
                        Hero_Stats.Visible = false;
                    }));
                if (Enemy_Stats.InvokeRequired)
                    Enemy_Stats.Invoke(new MethodInvoker(delegate
                    {
                        Enemy_Stats.Visible = false;
                    }));
                t.Abort();
            }
            else
            {
                if (Hero_Attacked)
                    Turn_Enemy.Invoke(this, new NumOfEnemy { i = g });
                else
                    Turn_Hero.Invoke(this, new NumOfEnemy { i = g });
            }
        }
        public Thread th1;
        public Stopwatch sw;
        public bool threadwork = false;
        private void TurnHero(object sender, NumOfEnemy e) //hero
        {
            th1 = new Thread(new ParameterizedThreadStart(_thread));
            if (th1.ThreadState == System.Threading.ThreadState.Running)
                th1.Abort();
            th1.Start(e.i);
        }
        void _thread(object obj)
        {
            byte g = (byte)obj;
            if (Attack_Button.InvokeRequired)
                Attack_Button.Invoke(new MethodInvoker(delegate { Attack_Button.Enabled = true; }));
            if (STOIKA_BUTTON.InvokeRequired)
                STOIKA_BUTTON.Invoke(new MethodInvoker(delegate { STOIKA_BUTTON.Enabled = true; }));
            sw = new Stopwatch();
            sw.Start();
            while (!Hero_Attacked)
            {
                if (sw.ElapsedMilliseconds >= 20000)
                    Hero_Attacked = true;
            }
            sw.Stop();
            UpdInfo.Invoke(this, new NumOfEnemy { i = g });
        }

        private void TurnEnemy(object sender, NumOfEnemy e) //enemy
        {
            
            th1 = new Thread(new ParameterizedThreadStart(_thread2));
            if (th1.ThreadState != System.Threading.ThreadState.Running )
                th1.Start(e.i);
        }
        void _thread2(object obj)
        {
            if(hodiki.InvokeRequired)
            {
                hodiki.Invoke(new MethodInvoker(delegate
                {
                    hodiki.Text = "<--Ход противника";
                    hodiki.ForeColor = Color.Red;
                }));
            }
            byte g = (byte)obj;
            Random rnd = RandomProvider.GetThreadRandom();
            // Ход врага
            if (Attack_Button.InvokeRequired)
                Attack_Button.Invoke(new MethodInvoker(delegate { Attack_Button.Enabled = false; }));
            if (STOIKA_BUTTON.InvokeRequired)
                STOIKA_BUTTON.Invoke(new MethodInvoker(delegate { STOIKA_BUTTON.Enabled = false; }));

            if (rnd.NextDouble() > 0.25) //бьёт с вероятностью 75%
            {
                if (rnd.NextDouble() <= (double)hero.BlockChance / 100)
                {
                    if (stoika_opt)
                        if (rnd.NextDouble() <= enemy.EvadeChance) //10 процентов на срабатывание контратаки
                        {
                            //enemy.HP -= hero.DMG;
                        }
                        else if(rnd.NextDouble() <= 0.1)
                        {
                            enemy.HP -= hero.DMG;
                        }
                }//заблочил
                else
                {
                    if (rnd.NextDouble() <= (double)hero.EvadeChance / 100)
                    { } //увернулся
                    else
                    {
                        if (rnd.NextDouble() <= (double)enemy.CriticalDMGChance / 100) //крит. шанс
                            hero.HP -= (int)(enemy.DMG * (enemy.CriticalDMG / 100));
                        else
                            hero.HP -= enemy.DMG;
                    }
                }
            }
            else //Блокирует с +25% шансом, не бьёт
            {
                Enemy_is_blocking = true;
                enemy.BlockChance += 25;
            }
            if (stoika_opt)
            {
                stoika_opt = false;
                hero.BlockChance -= 50;
                hero.DMG -= 7;
                enemy.EvadeChance -= 30;
            }
            Hero_Attacked = false;
            Thread.Sleep(1500);
            if (hodiki.InvokeRequired)
            {
                hodiki.Invoke(new MethodInvoker(delegate
                {
                    hodiki.Text = "<--Ваш ход";
                    hodiki.ForeColor = Color.Green;
                }));
            }
            UpdInfo.Invoke(this, new NumOfEnemy { i = g });
        }

        public static event EventHandler Fight_Started;
        public static event EventHandler Turn_Hero;
        public static event EventHandler Turn_Enemy;
        public static event EventHandler UpdInfo;
        public delegate void EventHandler(object sender, NumOfEnemy arg);
    }
}
