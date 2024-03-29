﻿using EntityHomeworkThird.Model.Entities;
using EntityHomeworkThird.Model.Configs;
using EntityHomeworkThird.ViewModel;

namespace EntityHomeworkThird.Model.Context
{
    /// <summary>
    /// Custom Entity database context;
    /// <br />
    /// Кастомный контекст датабазы из Entity;
    /// </summary>
    public class CurrentDatabaseContext : DbContext
    {


        #region PROPERTIES


        /// <summary>
        /// The 'Students' table contents;
        /// <br />
        /// Содержимое таблицы "Students";
        /// </summary>
        public DbSet<Student> Students { get; set; } = null!;


        /// <summary>
        /// The 'Cards' table contents;
        /// <br />
        /// Содержимое таблицы "Cards";
        /// </summary>
        public DbSet<Card> Cards { get; set; } = null!;


        /// <summary>
        /// The 'Marks' table contents;
        /// <br />
        /// Содержимое таблицы "Marks";
        /// </summary>
        public DbSet<Mark> Marks { get; set; } = null!;


        /// <summary>
        /// The 'Subjects' table contents;
        /// <br />
        /// Содержимое таблицы "Subjects";
        /// </summary>
        public DbSet<Subject> Subjects { get; set; } = null!;


        #endregion PROPERTIES




        #region CONTEXT OVERRIDES


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(MainWindowViewModel.ConnectionString);
        }


        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new CardConfiguration());
            modelBuilder.ApplyConfiguration(new MarkConfiguration());
            modelBuilder.ApplyConfiguration(new SubjectConfiguration());
        }


        #endregion CONTEXT OVERRIDES




        #region CONSTRUCTION



        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public CurrentDatabaseContext()
        {
            Database.EnsureCreated();
        }



        #endregion CONSTRUCTION


    }
}
