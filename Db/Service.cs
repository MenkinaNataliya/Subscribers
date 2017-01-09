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

        public static Member GetUserById(int id)
        {
            using (DbModel db = new DbModel())
                return db.Members.Find(id);
        }

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
                if (user.Deactivated != "deleted")
                {
                    db.Members.Add(user);
                    db.SaveChanges();
                    Member dbMember = db.Members.Find(user.Uid);

                    foreach (var fr in user.Friends)
                    {
                        db.Members.Add(fr);
                        Member friend = db.Members.Find(fr.Uid);

                        dbMember.Friends.Add(friend);
                        db.SaveChanges();

                    }

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
    }
}
