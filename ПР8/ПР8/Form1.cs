using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ПР8
{
    public partial class Form1 : Form
    {
        bool drawing;
        int historyCounter; //Счетчик истории
        public GraphicsPath currentPath;
        public Point oldLocation;
        public Pen currentPen;
        private bool isEraserMode = false;
        public Color historyColor; // Глобальная переменная для сохранения исторического цвета пера 
        public List<Image> History; //Список для истории
       

        public Form1()
        {
            InitializeComponent();
            drawing = false; //Переменная, ответственная за рисование
            currentPen = new Pen(Color.Black); //Инициализация пера с черным цветом
            currentPen.Width = trackBar1.Value; //Инициализация толщины пера
            
            History = new List<Image>(); //Инициализация списка для истории
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Paint += new PaintEventHandler(pictureBox1_Paint);
            trackBar1.Value = 5; // Значение по умолчанию для толщины пера
            currentPen.Width = trackBar1.Value; // Инициализация толщины пера

            // Привязка события Scroll TrackBar к обработчику
            trackBar1.Scroll += new EventHandler(trackBar1_Scroll);
            pictureBox1.MouseMove += new MouseEventHandler(pictureBox1_MouseMove);
            pictureBox1.MouseDown += new MouseEventHandler(pictureBox1_MouseDown);
            pictureBox1.MouseUp += new MouseEventHandler(pictureBox1_MouseUp);
        }

        private void ОткрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
{
    if (currentPath != null)
    {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        e.Graphics.DrawPath(currentPen, currentPath);
    }
}
        private void карандашToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog dlg = new OpenFileDialog())
            {


                dlg.Title = "Open Image";
                dlg.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNG Image|*.png";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = new Bitmap(dlg.FileName);
                }
            }

            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Сначала создайте новый файл!");
                return;
            }
        }

    private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveDlg = new SaveFileDialog();

            SaveDlg.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNG Image|*.png";

            SaveDlg.Title = "Save an Image File";

            SaveDlg.FilterIndex = 4; //По умолчанию будет выбрано последнее расширение *.png
            SaveDlg.ShowDialog();

            if (SaveDlg.FileName != "") //Если введено не пустое имя
            {
                System.IO.FileStream fs =

                (System.IO.FileStream)SaveDlg.OpenFile();
                switch (SaveDlg.FilterIndex)
                {

                    case 1:

                        this.pictureBox1.Image.Save(fs, ImageFormat.Jpeg);

                        break;
                    case 2:

                        this.pictureBox1.Image.Save(fs, ImageFormat.Bmp);

                        break;
                    case 3:

                        this.pictureBox1.Image.Save(fs, ImageFormat.Gif);

                        break;
                    case 4:

                        this.pictureBox1.Image.Save(fs, ImageFormat.Png);

                        break;

                }
                fs.Close();

            }

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            currentPen.Width = trackBar1.Value;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void новыйФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            History.Clear();

            historyCounter = 0;

            Bitmap pic = new Bitmap(750, 500);

            pictureBox1.Image = pic;

            History.Add(new Bitmap(pictureBox1.Image));

            using (OpenFileDialog dlg = new OpenFileDialog())
            {

                    dlg.Title = "Open Image";
                    dlg.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNG Image|*.png";
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        pictureBox1.Image = new Bitmap(dlg.FileName);
                    }
            }

                if (pictureBox1.Image == null)
                {
                    MessageBox.Show("Сначала создайте новый файл!");
                    return;
                }
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveDlg = new SaveFileDialog();

            SaveDlg.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNG Image|*.png";

            SaveDlg.Title = "Save an Image File";

            SaveDlg.FilterIndex = 4; //По умолчанию будет выбрано последнее расширение *.png
            SaveDlg.ShowDialog();

            if (SaveDlg.FileName != "") //Если введено не пустое имя
            {
                System.IO.FileStream fs =

                (System.IO.FileStream)SaveDlg.OpenFile();
                switch (SaveDlg.FilterIndex)
                {

                    case 1:

                        this.pictureBox1.Image.Save(fs, ImageFormat.Jpeg);

                        break;
                    case 2:

                        this.pictureBox1.Image.Save(fs, ImageFormat.Bmp);

                        break;
                    case 3:

                        this.pictureBox1.Image.Save(fs, ImageFormat.Gif);

                        break;
                    case 4:

                        this.pictureBox1.Image.Save(fs, ImageFormat.Png);

                        break;

                }
                fs.Close();

            }



        }

        private void открытьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OP = new OpenFileDialog();

            OP.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNG Image|*.png";

            OP.Title = "Open an Image File";

            OP.FilterIndex = 1; //По умолчанию будет выбрано первое расширение *.jpg
            //И, когда пользователь укажет нужный путь к картинке, ее нужно будет загрузить в PictureBox:

            if (OP.ShowDialog() != DialogResult.Cancel)

                pictureBox1.Load(OP.FileName);
            pictureBox1.AutoSize = true;
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            OpenFileDialog OP = new OpenFileDialog();

            OP.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNG Image|*.png";

            OP.Title = "Open an Image File";

            OP.FilterIndex = 1; //По умолчанию будет выбрано первое расширение *.jpg
           
            if (OP.ShowDialog() != DialogResult.Cancel)

                pictureBox1.Load(OP.FileName);
            pictureBox1.AutoSize = true;
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)

        {
            if (pictureBox1.Image == null)
            {
             MessageBox.Show("Сначала создайте новый файл!");
             return;
            }
             if (e.Button == MouseButtons.Left)
             {
                drawing = true;
                oldLocation = e.Location;
                currentPath = new GraphicsPath();
             }

            if (e.Button == MouseButtons.Right)
            {
                isEraserMode = true;
                historyColor = currentPen.Color;
                currentPen.Color = Color.White;
            }

            drawing = true;
            currentPath = new GraphicsPath();
            currentPath.StartFigure();
            currentPath.AddLine(e.X, e.Y, e.X, e.Y);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {

            drawing = false;
            

            if (isEraserMode)
            {
                currentPen.Color = historyColor;
                isEraserMode = false;
            }

            SaveCurrentImage();
            pictureBox1.Invalidate();

            ////Очистка ненужной истории
            //History.RemoveRange(historyCounter + 1, History.Count - historyCounter - 1);
            //History.Add(new Bitmap(pictureBox1.Image));
            //if (historyCounter + 1 < 10) historyCounter++;
            //if (History.Count - 1 == 10) History.RemoveAt(0);
            //drawing = false;
            //try
            //{
            //currentPath.Dispose();
            //}
            //catch 
            //{
            //};
            //ластик
            if (e.Button == MouseButtons.Right)
            {
                currentPen.Color = historyColor; // Восстанавливаем исторический цвет пера
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Text = e.X.ToString() + ", " + e.Y.ToString(); // Вывод текущих координат мыши

            if (drawing)
            {
                currentPath.AddLine(e.X, e.Y, e.X, e.Y);
                pictureBox1.Invalidate();
            }
        }
        private void pictureBox1_Load(object sender, EventArgs e)
        {
            // Добавьте обработчики событий для мыши
            this.MouseDown += pictureBox1_MouseDown;
            this.MouseUp += pictureBox1_MouseUp;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    Color selectedColor = colorDialog.Color;
                    // Используйте выбранный цвет здесь
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            panel2.Controls.Add(pictureBox1);

        }

        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (History.Count != 0 && historyCounter != 0)
            {
                pictureBox1.Image = new Bitmap(History[--historyCounter]);

            }

            else MessageBox.Show("История пуста");
        }

        private void вернутьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (historyCounter < History.Count - 1)
            {
                pictureBox1.Image = new Bitmap(History[++historyCounter]);
            }
             else MessageBox.Show("История пуста");
        }

        private void solidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPen.DashStyle = DashStyle.Solid;
            solidToolStripMenuItem.Checked = true;

            dotToolStripMenuItem.Checked = false;

            dashDotDotToolStripMenuItem.Checked = false;
        }

        private void dotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPen.DashStyle = DashStyle.Dot;
            solidToolStripMenuItem.Checked = false;

            dotToolStripMenuItem.Checked = true;

            dashDotDotToolStripMenuItem.Checked = false;
        }

        private void dashDotDotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPen.DashStyle = DashStyle.DashDotDot;
            solidToolStripMenuItem.Checked = false;

            dotToolStripMenuItem.Checked = false;
            dashDotDotToolStripMenuItem.Checked = true;
        }

        private void цветToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    Color selectedColor = colorDialog.Color;
                    // Используйте выбранный цвет здесь
                }
            }
        }
        private void trackBarPen_Scroll(object sender, EventArgs e)
        {
            currentPen.Width = trackBar1.Value; // Обновление толщины пера
        }
        private void SaveCurrentImage()
        {
            Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(bitmap, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
            bitmap.Save("image.png"); // Замените "image.png" на путь и имя файла, куда нужно сохранить изображение
        }
    }
}
