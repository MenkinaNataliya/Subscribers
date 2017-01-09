using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace Db
{

    public class Group
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Uid { get; set; }
        public string Name { get; set; }
        public List<Member> Subscribers { get; set; }
        public Group()
        {
            Subscribers = new List<Member>();
        }
    }

    public class Member
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Uid { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Deactivated { get; set; }
        public string Photo { get; set; }
        public List<Member> Friends { get; set; }

        public Member()
        {
            Friends = new List<Member>();
        }
    }
}
