using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;

namespace DataAnalysisLab2
{
    public partial class Form1 : Form
    {
        Dictionary<string, string> xPaths = new Dictionary<string, string>()
        {
            { "Получить все занятия на данной неделе","/Timetable/Day/Subject"},
            { "Получить все аудитории, в которых проходят занятия", "/Timetable/Day/Subject/Classroom"},
            { "Получить все практические занятия на неделе", "//Subject[@Type='Seminar']" },
            { "Получить все лекции, проводимые в указанной аудитории", "//Subject[@Type='Lecture'][Classroom='Дистанционно']"},
            { "Получить список всех преподавателей, проводящих практики в указанной аудитории", "distinct-values(//Subject[@Type='Seminar'][Classroom='Дистанционно']//Tutor)" },
            { "Получить последнее занятие для каждого дня делели", "//Day/Subject[last()]" },
            { "Получить общее количество занятий за всю неделю", "/Timetable/Day/Subject" }
        };
        XmlDocument doc;
        XmlElement xRoot;
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            if (comboBox1.Text != "Получить общее количество занятий за всю неделю")
            {
                XmlNodeList childnodes = xRoot.SelectNodes(xPaths[comboBox1.Text]);
                foreach (XmlNode n in childnodes)
                {
                    richTextBox1.Text = richTextBox1.Text + n.InnerText + "\n";
                }
            }
            else
            {
                XmlNodeList childnodes = xRoot.SelectNodes("/Timetable/Day/Subject");
                richTextBox1.Text = childnodes.Count.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            doc = new XmlDocument();
            doc.Load(textBox1.Text);
            xRoot = doc.DocumentElement;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(textBox2.Text);
            xslt.Transform(textBox1.Text, "result.html");
        }
    }
}
