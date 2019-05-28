using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace CLIENT
{
    class Program
    {
        static Socket Connection;

        static void ClientHandler(Object StateInfo)
        {
            byte[] bytes = new byte[1024];
            string data = "";
            while (true)
            {
                int bytesCount = Connection.Receive(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, bytesCount);
                Console.Write("[friend] " + data + "\n");
                data = "";
            }
        }

        public static void Start(string[] args)
        {
            IPAddress ipAddr = IPAddress.Parse("192.168.0.109");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 1111);

            Connection = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            Console.Write("Введите логин: ");
            string login = Console.ReadLine();
            Console.Write("Введите пароль: ");
            string password = Console.ReadLine();

            Console.Write("1) Войти\n2) Создать аккаунт\n» ");
            int choice = Convert.ToInt32(Console.ReadLine());

            byte[] blogin;

            if (choice == 1)
            {
                blogin = Encoding.ASCII.GetBytes("lgnacc " + login + " " + password);
            }
            else
            {
                Console.Write("Введите имя: ");
                string name = Console.ReadLine();
                blogin = Encoding.ASCII.GetBytes("crnacc " + login + " " + password + " " + name);
            }

            Connection.Connect(ipEndPoint);

            Connection.Send(blogin);
            byte[] rec_mess = new byte[32];
            Connection.Receive(rec_mess); // подтверждение входа 
            int responce = Convert.ToInt32(Encoding.ASCII.GetString(rec_mess));

            if (responce == 1)
            {
                Thread thread = new Thread(new ParameterizedThreadStart(ClientHandler));
                thread.Start();

                Console.WriteLine("Подключен.");

                while (true)
                {
                    Console.Write("Выбериет команду:" +
                    "1) отправить сообщение\n" +
                    "2) получение списка друзей [ login ]\n" +
                    "3) получение информации со страницы пользователя [ login ]\n" +
                    "4) получение сообщений со страницы пользователя [ login ]\n" +
                    "5) поиск пользователя по логину [ login ]\n" +
                    "6) поиск пользователя по имени [ name ]\n" +
                    "7) получить весь чат с данным пользователем [ login, friend_login ]\n" +
                    "» ");
                    choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            Console.Write("Логин друга: ");
                            string friend_login = Console.ReadLine();
                            Console.Write("Введите сообщение: ");
                            string message = Console.ReadLine();
                            if (message != "")
                            {
                                byte[] bytes = Encoding.ASCII.GetBytes("sndmsg " + login + " " + friend_login + " " + message);
                                Connection.Send(bytes);
                            }
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Ошибка подключения.");
                string d = Console.ReadLine();
            }
        }
    }
}