using System.Diagnostics;
namespace FinalProject
{
    public partial class 股票資訊 : Form
    {
        public 股票資訊()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox4.Items.Add("KD");
            comboBox4.Items.Add("General");
            String line;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("C:/FinalProject/sql/date.csv");
                //Read the first line of text
                line = sr.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    //Read the next line
                    line = sr.ReadLine();
                    comboBox3.Items.Add(line);
                    comboBox2.Items.Add(line);
                }
                //close the file
                sr.Close();
                Console.ReadLine();
            }
            catch (Exception)
            {
                Console.WriteLine("Can't found file!");
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }

            String Stockline;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("C:/FinalProject/sql/testdata2.csv");
                //Read the first line of text
                Stockline = sr.ReadLine();
                //Continue to read until you reach end of file
                while (Stockline != null)
                {
                    //Read the next line
                    Stockline = sr.ReadLine();
                    //var values = Stockline.Split(',');
                    comboBox1.Items.Add(Stockline);
                }
                //close the file
                sr.Close();
                Console.ReadLine();
            }
            catch (Exception)
            {
                Console.WriteLine("Can't found file!");
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Name = (comboBox1.SelectedItem.ToString());
            label1.Text = Name;

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            label5.Text = (comboBox3.SelectedItem.ToString());



        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            label7.Text = (comboBox2.SelectedItem.ToString());
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        public string ExecutePythonScript(string filePythonScript, string name, string datafname, string datalname, string mode, out string standardError)
        {
            string outputText = string.Empty;
            standardError = string.Empty;
            //label9.Text = "\"" + name + " " + datafname + " " + datalname + "\"";
            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo(@"C:/Users/victor/AppData/Local/Microsoft/WindowsApps/python.exe")
                    {
                        Arguments = (filePythonScript + " "+ name + " " + datalname + " " + datafname + " " + mode),
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    };
                    process.Start();
                    outputText = process.StandardOutput.ReadToEnd();
                    outputText = outputText.Replace(Environment.NewLine, string.Empty);
                    standardError = process.StandardError.ReadToEnd();
                    process.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                string exceptionMessage = ex.Message;
            }
            return outputText;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string err;
            string outResult;
            string name;
            string datafname;
            string datalname;
            string mode;
            bool uncorrect = true;
            //while(uncorrect)
            try
            {
                 name = comboBox1.SelectedItem.ToString();
                // datafname = comboBox2.SelectedItem.ToString();
                 //datalname = comboBox3.SelectedItem.ToString();
                 //mode = comboBox4.SelectedItem.ToString();
            }
            catch
            {
                MessageBox.Show("please selection the options!");
            }
            name = comboBox1.SelectedItem.ToString();
            datafname = comboBox2.SelectedItem.ToString();
            datalname = comboBox3.SelectedItem.ToString();
            mode = comboBox4.SelectedItem.ToString();
            outResult = ExecutePythonScript(@"C:/FinalProject/sql/back.py", name, datafname, datalname, mode, out err);

                label8.Text = outResult;
 
                label9.Text = err;

            string IMGpath = "C:/FinalProject/sql/" + outResult + ".png";


           pictureBox1.Load(IMGpath);
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            label10.Text = (comboBox4.SelectedItem.ToString());
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}