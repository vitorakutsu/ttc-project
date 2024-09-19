using System.Drawing.Imaging;

namespace ttc_project.methods
{
    class zhang_suen
    {
        public static void thinningImage_withoutDMA(Bitmap image, Bitmap convertedImage)
        {
            global_filters.convertToBlackAndWhite_withDMA(image, convertedImage);

            int width = convertedImage.Width;
            int height = convertedImage.Height;
            bool shouldStop = false;
            List<(int x, int y)> toBeDeleted = new List<(int x, int y)>();

            while (!shouldStop)
            {
                shouldStop = true;

                for (int x = 1; x < width - 1; x++)
                {
                    for (int y = 1; y < height - 1; y++)
                    {
                        bool firstRule = hasConnectivity(convertedImage, x, y);
                        bool secondRule = hasBlackNeighbours(convertedImage, x, y);
                        bool thirdRule = hasWhiteBackground(convertedImage, x, y, true, true);
                        bool fourthRule = hasWhiteBackground(convertedImage, x, y, true, false);

                        if (firstRule && secondRule && thirdRule && fourthRule)
                        {
                            toBeDeleted.Add((x, y));
                        }
                    }
                }

                if (toBeDeleted.Count > 0)
                {
                    foreach ((int x, int y) in toBeDeleted)
                    {
                        Color newcolor = Color.FromArgb(255, 255, 255);
                        convertedImage.SetPixel(x, y, newcolor);
                    }

                    toBeDeleted.Clear();
                }

                for (int x = 1; x < width - 1; x++)
                {
                    for (int y = 1; y < height - 1; y++)
                    {
                        bool firstRule = hasConnectivity(convertedImage, x, y);
                        bool secondRule = hasBlackNeighbours(convertedImage, x, y);
                        bool thirdRule = hasWhiteBackground(convertedImage, x, y, false, true);
                        bool fourthRule = hasWhiteBackground(convertedImage, x, y, false, false);

                        if (firstRule && secondRule && thirdRule && fourthRule)
                        {
                            toBeDeleted.Add((x, y));
                        }
                    }
                }

                if(toBeDeleted.Count > 0)
                {
                    foreach ((int x, int y) in toBeDeleted)
                    {
                        Color newcolor = Color.FromArgb(255, 255, 255);
                        convertedImage.SetPixel(x, y, newcolor);
                    }

                    toBeDeleted.Clear();
                    shouldStop = false;
                }
            }
        }

        //public static void thinningImage_withDMA(Bitmap image, Bitmap convertedImage)
        //{
        //    global_filters.convertToBlackAndWhite_withDMA(image, convertedImage);

        //    int width = convertedImage.Width;
        //    int height = convertedImage.Height;
        //    int pixelSize = 3;
        //    bool shouldStop = false;

        //    BitmapData bitmapImage = convertedImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

        //    int padding = bitmapImage.Stride - (width * pixelSize);

        //    unsafe
        //    {
        //        byte* target = (byte*)bitmapImage.Scan0.ToPointer();

        //        while (!shouldStop)
        //        {
        //            List<(int x, int y)> toBeDeleted = new List<(int x, int y)>();
        //            bool firstIteration = true, secondIteration = true;

        //            for (int y = 0; y < height; y++)
        //            {
        //                for (int x = 0; x < width; x++)
        //                {
        //                    byte* pixel = target + (y * bitmapImage.Stride) + (x * pixelSize);

        //                    bool firstRule = withDMA.hasConnectivity_firstRule(target, x, y, width, height, pixelSize, bitmapImage.Stride);
        //                    bool secondRule = withDMA.hasBlackNeighbours_secondRule(target, x, y, width, height, pixelSize, bitmapImage.Stride);
        //                    bool thirdRule = withDMA.hasWhiteBackground_thirdRule_firstSubiteration(target, x, y, width, height, pixelSize);
        //                    bool fourthRule = withDMA.hasWhiteBackground_fourthRule_firstSubiteration(target, x, y, width, height, pixelSize);

        //                    if (firstRule && secondRule && thirdRule && fourthRule)
        //                    {
        //                        toBeDeleted.Add((x, y));
        //                    }
        //                }
        //            }

        //            if (toBeDeleted.Count == 0)
        //            {
        //                firstIteration = false;
        //            }

        //            foreach ((int x, int y) in toBeDeleted)
        //            {
        //                byte* pixel = target + (y * bitmapImage.Stride) + (x * pixelSize);
        //                pixel[0] = 255;
        //                pixel[1] = 255;
        //                pixel[2] = 255;
        //            }

        //            toBeDeleted.Clear();

        //            for (int y = 0; y < height; y++)
        //            {
        //                for (int x = 0; x < width; x++)
        //                {
        //                    byte* pixel = target + (y * bitmapImage.Stride) + (x * pixelSize);

        //                    bool firstRule = withDMA.hasConnectivity_firstRule(target, x, y, width, height, pixelSize, bitmapImage.Stride);
        //                    bool secondRule = withDMA.hasBlackNeighbours_secondRule(target, x, y, width, height, pixelSize, bitmapImage.Stride);
        //                    bool thirdRule = withDMA.hasWhiteBackground_thirdRule_secondSubiteration(target, x, y, width, height, pixelSize);
        //                    bool fourthRule = withDMA.hasWhiteBackground_fourthRule_secondSubiteration(target, x, y, width, height, pixelSize);

        //                    if (firstRule && secondRule && thirdRule && fourthRule)
        //                    {
        //                        toBeDeleted.Add((x, y));
        //                    }
        //                }
        //            }

        //            if (toBeDeleted.Count == 0)
        //            {
        //                secondIteration = false;
        //            }

        //            foreach ((int x, int y) in toBeDeleted)
        //            {
        //                byte* pixel = target + (y * bitmapImage.Stride) + (x * pixelSize);
        //                pixel[0] = 255;
        //                pixel[1] = 255;
        //                pixel[2] = 255;
        //            }

        //            toBeDeleted.Clear();

        //            if (!firstIteration && !secondIteration)
        //            {
        //                shouldStop = true;
        //            }
        //        }
        //    }

        //    convertedImage.UnlockBits(bitmapImage);
        //}

       
        private static Color[] getNeighbours(Bitmap image, int x, int y)
        {
            Color[] neighbours = new Color[8];

            neighbours[0] = image.GetPixel(x - 1, y - 1);
            neighbours[1] = image.GetPixel(x, y - 1);
            neighbours[2] = image.GetPixel(x + 1, y - 1);
            neighbours[3] = image.GetPixel(x + 1, y);
            neighbours[4] = image.GetPixel(x + 1, y + 1);
            neighbours[5] = image.GetPixel(x, y + 1);
            neighbours[6] = image.GetPixel(x - 1, y + 1);
            neighbours[7] = image.GetPixel(x - 1, y);

            return neighbours;
        }

        private static bool hasConnectivity(Bitmap image, int x, int y)
        {
            Color[] neighbours = getNeighbours(image, x, y);
            int count = 0;

            for (int i = 0; i < neighbours.Length - 1; i++) if (utils.withoutDMA.isWhite(neighbours[i]) && utils.withoutDMA.isBlack(neighbours[i + 1])) count++;
            if (utils.withoutDMA.isWhite(neighbours[neighbours.Length - 1]) && utils.withoutDMA.isBlack(neighbours[0])) count++;

            return count == 1;
        }

        private static bool hasBlackNeighbours(Bitmap image, int x, int y)
        {
            int count = 0;

            for (int i = y; i <= y; i++) for (int j = x - 1; j <= x + 1; j++) if (!(i == y && j == x) && utils.withoutDMA.isBlack(image.GetPixel(x, y))) count++;

            return count >= 2 && count <= 6;
        }

        private static bool hasWhiteBackground(Bitmap image, int x, int y, bool isFirstSubiteration, bool isThirdRule)
        {
            if (isFirstSubiteration) if (isThirdRule) return utils.withoutDMA.isWhite(image.GetPixel(x, y + 1)) || utils.withoutDMA.isWhite(image.GetPixel(x - 1, y)) || utils.withoutDMA.isWhite(image.GetPixel(x, y - 1));
                else return utils.withoutDMA.isWhite(image.GetPixel(x - 1, y)) || utils.withoutDMA.isWhite(image.GetPixel(x + 1, y)) || utils.withoutDMA.isWhite(image.GetPixel(x, y - 1));
            else if(isThirdRule) return utils.withoutDMA.isWhite(image.GetPixel(x - 1, y)) || utils.withoutDMA.isWhite(image.GetPixel(x, y + 1)) || utils.withoutDMA.isWhite(image.GetPixel(x + 1, y));
            else return utils.withoutDMA.isWhite(image.GetPixel(x, y + 1)) || utils.withoutDMA.isWhite(image.GetPixel(x + 1, y)) || utils.withoutDMA.isWhite(image.GetPixel(x, y - 1));
        }
    }
}