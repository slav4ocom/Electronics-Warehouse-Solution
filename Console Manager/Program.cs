using CommonModels;
using Microsoft.EntityFrameworkCore;
using PictureProcessing;
using System;

namespace Console_Manager
{
    class Program
    {
        static void Main(string[] args)
        {
            PictureProcessor.picturePath = PictureProcessor.picturePathConsole;
            Engine.Run();
        }
    }
}
