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

        public static string picturePath { get; set; }

        public void Download()
        {
            Console.WriteLine("Enter filename");
            var href = Console.ReadLine();
            var filename = href.Split("/").ToList().Last();
            new WebClient().DownloadFile(href, @$"C:\inetpub\Warehouse2\wwwroot\pics\{filename}");

            Resize(@$"C:\inetpub\Warehouse2\wwwroot\pics\{filename}");
        }

        private void Resize(string inputPath)
        {
            const int size = 150;
            const int quality = 75;

            using (var image = new Bitmap(System.Drawing.Image.FromFile(inputPath)))
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

                    using (var output = File.Open(@$"C:\inetpub\Warehouse2\wwwroot\pics\{filename}_small.jpg"
                        , FileMode.Create))
                    //OutputPath(path, outputDirectory, SystemDrawing), FileMode.Create))
                    {
                        var qualityParamId = Encoder.Quality;
                        var encoderParameters = new EncoderParameters(1);
                        encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);
                        var codec = ImageCodecInfo.GetImageDecoders()
                            //.FirstOrDefault(codec => codec.FormatID == ImageFormat.Jpeg.Guid);
                            .FirstOrDefault(codec => codec.FormatID == ImageFormat.Jpeg.Guid);
                        //resized.Save(output, codec, encoderParameters);
                        resized.Save(output, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                }
            }
        }
    }
}
