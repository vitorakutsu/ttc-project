namespace ttc_project.methods
{
    class contour_extraction
    {
        public static void outlineExtractionImage_withoutDMA(Bitmap image, Bitmap convertedImage)
        {
            int width = image.Width;
            int height = image.Height;
            bool test = true;

            List<(int x, int y)> toBeMarked = new List<(int x, int y)>();

            for (int y = 0; y < height - 1 && test; y++)
            {
                for (int x = 0; x < width - 1 && test; x++)
                {
                    if (utils.isWhite(convertedImage.GetPixel(x, y)) && utils.isBlack(convertedImage.GetPixel(x + 1, y)))
                    {
                        Console.WriteLine("Encontrou um ponto");
                        bool stop = false;
                        int i = x, j = y;
                        int xMax = x, xMin = x, yMax = y, yMin = y;

                        bool isExtern = utils.isWhite(convertedImage.GetPixel(x, y - 1));

                        while (!stop)
                        {
                            Console.WriteLine(i + " " + j);
                            toBeMarked.Add((i, j));
                            (i, j) = getDirection(convertedImage, i, j, isExtern, toBeMarked);
                            if (i == -1 && j == -1) stop = true;
                        }

                        if (toBeMarked.Count > 0)
                        {
                            foreach ((int a, int b) in toBeMarked)
                            {
                                convertedImage.SetPixel(a, b, Color.Red);
                            }
                            toBeMarked.Clear();
                        }
                    }
                }
            }

            for(int y = 0; y < height - 1; y++)
            {
                for (int x = 0; x < width - 1; x++) 
                {
                    if (!utils.isRed(convertedImage.GetPixel(x, y))) convertedImage.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                }
            }

            for (int y = 0; y < height - 1; y++)
            {
                for (int x = 0; x < width - 1; x++)
                {
                    if (utils.isRed(convertedImage.GetPixel(x, y))) convertedImage.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                }
            }

        }

        private static Color[] getNeighbours(Bitmap image, int x, int y)
        {
            Color[] neighbours = new Color[8];

            int width = image.Width;
            int height = image.Height;

            if (x + 1 < width) neighbours[0] = image.GetPixel(x + 1, y);
            if (x + 1 < width && y - 1 >= 0) neighbours[1] = image.GetPixel(x + 1, y - 1);
            if (y - 1 >= 0) neighbours[2] = image.GetPixel(x, y - 1);
            if (x - 1 >= 0 && y - 1 >= 0) neighbours[3] = image.GetPixel(x - 1, y - 1);
            if (x - 1 >= 0) neighbours[4] = image.GetPixel(x - 1, y);
            if (x - 1 >= 0 && y + 1 < height) neighbours[5] = image.GetPixel(x - 1, y + 1);
            if (y + 1 < height) neighbours[6] = image.GetPixel(x, y + 1);
            if (x + 1 < width && y + 1 < height) neighbours[7] = image.GetPixel(x + 1, y + 1);

            return neighbours;
        }

        private static (int i, int j) getDirection(Bitmap image, int x, int y, bool isExtern, List<(int x, int y)> toBeMarked)
        {
            Color[] neighbours = getNeighbours(image, x, y);

            if (isExtern)
            {
                // Casos em que o index passa para a posicao 0
                if (utils.isWhite(neighbours[0]) && utils.isBlack(neighbours[1]))
                {
                    var candidate = (x + 1, y);
                    if (!toBeMarked.Contains(candidate))
                    {
                        return candidate;
                    }
                }

                // Casos em que o index passa para a posicao 1
                if (utils.isWhite(neighbours[1]) && utils.isBlack(neighbours[2]))
                {
                    if (utils.isWhite(neighbours[0]))
                    {
                        var candidate = (x + 1, y - 1);
                        if (!toBeMarked.Contains(candidate))
                        {
                            return candidate;
                        }
                    }
                }

                // Casos em que o index passa para a posicao 2
                if (utils.isWhite(neighbours[2]) && utils.isBlack(neighbours[3]))
                {
                    var candidate = (x, y - 1);
                    if (!toBeMarked.Contains(candidate))
                    {
                        return candidate;
                    }
                }

                // Casos em que o index passa para a posicao 3
                if (utils.isWhite(neighbours[3]) && utils.isBlack(neighbours[4]))
                {
                    if (utils.isWhite(neighbours[2]))
                    {
                        var candidate = (x - 1, y - 1);
                        if (!toBeMarked.Contains(candidate))
                        {
                            return candidate;
                        }
                    }
                }

                // Casos em que o index passa para a posicao 4
                if (utils.isWhite(neighbours[4]) && utils.isBlack(neighbours[5]))
                {
                    var candidate = (x - 1, y);
                    if (!toBeMarked.Contains(candidate))
                    {
                        return candidate;
                    }
                }

                // Casos em que o index passa para a posicao 5 
                if (utils.isWhite(neighbours[5]) && utils.isBlack(neighbours[6]))
                {
                    if (utils.isWhite(neighbours[4]))
                    {
                        var candidate = (x - 1, y + 1);
                        if (!toBeMarked.Contains(candidate))
                        {
                            return candidate;
                        }
                    }
                }

                // Casos em que o index passa para a posicao 6
                if (utils.isWhite(neighbours[6]) && utils.isBlack(neighbours[7]))
                {
                    var candidate = (x, y + 1);
                    if (!toBeMarked.Contains(candidate))
                    {
                        return candidate;
                    }
                }

                // Casos em que o index passa para a posicao 7
                if (utils.isWhite(neighbours[7]) && utils.isBlack(neighbours[0]))
                {
                    if (utils.isWhite(neighbours[6]))
                    {
                        var candidate = (x + 1, y + 1);
                        if (!toBeMarked.Contains(candidate))
                        {
                            return candidate;
                        }
                    }
                }
            }
            else
            {
                // Casos em que o index passa para a posicao 0
                if (utils.isWhite(neighbours[0]) && utils.isBlack(neighbours[7]))
                {
                    var candidate = (x + 1, y);
                    if (!toBeMarked.Contains(candidate))
                    {
                        return candidate;
                    }
                }

                // Casos em que o index passa para a posicao 1
                if (utils.isWhite(neighbours[1]) && utils.isBlack(neighbours[0]))
                {
                    if (utils.isWhite(neighbours[2]))
                    {
                        var candidate = (x + 1, y - 1);
                        if (!toBeMarked.Contains(candidate))
                        {
                            return candidate;
                        }
                    }
                }

                // Casos em que o index passa para a posicao 2
                if (utils.isWhite(neighbours[2]) && utils.isBlack(neighbours[1]))
                {
                    var candidate = (x, y - 1);
                    if (!toBeMarked.Contains(candidate))
                    {
                        return candidate;
                    }
                }

                // Casos em que o index passa para a posicao 3
                if (utils.isWhite(neighbours[3]) && utils.isBlack(neighbours[2]))
                {
                    if (utils.isWhite(neighbours[4]))
                    {
                        var candidate = (x - 1, y - 1);
                        if (!toBeMarked.Contains(candidate))
                        {
                            return candidate;
                        }
                    }
                }

                // Casos em que o index passa para a posicao 4
                if (utils.isWhite(neighbours[4]) && utils.isBlack(neighbours[3]))
                {
                    var candidate = (x - 1, y);
                    if (!toBeMarked.Contains(candidate))
                    {
                        return candidate;
                    }
                }

                // Casos em que o index passa para a posicao 5 
                if (utils.isWhite(neighbours[5]) && utils.isBlack(neighbours[4]))
                {
                    if (utils.isWhite(neighbours[6]))
                    {
                        var candidate = (x - 1, y + 1);
                        if (!toBeMarked.Contains(candidate))
                        {
                            return candidate;
                        }
                    }
                }

                // Casos em que o index passa para a posicao 6
                if (utils.isWhite(neighbours[6]) && utils.isBlack(neighbours[5]))
                {
                    var candidate = (x, y + 1);
                    if (!toBeMarked.Contains(candidate))
                    {
                        return candidate;
                    }
                }

                // Casos em que o index passa para a posicao 7
                if (utils.isWhite(neighbours[7]) && utils.isBlack(neighbours[6]))
                {
                    if (!utils.isBlack(neighbours[0]))
                    {
                        var candidate = (x + 1, y + 1);
                        if (!toBeMarked.Contains(candidate))
                        {
                            return candidate;
                        }
                    }
                }
            }


            return (x = -1, y = -1);
        }
    }
}
