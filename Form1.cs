using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Snake_vs_Blocks
{
    public partial class Form1 : Form
    {

        class actor {
            public Rectangle Rdes, Rsrc;
            public Bitmap img;
            public int x, y, w, h, n, m, py, px, y1, y2;
            public Color clr;
            public int r, g, b;


            public void rgb(int r, int g, int b)
            {
                this.r = r;
                this.g = g;
                this.b = b;
            }
        }



        /// <var>
        int time = 0;
        Bitmap off;
        Timer t = new Timer();
        List<actor> hero = new List<actor>();
        List<actor> points = new List<actor>();
        List<actor> blocks = new List<actor>();
        List<actor> colors = new List<actor>();
        List<int> blocksp = new List<int>();
        List<actor> soloblocks = new List<actor>();
        List<actor> lines = new List<actor>();
        List<actor> bombs = new List<actor>();
        List<actor> shields = new List<actor>();
        List<actor> mgntcs = new List<actor>();

        actor shield = new actor();



        int[] linelengh = { 100, 120, 140, 160 };

        List<int> bpostionts = new List<int>();
        int l = 0, r = 0;
        int ru = 0, x = 0;
        bool start = false;
        int tt = 0;
        int blocktime = 70;
        int cb = 0;
        int n = 0;
        int fblock = 0;
        int vx = 9;
        int m = 0;
        int ctu = 0;
        /// </var>
        bool mv = true;
        int ex = 0;
        int fr = 0;
        int fl = 0;
        bool game = true;
        bool hitg = false;
        int hitt = 0;
        int hitn = 0;
        int recoverY = 0;
        int pt = 0;
        int fc = 0;
        int fs = 0;
        int st = 0;
        int b = 0;
        int bt = 0;
        int fm = 0;
        int fmt = 0;
        Bitmap img = new Bitmap("Untitled design (8).png");
        Bitmap img2 = new Bitmap("Untitled design (10).png");
        Bitmap gv = new Bitmap("gameover.bmp");
        Bitmap bomb = new Bitmap("magnet.bmp");

        public Form1()
        {

            this.Size = new System.Drawing.Size(500, 580);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(410, 110);
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            t.Interval = 100;
            t.Tick += T_Tick;
            t.Start();
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;

        }



        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            start = true;
            if (e.KeyCode == Keys.Left)
            {
                l = 0;


            }
            if (e.KeyCode == Keys.Right)
            {
                r = 0;
                ru = 1;

                x = 0;

            }

            drawdub(this.CreateGraphics());
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                l = 1;
                r = 0;
            }
            if (e.KeyCode == Keys.Right)
            {
                r = 1;
                l = 0;
            }
        }
        void creatpoints()
        {
            Random rnd = new Random();
            if (time % 30 == 0)
            {
                rnd = new Random();
                actor pnn = new actor();
                pnn.y = 20;
                pnn.w = 20;
                pnn.h = 20;


                pnn.x = rnd.Next(10, this.Width - 50);
                pnn.n = rnd.Next(1, 5);
                points.Add(pnn);
            }


            for (int j = 0; j < blocks.Count; j++)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    if (points[i].y > blocks[j].y && points[i].y < blocks[j].y + blocks[j].h)
                    {
                        points.RemoveAt(i);

                    }
                }
            }


        }
        void ishitpoint()
        {
            int f = 0;
            int n = 0;
            if (hero.Count > 0)
            {



                
                for (int i = 0; i < points.Count; i++)
                {
                    if
                        ((hero[0].x - 2 < points[i].x &&
                        hero[0].x + 15 > points[i].x &&
                        hero[0].y + 5 > points[i].y &&
                        hero[0].y < points[i].y + 25) || (hero[0].x > points[i].x &&
                        hero[0].x + 20 < points[i].x + 30 &&
                        hero[0].y + 5 > points[i].y &&
                        hero[0].y < points[i].y + 25))
                    {
                        hero[0].n += points[i].n;
                        n = points[i].n;
                        points.RemoveAt(i);

                        f = 1;
                    }
                    else
                    if (fm == 1)
                    {
                        if (points[i].y > hero[0].y - 50 &&
                        points[i].y < hero[0].y + 50 &&
                        points[i].x > hero[0].x - 50 &&
                        points[i].x < hero[0].x + 50)
                        {
                            hero[0].n += points[i].n;
                            n = points[i].n;
                            points.RemoveAt(i);

                            f = 1;
                        }
                    }
                }

                if (f == 1)
                {
                    int x = hero[hero.Count - 1].x;
                    int y = hero[hero.Count - 1].y + 20;
                    for (int i = 0; i < n; i++)
                    {
                        if (hero[0].n == 0)
                        {
                            hero[0].n++;
                        }
                        else
                        {
                            actor pnn = new actor();
                            pnn.x = x;
                            pnn.y = y;
                            pnn.py = y;
                            pnn.clr = (Color.Yellow);
                            pnn.m = -1 * i;
                            hero.Add(pnn);
                            y += 21;
                        }
                    }
                }
            }
        }
        void ishitblock()
        {
            Color c;
            bool hit = false;
            int j = -1;
            int n = 0;
            if (fs == 0)
            {
                for (int i = 0; i < blocks.Count; i++)
                {

                    if (hero.Count - 1 >= 0)
                    {

                        if
                            (hero[0].x > blocks[i].x &&
                            hero[0].x < blocks[i].x + blocks[i].w &&
                            hero[0].y + 3 > blocks[i].y &&
                            hero[0].y < blocks[i].y + blocks[i].h + 5)
                        {
                            hit = true;
                            j = i;
                            n = blocks[i].n;
                            hitg = true;
                            hitn = n;
                            break;
                        }
                        else
                        {
                            if (hero[0].y > blocks[i].y &&
                            hero[0].y < blocks[i].y + blocks[i].h)
                            {
                                if (hero[0].x > blocks[i].x && hero[0].x < blocks[i].x + 22)
                                {
                                    hit = true;
                                    j = i;
                                    n = blocks[i].n;
                                    hitg = true;
                                    hitn = n;
                                    break;
                                }
                                else
                                {
                                    if (hero[0].x < blocks[i].x && hero[0].x > blocks[i].x - 20)
                                    {
                                        hit = true;
                                        j = i;
                                        n = blocks[i].n;
                                        hitg = true;
                                        hitn = n;
                                        break;
                                    }

                                }
                            }
                        }
                    }
                }
            }

            if (hit)
            {
                if (fs == 0)
                {
                    if (hero[0].n != 0)
                    {
                        hero[0].n--;
                        blocks[j].n--;
                        pt++; ;
                    }


                    if (hero[0].n == 0 && blocks[j].n == 0)
                    {

                    }
                    else
                    {
                        hero.RemoveAt(hero.Count - 1);

                    }
                    if (blocks[j].n == 0)
                    {
                        blocks.RemoveAt(j);

                    }


                    if (hero.Count == 0)
                    {
                        game = false;
                    }
                }

            }


        }
        void createbomb()
        {
            Random r = new Random();
            int rr = r.Next(10, this.Width - 70);

            Bitmap img = new Bitmap("bomb (1).bmp");
            Color c = img.GetPixel(0, 0);
            img.MakeTransparent(c);
            actor pnn = new actor();
            pnn.img = img;
            pnn.x = rr;
            pnn.y = -300;
            bombs.Add(pnn);

            for (int j = 0; j < blocks.Count; j++)
            {
                for (int i = 0; i < bombs.Count; i++)
                {
                    if (bombs[i].y > blocks[j].y && bombs[i].y < blocks[j].y + blocks[i].h)
                    {
                        bombs.RemoveAt(i);

                    }
                }

            }
            for (int i = 0; i < bombs.Count; i++)
            {
                for (int j = 0; j < lines.Count; j++)
                {
                    if (bombs[i].y > lines[j].y && bombs[i].y < lines[j].y + lines[i].h
                        && bombs[i].x < lines[j].x && bombs[i].x + bombs[i].img.Width > lines[i].x)
                    {
                        bombs.RemoveAt(i);
                    }

                }
            }
        }
        void createmgntc()
        {
            Random r = new Random();
            int rr = r.Next(10, this.Width - 70);

            Bitmap img = new Bitmap("magnet.bmp");
            Color c = img.GetPixel(0, 0);
            img.MakeTransparent(c);
            actor pnn = new actor();
            pnn.img = img;
            pnn.x = rr;
            pnn.y = -300;
            mgntcs.Add(pnn);


            for (int j = 0; j < blocks.Count; j++)
            {
                for (int i = 0; i < mgntcs.Count; i++)
                {
                    if (mgntcs[i].y > blocks[j].y && mgntcs[i].y < blocks[j].y + blocks[i].h)
                    {
                        mgntcs.RemoveAt(i);

                    }
                }

            }
            for (int i = 0; i < mgntcs.Count; i++)
            {
                for (int j = 0; j < lines.Count; j++)
                {
                    if (mgntcs[i].y > lines[j].y && mgntcs[i].y < lines[j].y + lines[i].h
                        && mgntcs[i].x < lines[j].x && mgntcs[i].x + mgntcs[i].img.Width > lines[i].x)
                    {
                        mgntcs.RemoveAt(i);
                    }

                }
            }

        }

        void touchmagntic()
        {
            if (hero.Count > 0)
            {
                for (int i = 0; i < mgntcs.Count; i++)
                {
                    if
                        ((hero[0].x - 2 < mgntcs[i].x &&
                        hero[0].x + 15 > mgntcs[i].x &&
                        hero[0].y + 5 > mgntcs[i].y &&
                        hero[0].y < mgntcs[i].y + mgntcs[i].img.Height) || (hero[0].x > mgntcs[i].x &&
                        hero[0].x + 20 < mgntcs[i].x + 30 &&
                        hero[0].y + 5 > mgntcs[i].y &&
                        hero[0].y < mgntcs[i].y + mgntcs[i].img.Height))
                    {
                        mgntcs.RemoveAt(i);

                        fm = 1;
                    }


                }

                if (hero.Count > 0)
                {
                    if (fm == 1)
                    {
                        for (int i = 0; i < points.Count; i++)
                        {
                            if (points[i].y < hero[0].y + 200 &&
                                points[i].y > hero[0].y - 200)
                            {
                                if (points[i].x > hero[0].x)
                                    points[i].x -= 20;

                                if (points[i].x < hero[0].x)
                                    points[i].x += 20;

                                if (points[i].y > hero[0].y)
                                    points[i].y -= 20;

                                if (points[i].y < hero[0].y)
                                    points[i].y += 20;
                            }
                            
                        }
                    }
                }
            }
        }
        void creatsheild()
        {
            Random r = new Random();
            int rr = r.Next(10, this.Width - 70);

            Bitmap img = new Bitmap("shield.bmp");
            Color c = img.GetPixel(0, 0);
            img.MakeTransparent(c);
            actor pnn = new actor();
            pnn.img = img;
            pnn.x = rr;
            pnn.y = -300;
            shields.Add(pnn);

            for (int j = 0; j < blocks.Count; j++)
            {
                for (int i = 0; i < shields.Count; i++)
                {
                    if (shields[i].y > blocks[j].y && shields[i].y < blocks[j].y + blocks[i].h)
                    {
                        shields.RemoveAt(i);
                    }
                }

               
            }
            for (int i = 0; i < shields.Count; i++)
            {
                for (int j = 0; j < lines.Count; j++)
                {
                    if (shields[i].y > lines[j].y && shields[i].y < lines[j].y + lines[i].h
                        && shields[i].x < lines[j].x && shields[i].x + shields[i].img.Width > lines[i].x)
                    {
                        shields.RemoveAt(i);
                    }

                }
            }
      
        }
        void touchshield()
        {
            if (hero.Count > 0)
            {
                for (int i = 0; i < shields.Count; i++)
                {
                    if
                        ((hero[0].x - 2 < shields[i].x &&
                        hero[0].x + 15 > shields[i].x &&
                        hero[0].y + 5 > shields[i].y &&
                        hero[0].y < shields[i].y + shields[i].img.Height) || (hero[0].x > shields[i].x &&
                        hero[0].x + 20 < shields[i].x + 30 &&
                        hero[0].y + 5 > shields[i].y &&
                        hero[0].y < shields[i].y + shields[i].img.Height))
                    {
                        shields.RemoveAt(i);

                        fs = 1;
                    }
                   
                        
                }
            }
        }
        void creatblocks()
        {
            int x = 7;
            Random rnd = new Random();
            int w = 90;
            Random l = new Random();
            for (int i = 0; i < 5; i++)
            {
                actor pnn = new actor();
                pnn.x = x;
                pnn.y = -200;
                pnn.w = w;
                pnn.h = w-5;
                pnn.n = rnd.Next(1,15);
                int n = rnd.Next(colors.Count - 1);
                pnn.clr = Color.FromArgb(colors[n].r, colors[n].g, colors[n].b);
                if(fblock==0)
                {
                    pnn.n = rnd.Next(1,5);
                }
                x += 95 ;
                int ll = l.Next(0, 10);
                if(ll %2==0)
                {
                    Random nn = new Random();
                    actor pnn2 = new actor();
                    int nnn = nn.Next(0, linelengh.Length - 1);
                    pnn2.x = pnn.x;
                    pnn2.y1 = pnn.y + pnn.h;
                    pnn2.h = linelengh[nnn];
                    lines.Add(pnn2);
                }
                blocks.Add(pnn);

            }

           





        }
        void creatlines()
        {
            if (blocks.Count > 0)
            {
                Random r = new Random();
               
              

                for (int i = 0; i < blocks.Count; i++)
                {
                    
                }

            }
        }
        void touchbom()
        {
            if (hero.Count > 0)
            {
                for (int i = 0; i < bombs.Count; i++)
                {
                    if
                        ((hero[0].x - 2 < bombs[i].x &&
                        hero[0].x + 15 > bombs[i].x &&
                        hero[0].y + 5 > bombs[i].y &&
                        hero[0].y < bombs[i].y + bombs[i].img.Height) || (hero[0].x > bombs[i].x &&
                        hero[0].x + 20 < bombs[i].x + 30 &&
                        hero[0].y + 5 > bombs[i].y &&
                        hero[0].y < bombs[i].y+ bombs[i].img.Height))
                    {
                        blocks.Clear();
                        lines.Clear();
                        b = 1;

                        bombs.RemoveAt(i);

                    }
                }
            }
        }
        void moveall()
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i].y +=vx;
                if (blocks[i].y > this.Height*2)
                {
                    blocks.RemoveAt(i);
                }
            }
            for (int i = 0; i < soloblocks.Count; i++)
            {
                soloblocks[i].y += vx;
            }

            
            for (int i = 0; i < points.Count; i++)
            {
                if(time%2==0)
                {
                    points[i].w-=2;
                    points[i].h-=2;
                }
                if(time%2 !=0)
                {
                    points[i].w+=2;
                    points[i].h+=2;
                }

                points[i].y += vx;

                if (points[i].y > this.Height)
                {
                    points.RemoveAt(i);
                }
                
               

            }
            for (int i = 0; i < lines.Count; i++)
            {
                lines[i].y1 += vx;
            }

            for (int i = 0; i < bombs.Count; i++)
            {
                bombs[i].y += vx;
            }
            for (int i = 0; i < shields.Count; i++)
            {
                shields[i].y += vx;
            }

            for (int i = 0; i < mgntcs.Count; i++)
            {
                mgntcs[i].y += vx;
            }

        }
        void move()
        {
            fr = 1;
            fl = 1;
            for (int i = 0; i < lines.Count; i++)
            {
                if (hero[0].y > lines[i].y1 &&
                    hero[0].y < lines[i].y1 + lines[i].h )
                {
                    if (lines[i].x < hero[0].x && hero[0].x  < lines[i].x+10)
                    {
                        fl = 0;
                    }
                    else
                    {
                        if (lines[i].x > hero[0].x && hero[0].x + 20 > lines[i].x)
                        {
                            fr = 0;
                        }
                    }

                }

            }

            for (int i = 0; i < blocks.Count; i++)
            {
                if (hero[0].y > blocks[i].y &&
                       hero[0].y < blocks[i].y + blocks[i].h)
                {
                    if (hero[0].x > blocks[i].x && hero[0].x < blocks[i].x + 22)
                    {
                        fl = 0;
                    }
                    else
                    {
                        if (hero[0].x < blocks[i].x && hero[0].x > blocks[i].x - 20)
                        {
                            fr = 0;
                        }
                    }
                }
            }
            ctu++;

            if (r == 1 && hero[0].x <this.Width-40 && fr==1)
            {
                

                hero[0].x += vx;
                fr = 1;
            }

            if (true)
            {


                for (int i = 1; i < hero.Count; i++)
                {
                    if (hero[i].x < hero[i-1].x)
                    {
                        hero[i].x += vx - i;
                        if (ctu % hero.Count - 1 == 0)
                        {
                            hero[i].x += i;
                        }



                        if (ctu > hero.Count - 1)
                        {

                            if (hero[i].y > hero[0].y + 2)
                            {
                                hero[i].y--;

                            }
                        }


                    }
                }

            }






            //if (r == 0)
            //{
            //    for (int i = 1; i < hero.Count; i++)
            //    {
            //        if (hero[i].x < hero[0].x)
            //        {
            //            hero[i].x += 7;
            //            if (hero[i].y < hero[i].py)
            //            {
            //                hero[i].y += 5;
            //            }

            //        }


            //    }


            //}
            if (l == 1 && hero[0].x > 0 && fl==1)
            {
                hero[0].x -= vx;
                fl = 1;

                //if (hero[hero.Count - 1].x > hero[0].x)
                //{
                for (int i = 1; i < hero.Count; i++)
                {
                    if (hero[i].x <= hero[i-1].x)
                    {
                        fl = 0;
                        break;

                    }
                    hero[i].x -= vx - i;
                    if (ctu % hero.Count - 1 == 0)
                    {
                        hero[i].x -= i;
                    }


                    if (ctu > hero.Count - 1)
                    {

                        if (hero[i].y > hero[0].y + 2)
                        {
                            hero[i].y--;

                        }
                    }

                }
            }





            else
            {

                if (l == 0)
                {


                    for (int i = 1; i < hero.Count; i++)
                    {
                        if (hero[i].x > hero[0].x)
                        {
                            hero[i].x -= vx;
                            if (hero[i].y < hero[i].py)
                            {
                                hero[i].y += vx;
                            }
                        }
                    }

                }
            }

            if (ctu > hero.Count)
            {
                ctu = 0;
            }
        }
        void creatsoloblocks()
        {
            Random rnd = new Random();
            int w = 90;
            actor pnn = new actor();
            pnn.x = rnd.Next(0,this.Width-9);
            pnn.y = -200;
            pnn.w = w;
            pnn.h = w - 5;
            pnn.n = rnd.Next(1, 15);
            int n = rnd.Next(colors.Count - 1);
            pnn.clr = Color.FromArgb(colors[n].r, colors[n].g, colors[n].b);
            soloblocks.Add(pnn);
        }
        void sheild()
        {
            if(fs ==1)
            {

                Bitmap img = new Bitmap("security.bmp");
                Color c = img.GetPixel(0, 0);
                img.MakeTransparent(c);
                shield.img = img;
                shield.x = hero[0].x +hero[0].w/2;
                shield.y = hero[0].y + hero[0].h / 2;

            }
        }
        void genrateblockpostion()
        {
            Random r;
            r = new Random();
             n = r.Next(bpostionts.Count);
            
        }
        private void T_Tick(object sender, EventArgs e)
        {
            Random r;

            if (start == false)
            {
                tt++;
                if(tt>5)
                {
                    tt = 0;
                }
            }

            if (start == true && game)
            {
                if (time %2==0)
                {
                    creatpoints();
                }
                else
                {
                    if (blocktime == bpostionts[n])
                    {

                        genrateblockpostion();
                        blocktime = 0;
                        cb = 1;

                        creatblocks();
                        fblock = 1;

                    }

                    if (time %7  == 0)
                    {
                        //creatsoloblocks();
                    }

                    

                }
                if(cb == 1)
                {
                    blocktime++;
                }
                if(mv)
                {
                    move();
                    moveall();

                }

                if(hitg)
                {
                    //for (int i = 0; i < hero.Count; i++)
                    //{
                    //    hero[i].y += 10;
                    //    hero[i].py += 10;
                    //}
                    hitt++;
                    mv = false;
                }
                if (hitt==2)
                {
                   
                    mv = true;
                   
                    hitt = 0;
                    hitg = false;
                }
               

                if (game)
                {
                    ishitblock();
                    ishitpoint();
                    touchbom();
                    touchshield();
                    sheild();
                    touchmagntic();

                }
                if(fs==1)
                {
                    st++;
                }
                if(st==100)
                {
                    fs = 0;
                    st = 0;
                }

                if(fm==1)
                {
                    fmt++;
                }
                if(fmt==100)
                {
                    fm = 0;
                    fmt = 0;
                }

                //if (pt%20==0 && pt!=0)
                //{
                //    vx += 2;
                //}
                

                time++;
                if (time % 100 == 0)
                {
                    if (fc % 2 == 0)

                        createbomb();
                    else
                        if (fc % 3 == 0)
                        createmgntc();
                    else
                        creatsheild();

                    fc++;
                }
                if(b==1)
                {
                    bt++;
                }
                if(bt==2)
                {
                    bt = 0;
                    b = 0;
                }
            }



            drawdub(this.CreateGraphics());
        }
        void drawsence(Graphics g2)
        {
            int x = 5;
            int f = 0;

          
            if (start == false)
            {
                g2.Clear(Color.Black);

                //g2.DrawString("Snake", new Font("console", 40), Brushes.YellowGreen, 160, 80);
                //g2.DrawString("VS", new Font("console", 30), Brushes.White, 220, 150);
                //g2.DrawString("Blocks", new Font("console", 40), Brushes.YellowGreen, 160, 220);
                g2.DrawImage(img2, 0, 0);
                if(tt>0)
                {
                    g2.DrawString("Press Any Key To start", new Font("console", 10), Brushes.White, 175, 520);

                }
            }
            else
            {
                g2.Clear(Color.Black);
               g2.DrawImage(img, 0, 0);

                for (int i = 0; i < points.Count; i++)
                {
                    SolidBrush b = new SolidBrush(Color.Yellow);
                    g2.FillEllipse(b, points[i].x, points[i].y, points[i].w, points[i].h);
                    g2.DrawString(points[i].n + "", new Font("console", 10), Brushes.White, points[i].x+x , points[i].y - 20);
                }
                if (hero.Count - 1 >= 0)
                {
                    if (hero[0].n > 9 && f == 0)
                    {
                        f = 1;
                        x -= 3;
                    }
                }
                if (fs == 1)
                {
                    if (hero.Count > 0)
                    {
                        g2.DrawImage(shield.img, hero[0].x-5 , hero[0].y - 11);

                    }
                }
                for (int i = 0; i < bombs.Count; i++)
                {
                    g2.DrawImage(bombs[i].img, bombs[i].x, bombs[i].y);
                }
                for (int i = 0; i < shields.Count; i++)
                {
                    g2.DrawImage(shields[i].img, shields[i].x, shields[i].y);
                }

                for (int i = 0; i < mgntcs.Count; i++)
                {
                    g2.DrawImage(mgntcs[i].img, mgntcs[i].x, mgntcs[i].y);
                }
                for (int i = 0; i < blocks.Count; i++)
                {
                    SolidBrush b = new SolidBrush(blocks[i].clr);

                    g2.FillRectangle(b, blocks[i].x, blocks[i].y, blocks[i].w, blocks[i].h);
                    g2.DrawString(blocks[i].n + "", new Font("console", 15), Brushes.White, blocks[i].x + 30, blocks[i].y + 30);

                }




                for (int i = 0; i < hero.Count; i++)
                {
                    SolidBrush b = new SolidBrush(hero[i].clr);
                    g2.FillEllipse(b, hero[i].x, hero[i].y, 20, 20);

                }
                if (hero.Count - 1 >= 0)
                {
                    g2.DrawString(hero[0].n + "", new Font("console", 10), Brushes.White, hero[0].x + x, hero[0].y - 20);
                }
                for (int i = 0; i < soloblocks.Count; i++)
                {
                    SolidBrush b = new SolidBrush(soloblocks[i].clr);

                    g2.FillRectangle(b, soloblocks[i].x, soloblocks[i].y, soloblocks[i].w, soloblocks[i].h);
                    g2.DrawString(soloblocks[i].n + "", new Font("console", 15), Brushes.White, soloblocks[i].x + 30,soloblocks[i].y + 30);

                }

                

                g2.DrawString("Points " +pt, new Font("console", 15), Brushes.White,200, 10);


                
                for (int i = 0; i < lines.Count; i++)
                {
                    SolidBrush b = new SolidBrush(Color.White);
                    g2.FillRectangle(b, lines[i].x, lines[i].y1, 5,lines[i].h);


                }
                if(b==1)
                {
                    SolidBrush b = new SolidBrush(Color.White);
                    g2.FillRectangle(b,0, 0, this.Width, this.Height);

                }
                if(fm==1)
                {
                    bomb.MakeTransparent();
                    g2.DrawImage(bomb, 300, 10);
                }
              



            }
            if (game == false)
            {
                gv.MakeTransparent();
                g2.DrawImage(gv, 0, 200);

            }
        }
        void colorpallete()
        {
            actor pn = new actor();
            pn.rgb(0, 177, 204);
            colors.Add(pn);

            pn = new actor();
            pn.rgb(0, 110, 127);
            colors.Add(pn);

            pn = new actor();
            pn.rgb(0, 221, 255);
            colors.Add(pn);

            pn = new actor();
            pn.rgb(76, 231, 255);
            colors.Add(pn);


            pn = new actor();
            pn.rgb(0, 110, 127);
            colors.Add(pn);


            pn = new actor();
            pn.rgb(217, 86, 0);
            colors.Add(pn);


            pn = new actor();
            pn.rgb(212, 103, 4);
            colors.Add(pn);

            pn = new actor();
            pn.rgb(217, 136, 9);
            colors.Add(pn);

            pn = new actor();
            pn.rgb(234, 181, 54);
            colors.Add(pn);


            pn = new actor();
            pn.rgb(232, 217, 136);
            colors.Add(pn);



            pn = new actor();
            pn.rgb(3, 166, 74);
            colors.Add(pn);


            pn = new actor();
            pn.rgb(1, 64, 41);
            colors.Add(pn);

            pn = new actor();
            pn.rgb(2, 115, 74);
            colors.Add(pn);


            pn = new actor();
            pn.rgb(3, 166, 106);
            colors.Add(pn);


            pn = new actor();
            pn.rgb(3, 166, 106);
            colors.Add(pn);
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            drawdub(e.Graphics);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(this.Width, this.Height);



            colorpallete();

            int y = 400;
            int j = 1;

            for (int i = 0; i < 5; i++)
            {
                actor pnn = new actor();

                pnn.x = this.Width / 2 - 20;
                pnn.y = y;
                pnn.clr = (Color.Yellow);
                pnn.m = -1 * i;
                pnn.py = y;
                pnn.px = j;
                j++;
                
                hero.Add(pnn);
                y += 21;

            }
            int m = 70;
            for (int i = 0; i < 4; i++)
            {
                bpostionts.Add(m);
                m += 20;
            }
            hero[0].n = 5;


           
        }




        void drawdub(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            drawsence(g2);
            g.DrawImage(off, 0, 0);
        }
    }
}
