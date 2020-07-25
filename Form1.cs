using linkedLists;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{    
    public partial class Form1 : Form
    {
        Bitmap canvas;
        Graphics g;
        CircularLinkedList<Snake> snake;
        Food food;
        Snake snakebody;
        public int x;
        public int y;
        public int score;
        public bool restart;
        public int previousX;
        public int previousY;
        public bool finished;
        public Random rand = new Random();

        public enum direction {up = 1, down = 2, left = 3, right = 4};
        public direction currentDirection = direction.right;
        public direction previousDirection;

        public Form1()
        {
            finished = false;
            snake = new CircularLinkedList<Snake>();
            InitializeComponent();
            canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(canvas);
            Snake snakehead;
            snakehead = new Snake(0, 0, 30, 30, 30, 4);
            snake.AddNodeToFront(snakehead);
            x = rand.Next(0, canvas.Width - 30);
            y = rand.Next(0, canvas.Height - 30);
            if (x % 30 > 0)
            {
                while (x % 30 > 0)
                {
                    x = rand.Next(30, canvas.Width - 30);
                }
            }
            if (y % 30 > 0)
            {
                while (y % 30 > 0)
                {
                    y = rand.Next(30, canvas.Height - 30);
                }
            }
            food = new Food(x, y, 30, 30);
            score = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (snake.Head.data.HitBox.IntersectsWith(food.HitBox))
            {   
                x = rand.Next(30, canvas.Width - 30);
                y = rand.Next(30, canvas.Height - 30);
                if (x % 30 > 0)
                {
                    while (x % 30 > 0)
                    {
                        x = rand.Next(30, canvas.Width - 30);
                    }
                }
                if (y % 30 > 0)
                {
                    while (y % 30 > 0)
                    {
                        y = rand.Next(30, canvas.Height - 30);
                    }
                }
                food.X = x;
                food.Y = y;
                Snake Tail = snake.Tail.data;
                if (currentDirection == direction.left)
                {
                    snakebody = new Snake(Tail.X + Tail.W, Tail.Y, 30, 30, 10, 3);                    
                }
                else if (currentDirection == direction.right)
                {
                    snakebody = new Snake(Tail.X - Tail.W, Tail.Y, 30, 30, 10, 4);
                }
                else if (currentDirection == direction.up)
                {
                    snakebody = new Snake(Tail.X, Tail.Y - Tail.W, 30, 30, 10, 1);
                }
                else if (currentDirection == direction.down)
                {
                    snakebody = new Snake(Tail.X, Tail.Y + Tail.W, 30, 30, 10, 2);
                }                
                snake.AddNodeToLast(snakebody);
                score += 10;
                label1.Text = $"Score: {score}";
            }
            g.FillRectangle(Brushes.DimGray, 0, 0, canvas.Width, canvas.Height);
            pictureBox1.Image = canvas;
            CircularLinkedListNode<Snake> runner = snake.Head;
            for (int i = 0; i < snake.Count; i++)
            {
                runner.data.Draw(g);
                runner = runner.next;
            }          
            food.Draw(g);
            if (snake.Head.data.X < 0 || snake.Head.data.Y < 0 || snake.Head.data.X + snake.Head.data.W > canvas.Width || snake.Head.data.Y + snake.Head.data.H > canvas.Height)
            {
                timer1.Enabled = false;
                restart = true;
                score = 0;
                MessageBox.Show("Game Over.");
                MessageBox.Show("Press any button to restart.");               
            }
            else
            {
                runner = snake.Head;
                if (currentDirection == direction.left)
                {
                    snake.Head.data.X -= snake.Head.data.S;
                }
                if (currentDirection == direction.right)
                {
                    snake.Head.data.X += snake.Head.data.S;
                }
                if (currentDirection == direction.up)
                {
                    snake.Head.data.Y -= snake.Head.data.S;
                }
                if (currentDirection == direction.down)
                {
                    snake.Head.data.Y += snake.Head.data.S;
                }               
                for (int i = snake.Count - 1; i > 0; i--)
                {                   
                    if (i == 1)
                    {
                        runner.data.X = previousX;
                        runner.data.Y = previousY;
                    }
                    if (snake.Count > 1)
                    {
                        runner.data.X = runner.prev.data.X;
                        runner.data.Y = runner.prev.data.Y;
                    }       
                    runner = runner.next;             
                    if (snake.Head.data.HitBox.IntersectsWith(runner.data.HitBox) && runner.data != snake.Head.next.data)
                    {
                        timer1.Enabled = false;
                        restart = true;
                        MessageBox.Show("Game Over.");
                        MessageBox.Show("Press any button to restart.");                        
                    }                    
                }
                previousX = snake.Head.data.X;
                previousY = snake.Head.data.Y;
            }
        }

        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (restart == true)
            {                
                restart = false;
                snake.Clear();
                Snake snakehead;
                snakehead = new Snake(0, 0, 30, 30, 30, 4);
                snake.AddNodeToFront(snakehead);
                snake.Head.data.X = 0;
                snake.Head.data.Y = 0;
                score = 0;
                label1.Text = "Score: 0";
                currentDirection = direction.right;
                CircularLinkedListNode<Snake> runner1 = snake.Head;
                for (int i = snake.Count - 1; i > 0; i--)
                {
                    snake.Remove(runner1.data);
                    runner1 = runner1.next;
                }
                timer1.Enabled = true;
            }
            if (e.KeyCode == Keys.Left && currentDirection != direction.right)
            {
                currentDirection = direction.left;
            }
            if (e.KeyCode == Keys.Right && currentDirection != direction.left)
            {
                currentDirection = direction.right;
            }
            if (e.KeyCode == Keys.Up && currentDirection != direction.down)
            {
                currentDirection = direction.up;
            }
            if (e.KeyCode == Keys.Down && currentDirection != direction.up)
            {
                currentDirection = direction.down;
            }
            CircularLinkedListNode<Snake> runner = snake.Head;
            for (int i = snake.Count-1; i > 0; i--)
            {
                runner.data.Direction = runner.prev.data.Direction;
                runner = runner.next;
            }
        }
    }
}
