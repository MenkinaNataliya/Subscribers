using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new DbModel())
            {
                db.Members.Add(new Member
                {
                    Uid = 98,
                    SecondName = "123",
                    FirstName = "455"
                });
                db.SaveChanges();
            }
        }
    }
}
