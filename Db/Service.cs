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



        public static void FillingDatabase(Member user)
        {
            using (DbModel db = new DbModel())
            {
                var friends = user.Friends;
                user.Friends = new List<Member>();
                db.Members.AddOrUpdate(user);
                db.SaveChanges();
                Member dbMember = db.Members.Find(user.Uid);

                foreach (var fr in friends)
                {
                    db.Members.AddOrUpdate(fr);
                    Member friend = db.Members.Find(fr.Uid);
                   
                    dbMember.Friends.Add(friend);
                    db.SaveChanges();
                   
                }
                db.SaveChanges();

                var group = db.Groups.Where(x => x.Name == "csu_iit").First();
                group.Subscribers.Add(dbMember);
                db.SaveChanges();

            }
        }

        public static void UpdateDb(Member user, List<Member> friends)
        {
           /* using (DbModel db = new DbModel())
            {
                Member dbMember = db.Members.Find(user.Uid);
                if (dbMember == null) dbMember = db.Members.Add(user);
                foreach (var friend in friends)
                {
                    Member dbUser = db.Users.Find(friend.Uid);
                    if (dbUser == null) dbUser = db.Users.Add(friend);
                    //var dop = ;
                    var list = db.Friends.Where(x => x.Member == dbMember.Uid).Where(x => x.User == dbUser.Uid).ToList();

                    if (list.Count == 0)
                    {
                        MemberFriend memFriend = new MemberFriend { Member = dbMember.Uid, User = dbUser.Uid };
                        db.Friends.Add(memFriend);
                    }


                }
                db.SaveChanges();
            }*/
        }

        public static List<Member> GetMembers(string namegroup)
        {
            using (var db = new DbModel())
            {
                var group = db.Groups.Include("Subscribers")
                                    .Where(x => x.Name == namegroup).First();
                var ids =  group.Subscribers.ToList();
                return ids;
                var mem = new List<Member>();
                foreach(var id in ids)
                {
                    mem.Add(db.Members.Find(id));
                }
                return mem;
            }
        }

        public static Member GetUserById(int id)
        {
            using (var db = new DbModel())
                return db.Members.Find(id);
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
