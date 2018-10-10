using Axinom.LiveStreamValidation;
using System;
using System.IO;

namespace DevApp
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            Console.Write("Manifest URL: ");
            var urlString = Console.ReadLine();

            if (!Uri.TryCreate(urlString, UriKind.Absolute, out var url))
            {
                Console.WriteLine("Not a valid URL.");
                return -1;
            }

            var feedback = new ConsoleFeedback();
            LiveStream.Validate(url, feedback);

            if (feedback.ContentIsInvalid)
                return 1;
            else
                return 0;
        }

        private sealed class ConsoleFeedback : IFeedbackSink
        {
            public bool ContentIsInvalid { get; private set; }

            public void Info(string message)
            {
                Console.WriteLine();
                Console.WriteLine(message);
            }

            public void InvalidContent(string message)
            {
                ContentIsInvalid = true;

                Console.WriteLine();

                try
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Black;

                    Console.WriteLine("Error: " + message);
                }
                finally
                {
                    Console.ResetColor();
                }
            }

            public void WillSkipSomeData(string message)
            {
                Console.WriteLine();

                try
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.BackgroundColor = ConsoleColor.Black;

                    Console.WriteLine("Warning: " + message);
                }
                finally
                {
                    Console.ResetColor();
                }
            }

            public void DownloadedManifest(string contents)
            {
                File.WriteAllText("Manifest.mpd", contents);

                Console.WriteLine();
                Console.WriteLine("Downloaded manifest saved as Manifest.mpd.");
            }
        }
    }
}
