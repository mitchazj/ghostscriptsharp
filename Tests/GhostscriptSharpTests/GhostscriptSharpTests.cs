using System;
using GhostscriptSharp;
using System.IO;
using System.Collections.Generic;

namespace GhostscriptSharpTests
{
    public static class GhostscriptSharpTests
    {
        private static readonly string TEST_FILE_LOCATION      = "test.pdf";
        private static readonly string SINGLE_FILE_LOCATION    = "output.jpg";
        private static readonly string MULTIPLE_FILE_LOCATION  = "output%d.jpg";

        private static readonly int MULTIPLE_FILE_PAGE_COUNT = 10;

        public static void Main(string[] args) {
            GenerateSinglePageThumbnails();
        }

        public static void GenerateSinglePageThumbnail()
        {
            GhostscriptWrapper.GeneratePageThumb(TEST_FILE_LOCATION, SINGLE_FILE_LOCATION, 1, 100, 100);
        }

        public static void GenerateSinglePageThumbnails()
        {
            DirectoryInfo[] di_subs = (new DirectoryInfo("pdf")).GetDirectories();
            List<string> pdf_paths = new List<string>();
            foreach (DirectoryInfo di in di_subs) {
                FileInfo[] pdfs = di.GetFiles("*.pdf");
                foreach (FileInfo fi in pdfs)
                    pdf_paths.Add(fi.FullName);
            }

            foreach (string s in pdf_paths) {
                if (!File.Exists(s.Replace(".pdf", ".jpg")))
                    GhostscriptWrapper.GeneratePageThumb(s, s.Replace(".pdf", ".jpg"), 1, 100, 100);
            }
        }

        public static void Cleanup()
        {
            File.Delete(SINGLE_FILE_LOCATION);
            for (var i = 1; i <= MULTIPLE_FILE_PAGE_COUNT; i++)
                File.Delete(String.Format("output{0}.jpg", i));
        }
    }
}
