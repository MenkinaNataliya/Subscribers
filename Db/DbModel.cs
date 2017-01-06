namespace Db
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DbModel : DbContext
    {
        // Контекст настроен для использования строки подключения "DbModel" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "Db.DbModel" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "DbModel" 
        // в файле конфигурации приложения.
        public DbModel()
            : base("BaseVkUser")
        {
        }

        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>()
                 .HasMany(c => c.Friends)
                 .WithMany()
                 .Map(m =>
                 {
                     // Ссылка на промежуточную таблицу
                     m.ToTable("MemberFriend");

                     // Настройка внешних ключей промежуточной таблицы
                     m.MapLeftKey("MemberId");
                     m.MapRightKey("FriendId");
                 });

            modelBuilder.Entity<Group>()
                .HasMany(c => c.Subscribers)
                .WithMany()
                .Map(m =>
                {
                    // Ссылка на промежуточную таблицу
                    m.ToTable("GroupMembers");

                    // Настройка внешних ключей промежуточной таблицы
                    m.MapLeftKey("GroupId");
                    m.MapRightKey("MemberId");
                });

        }
    }



    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}