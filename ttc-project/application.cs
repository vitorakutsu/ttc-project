using ttc_project.methods;

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
            zhang_suen.thinningImage_withoutDMA(imageBitmap, convertedImage);
            image = convertedImage;
            convertedImagePictureBox.Image = convertedImage;
        }

        private void actionToThinningWithDMA(object sender, EventArgs e)
        {
            Bitmap convertedImage = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            zhang_suen.thinningImage_withDMA(imageBitmap, convertedImage);
            image = convertedImage;
            convertedImagePictureBox.Image = convertedImage;
        }

        private void actionToOutlineExtractionWithoutDMA(object sender, EventArgs e)
        {
            Bitmap convertedImage = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            outline.outlineExtractionImage_withoutDMA(imageBitmap, convertedImage);
            convertedImagePictureBox.Image = convertedImage;
        }
    }
}
