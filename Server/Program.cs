﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppServer
{
    class Program
    {
        static void Main(string[] args)
        {
            int num = 0;
            // устанавливаем метод обратного вызова
            TimerCallback tm = new TimerCallback(Count);
            // создаем таймер
            Timer timer = new Timer(tm, num, 0, 86400000);//раз в сутки

            Console.ReadLine();
        }

        public static void Count(object obj)
        {
            Db.Service.CleanDB();
            Db.Service.StartDB();
            var vkUsr = VkApi.Service.ParseUsers("csu_iit");

            var dbUsr = vkUsr.ConvertAll(new Converter<VkApi.VkUser, Db.Member>(Translate.VkUserToDbUser));
            foreach (var usr in dbUsr)
                Db.Service.FillingDatabase(usr);
        }
    }
}
