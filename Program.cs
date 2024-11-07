using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: DataIntegrationApp <inputFile> <mappingFile> <outputFile>");
            return;
        }

        string inputFile = args[0];
        string mappingFile = args[1];
        string outputFile = args[2];

        try
        {
            // Чтение маппинга
            var mapping = ReadMapping(mappingFile);

            // Обработка данных и запись в выходной файл
            using (var reader = new StreamReader(inputFile))
            using (var writer = new StreamWriter(outputFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var transformedData = TransformData(line, mapping);
                    if (transformedData != null)
                    {
                        writer.WriteLine(transformedData);
                    }
                }
            }

            Console.WriteLine("Данные успешно обработаны и записаны в выходной файл.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }

    static Dictionary<string, string> ReadMapping(string mappingFile)
    {
        var mapping = new Dictionary<string, string>();

        // Чтение файла маппинга
        if (File.Exists(mappingFile))
        {
            using (var reader = new StreamReader(mappingFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(new[] { '=' }, 2);
                    if (parts.Length == 2)
                    {
                        mapping[parts[0].Trim()] = parts[1].Trim();
                    }
                }
            }
        }

        return mapping;
    }

    static string TransformData(string inputLine, Dictionary<string, string> mapping)
    {
        // Преобразование данных на основе маппинга
        foreach (var kvp in mapping)
        {
            inputLine = inputLine.Replace(kvp.Key, kvp.Value);
        }

        return inputLine; // Вернуть преобразованную строку
    }
}
