using FileSearch.Core.Extensions;
using FileSearch.Core.Models;
using FileSearch.Infrastructure.Services;

class Program
{
    static void Main(string[] args)
    {
        DemonstrateGetMax();
        
        Console.WriteLine();

        DemonstrateFileSearch();

        Console.WriteLine("\nНажмите любую клавишу для завершения...");
        Console.ReadKey();
    }

    static void DemonstrateGetMax()
    {
        Console.WriteLine("1. Поиск максимального элемента в коллекции:");

        var numbers = new List<string> { "1", "42", "15", "8", "100" };
        try
        {
            string maxString = numbers.GetMax(s => float.Parse(s));
            Console.WriteLine($"Максимальное число в коллекции строк: {maxString}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }

        var products = new List<Product>
            {
                new Product("Товар 1", 150.5f),
                new Product("Товар 2", 300.0f),
                new Product("Товар 3", 75.25f),
                new Product("Товар 4", 500.75f)
            };

        try
        {
            Product maxProduct = products.GetMax(p => p.Price);
            Console.WriteLine($"Самый дорогой товар: {maxProduct.Name} - {maxProduct.Price} руб.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static void DemonstrateFileSearch()
    {
        Console.WriteLine("2. Поиск файлов с использованием событий:");

        var searcher = new FileSearcher();
        searcher.FileFound += OnFileFound;

        Console.WriteLine("Введите путь к каталогу для поиска файлов (или нажмите Enter для текущего каталога):");
        string path = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(path))
            path = Environment.CurrentDirectory;

        Console.WriteLine($"\nПоиск файлов в каталоге: {path}\n");
        searcher.SearchFiles(path);
    }

    // Обработчик события нахождения файла
    static void OnFileFound(object sender, FileArgs e)
    {
        Console.WriteLine($"Найден файл: {e.FileName}");

        // Пример отмены поиска при нахождении определенного файла (отмена происходит при нахождении файла с расширением .exe)
        if (e.FileName.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Найден .exe файл. Отмена дальнейшего поиска.");
            e.Cancel = true;
        }
    }
}