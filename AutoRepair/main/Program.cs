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
            

            Console.Write("Выберите способ работы \n 1 - Ввести информацию из файла. \n 2 - Ввести иинформацию в ручную. \n Ввод: ");
            int sw = int.Parse(Console.ReadLine());

            AutoOwner owner = new AutoOwner();
            Repair repair = new Repair();
            Car car = new Car();
            Mechanic mechanic = new Mechanic();
            Logger log =  new Logger(Console.Out);
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
                    
                    LoadManager loader = new LoadManager(str);
                    LoadLogger a = new LoadLogger(loader, log);

                    loader.BeginRead();
                    mechanic = loader.Read(new Mechanic.Loader()) as Mechanic;
                    owner = loader.Read(new AutoOwner.Loader()) as AutoOwner;
                    car = loader.Read(new Car.Loader()) as Car;
                    repair = loader.Read(new Repair.Loader()) as Repair;

                    loader.EndRead();

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
                    Environment.Exit(0);
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