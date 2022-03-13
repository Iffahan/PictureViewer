namespace PictureViewer
{
    public partial class PictureViewer : Form
    {
        int counter = 1;
        FolderBrowserDialog fbd = new FolderBrowserDialog();
        private OpenFileDialog openFileDialog1;
        string[] images;

        public PictureViewer()
        {
            InitializeComponent();
        }

        private string[] GetFiles(string folderPath, String Searchpattern)
        {
            List<string> files = new List<String>();
            var patterns = Searchpattern.Split('|');

            foreach (var pattern in patterns)
            {
                string[] temps = Directory.GetFiles(folderPath, pattern);
                files.AddRange(temps);
            }

            return files.ToArray();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = fbd.ShowDialog();

            images = GetFiles(fbd.SelectedPath, "*.gif|*.jpg|*.png|*.bmp");
            System.Diagnostics.Debug.WriteLine(result.ToString());
            System.Diagnostics.Debug.WriteLine(images.Length);
            System.Diagnostics.Debug.WriteLine(fbd.SelectedPath.ToString());

            pictureBox1.Image = Image.FromFile(images[counter]);
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            counter++;
            if (counter > images.Length - 1)
            {
                counter = 0;
            }

            if (counter < 0)
            {
                counter = images.Length - 1;
            }

            pictureBox1.Image = Image.FromFile(images[counter]);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            counter--;
            if (counter < 0)
            {
                counter = images.Length - 1;
            }

            if (counter > images.Length - 1)
            {
                counter = 0;
            }


            pictureBox1.Image = Image.FromFile(images[counter]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog();
            string Direct = fbd.SelectedPath.ToString();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string source = openFileDialog1.FileName;
            }

            string Fname = Path.GetFileName(openFileDialog1.FileName);
            string destpath = Direct + "\\" + Fname;
            string sourcepath = openFileDialog1.FileName;
            destpath = Path.Combine(sourcepath, destpath);
            File.Copy(sourcepath, destpath, true);
            MessageBox.Show("Add The Picyure You Select To Alblum Folder");
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {

        }
    }
}