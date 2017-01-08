using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            // Db.Service.CleanDataBase();
            Db.Service.CleanDB();
            Db.Service.StartDB();
            var vkUsr = VkApi.Service.ParseUsers("csu_iit");
            //Mapper.Initialize(cfg => cfg.CreateMap<VkApi.VkUser, Db.Member>());
            //var dbUsr = Mapper.Map<List<Db.Member>>(vkUsr);

            var dbUsr =vkUsr.ConvertAll(new Converter<VkApi.VkUser, Db.Member>(VkUserToDbUser));
            foreach (var usr in dbUsr)
                Db.Service.FillingDatabase(usr);
           // Db.Service.FillingDatabaseGroup(dbUsr);

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
