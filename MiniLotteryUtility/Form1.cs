using System.Data;
using System.Diagnostics;

namespace MiniLotteryUtility
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.mgpTables = (new MgpTable[] {
                    new MgpTable() { idx =  6, mgp = 10000},
                    new MgpTable() { idx =  7, mgp =    36},
                    new MgpTable() { idx =  8, mgp =   720},
                    new MgpTable() { idx =  9, mgp =   360},
                    new MgpTable() { idx = 10, mgp =    80},
                    new MgpTable() { idx = 11, mgp =   252},
                    new MgpTable() { idx = 12, mgp =   108},
                    new MgpTable() { idx = 13, mgp =    72},
                    new MgpTable() { idx = 14, mgp =    54},
                    new MgpTable() { idx = 15, mgp =   180},
                    new MgpTable() { idx = 16, mgp =    72},
                    new MgpTable() { idx = 17, mgp =   180},
                    new MgpTable() { idx = 18, mgp =   119},
                    new MgpTable() { idx = 19, mgp =    36},
                    new MgpTable() { idx = 20, mgp =   306},
                    new MgpTable() { idx = 21, mgp =  1080},
                    new MgpTable() { idx = 22, mgp =   144},
                    new MgpTable() { idx = 23, mgp =  1800},
                    new MgpTable() { idx = 24, mgp =  3600}
                }).ToList();
            this.dataGridView1.DataSource = this.mgpTables;
        }

        private List<ComboBox> ComboBoxes
        {
            get
            {
                return (new ComboBox[] { Cmb11, Cmb12, Cmb13, Cmb21, Cmb22, Cmb23, Cmb31, Cmb32, Cmb33 }).ToList();
            }
        }

        public class MgpTable
        {
            public int idx { get; set; }
            public int mgp { get; set; }
        }

        private List<MgpTable> mgpTables;

        private List<string> SelectedData
        {
            get
            {
                List<string> ret = new();
                foreach (ComboBox comboBox in ComboBoxes)
                {
                    switch (comboBox.Text)
                    {
                        case "‚P":
                            ret.Add("1");
                            break;
                        case "‚Q":
                            ret.Add("2");
                            break;
                        case "‚R":
                            ret.Add("3");
                            break;
                        case "‚S":
                            ret.Add("4");
                            break;
                        case "‚T":
                            ret.Add("5");
                            break;
                        case "‚U":
                            ret.Add("6");
                            break;
                        case "‚V":
                            ret.Add("7");
                            break;
                        case "‚W":
                            ret.Add("8");
                            break;
                        case "‚X":
                            ret.Add("9");
                            break;
                        default:
                            ret.Add(null);
                            break;
                    }
                }
                return ret;
            }
        }

        private List<int[]> SelectableData
        {
            get
            {
                List<int[]> ret = new();
                string[] baseData = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                var baseData2 = SelectedData;
                int[] baseData3 = (from i in baseData where !baseData2.Contains(i) select int.Parse(i)).ToArray();
                foreach (string data in baseData2)
                {
                    if (data != null)
                    {
                        ret.Add(new int[] { int.Parse(data) });
                    }
                    else
                    {
                        ret.Add(baseData3);
                    }
                }
                return ret;
            }
        }

        private Dictionary<int, int> GetDirectory(int[] values, Dictionary<int, int> ret)
        {
            ret.Clear();
            foreach (int value in values)
            {
                ret.Add(value, value);
            }

            return ret;
        }

        private List<int[]> first(int[] value1)
        {
            List<int[]> ret = new();
            foreach (int value3 in value1)
            {
                ret.Add(new int[] { value3 });
            }
            return ret;
        }

        private List<int[]> collect(List<int[]> value1, int[] value2)
        {
            List<int[]> ret2 = new();
            Dictionary<int, int> map = new();
            foreach (int[] value3 in value1)
            {
                map = GetDirectory(value3, map);

                foreach (int value4 in value2)
                {
                    if (map.ContainsKey(value4))
                    {
                        continue;
                    }

                    List<int> ret = value3.ToList();
                    ret.Add(value4);
                    ret2.Add(ret.ToArray());
                }
            }
            return ret2;
        }

        private const int C11 = 0;
        private const int C12 = 1;
        private const int C13 = 2;
        private const int C21 = 3;
        private const int C22 = 4;
        private const int C23 = 5;
        private const int C31 = 6;
        private const int C32 = 7;
        private const int C33 = 8;

        private void Hoge()
        {
            List<int[]> data = this.SelectableData;
            if (data[0].Length == 9)
            {
                button1_Click(this, EventArgs.Empty);
                return;
            }
            List<int[]> ret = null;
            foreach (int[] value in data)
            {
                if (ret == null)
                {
                    ret = first(value);
                }
                else
                {
                    ret = collect(ret, value);
                }
            }

            Dictionary<int, int> MgpTableMap = new();
            foreach (MgpTable table in mgpTables)
            {
                MgpTableMap.Add(table.idx, table.mgp);
            }
            setValue(txt11, txt12, txt13, (from i in ret select MgpTableMap[i[C11] + i[C22] + i[C33]]));
            setValue(txt21, txt22, txt23, (from i in ret select MgpTableMap[i[C11] + i[C21] + i[C31]]));
            setValue(txt31, txt32, txt33, (from i in ret select MgpTableMap[i[C12] + i[C22] + i[C32]]));
            setValue(txt41, txt42, txt43, (from i in ret select MgpTableMap[i[C13] + i[C23] + i[C33]]));
            setValue(txt51, txt52, txt53, (from i in ret select MgpTableMap[i[C13] + i[C22] + i[C31]]));
            setValue(txt61, txt62, txt63, (from i in ret select MgpTableMap[i[C11] + i[C12] + i[C13]]));
            setValue(txt71, txt72, txt73, (from i in ret select MgpTableMap[i[C21] + i[C22] + i[C23]]));
            setValue(txt81, txt82, txt83, (from i in ret select MgpTableMap[i[C31] + i[C32] + i[C33]]));

            return;
        }

        private static void setValue(TextBox textBox1, TextBox textBox2, TextBox textBox3, IEnumerable<int> ret5)
        {
            if (ret5.Count() == 0)
            {
                textBox1.Text = "---";
                textBox2.Text = "---";
                textBox3.Text = "---";
            }
            else
            {
                textBox1.Text = ret5.Min().ToString();
                textBox2.Text = ((int)ret5.Average()).ToString();
                textBox3.Text = ret5.Max().ToString();

            }
        }

        private void Cmb11_SelectedValueChanged(object sender, EventArgs e)
        {
            if (inProcess) return;
            Stopwatch sw = new();
            sw.Start();
            this.Hoge();
            sw.Stop();
            label1.Text = $"{sw.ElapsedMilliseconds} ms";
        }

        bool inProcess = false;
        private void button1_Click(object sender, EventArgs e)
        {
            inProcess = true;
            Cmb11.Text = "";
            Cmb12.Text = "";
            Cmb13.Text = "";
            Cmb21.Text = "";
            Cmb22.Text = "";
            Cmb23.Text = "";
            Cmb31.Text = "";
            Cmb32.Text = "";
            Cmb33.Text = "";
            inProcess = false;
            txt11.Text = "";
            txt12.Text = "";
            txt13.Text = "";
            txt21.Text = "";
            txt22.Text = "";
            txt23.Text = "";
            txt31.Text = "";
            txt32.Text = "";
            txt33.Text = "";
            txt41.Text = "";
            txt42.Text = "";
            txt43.Text = "";
            txt51.Text = "";
            txt52.Text = "";
            txt53.Text = "";
            txt61.Text = "";
            txt62.Text = "";
            txt63.Text = "";
            txt71.Text = "";
            txt72.Text = "";
            txt73.Text = "";
            txt81.Text = "";
            txt82.Text = "";
            txt83.Text = "";
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (inProcess) return;
            Stopwatch sw = new();
            sw.Start();
            this.Hoge();
            sw.Stop();
            label1.Text = $"{sw.ElapsedMilliseconds} ms";
        }
    }
}