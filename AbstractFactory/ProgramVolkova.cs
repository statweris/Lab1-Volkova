using System;
using System.Collections.Generic;

namespace VolkovaAbstractFactory
{
    internal class ProgramVolkova
    {
        abstract class AudioTrackVolkova
        {
            public abstract string GetAudioInfo();
            public abstract string GetQualityInfo();
        }

        abstract class SubtitlesVolkova
        {
            public abstract string GetSubtitleInfo();
            public abstract string GetSubtitleStyle();
        }

        class RussianAudioVolkova : AudioTrackVolkova
        {
            public override string GetAudioInfo() => "Аудиодорожка: Русский (Волкова студия)";
            public override string GetQualityInfo() => "Качество: 5.1 Surround Sound";
        }

        class RussianSubtitlesVolkova : SubtitlesVolkova
        {
            public override string GetSubtitleInfo() => "Субтитры: Русские";
            public override string GetSubtitleStyle() => "Стиль: Современный";
        }

        class EnglishAudioVolkova : AudioTrackVolkova
        {
            public override string GetAudioInfo() => "Audio Track: English (Volkova Studio)";
            public override string GetQualityInfo() => "Quality: Dolby Digital";
        }

        class EnglishSubtitlesVolkova : SubtitlesVolkova
        {
            public override string GetSubtitleInfo() => "Subtitles: English";
            public override string GetSubtitleStyle() => "Style: Classic";
        }

        class FrenchAudioVolkova : AudioTrackVolkova
        {
            public override string GetAudioInfo() => "Piste audio: Français (Studio Volkova)";
            public override string GetQualityInfo() => "Qualité: Stéréo";
        }

        class FrenchSubtitlesVolkova : SubtitlesVolkova
        {
            public override string GetSubtitleInfo() => "Sous-titres: Français";
            public override string GetSubtitleStyle() => "Style: Moderne";
        }

        abstract class MovieFactoryVolkova
        {
            public abstract AudioTrackVolkova CreateAudioTrack();
            public abstract SubtitlesVolkova CreateSubtitles();
            public abstract string GetFactoryInfo();
        }

        class RussianMovieFactoryVolkova : MovieFactoryVolkova
        {
            public override AudioTrackVolkova CreateAudioTrack() => new RussianAudioVolkova();
            public override SubtitlesVolkova CreateSubtitles() => new RussianSubtitlesVolkova();
            public override string GetFactoryInfo() => "Фабрика: Русская студия Волкова";
        }

        class EnglishMovieFactoryVolkova : MovieFactoryVolkova
        {
            public override AudioTrackVolkova CreateAudioTrack() => new EnglishAudioVolkova();
            public override SubtitlesVolkova CreateSubtitles() => new EnglishSubtitlesVolkova();
            public override string GetFactoryInfo() => "Factory: English Volkova Studio";
        }

        class FrenchMovieFactoryVolkova : MovieFactoryVolkova
        {
            public override AudioTrackVolkova CreateAudioTrack() => new FrenchAudioVolkova();
            public override SubtitlesVolkova CreateSubtitles() => new FrenchSubtitlesVolkova();
            public override string GetFactoryInfo() => "Usine: Studio Français Volkova";
        }

        class MovieVolkova
        {
            private AudioTrackVolkova audioTrackVolkova;
            private SubtitlesVolkova subtitlesVolkova;
            private MovieFactoryVolkova factoryVolkova;

            public MovieVolkova(MovieFactoryVolkova factory)
            {
                factoryVolkova = factory;
                audioTrackVolkova = factory.CreateAudioTrack();
                subtitlesVolkova = factory.CreateSubtitles();
            }

            public void ShowVolkovaInfo()
            {
                Console.WriteLine("\n=== Информация о фильме ===");
                Console.WriteLine(factoryVolkova.GetFactoryInfo());
                Console.WriteLine(audioTrackVolkova.GetAudioInfo());
                Console.WriteLine(audioTrackVolkova.GetQualityInfo());
                Console.WriteLine(subtitlesVolkova.GetSubtitleInfo());
                Console.WriteLine(subtitlesVolkova.GetSubtitleStyle());
                Console.WriteLine("====================================\n");
            }

            public void PlayMovieVolkova()
            {
                Console.WriteLine("Фильм воспроизводится...");
                Console.WriteLine(audioTrackVolkova.GetAudioInfo());
                Console.WriteLine(subtitlesVolkova.GetSubtitleInfo());
            }
        }

        class FilmDistributionManagerVolkova
        {
            private List<MovieVolkova> moviesVolkova = new List<MovieVolkova>();

            public void AddMovieVolkova(MovieVolkova movie)
            {
                moviesVolkova.Add(movie);
            }

            public void ShowAllMoviesVolkova()
            {
                Console.WriteLine("\n=== Все фильмы в распределении ===");
                for (int i = 0; i < moviesVolkova.Count; i++)
                {
                    Console.WriteLine($"Фильм #{i + 1}:");
                    moviesVolkova[i].ShowVolkovaInfo();
                }
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в систему Кинопрокат Волкова!");
            Console.WriteLine("Выберите язык: 1 - Русский, 2 - Английский, 3 - Французский");

            FilmDistributionManagerVolkova distributionManagerVolkova = new FilmDistributionManagerVolkova();

            while (true)
            {
                string choice = Console.ReadLine();
                MovieFactoryVolkova factoryVolkova;

                switch (choice)
                {
                    case "1":
                        factoryVolkova = new RussianMovieFactoryVolkova();
                        break;
                    case "2":
                        factoryVolkova = new EnglishMovieFactoryVolkova();
                        break;
                    case "3":
                        factoryVolkova = new FrenchMovieFactoryVolkova();
                        break;
                    case "0":
                        Console.WriteLine("\n=== Итоговый отчет ===");
                        distributionManagerVolkova.ShowAllMoviesVolkova();
                        return;
                    default:
                        Console.WriteLine("Неверный ввод! Доступные варианты: 1, 2, 3 (0 - завершить)");
                        continue;
                }

                MovieVolkova movieVolkova = new MovieVolkova(factoryVolkova);
                distributionManagerVolkova.AddMovieVolkova(movieVolkova);

                movieVolkova.ShowVolkovaInfo();
                movieVolkova.PlayMovieVolkova();

                Console.WriteLine("\nВыберите следующий язык (1-3) или 0 для завершения:");
            }
        }
    
    }
}
  
