namespace ttc_project.methods
{
    class outline
    {
        public static void outlineExtractionImage_withoutDMA(Bitmap image, Bitmap convertedImage)
        {
            int width = image.Width;
            int height = image.Height;

            Queue<(int x, int y)> queue = new Queue<(int x, int y)>();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height - 1; y++)
                {
                    if (utils.withoutDMA.isWhite(image.GetPixel(x, y)) && utils.withoutDMA.isBlack(image.GetPixel(x, y + 1)) && !queue.Contains((x, y)))
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
            return (x, y);
        }
    }
}
