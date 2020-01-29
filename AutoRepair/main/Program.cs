using System;
using System.CodeDom;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml;


namespace main

{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Выберите способ работы \n 1 - Ввести информацию из файла. \n 2 - Ввести иинформацию в ручную. \n");
            int sw = int.Parse(Console.ReadLine());

            LoadManager loader = new LoadManager("2.txt");
            
            
            AutoOwner owner = new AutoOwner(loader);
            Car car = new Car(loader);
            Mechanic mechanic = new Mechanic(loader);
            Repair repair = new Repair(loader);
            
         
            string str;

            SaveManager info = new SaveManager("info.txt");

            switch (sw)
            {
                case 1:
                    Console.Write("Введите название файла для открытия: ");
                    str = Console.ReadLine();
                    str = str + ".txt";
            
                    while (true)
                    {
                        if (!File.Exists(str))
                        {
                            Console.Write("Такого файла не существует, введите другое название: ");
                            str = Console.ReadLine();
                            str = str + ".txt";
                        }
                        else break;
                    }
                    string text = File.ReadAllText(str, Encoding.GetEncoding(1251));
                    if (String.IsNullOrEmpty(text))
                    {
                        Console.WriteLine("Файл пуст.");
                        Environment.Exit(0);
                    }
                    break;
                case 2:
                    owner.AutoOwnerConsoleReader();
                    car.CarConsoleReader();
                    mechanic.MechanicConsoleReader();
                    repair.RepairConsoleReader();
                    break;
                default:
                    Console.WriteLine("Не верно введено значение");
                    Console.ReadKey();
                    break;
            }
            
            owner.getAutoOwnerConsoleInfo();
            car.getCarConsoleInfo();
            mechanic.getMechanicConsoleInfo();
            repair.getRepairConsoleInfo();
            Console.WriteLine($"Общая стоимость наряда на ремонт: {repair.CostOfRepair(mechanic.tarif())} рублей.");

            owner.Write(info);
            car.Write(info);
            mechanic.Write(info);
            repair.Write(info);
            
            StreamWriter s = new StreamWriter("info.txt", true, Encoding.GetEncoding(1251));
            s.WriteLine($"Общая стоимость наряда на ремонт: {repair.CostOfRepair(mechanic.tarif())} рублей.");
            s.Close();
            Console.ReadKey();
        }
    }
}