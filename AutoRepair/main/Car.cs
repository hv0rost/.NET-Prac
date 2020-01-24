using System;
using System.IO;
using System.Text;

namespace main
{
    class Car : IWritableObject
    {
        private string autonumber;
        private string brand;
        private int power;
        private int data;
        private string color;
        private string pts;

        public void getCarConsoleInfo()
        {
            Console.WriteLine("Информация о машине");
            Console.WriteLine($"Номер автомобиля: {autonumber}");
            Console.WriteLine($"Марка автомобиля: {brand}");
            Console.WriteLine($"Мощность автомобиля: {power}");
            Console.WriteLine($"Год выпуска автомобиля: {data}");
            Console.WriteLine($"Цвет автомобиля: {color}");
            Console.WriteLine($"Номер ПТС: {pts}");
            Console.WriteLine("\n===================================================================\n");
        }

        public void CarFileReader(string st)
        {
            bool f1, f2;
            string[] text = File.ReadAllLines(st, Encoding.GetEncoding(1251));

            foreach (string str in text)
            {
                if (str.StartsWith("Номер автомобиля:"))
                {
                    autonumber = str.Substring(str.IndexOf(':') + 2);
                    while (true)
                    {
                        if (autonumber.Length != 6)
                        {
                            Console.Write(
                                "Некорректный номер машины. Номер мащины не может быть короче 6 символов. Введите заново:");
                            autonumber = Console.ReadLine();
                            continue;
                        }

                        char[] aut = autonumber.ToCharArray();
                        if (!(char.IsLetter(aut[0]) && char.IsDigit(aut[1]) && char.IsDigit(aut[2]) &&
                              char.IsDigit(aut[3]) && char.IsLetter(aut[4]) && char.IsLetter(aut[5])))
                        {
                            Console.Write(
                                "Некорректный номер машины. Номер должен соотвествовать формату - а111аа. Введите заново: ");
                            autonumber = Console.ReadLine();
                        }
                        else break;
                    }
                }
                else if (str.StartsWith("Марка автомобиля:"))
                {
                    brand = str.Substring(str.IndexOf(':') + 2);
                    while (true)
                    {
                        f1 = true;
                        foreach (char c in brand)
                        {
                            if (!char.IsLetter(c))
                            {
                                Console.Write("Марка авто введена не корректно. Введите заново:");
                                brand = Console.ReadLine();
                                f1 = false;
                            }
                        }
                        if (f1) break;
                    }
                }
                else if (str.StartsWith("Мощность автомобиля:"))
                {
                    power = int.Parse(str.Substring(str.IndexOf(':') + 2));
                    while (true)
                    {
                        if (power < 0)
                        {
                            Console.Write("Мощность авто не может быть меньше нуля. Введите заново: ");
                            power = int.Parse(Console.ReadLine());
                        }
                        else break;
                    }
                }
                else if (str.StartsWith("Год выпуска автомобиля:"))
                {
                    data = int.Parse(str.Substring(str.IndexOf(':') + 2));
                    while (true)
                    {
                        if (data < 1806 || data > 2019)
                        {
                            Console.Write("Год выпуска указан некорректно. Введите заново: ");
                            data = int.Parse(Console.ReadLine());
                        }
                        else break;
                    }
                }
                else if (str.StartsWith("Цвет автомобиля:"))
                {
                    color = str.Substring(str.IndexOf(':') + 2);
                    while (true)
                    {
                        f1 = true;
                        foreach (char c in color)
                        {
                            if (!char.IsLetter(c))
                            {
                                Console.Write("Цвет авто введен не корректно. Введите заново: ");
                                color = Console.ReadLine();
                                f1 = false;
                            }
                        }
                        if (f1) break;
                    }
                }
                else if (str.StartsWith("Номер ПТС:"))
                {
                    f1 = false;
                    f2 = false;
                        pts = str.Substring(str.IndexOf(':') + 2);
                    string tmp;

                    while (true)
                    {
                        tmp = pts;
                        tmp = tmp.Replace(" ", "");
                        if (tmp.Length != 10)
                        {
                            Console.Write(
                                "Номер ПТС указан некорректно. Номер ПТС не может быть меньше 10 символов. Введите заново: ");
                            pts = Console.ReadLine();
                            continue;
                        }

                        char[] t = tmp.ToCharArray();
                        if (!(char.IsDigit(t[0]) && char.IsDigit(t[1]) && char.IsLetter(t[2]) && char.IsLetter(t[3])))
                        {
                            Console.Write(
                                "Номер ПТС указан некорректно. Номер ПТС должен быть формата 11 аа 111111. Введите заново: ");
                            pts = Console.ReadLine();
                        }
                        else f1 = true;

                        if (!(char.IsDigit(t[4]) && char.IsDigit(t[5]) && char.IsDigit(t[6]) && char.IsDigit(t[7]) &&
                              char.IsDigit(t[8]) && char.IsDigit(t[9])))
                        {
                            Console.Write(
                                "Номер ПТС указан некорректно. Номер ПТС должен быть формата 11 аа 111111. Введите заново: ");
                            pts = Console.ReadLine();
                        }
                        else f2 = true;
                        
                        if (f1 && f2) break;
                    }
                }
            }
        }
        
        public void CarConsoleReader()
        {
            bool f1, f2;
            
            Console.Write("Введите номер машины(формат а111аа): ");
            autonumber = Console.ReadLine();
            while (true)
            {
                if (autonumber.Length != 6)
                {
                    Console.Write(
                        "Некорректный номер машины. Номер мащины не может быть короче 6 символов. Введите заново:");
                    autonumber = Console.ReadLine();
                    continue;
                }
                char[] aut = autonumber.ToCharArray();
                if (!(char.IsLetter(aut[0]) && char.IsDigit(aut[1]) && char.IsDigit(aut[2]) &&
                      char.IsDigit(aut[3]) && char.IsLetter(aut[4]) && char.IsLetter(aut[5])))
                {
                    Console.Write(
                        "Некорректный номер машины. Номер должен соотвествовать формату - а111аа. Введите заново: ");
                    autonumber = Console.ReadLine();
                }
                else break;
            }
            Console.Write("Введите марку машины: ");
            brand = Console.ReadLine();
            
            Console.Write("Введите мощность двигателя: ");
            power = int.Parse(Console.ReadLine());
            while (true)
            {
                if (power < 0)
                {
                    Console.Write("Мощность авто не может быть меньше нуля. Введите заново: ");
                    power = int.Parse(Console.ReadLine());
                }
                else break;
            }
               
            Console.Write("Введите дату выпуска авто: ");
            data = int.Parse(Console.ReadLine());
            while (true)
            {
                if (data < 1806 || data > 2019)
                {
                    Console.Write("Год выпуска указан некорректно. Введите заново: ");
                    data = int.Parse(Console.ReadLine());
                }
                else break;
            }
            
            Console.Write("Введите цвет: ");
            color = Console.ReadLine();
            
            f1 = false;
            f2 = false;
            Console.Write("Введите номер ПТС(формат - 11 аа 111111): ");
            pts = Console.ReadLine();
            string tmp;
            while (true)
            {
                tmp = pts;
                tmp = tmp.Replace(" ", "");
                if (tmp.Length != 10)
                {
                    Console.Write(
                        "Номер ПТС указан некорректно. Номер ПТС не может быть меньше 10 символов. Введите заново: ");
                    pts = Console.ReadLine();
                    continue;
                }
                char[] t = tmp.ToCharArray();
                if (!(char.IsDigit(t[0]) && char.IsDigit(t[1]) && char.IsLetter(t[2]) && char.IsLetter(t[3])))
                {
                    Console.Write(
                        "Номер ПТС указан некорректно. Номер ПТС должен быть формата 11 аа 111111. Введите заново: ");
                    pts = Console.ReadLine();
                }
                else f1 = true;
                if (!(char.IsDigit(t[4]) && char.IsDigit(t[5]) && char.IsDigit(t[6]) && char.IsDigit(t[7]) &&
                      char.IsDigit(t[8]) && char.IsDigit(t[9])))
                {
                    Console.Write(
                        "Номер ПТС указан некорректно. Номер ПТС должен быть формата 11 аа 111111. Введите заново: ");
                    pts = Console.ReadLine();
                }
                else f2 = true;
                
                if (f1 && f2) break;
            }
        }

        public void Write(SaveManager info)
        {
            info.WriteInfo("Информация о машине");
            info.WriteInfo($"Номер автомобиля: {autonumber}");
            info.WriteInfo($"Марка автомобиля: {brand}");
            info.WriteInfo($"Мощность автомобиля: {power}");
            info.WriteInfo($"Год выпуска автомобиля: {data}");
            info.WriteInfo($"Цвет автомобиля: {color}");
            info.WriteInfo($"Номер ПТС: {pts}");
            info.WriteInfo("\n===================================================================\n");
        }
    }
}
