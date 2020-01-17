using System;
using System.IO;
using System.Text;

namespace main
{
    public class AutoOwner
    {
        private string number;
        protected string FIO;
        protected string adres;
        protected string phone_number;
        public void getAutoOwnerConsoleInfo()
        {
            Console.WriteLine("Информация об автовладельце");
            Console.WriteLine($"ФИО владельца: {FIO}");
            Console.WriteLine($"Номер водительских прав: {number}");
            Console.WriteLine($"Адрес владельца: {adres}");
            Console.WriteLine($"Телефон владельца: {phone_number}");
            Console.WriteLine("\n===================================================================\n");
        }
        
        public void AutoOwnerWriter()
        {
            StreamWriter st = new StreamWriter("info.txt", true,Encoding.GetEncoding(1251));
            
            st.WriteLine("Информация об автовладельце");
            st.WriteLine($"ФИО владельца: {FIO}");
            st.WriteLine($"Номер водительских прав: {number}");
            st.WriteLine($"Адрес владельца: {adres}");
            st.WriteLine($"Телефон владельца: {phone_number}");
            st.WriteLine("\n===================================================================\n");
            st.Close();
        }

        public AutoOwner() {  }

        public void AutoOwnerFileReader(string st)
        {
            string[] text = File.ReadAllLines(st, Encoding.GetEncoding(1251));

            foreach (string str in text)
            {
                if (str.StartsWith("ФИО владельца:"))
                {
                    bool flag = false;
                    FIO = str.Substring(str.IndexOf(':') + 2);
                    while (true)
                    {
                        foreach (char c in FIO)
                        {
                            if (!char.IsLetter(c) && c != ' ')
                            {
                                Console.Write("Имя не может содержать символы и цифры. ");
                                FIO = Console.ReadLine();
                            }
                            else flag = true;
                        }

                        if (flag) break;
                    }
                }
                else if (str.StartsWith("Номер водительских прав:"))
                {
                    number = str.Substring(str.IndexOf(':') + 2);
                    int count;
                    while (true)
                    {
                        count = 0;
                        foreach (char c in number)
                        {
                            if (!char.IsDigit(c) && c != ' ')
                            {
                                Console.Write(
                                    "Некорректно введен номер водительских прав. Номер водительских прав должен содержать только цифры. Введите заново:");
                                number = Console.ReadLine();
                            }

                            count++;
                        }

                        if (count != 10)
                        {
                            Console.Write(
                                "Некорректно введен номер водительских прав. Номер водительских прав не может быть короче 10 символов. Введите заново:");
                            number = Console.ReadLine();
                        }
                        else break;
                    }
                }
                else if (str.StartsWith("Адрес владельца:"))
                {
                    adres = str.Substring(str.IndexOf(':') + 2);
                }
                else if (str.StartsWith("Телефон владельца:"))
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
                                Console.Write(
                                    "Некорректно введен номер телефона. Номер должен содержать только цифры. Введите заново: ");
                                phone_number = Console.ReadLine();
                            }

                            count++;
                        }

                        if (count != 11)
                        {
                            Console.Write(
                                "Некорректно введен номер телефона. Номер должен состоять из 11 цифр. Введите заново: ");
                            phone_number = Console.ReadLine();
                        }
                        else break;
                    }
                }
            }
        }

        public void AutoOwnerConsoleReader()
        {
            bool flag = false;
            Console.Write("Введите имя владельца авто: ");
            FIO = Console.ReadLine();
            while(true)
            {
                foreach (char c in FIO)
                {
                    if (!char.IsLetter(c) && c != ' ')
                    {
                        Console.Write("Имя не может содержать символы и цифры. ");
                        FIO = Console.ReadLine();
                    }
                    else flag = true;
                }
                if (flag) break;
            }
             
            Console.Write("Введите номeр водительских прав(10 цифр): ");
            number = Console.ReadLine();
            int count;
            while (true)
            {
                count = 0;
                foreach (char c in number)
                {
                    if (!char.IsDigit(c) && c != ' ')
                    {
                        Console.Write(
                            "Некорректно введен номер водительских прав. Номер водительских прав должен содержать только цифры. Введите заново:");
                        number = Console.ReadLine();
                    }
                    count++;
                }

                if (count != 10)
                {
                    Console.Write(
                        "Некорректно введен номер водительских прав. Номер водительских прав не может быть короче 10 символов. Введите заново: ");
                    number = Console.ReadLine();
                }
                else break;
            }

            Console.Write("");
                Console.Write("Введите адрес владльца:");
                adres = Console.ReadLine();
                 
                Console.Write("Введите номер телефона владельца(11 цифр): ");
                phone_number = Console.ReadLine();
                while (true)
                {
                    count = 0;
                    foreach (char k in phone_number)
                    {
                        if (!char.IsDigit(k))
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
