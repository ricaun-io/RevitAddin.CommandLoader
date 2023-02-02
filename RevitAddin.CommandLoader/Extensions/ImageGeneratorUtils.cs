using System;

namespace RevitAddin.CommandLoader.Extensions
{
    public static class ImageGeneratorUtils
    {
        private static int imageNumber = 0;
        public static string GetLargeImageUri()
        {
            char GetLetter()
            {
                var num = imageNumber++ % 26;
                char let = (char)('a' + num);
                return let;
            }
            var baseImage = @"https://img.icons8.com/small/32/111111/circled-{0}.png";
            var imageLarge = string.Format(baseImage, GetLetter());
            return imageLarge;
        }
    }
}
