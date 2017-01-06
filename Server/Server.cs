using AutoMapper;
using Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer
{
    public class Server
    {
        public static List<User> GetMembers( string group)
        {
            //UpdateDb(null);
            // return Translate.DbUsersInUsers(Service.GetMembers());
            Mapper.Initialize(cfg => cfg.CreateMap<Member, User>());
            var tmp = Mapper.Map<List<User>>(Service.GetMembers(group));
            return tmp;
        }


    }
}
