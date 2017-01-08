using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            int num = 0;
            // устанавливаем метод обратного вызова
            TimerCallback tm = new TimerCallback(Count);
            // создаем таймер
            Timer timer = new Timer(tm, num, 0, 2000);

            Console.ReadLine();
        }

        public static void Count(object obj)
        {
            Db.Service.CleanDB();
            Db.Service.StartDB();
            var vkUsr = VkApi.Service.ParseUsers("csu_iit");

            var dbUsr = vkUsr.ConvertAll(new Converter<VkApi.VkUser, Db.Member>(VkUserToDbUser));
            foreach (var usr in dbUsr)
                Db.Service.FillingDatabase(usr);
        }

        public static Db.Member VkUserToDbUser(VkApi.VkUser user)
        {
            return new Db.Member
            {
                Uid = user.id,
                FirstName = user.first_name,
                Deactivated = user.deactivated,
                SecondName = user.last_name,
                Photo = user.photo_200_orig,
                Friends = (user.Friends == null) ? null : user.Friends.ConvertAll(new Converter<VkApi.VkUser, Db.Member>(VkUserToDbUser))
                //FlagIitGroup = (user.Friends == null) ? false : true

            };
        }

       
    }
}
