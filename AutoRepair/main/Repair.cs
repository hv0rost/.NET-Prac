using System;
using System.IO;
using System.Text;
using System.Xml;

namespace main
{
    public class Repair 
    {
        private string category;
        private string description;
        private DateTime endofrepair_plan;
        private DateTime startofrepair; //2019.3.22 
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

        public void RepairWriter()
        {
            StreamWriter st = new StreamWriter("info.txt", true, Encoding.GetEncoding(1251));

            st.WriteLine("Информация о починке");
            st.WriteLine($"Дата выдачи наряда: {startofrepair.ToString("yyyy.MM.dd")}");
            st.WriteLine($"Плановая дата окончания ремонта: {endofrepair_plan.ToString("yyyy.MM.dd")}");
            st.WriteLine($"Реальная дата окончания ремонта: {endofrepair_real.ToString("yyyy.MM.dd")}");
            st.WriteLine($"Количество часов потраченых на работу(по плану): {((endofrepair_plan.Subtract(startofrepair).Days)*8)}");
            st.WriteLine($"Количество часов потраченых на работу(реальных): {((endofrepair_real.Subtract(startofrepair).Days)*8)}");
            st.WriteLine($"Категория работ: {category}");
            st.WriteLine($"Описание работы: {description}");
            st.WriteLine("НДС: 20%");
            st.WriteLine();
            st.Close();
        }

        public Repair(){  }
        
        public void RepairFileReader(string st)
        {
            int i = 0;
            string[] text = File.ReadAllLines(st, Encoding.GetEncoding(1251));

            foreach (string str in text)
            {
                if (str.StartsWith("Дата выдачи наряда:"))
                {
                    i = 0;
                    while (true)
                    {
                        try
                        {
                            if (i == 0)
                                startofrepair = DateTime.Parse(str.Substring(str.IndexOf(':') + 2));
                            else startofrepair = DateTime.Parse(Console.ReadLine());
                                
                        }
                        catch (FormatException)
                        {
                            Console.Write("Дата выдачи наряда введена некорректно.  Введите заново: ");
                            i++;
                            continue;
                        }
                        break;
                    }
                }
                else if (str.StartsWith("Категория работ:"))
                {
                    while (true)
                    {
                        category = str.Substring(str.IndexOf(':') + 2);
                        if (!(category == "Капитальный ремонт" || category == "Средний ремонт" ||
                              category == "Текущий ремонт"))
                        {
                            Console.Write("Категория работ введена некорректно. Введите заново:");
                            category = Console.ReadLine();
                        }
                        else break;
                    }
                }
                else if (str.StartsWith("Описание работы:"))
                {
                    description = str.Substring(str.IndexOf(':') + 2);
                }
                else if (str.StartsWith("Плановая дата окончания ремонта:"))
                {
                    i = 0;
                    while (true)
                    {
                        try
                        {
                            if (i == 0)
                                endofrepair_plan = DateTime.Parse(str.Substring(str.IndexOf(':') + 2));
                            else endofrepair_plan = DateTime.Parse(Console.ReadLine());
                                
                        }
                        catch (FormatException)
                        {
                            Console.Write("Плановая дата окончания ремонта введена некорректно.  Введите заново: ");
                            i++;
                            continue;
                        }
                        break;
                    }
                }
            
                else if (str.StartsWith("Реальная дата окончания ремонта:"))
                {
                    i = 0;
                    while (true)
                    {
                        try
                        {
                            if (i == 0)
                                endofrepair_real = DateTime.Parse(str.Substring(str.IndexOf(':') + 2));
                            else endofrepair_real = DateTime.Parse(Console.ReadLine());
                                
                        }
                        catch (FormatException)
                        {
                            Console.Write("Реальная дата окончания ремонта введена некорректно.  Введите заново: ");
                            i++;
                            continue;
                        }
                        break;
                    }
                }
            }
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
    }
}
