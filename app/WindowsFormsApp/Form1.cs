using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ttt = "交貨資料" == "交貨資料";



            IDataObject data = Clipboard.GetDataObject();
            var tt=data.GetFormats();

            if (data.GetDataPresent(DataFormats.Text))
            {
                var t = data.GetData(DataFormats.Text).ToString();
            }
            //this.checkBox1.Checked = IsBig5Encoding(textBox1.Text);
        }
        //偵測byte[]是否為BIG5編碼
        public static bool IsBig5Encoding(string text)
        {
            Encoding big5 = Encoding.GetEncoding(950);
            byte[] bytes = big5.GetBytes(text);
            //將byte[]轉為string再轉回byte[]看位元數是否有變
            return bytes.Length ==
                big5.GetByteCount(big5.GetString(bytes));
        }
    }
}
