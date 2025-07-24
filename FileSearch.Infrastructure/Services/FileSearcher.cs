using FileSearch.Core.Models;

namespace FileSearch.Infrastructure.Services
{
    public class FileSearcher
    {
        public event EventHandler<FileArgs> FileFound;

        protected virtual void OnFileFound(FileArgs e)
        {
            FileFound?.Invoke(this, e);
        }

        public void SearchFiles(string directoryPath)
        {
            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    Console.WriteLine($"Каталог не найден: {directoryPath}");
                    return;
                }

                var files = Directory.GetFiles(directoryPath);
                foreach (var file in files)
                {
                    var args = new FileArgs(file);
                    OnFileFound(args);

                    if (args.Cancel)
                    {
                        Console.WriteLine("Поиск отменен пользователем");
                        return;
                    }
                }

                // Рекурсивный поиск в подкаталогах
                var directories = Directory.GetDirectories(directoryPath);
                foreach (var directory in directories)
                {
                    SearchFiles(directory);
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"Нет доступа к каталогу: {directoryPath}");
            }
            catch (PathTooLongException)
            {
                Console.WriteLine($"Слишком длинный путь: {directoryPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при поиске файлов в {directoryPath}: {ex.Message}");
            }
        }
    }
}
