namespace PictureViewer
{
    public partial class PictureViewer : Form
    {
        int counter = 0;
        FolderBrowserDialog fbd = new FolderBrowserDialog();
        private OpenFileDialog openFileDialog1;
        string[] images;
        FileInfo imageFileinfo;
        string path = @"E:\TESTP2\"; // this is the path that you are checking.



        public PictureViewer()
        {
            InitializeComponent();
            NextButton.Hide();
            BackButton.Hide();
            AddButton.Hide();
            DeleteButton.Hide();
            textBox1.Hide();
            textBox2.Hide();
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
            MessageBox.Show("Please Select an Album Folder First.");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = fbd.ShowDialog();

            images = GetFiles(fbd.SelectedPath, "*.gif|*.jpg|*.png|*.bmp");



            if (result == DialogResult.OK)
            {
                textBox1.Text = fbd.SelectedPath.ToString();
                textBox2.Text = "Number of Pictures in Alblum:  " + images.Length.ToString();

                NextButton.Show();
                BackButton.Show();
                AddButton.Show();
                DeleteButton.Show();
                textBox1.Show();
                textBox2.Show();

            }

            if (images.Length == 0)
            {
                MessageBox.Show("There Are No Images in The Folder You Selected.");
            }
            else
            {
                Image image = GetCopyImage(images[counter]);
                pictureBox1.Image = image;
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

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            Image image = GetCopyImage(images[counter]);
            pictureBox1.Image = image;
            pictureBox1.Dispose();
       
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

        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureViewer pictureViewer = new PictureViewer();
            pictureViewer.Hide();
            this.Dispose(false);
        }

        private Image GetCopyImage(string path)
        {
            using (Image image = Image.FromFile(path))
            {
                Bitmap bitmap = new Bitmap(image);
                return bitmap;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists(path))
            {
                OpenFileDialog ofd = new OpenFileDialog();  //make ofd local
                ofd.InitialDirectory = path;
                DialogResult dr = new DialogResult();
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Image img = new Bitmap(ofd.FileName);
                    imageFileinfo = new FileInfo(ofd.FileName);  // save the file name
                    string imgName = ofd.SafeFileName;
                    pictureBox1.Image = img.GetThumbnailImage(350, 350, null, new IntPtr());
                    ofd.RestoreDirectory = true;
                    img.Dispose();
                }
                ofd.Dispose();  //don't forget to dispose it!
            }
            else
            {
                return;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {

            if (Directory.Exists(path))
            {

                var directory = new DirectoryInfo(path);
                foreach (FileInfo file in directory.GetFiles())
                    if (!IsFileLocked(imageFileinfo))
                    {
                        File.Delete(@"E:\TESTP2\1.jpg");
                    }
            }
        }

        public static Boolean IsFileLocked(FileInfo path)
        {
            FileStream stream = null;
            try
            { //Don't change FileAccess to ReadWrite,
                //because if a file is in readOnly, it fails.
                stream = path.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            { //the file is unavailable because it is:
                //still being written to or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            //file is not locked
            return false;
        }
    }
}