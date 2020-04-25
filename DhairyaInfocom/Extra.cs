using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DhairyaInfocom
{
    public partial class Extra : UserControl
    {
        public Extra()
        {
            InitializeComponent();
        }

     
        private void btnColor_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bt = new Bitmap(Properties.Resources.test_color);
            Image img_color = bt;
            e.Graphics.DrawImage(img_color,0,0, 874, 1200);
        }

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bt = new Bitmap(Properties.Resources.test_black);
            Image img_color = bt;
            e.Graphics.DrawImage(img_color, 0, 0, 874, 1200);
        }

        private void btnBlackWhite_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument2;
            printPreviewDialog1.ShowDialog();
        }
    }
}
