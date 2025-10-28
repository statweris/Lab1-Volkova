using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace VolkovaBuilder
{
    internal class ProgramVolkova
    {
        static void Main(string[] args)
        {
            string currentDirectory = AppContext.BaseDirectory;
            string inputFile = Path.Combine(currentDirectory, "article.txt");
            string outputFile = Path.Combine(currentDirectory, "article.json");

            var parser = new ArticleParserVolkova();
            var article = parser.ParseText(inputFile);
            article.IsHashValid = HashValidatorVolkova.Verify(article);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            string json = JsonSerializer.Serialize(article, options);
            File.WriteAllText(outputFile, json, Encoding.UTF8);

            Console.WriteLine("Файл статьи сохранён в формате JSON!");
            Console.WriteLine($"Путь: {outputFile}");
        }
    }

    public class ArticleVolkova
    {
        public string Title { get; set; }
        public List<string> Authors { get; set; }
        public string Text { get; set; }
        public string Hash { get; set; }
        public bool IsHashValid { get; set; }
    }

    public class ArticleBuilderVolkova
    {
        private ArticleVolkova Article { get; set; }

        public ArticleBuilderVolkova()
        {
            Article = new ArticleVolkova();
        }

        public ArticleBuilderVolkova SetTitle(string title)
        {
            Article.Title = title?.Trim();
            return this;
        }

        public ArticleBuilderVolkova SetAuthors(string authorsLine)
        {
            var authors = new List<string>();
            if (!string.IsNullOrEmpty(authorsLine))
            {
                foreach (var author in authorsLine.Split(','))
                {
                    authors.Add(author.Trim());
                }
            }
            Article.Authors = authors;
            return this;
        }

        public ArticleBuilderVolkova SetContent(string text)
        {
            Article.Text = text?.Trim();
            return this;
        }

        public ArticleBuilderVolkova SetHash(string hash)
        {
            Article.Hash = hash?.Trim();
            return this;
        }

        public ArticleVolkova Build()
        {
            return Article;
        }
    }

    public class ArticleParserVolkova
    {
        public ArticleVolkova ParseText(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Файл не найден: {filePath}");
            }

            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
            ArticleBuilderVolkova builder = new ArticleBuilderVolkova();

            if (lines.Length == 0)
            {
                throw new InvalidDataException("Файл пуст");
            }

            builder.SetTitle(lines[0]);

            if (lines.Length > 1)
            {
                builder.SetAuthors(lines[1]);
            }

            string text = string.Empty;
            for (int i = 2; i < lines.Length; i++)
            {
                if (i == lines.Length - 1)
                {
                    builder.SetHash(lines[i]);
                    builder.SetContent(text);
                }
                else
                {
                    text += lines[i] + "\n";
                }
            }

            if (lines.Length == 2)
            {
                builder.SetContent(string.Empty);
                builder.SetHash(string.Empty);
            }

            return builder.Build();
        }
    }

    public static class HashValidatorVolkova
    {
        public static bool Verify(ArticleVolkova article)
        {
            if (string.IsNullOrEmpty(article.Hash) || string.IsNullOrEmpty(article.Text))
            {
                return false;
            }

            using (var md5 = MD5.Create())
            {
                byte[] contentBytes = Encoding.UTF8.GetBytes(article.Text);
                byte[] hashBytes = md5.ComputeHash(contentBytes);
                string computedHash = BitConverter.ToString(hashBytes);
                return computedHash.Equals(article.Hash, StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}
