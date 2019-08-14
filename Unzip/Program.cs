using System;
using System.IO;
using System.IO.Compression;

namespace Unzip
{
    public class Program
    {
        public static string directoryPath;
        public static void Main(string[] args)
        {


            directoryPath = @"C:\Users\JulioPC\Desktop\13Agosto\dataFiles\f92a043e-e462-475d-9738-6fbb1f86d8d1";

            DirectoryInfo directorySelected = new DirectoryInfo(directoryPath);
            foreach (FileInfo fileToDecompress in directorySelected.GetFiles("*.txt"))
            {
                Console.WriteLine(fileToDecompress);
            }

            foreach (FileInfo fileToDecompress in directorySelected.GetFiles("*.gz"))
            {
                Decompress(fileToDecompress);
            }
        }

        public static void Decompress(FileInfo fileToDecompress)
        {
            using (FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                string currentFileName = fileToDecompress.FullName;
                string nombre = currentFileName.Substring(directoryPath.Length);
                string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length) + ".txt";

                using (FileStream decompressedFileStream = File.Create(newFileName))
                {
                    using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                        Console.WriteLine("Decompressed: {0}", fileToDecompress.Name);
                    }
                }
            }
        }
    }
}
