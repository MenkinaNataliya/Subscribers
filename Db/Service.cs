using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db
{
    public class Service
    {
        public static DbModel db = new DbModel();


        public static int CountNumberFriends(long id)
        {
            using (DbModel db = new DbModel())
            {
                var user = db.Members.Find(id);
               return  user.Friends.Count();
            }
        }
        public static void CleanDB()
        {
            using (DbModel db = new DbModel())
            {
                db.Database.Delete();
                
                db.SaveChanges();
            }
                
        }
        public static void StartDB()
        {
            using (DbModel db = new DbModel())
            {
                db.Groups.Add(new Group { Name = "csu_iit", Uid = "csu_iit" });
                db.SaveChanges();
            }
        }

        public static void FillingDatabase(Member user)
        {
            using (DbModel db = new DbModel())
            {
                //var tmp = db.Groups.ToList();
                if (user.Deactivated != "deleted")
                {
                    var friends = user.Friends;
                    user.Friends = new List<Member>();
                    db.Members.Add(user);
                    db.SaveChanges();
                    Member dbMember = db.Members.Find(user.Uid);

                    foreach (var fr in friends)
                    {
                        db.Members.Add(fr);
                        Member friend = db.Members.Find(fr.Uid);

                        dbMember.Friends.Add(friend);
                        db.SaveChanges();

                    }
                    db.SaveChanges();

                    var group = db.Groups.Where(x => x.Name == "csu_iit").FirstOrDefault();
                    group.Subscribers.Add(dbMember);
                    db.SaveChanges();
                }
            }
        }

        public static List<Member> GetMembers(string namegroup)
        {
            using (var db = new DbModel())
            {
                var group = db.Groups.Include("Subscribers")
                                    .Where(x => x.Name == namegroup).First();
                return group.Subscribers.ToList();
            }
        }

       /* public static User GetFriendById(long id)
        {
            using (var db = new DbModel())
                return db.Users.Find(id);
        }

        public static List<User> GetFriends(long member)
        {
            using (var db = new DbModel())
            {
                // var memid = GetUserById((int)member);
                var users = db.Friends.Where(x => x.Member == member).ToList();
                List<User> friends = new List<User>();
                foreach (var user in users)
                {
                    friends.Add(GetFriendById(user.User));
                }
                return friends;
            }

            // return db.Users.Where(x => x.Friends.Any(f => f.Uid == member.Uid)).ToList();

        }*/
    }
}
