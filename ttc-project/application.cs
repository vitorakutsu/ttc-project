namespace ttc_project
{
    public partial class Application : Form
    {
        private Image image;
        private Bitmap imageBitmap;

        public Application()
        {
            InitializeComponent();
        }

        private void actionToOpenImage(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                image = Image.FromFile(openFileDialog.FileName);
                imageBitmap = new Bitmap(openFileDialog.FileName);
                baseImagePictureBox.SizeMode = PictureBoxSizeMode.Normal;
                baseImagePictureBox.Image = image;
            }
        }

        private void actionToClearImage(object sender, EventArgs e)
        {
            baseImagePictureBox.Image = convertedImagePictureBox.Image = null;
        }

        private void actionToThinningWithoutDMA(object sender, EventArgs e)
        {
            Bitmap convertedImage = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            filters.thinningImage_withoutDMA(imageBitmap, convertedImage);
            image = convertedImage;
            convertedImagePictureBox.Image = convertedImage;
        }

        private void actionToOutlineExtractionWithoutDMA(object sender, EventArgs e)
        {
            Bitmap convertedImage = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            filters.outlineExtractionImage_withoutDMA(imageBitmap, convertedImage);
            convertedImagePictureBox.Image = convertedImage;
        }
    }
}
