using System.Drawing.Imaging;
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

        private void actionToSaveImage(object sender, EventArgs e)
        {
            if (convertedImagePictureBox.Image == null)
            {
                MessageBox.Show("Nenhuma imagem convertida disponível para salvar.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif";
                saveFileDialog.Title = "Save Image File";
                saveFileDialog.FileName = "converted_image";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileExtension = System.IO.Path.GetExtension(saveFileDialog.FileName).ToLower();

                    ImageFormat format = ImageFormat.Png;
                    if (fileExtension == ".jpg" || fileExtension == ".jpeg")
                    {
                        format = ImageFormat.Jpeg;
                    }
                    else if (fileExtension == ".bmp")
                    {
                        format = ImageFormat.Bmp;
                    }
                    else if (fileExtension == ".gif")
                    {
                        format = ImageFormat.Gif;
                    }

                    convertedImagePictureBox.Image.Save(saveFileDialog.FileName, format);
                    MessageBox.Show("Imagem salva com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void actionToThinningWithoutDMA(object sender, EventArgs e)
        {
            Bitmap convertedImage = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            thinning.thinningImage_withoutDMA(imageBitmap, convertedImage);
            image = convertedImage;
            convertedImagePictureBox.Image = convertedImage;
        }

        private void actionToMinRectangleWithoutDMA(object sender, EventArgs e)
        {
            Bitmap convertedImage = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            minimun_rectangle.minRectangleImage_withoutDMA(imageBitmap, convertedImage);
            convertedImagePictureBox.Image = convertedImage;
        }

        private void actionToThinningWithDMA(object sender, EventArgs e)
        {
            Bitmap convertedImage = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            image = convertedImage;
            convertedImagePictureBox.Image = convertedImage;
        }

        private void actionToOutlineExtractionWithoutDMA(object sender, EventArgs e)
        {
            Bitmap convertedImage = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            contour_extraction.outlineExtractionImage_withoutDMA(imageBitmap, convertedImage);
            convertedImagePictureBox.Image = convertedImage;
        }
    }
}
