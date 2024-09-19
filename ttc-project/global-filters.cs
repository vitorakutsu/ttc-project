using System.Drawing.Imaging;

namespace ttc_project
{
    class global_filters
    {
        public static void convertToBlackAndWhite_withDMA(Bitmap image, Bitmap convertedImage)
        {
            int width = image.Width;
            int height = image.Height;
            int pixelSize = 3;

            BitmapData bitmapImage = image.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData convertedBitmapImage = convertedImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int padding = bitmapImage.Stride - (width * pixelSize);
            Int32 gs;

            unsafe
            {
                byte* source = (byte*)bitmapImage.Scan0.ToPointer();
                byte* target = (byte*)convertedBitmapImage.Scan0.ToPointer();

                int r, g, b;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        b = *(source++);
                        g = *(source++);
                        r = *(source++);

                        gs = (Int32)(r * 0.2990 + g * 0.5870 + b * 0.1140);
                        byte bw = (byte)(gs > 200 ? 255 : 0);
                        *(target++) = bw;
                        *(target++) = bw;
                        *(target++) = bw;
                    }

                    source += padding;
                    target += padding;
                }
            }
            image.UnlockBits(bitmapImage);
            convertedImage.UnlockBits(convertedBitmapImage);
        }
    }
}
