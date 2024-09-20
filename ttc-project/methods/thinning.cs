using System.Drawing.Imaging;

namespace ttc_project.methods
{
    class thinning
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
                        if (utils.isBlack(convertedImage.GetPixel(x, y)))
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
                       if(utils.isBlack(convertedImage.GetPixel(x, y)))
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
                }

                Console.WriteLine(toBeDeleted.Count);

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

            for (int i = 0; i < neighbours.Length - 1; i++) if (utils.isWhite(neighbours[i]) && utils.isBlack(neighbours[i + 1])) count++;
            if (utils.isWhite(neighbours[neighbours.Length - 1]) && utils.isBlack(neighbours[0])) count++;

            return count == 1;
        }

        private static bool hasBlackNeighbours(Bitmap image, int x, int y)
        {
            Color[] neighbours = getNeighbours(image, x, y);
            int count = 0;

            for (int i = 0; i < neighbours.Length - 1; i++) if (utils.isBlack(neighbours[i])) count++;

            return count >= 2 && count <= 6;
        }

        private static bool hasWhiteBackground(Bitmap image, int x, int y, bool isFirstSubiteration, bool isThirdRule)
        {
            if (isFirstSubiteration) if (isThirdRule) return utils.isWhite(image.GetPixel(x, y + 1)) || utils.isWhite(image.GetPixel(x - 1, y)) || utils.isWhite(image.GetPixel(x, y - 1));
                else return utils.isWhite(image.GetPixel(x - 1, y)) || utils.isWhite(image.GetPixel(x + 1, y)) || utils.isWhite(image.GetPixel(x, y - 1));
            else if(isThirdRule) return utils.isWhite(image.GetPixel(x - 1, y)) || utils.isWhite(image.GetPixel(x, y + 1)) || utils.isWhite(image.GetPixel(x + 1, y));
            else return utils.isWhite(image.GetPixel(x, y + 1)) || utils.isWhite(image.GetPixel(x + 1, y)) || utils.isWhite(image.GetPixel(x, y - 1));
        }
    }
}