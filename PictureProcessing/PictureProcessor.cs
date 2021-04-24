using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PictureProcessing
{
    public class PictureProcessor
    {

        public PictureProcessor()
        {

        }

        public static void GetPaths()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .Build();

            
            pictureAbsolutePath = config["PictureAbsolutePath"];
            profileAbsolutePath = config["ProfileAbsolutePath"];
            homeworksPath = config["HomeworksPath"];

        }
        public static readonly string picturePathWeb = @"..\pictures";
        public static readonly string profilePathWeb = @"..\profiles";
        public static readonly string picturePathConsole = @"..\..\..\..\pictures\";
        public static string pictureAbsolutePath;
        public static string profileAbsolutePath;
        public static string homeworksPath;

        public static async Task SaveFileAsync(IFormFile inputFile, string fileName)
        {
            if (inputFile.Length > 0)
            {
                var fileNameWithPath = @$"{homeworksPath}{fileName}";

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    await inputFile.CopyToAsync(stream);
                }
            }
        }
        public void Download(string href)
        {
            //var filename = href.Split("/").ToList().Last();
            var filename = href.Split("/").ToList().Last().Replace("%", "_");
            using (var client = new WebClient())
            {
                client.DownloadFile(href, @$"{pictureAbsolutePath}{filename}");
            };
            Resize(@$"{pictureAbsolutePath}{filename}");

        }

        public static void Resize(string inputPath)
        {
            const int size = 150;
            const int quality = 75;

            var pathParts = inputPath.Split(".");
            var smallNamePath = $"{pathParts[0]}_small.{pathParts[1]}";

            using (var rawData = Image.FromFile(inputPath))
            {

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

                        using (var output = File.Open(@$"{inputPath.Split(".avatar").First()}_small.jpg"
                        //var result = inputPath.Split(".avatar").First();
                        //using (var output = File.Open(@$"{inputPath}_small.jpg"
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
                //return $"{nameParts[0]}_small.{nameParts[1]}";
                return $"{nameParts[0]}_small.jpg";
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
