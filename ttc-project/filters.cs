using System.Drawing.Imaging;

namespace ttc_project
{
    class filters
    {
        private static bool isBlack(Color color)
        {
            return color.R == 0 && color.G == 0 && color.B == 0;
        }

        private static bool isWhite(Color color)
        {
            return color.R == 255 && color.G == 255 && color.B == 255;
        }

        private static bool isRed(Color color)
        {
            return color.R == 255 && color.G == 0 && color.B == 0;
        }

        private static void convertToBlackAndWhite_withDMA(Bitmap image, Bitmap convertedImage)
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

        public static void thinningImage_withoutDMA(Bitmap image, Bitmap convertedImage)
        {
            convertToBlackAndWhite_withDMA(image, convertedImage);

            int width = convertedImage.Width;
            int height = convertedImage.Height;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    bool firstRule = hasConnectivity_firstRule(convertedImage, x, y);
                    bool secondRule = hasBlackNeighbours_secondRule(convertedImage, x, y);
                    bool thirdRule = hasWhiteBackground_thirdRule_firstSubiteration(convertedImage, x, y);
                    bool fourthRule = hasWhiteBackground_fourthRule_firstSubiteration(convertedImage, x, y);

                    if (firstRule && secondRule && thirdRule && fourthRule)
                    {
                        Color newcolor = Color.FromArgb(255, 255, 255);
                        convertedImage.SetPixel(x, y, newcolor);
                    }
                }
            }

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    bool firstRule = hasConnectivity_firstRule(convertedImage, x, y);
                    bool secondRule = hasBlackNeighbours_secondRule(convertedImage, x, y);
                    bool thirdRule = hasWhiteBackground_thirdRule_secondSubiteration(convertedImage, x, y);
                    bool fourthRule = hasWhiteBackground_fourthRule_secondSubiteration(convertedImage, x, y);

                    if (firstRule && secondRule && thirdRule && fourthRule)
                    {
                        Color newcolor = Color.FromArgb(255, 255, 255);
                        convertedImage.SetPixel(x, y, newcolor);
                    }
                }
            }
        }

        private static bool hasConnectivity_firstRule(Bitmap image, int x, int y)
        {
            int count = 0;

            if (x - 1 >= 0 && y + 1 < image.Height && isWhite(image.GetPixel(x - 1, y)) && isBlack(image.GetPixel(x - 1, y + 1))) count++;
            if (x - 1 >= 0 && y + 1 < image.Height && isWhite(image.GetPixel(x - 1, y + 1)) && isBlack(image.GetPixel(x, y + 1))) count++;
            if (x + 1 < image.Width && y + 1 < image.Height && isWhite(image.GetPixel(x, y + 1)) && isBlack(image.GetPixel(x + 1, y + 1))) count++;
            if (x + 1 < image.Width && y + 1 < image.Height && isWhite(image.GetPixel(x + 1, y + 1)) && isBlack(image.GetPixel(x + 1, y))) count++;
            if (y - 1 >= 0 && x + 1 < image.Width && isWhite(image.GetPixel(x + 1, y)) && isBlack(image.GetPixel(x + 1, y - 1))) count++;
            if (y - 1 >= 0 && x + 1 < image.Width && isWhite(image.GetPixel(x + 1, y - 1)) && isBlack(image.GetPixel(x, y - 1))) count++;
            if (x - 1 >= 0 && y - 1 >= 0 && isWhite(image.GetPixel(x, y - 1)) && isBlack(image.GetPixel(x - 1, y - 1))) count++;

            return count == 1;
        }

        private static bool hasBlackNeighbours_secondRule(Bitmap image, int x, int y)
        {
            int count = 0;

            if (x - 1 >= 0 && isBlack(image.GetPixel(x - 1, y))) count++;
            if (x - 1 >= 0 && y - 1 >= 0 && isBlack(image.GetPixel(x - 1, y - 1))) count++;
            if (y - 1 >= 0 && isBlack(image.GetPixel(x, y - 1))) count++;
            if (y - 1 >= 0 && x + 1 < image.Width && isBlack(image.GetPixel(x + 1, y - 1))) count++;
            if (x + 1 < image.Width && isBlack(image.GetPixel(x + 1, y))) count++;
            if (x + 1 < image.Width && y + 1 < image.Height && isBlack(image.GetPixel(x + 1, y + 1))) count++;
            if (y + 1 < image.Height && isBlack(image.GetPixel(x, y + 1))) count++;
            if (x - 1 >= 0 && y + 1 < image.Height && isBlack(image.GetPixel(x - 1, y + 1))) count++;

            return count >= 2 && count <= 6;
        }

        private static bool hasWhiteBackground_thirdRule_firstSubiteration(Bitmap image, int x, int y)
        {
            // I(i, j + 1), I(i − 1, j) and I(i, j − 1)
            int count = 0;

            if (y + 1 < image.Height && isWhite(image.GetPixel(x, y + 1))) count++; // P4
            if (x - 1 >= 0 && isWhite(image.GetPixel(x - 1, y))) count++; // P2
            if (y - 1 >= 0 && isWhite(image.GetPixel(x, y - 1))) count++; // P6

            return count >= 1;
        }

        private static bool hasWhiteBackground_fourthRule_firstSubiteration(Bitmap image, int x, int y)
        {
            // I(i − 1, j), I(i + 1, j) and I(i, j − 1)
            int count = 0;

            if (x - 1 >= 0 && isWhite(image.GetPixel(x - 1, y))) count++; // P2
            if (x + 1 < image.Width && isWhite(image.GetPixel(x + 1, y))) count++; // P8
            if (y - 1 >= 0 && isWhite(image.GetPixel(x, y - 1))) count++; // P6

            return count >= 1;
        }

        private static bool hasWhiteBackground_thirdRule_secondSubiteration(Bitmap image, int x, int y)
        {
            // I(i − 1, j), I(i, j + 1) and I(i + 1, j)
            int count = 0;

            if (x - 1 >= 0 && isWhite(image.GetPixel(x - 1, y))) count++; // P2
            if (y + 1 < image.Height && isWhite(image.GetPixel(x, y + 1))) count++; // P4
            if (x + 1 < image.Width && isWhite(image.GetPixel(x + 1, y))) count++; // P8

            return count >= 1;
        }

        private static bool hasWhiteBackground_fourthRule_secondSubiteration(Bitmap image, int x, int y)
        {
            // I(i, j + 1), I(i + 1, j) and I(i, j − 1)
            int count = 0;

            if (y + 1 < image.Height && isWhite(image.GetPixel(x, y + 1))) count++; // P4
            if (x + 1 < image.Width && isWhite(image.GetPixel(x + 1, y))) count++; // P8
            if (y - 1 >= 0 && isWhite(image.GetPixel(x, y - 1))) count++; // P6

            return count >= 1;
        }

        public static void outlineExtractionImage_withoutDMA(Bitmap image, Bitmap convertedImage)
        {
            int width = image.Width;
            int height = image.Height;

            Queue<(int x, int y)> queue = new Queue<(int x, int y)>();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height - 1; y++)
                {
                    if (isWhite(image.GetPixel(x, y)) && isBlack(image.GetPixel(x, y + 1)) && !queue.Contains((x, y)))
                    {
                        bool stop = false;
                        int i = x, j = y;

                        while (!stop)
                        {
                            queue.Enqueue((i, j));

                            (i, j) = getDirection(image, i, j);

                            if (i == x && j == y) stop = true;
                        }
                    }
                }
            }

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    convertedImage.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                }
            }

            while (queue.Count > 0)
            {
                int x, y;
                (x, y) = queue.Dequeue();
                convertedImage.SetPixel(x, y, Color.FromArgb(0, 0, 0));
            }
        }

        private static (int i, int j) getDirection(Bitmap image, int x, int y)
        {
            if (y + 1 < image.Height && (isWhite(image.GetPixel(x, y + 1)) || isRed(image.GetPixel(x, y + 1))) && x - 1 >= 0 && isBlack(image.GetPixel(x - 1, y + 1)))
                return (x, y + 1);
            if (x - 1 >= 0 && y + 1 < image.Height && (isWhite(image.GetPixel(x - 1, y + 1)) || isRed(image.GetPixel(x - 1, y + 1))) && isBlack(image.GetPixel(x - 1, y)))
                return (x - 1, y + 1);
            if (x - 1 >= 0 && (isWhite(image.GetPixel(x - 1, y)) || isRed(image.GetPixel(x - 1, y))) && y - 1 >= 0 && isBlack(image.GetPixel(x - 1, y - 1)))
                return (x - 1, y);
            if (y - 1 >= 0 && x - 1 >= 0 && (isWhite(image.GetPixel(x - 1, y - 1)) || isRed(image.GetPixel(x - 1, y - 1))) && isBlack(image.GetPixel(x, y - 1)))
                return (x - 1, y - 1);
            if (y - 1 >= 0 && (isWhite(image.GetPixel(x, y - 1)) || isRed(image.GetPixel(x, y - 1))) && x + 1 < image.Width && isBlack(image.GetPixel(x + 1, y - 1)))
                return (x, y - 1);
            if (x + 1 < image.Width && y - 1 >= 0 && (isWhite(image.GetPixel(x + 1, y - 1)) || isRed(image.GetPixel(x + 1, y - 1))) && isBlack(image.GetPixel(x + 1, y)))
                return (x + 1, y - 1);
            if (x + 1 < image.Width && (isWhite(image.GetPixel(x + 1, y)) || isRed(image.GetPixel(x + 1, y))) && y + 1 < image.Height && isBlack(image.GetPixel(x + 1, y + 1)))
                return (x + 1, y);
            if (y + 1 < image.Height && x + 1 < image.Width && (isWhite(image.GetPixel(x + 1, y + 1)) || isRed(image.GetPixel(x + 1, y + 1))) && isBlack(image.GetPixel(x, y + 1)))
                return (x + 1, y + 1);

            return (x, y);
        }

    }
}
