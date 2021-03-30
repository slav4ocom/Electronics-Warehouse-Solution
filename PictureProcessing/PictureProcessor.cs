using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;

namespace PictureProcessing
{
    public class PictureProcessor
    {
        public PictureProcessor()
        {

        }

        public static readonly string picturePathWeb = @"..\pictures\";
        public static readonly string picturePathConsole = @"..\..\..\..\pictures\";
        public static readonly string pictureAbsolutePath = @"C:/Users/svetoslav12345678198/source/repos/Electronics Warehouse/pictures/";
        //public static string picturePath { get; set; } = picturePathWeb;

        public void Download(string href)
        {
            var filename = href.Split("/").ToList().Last();
            using (var client = new WebClient())
            {
                client.DownloadFile(href, @$"{pictureAbsolutePath}{filename}");
            };
            Resize(@$"{pictureAbsolutePath}{filename}");

        }
        
        private void Resize(string inputPath)
        {
            const int size = 150;
            const int quality = 75;

            using (var rawData = Image.FromFile(inputPath))
            {
                //using (var image = new Bitmap(System.Drawing.Image.FromFile(inputPath)))
                using (var image = new Bitmap(rawData))
                {
                    int width, height;
                    if (image.Width > image.Height)
                    {
                        width = size;
                        height = Convert.ToInt32(image.Height * size / (double)image.Width);
                    }
                    else
                    {
                        width = Convert.ToInt32(image.Width * size / (double)image.Height);
                        height = size;
                    }
                    var resized = new Bitmap(width, height);
                    using (var graphics = Graphics.FromImage(resized))
                    {
                        graphics.CompositingQuality = CompositingQuality.HighSpeed;
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.CompositingMode = CompositingMode.SourceCopy;
                        graphics.DrawImage(image, 0, 0, width, height);

                        var filename = inputPath.
                            Split(new string[] { "/", "\\" }, StringSplitOptions.RemoveEmptyEntries)
                            .ToList()
                            .Last()
                            .Split(".")
                            .First();

                        using (var output = File.Open(@$"{pictureAbsolutePath}{filename}_small.jpg"
                            , FileMode.Create))
                        {
                            var qualityParamId = Encoder.Quality;
                            var encoderParameters = new EncoderParameters(1);
                            encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);
                            var codec = ImageCodecInfo.GetImageDecoders()
                                .FirstOrDefault(codec => codec.FormatID == ImageFormat.Jpeg.Guid);
                            resized.Save(output, System.Drawing.Imaging.ImageFormat.Jpeg);

                        }

                    }


                }

            }


        }

        public static string GetSmallName(string name)
        {
            if (name != null)
            {
                var nameParts = name.Split(".");
                return $"{nameParts[0]}_small.{nameParts[1]}";
            }
            else
            {
                return "";
            }
        }

        public static void DeletePicture(string filename)
        {
            File.Delete($"{pictureAbsolutePath}{filename}");
            File.Delete($"{pictureAbsolutePath}{GetSmallName(filename)}");
        }
    }
}
