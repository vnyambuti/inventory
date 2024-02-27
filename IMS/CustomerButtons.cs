using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMS
{
    public partial class CustomerButtons : PictureBox
    {
        public Image HoverImage;
        public Image OriginalImage;

        public CustomerButtons()
        {
            InitializeComponent();
        }

        public Image ImageOriginal
        {
            get { return OriginalImage; }
            set { OriginalImage = value; }
        }

        public Image ImageHover
        {
            get { return HoverImage; }
            set { HoverImage = value; }
        }

        private void CustomerButtons_MouseLeave(object sender, EventArgs e)
        {
            this.Image = OriginalImage;
            Console.WriteLine("mouse leave");
        }

        private void CustomerButtons_MouseHover(object sender, EventArgs e)
        {
            this.ImageHover = HoverImage;
            Console.WriteLine("Image hover");
        }
    }
}
