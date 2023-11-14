using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace KrutoiProvodnik
{
    internal class StrMenu
    {
        private int minStrelochka;
        private int maxStrelochka;

        public StrMenu(int min, int max)
        {
            minStrelochka = min;
            maxStrelochka = max;
        }

        public static int Show(int minstrelochka, int maxstrelochka)
        {
            int pos = 0;
            ConsoleKeyInfo key;
            do
            {
                Console.SetCursorPosition(0, pos);
                Console.WriteLine("->");


                key = Console.ReadKey();

                Console.SetCursorPosition(0, pos);
                Console.WriteLine("  ");

                if (key.Key == ConsoleKey.UpArrow)
                {
                    pos--;
                    if (pos == -1)
                    {
                        pos = maxstrelochka - 1;
                    }

                }

                if (key.Key == ConsoleKey.DownArrow)
                {
                    pos++;
                    if (pos == maxstrelochka)
                    {
                        pos = minstrelochka;
                    }
                }

                if (key.Key == ConsoleKey.Escape)
                {
                    pos = -2;
                    return pos;
                }

            } while (key.Key != ConsoleKey.Enter);
            return pos;
            static void showMenu()
            {
                while (true)
                {
                    if (Console.ReadKey().Key == ConsoleKey.R)
                    {
                        {
                            Console.Clear();
                            Console.WriteLine("Меню:");
                            Console.WriteLine("1 - Добавить файл");
                            Console.WriteLine("2 - Добавить директорию");
                            Console.WriteLine("3 - Удалить файл");
                            Console.WriteLine("4 - Удалить директорию");
                            Console.WriteLine("R - Закрыть меню");

                            ConsoleKeyInfo choice = Console.ReadKey();
                            Console.Clear();

                            switch (choice.KeyChar)
                            {
                                case '1':
                                    Console.WriteLine("Введите путь к директории:");
                                    string path1 = Console.ReadLine();
                                    AddFile(path1);
                                    break;
                                case '2':
                                    Console.WriteLine("Введите путь к директории:");
                                    string path2 = Console.ReadLine();
                                    AddDirectory(path2);
                                    break;
                                case '3':
                                    Console.WriteLine("Введите путь к файлу:");
                                    string path3 = Console.ReadLine();
                                    DeleteFile(path3);
                                    break;
                                case '4':
                                    Console.WriteLine("Введите путь к директории:");
                                    string path4 = Console.ReadLine();
                                    Directory.Delete(path4);
                                    Console.WriteLine("Директория успешно удалена.");
                                    Console.ReadKey();
                                    break;
                                case 'R':
                                    return;
                                default:
                                    Console.WriteLine("Некорректный выбор. Попробуйте еще раз.");
                                    Console.ReadKey();
                                    break;
                            }
                        }
                    }
                    static void AddFile(string path)
                    {
                        Console.WriteLine("Введите имя нового файла: ");
                        string fileName = Console.ReadLine();
                        string filePath = Path.Combine(path, fileName);

                        try
                        {
                            FileStream fs = File.Create(filePath);
                            fs.Close();
                            Console.WriteLine($"Файл {fileName} успешно создан.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка при создании файла: {ex.Message}");
                        }

                        Console.ReadKey();
                    }
                    static void DeleteFile(string filePath)
                    {
                        try
                        {
                            File.Delete(filePath);
                            Console.WriteLine("Файл успешно удален.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка при удалении файла: {ex.Message}");
                        }

                        Console.ReadKey();
                    }
                    static void DeleteDirectory(string path)
                    {
                        if (Directory.Exists(path))
                        {
                            Directory.Delete(path, true);
                            Console.WriteLine("Директория успешно удалена");
                        }
                        else
                        {
                            Console.WriteLine("Директория не существует");
                        }
                    }
                    static void AddDirectory(string path)
                    {
                        Console.WriteLine("Введите имя новой директории: ");
                        string directoryName = Console.ReadLine();
                        string directoryPath = Path.Combine(path, directoryName);

                        try
                        {
                            Directory.CreateDirectory(directoryPath);
                            Console.WriteLine($"Директория {directoryName} успешно создана.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка при создании директории: {ex.Message}");
                        }

                        Console.ReadKey();
                    }

                }
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            ExploreDrive(DriveInfo.GetDrives());//отображаем диски :3
        }


        static void ExploreDrive(DriveInfo[] drives)
        {
            int selectedDriveIndex = 0;
            while (true)
            {
                Console.WriteLine("Выберите диск пж");
                Console.WriteLine("Меню доп.функций вызывается на R");
                Console.WriteLine("Но я в этом не уверен)");
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                Console.Clear();
                int pos = StrMenu.Show(0, allDrives.Length);
                if (pos == -1)
                {
                    return;
                }
                else
                {
                    showpipka(allDrives[pos].RootDirectory.FullName);//отображаем все папки в дисках и их содержимое
                }

            }
        }

        static void showpipka(string p)
        {

            while (true)
            {
                Console.Clear();
                string[] paths = Directory.GetDirectories(p);
                string[] filepaths = Directory.GetFiles(p);
                string[] combined = paths.Concat(filepaths).ToArray();
                foreach (string i in combined)
                {
                    string rashir = Path.GetExtension(i);
                    Console.Write("   " + i);
                    Console.SetCursorPosition(30, Console.CursorTop);
                    Console.Write("     " + "      " );
                    Console.SetCursorPosition(70, Console.CursorTop);
                    Console.WriteLine("        " + "       " + rashir);
                }

                int pos = StrMenu.Show(0, combined.Length);
                if (pos == -2)
                {
                    return;
                }
                else
                {

                    try
                    {
                        showpipka(combined[pos]);
                    }
                    catch (IOException)
                    {
                        Process.Start(new ProcessStartInfo { FileName = combined[pos], UseShellExecute = true });
                    }

                }


            }
        }
    }

}

