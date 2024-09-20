namespace ttc_project
{
    class utils
    {
        public static bool isBlack(Color color)
        {
            return color.A == 255 && color.R == 0 && color.G == 0 && color.B == 0;
        }

        public static bool isWhite(Color color)
        {
            return color.A == 255 && color.R == 255 && color.G == 255 && color.B == 255;
        }

        public static bool isBlue(Color color)
        {
            return color.A == 255 && color.R == 0 && color.G == 0 && color.B == 255;
        }

        public static bool isRed(Color color)
        {
            return color.A == 255 && color.R == 255 && color.G == 0 && color.B == 0;
        }

        public class withDMA
        {
            public static unsafe bool isBlack(byte* pixel)
            {
                return pixel[0] == 0 && pixel[1] == 0 && pixel[2] == 0;
            }


            public static unsafe bool isWhite(byte* pixel)
            {
                return pixel[0] == 255 && pixel[1] == 255 && pixel[2] == 255;
            }

            public static unsafe bool isRed(byte* pixel)
            {
                return pixel[0] == 0 && pixel[1] == 0 && pixel[2] == 255;
            }
        }
    }
}
