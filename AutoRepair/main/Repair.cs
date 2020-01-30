using System;
using System.IO;
using System.Text;
using System.Xml;

namespace main
{
    class Repair : IWritableObject, IReadableObject
    {
        private string category;
        private string description;
        private DateTime endofrepair_plan;
        private DateTime startofrepair; 
        private DateTime endofrepair_real;
        private double cost_det;
        private double cost;
        private double NDS = 1.2;

        public double CostOfRepair(int tarif)
        {
            
            if (category == "Капитальный ремонт")
            {
                cost_det = 125000;
            }
            else if (category == "Средний ремонт")
            {
                cost_det = 55000;
            }
            else if (category == "Текущий ремонт")
            {
                cost_det = 25000;
            }

            if (endofrepair_real.Subtract(startofrepair).Days < 0)
            {
                Console.WriteLine("Дата окончания должна начинаться позже даты начала.");   
                while (true)
                {
                    try
                    { 
                        endofrepair_real = DateTime.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.Write("Реальная дата окончания ремонта введена некорректно.  Введите заново: ");
                        continue;
                    }
                    break;
                }
            }
            
            int workH;
            workH = (endofrepair_real.Subtract(startofrepair).Days)*8;
            cost = (cost_det + workH * tarif) * NDS;
            return cost;
        }

        public void getRepairConsoleInfo()
        {
            Console.WriteLine("Информация о починке");
            Console.WriteLine($"Дата выдачи наряда: {startofrepair.ToString("yyyy.MM.dd")}");
            Console.WriteLine($"Плановая дата окончания ремонта: {endofrepair_plan.ToString("yyyy.MM.dd")}");
            Console.WriteLine($"Реальная дата окончания ремонта: {endofrepair_real.ToString("yyyy.MM.dd")}");
            Console.WriteLine($"Количество часов потраченых на работу(по плану): {((endofrepair_plan.Subtract(startofrepair).Days)*8)}");
            Console.WriteLine($"Количество часов потраченых на работу(реальных): {((endofrepair_real.Subtract(startofrepair).Days)*8)}");
            Console.WriteLine($"Категория работ: {category}");
            Console.WriteLine($"Описание работы: {description}");
            Console.WriteLine("НДС: 20%");
            Console.WriteLine();
        }
        
        public void RepairConsoleReader()
        {
            Console.Write("Введите дату выдачи наряда: ");
            while (true)
            {
                try
                { 
                    startofrepair = DateTime.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.Write("Дата выдачи наряда введена некорректно.  Введите заново: ");
                    continue;
                }
                break;
            }
            
            Console.Write("Введите категорию(капитальный ремонт, средний ремонт, текущий ремонт): ");
            category = Console.ReadLine();
            while (true)
            {
                if (!(category == "капитальный ремонт" || category == "средний ремонт" ||
                      category == "текущий ремонт"))
                {
                    Console.Write("Категория работ введена некорректно. Введите заново:");
                    category = Console.ReadLine();
                }
                else break;
            }
            
            Console.Write("Введите описание работы: ");
            description = Console.ReadLine();
            
            Console.Write("Введите плановую дату окончания ремонта: ");
            while (true)
            {
                try
                {
                    endofrepair_plan = DateTime.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.Write("Плановая дата окончания ремонта введена некорректно.  Введите заново: ");
                    continue;
                }
                break;
            }
            
            Console.Write("Введите реальную дату окончания ремонта: ");
            while (true)
            {
                try
                { 
                    endofrepair_real = DateTime.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.Write("Реальная дата окончания ремонта введена некорректно.  Введите заново: ");
                    continue;
                }
                break;
            }
        }

        public void Write(SaveManager info)
        {
            info.WriteInfo("Информация о починке");
            info.WriteInfo($"Дата выдачи наряда: {startofrepair.ToString("yyyy.MM.dd")}");
            info.WriteInfo($"Плановая дата окончания ремонта: {endofrepair_plan.ToString("yyyy.MM.dd")}");
            info.WriteInfo($"Реальная дата окончания ремонта: {endofrepair_real.ToString("yyyy.MM.dd")}");
            info.WriteInfo($"Количество часов потраченых на работу(по плану): {((endofrepair_plan.Subtract(startofrepair).Days) * 8)}");
            info.WriteInfo($"Количество часов потраченых на работу(реальных): {((endofrepair_real.Subtract(startofrepair).Days) * 8)}");
            info.WriteInfo($"Категория работ: {category}");
            info.WriteInfo($"Описание работы: {description}");
            info.WriteInfo("НДС: 20%");
        }

        public Repair() { }
        public Repair(string category, string description, DateTime endofrepair_plan,DateTime startofrepair, DateTime endofrepair_real)
        {
            this.category = category;
            this.description = description;
            this.endofrepair_plan = endofrepair_plan;
            this.startofrepair = startofrepair;
            this.endofrepair_real = endofrepair_real;
        }

        public class Loader : ILoader
        {
            public IReadableObject Load(string[] text)
            {
                string category = null;
                string description = null;
                DateTime endofrepair_plan = new DateTime();
                DateTime startofrepair = new DateTime();
                DateTime endofrepair_real = new DateTime();

                foreach (string str in text)
                {
                    if (str.StartsWith("Дата выдачи наряда:"))
                    {
                        startofrepair = DateTime.Parse(str.Substring(str.IndexOf(':') + 2));
                    }
                    else if (str.StartsWith("Категория работ:"))
                    {
                         category = str.Substring(str.IndexOf(':') + 2);
                    }
                    else if (str.StartsWith("Описание работы:"))
                    {
                        description = str.Substring(str.IndexOf(':') + 2);
                    }
                    else if (str.StartsWith("Плановая дата окончания ремонта:"))
                    {
                         endofrepair_plan = DateTime.Parse(str.Substring(str.IndexOf(':') + 2));
                    }

                    else if (str.StartsWith("Реальная дата окончания ремонта:"))
                    {
                         endofrepair_real = DateTime.Parse(str.Substring(str.IndexOf(':') + 2));
                    }
                }
                return new Repair(category, description, endofrepair_plan, startofrepair, endofrepair_real);
            }
        }

    }
}
