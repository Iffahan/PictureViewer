namespace PictureViewer
{
    public partial class PictureViewer : Form
    {
        int counter = 0;
        FolderBrowserDialog fbd = new FolderBrowserDialog();
        private OpenFileDialog openFileDialog1;
        string[] images;
        


        public PictureViewer()
        {
            InitializeComponent();
            NextButton.Hide();
            BackButton.Hide();
            textBox1.Hide();
            textBox3.Hide();
            editToolStripMenuItem.Visible = false;
            addToolStripMenuItem.Visible = false;
            deleteToolStripMenuItem.Visible = false;

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

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK)
                {
                    images = GetFiles(fbd.SelectedPath, "*.gif|*.jpg|*.png|*.bmp");
                    textBox1.Text = images[counter].ToString();

                    NextButton.Show();
                    BackButton.Show();
                    textBox1.Show();
                    textBox3.Show();
                    editToolStripMenuItem.Visible = true;
                    addToolStripMenuItem.Visible = true;
                    deleteToolStripMenuItem.Visible = true;

                    if (images.Length == 0)
                    {
                        MessageBox.Show("The are No Image in the Folder You Selected");
                    }
                    else
                    {
                        Image image = GetCopyImage(images[counter]);
                        pictureBox1.Image = image;
                        textBox1.Text = images[counter].ToString();
                        textBox3.Text = counter.ToString() + " / " + images.Length.ToString();
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("The are No Image in the Folder You Selected", ex.Message);
            }

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

            Image image = GetCopyImage(images[counter]);
            pictureBox1.Image = image;
            textBox1.Text = images[counter].ToString();
            textBox3.Text = counter.ToString() + " / " + images.Length.ToString();
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

            Image image = GetCopyImage(images[counter]);
            pictureBox1.Image = image;
            textBox1.Text = images[counter].ToString();
            textBox3.Text = counter.ToString() + " / " + images.Length.ToString();

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private Image GetCopyImage(string path)
        {
            using (Image image = Image.FromFile(path))
            {
                Bitmap bitmap = new Bitmap(image);
                return bitmap;
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1 = new OpenFileDialog();
                string Direct = fbd.SelectedPath.ToString();


                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    string source = openFileDialog1.FileName;
                    string Fname = Path.GetFileName(openFileDialog1.FileName);
                    string destpath = Direct + "\\" + Fname;
                    string sourcepath = openFileDialog1.FileName;
                    destpath = Path.Combine(sourcepath, destpath);
                    File.Copy(sourcepath, destpath, true);

                    DialogResult dialogResult = MessageBox.Show("You Need to reset to show new pictures.", "Added a photo to the album. Success.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        PictureViewer pictureViewer = new PictureViewer();
                        pictureViewer.Show();
                        this.Dispose(false);
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        //nothing
                    }

                }
            }
            catch (Exception)
            {
                MessageBox.Show("This Image is Already in The Album.");
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image image = GetCopyImage(images[counter]);
            pictureBox1.Image = image;

            DialogResult DeleteResult = MessageBox.Show("You want to delete the current picture? ", "Sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DeleteResult == DialogResult.Yes)
            {

                System.IO.File.Delete(images[counter]);
                DialogResult ResetDelete = MessageBox.Show("You Need to Reset After Delete", "Reset to See New Alblum");

                if (ResetDelete == DialogResult.OK)
                {
                    PictureViewer pictureViewer = new PictureViewer();
                    pictureViewer.Show();
                    this.Dispose(false);
                }
            }

            else if (DeleteResult == DialogResult.No)
            {
                PictureViewer pictureViewer = new PictureViewer();
                pictureViewer.Show();
                this.Dispose(false);
            }

            else
            {

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

    }
}