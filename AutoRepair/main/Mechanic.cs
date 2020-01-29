using System;
using System.IO;
using System.Net.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace main
{
    class Mechanic : AutoOwner , IWritableObject, IReadbleObject 
    {
        protected int qualification;

        public void Write(SaveManager info)
        {
            info.WriteInfo("Информация о механике");
            info.WriteInfo($"ФИО механика: {FIO}");
            info.WriteInfo($"Квалификация (разряд) механика: {qualification}");
            info.WriteInfo($"Адрес механика: {adres}");
            info.WriteInfo($"Телефон механика: {phone_number}");
            info.WriteInfo("\n===================================================================\n");
        }
        public void MechanicConsoleReader()
        {
            bool flag;
            Console.Write("Введите имя механика: ");
            FIO = Console.ReadLine();
            while(true)
            {
                flag = true;
                foreach (char c in FIO)
                {
                    if (!char.IsLetter(c) && c != ' ')
                    {
                        Console.Write("Имя не может содержать символы и цифры. Введите заново: ");
                        FIO = Console.ReadLine();
                        flag = false;
                    }
                }
                if (flag) break;
            }
            
            Console.Write("Введите квалификацию механика(1-4): ");
            qualification = int.Parse(Console.ReadLine());
            while (true)
            {

                if (qualification < 1 || qualification > 6)
                {
                    Console.Write("Некорректно введен разряд механика. Правильный формат - число от 1 до 6. Введите заново: ");
                    qualification = int.Parse(Console.ReadLine());
                }
                else break;
            }
            Console.Write("Введите адрес механика: ");
            adres = Console.ReadLine();
            
            Console.Write("Введите номер телефона механика(11 цифр): ");
            phone_number = Console.ReadLine();
            int count;
            while (true)
            {
                count = 0;
                foreach (char c in phone_number)
                {
                    if (!char.IsDigit(c))
                    {
                        Console.Write("Некорректно введен номер телефона. Номер должен содержать только цифры. Введите заново: ");
                        phone_number = Console.ReadLine();
                    }
                    count++;
                }
                if (count != 11)
                {
                    Console.Write("Некорректно введен номер телефона. Номер должен состоять из 11 цифр. Введите заново: ");
                    phone_number = Console.ReadLine();
                }
                else break;
            }
        }

        public Mechanic(ILoadManager st)
        {
            string[] text = File.ReadAllLines(st.ToString(), Encoding.GetEncoding(1251));

            foreach (string str in text)
            {

                if (str.StartsWith("ФИО механика:"))
                {
                    bool flag;
                    FIO = str.Substring(str.IndexOf(':') + 2);
                    while(true)
                    {
                        flag = true;
                        foreach (char c in FIO)
                        {
                            if (!char.IsLetter(c) && c != ' ')
                            {
                                Console.Write("Имя не может содержать символы и цифры. Введите заново: ");
                                FIO = Console.ReadLine();
                                flag = false;
                            }
                        }
                        if (flag) break;
                    }
                }
                else if (str.StartsWith("Квалификация (разряд) механика:"))
                {
                    qualification = int.Parse(str.Substring(str.IndexOf(':') + 2));
                    while (true)
                    {

                        if (qualification < 1 || qualification > 6)
                        {
                            Console.Write("Некорректно введен разряд механика. Правильный формат - число от 1 до 6. Введите заново: ");
                            qualification = int.Parse(Console.ReadLine());
                        }
                        else break;
                    }
                }
                else if (str.StartsWith("Адрес механика:"))
                {
                    adres = str.Substring(str.IndexOf(':') + 2);
                }
                else if (str.StartsWith("Телефон механика:"))
                {
                    phone_number = str.Substring(str.IndexOf(':') + 2);
                    int count;
                    while (true)
                    {
                        count = 0;
                        foreach (char c in phone_number)
                        {
                            if (!char.IsDigit(c))
                            {
                                Console.Write("Некорректно введен номер телефона. Номер должен содержать только цифры. Введите заново: ");
                                phone_number = Console.ReadLine();
                            }
                            count++;
                        }
                        if (count != 11)
                        {
                            Console.Write("Некорректно введен номер телефона. Номер должен состоять из 11 цифр. Введите заново: ");
                            phone_number = Console.ReadLine();
                        }
                        else break;
                    }
                }
            }
        }
        public void MechanicFileReader(string st)
        {
            string[] text = File.ReadAllLines(st, Encoding.GetEncoding(1251));

            foreach (string str in text)
            {

                if (str.StartsWith("ФИО механика:"))
                {
                    bool flag;
                    FIO = str.Substring(str.IndexOf(':') + 2);
                    while(true)
                    {
                        flag = true;
                        foreach (char c in FIO)
                        {
                            if (!char.IsLetter(c) && c != ' ')
                            {
                                Console.Write("Имя не может содержать символы и цифры. Введите заново: ");
                                FIO = Console.ReadLine();
                                flag = false;
                            }
                        }
                        if (flag) break;
                    }
                }
                else if (str.StartsWith("Квалификация (разряд) механика:"))
                {
                    qualification = int.Parse(str.Substring(str.IndexOf(':') + 2));
                    while (true)
                    {

                        if (qualification < 1 || qualification > 6)
                        {
                            Console.Write("Некорректно введен разряд механика. Правильный формат - число от 1 до 6. Введите заново: ");
                            qualification = int.Parse(Console.ReadLine());
                        }
                        else break;
                    }
                }
                else if (str.StartsWith("Адрес механика:"))
                {
                    adres = str.Substring(str.IndexOf(':') + 2);
                }
                else if (str.StartsWith("Телефон механика:"))
                {
                    phone_number = str.Substring(str.IndexOf(':') + 2);
                    int count;
                    while (true)
                    {
                        count = 0;
                        foreach (char c in phone_number)
                        {
                            if (!char.IsDigit(c))
                            {
                                Console.Write("Некорректно введен номер телефона. Номер должен содержать только цифры. Введите заново: ");
                                phone_number = Console.ReadLine();
                            }
                            count++;
                        }
                        if (count != 11)
                        {
                            Console.Write("Некорректно введен номер телефона. Номер должен состоять из 11 цифр. Введите заново: ");
                            phone_number = Console.ReadLine();
                        }
                        else break;
                    }
                }
            }
        }
        public void getMechanicConsoleInfo()
        {
            Console.WriteLine("Информация о механике");
            Console.WriteLine($"ФИО механика: {FIO}");
            Console.WriteLine($"Квалификация (разряд) механика: {qualification}");
            Console.WriteLine($"Адрес механика: {adres}");
            Console.WriteLine($"Телефон механика: {phone_number}");
            Console.WriteLine("\n===================================================================\n");
        }
        public int tarif()
        {
            switch (qualification)
            {
                case 1: return 120;
                case 2: return 140;
                case 3: return 160;
                case 4: return 180;
                case 5: return 200;
                case 6: return 220;
            }
            return 0;
        }
    }
}