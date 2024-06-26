using InteractiveTextbook.Models;
using Microsoft.EntityFrameworkCore;

namespace InteractiveTextbook.Data
{
    public class InteractiveTextbookDbContext : DbContext
    {
        public InteractiveTextbookDbContext(DbContextOptions<InteractiveTextbookDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<InteractiveElement> InteractiveElements { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Teacher)
                .WithOne(t => t.User)
                .HasForeignKey<Teacher>(t => t.UserId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Student)
                .WithOne(s => s.User)
                .HasForeignKey<Student>(s => s.UserId);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.InteractiveElement)
                .WithOne()
                .HasForeignKey<Book>(b => b.InteractiveElementId);

            modelBuilder.Entity<InteractiveElement>()
                .HasOne(ie => ie.Media)
                .WithOne()
                .HasForeignKey<InteractiveElement>(ie => ie.MediaId);

            modelBuilder.Entity<InteractiveElement>()
                .HasOne(ie => ie.Quiz)
                .WithOne()
                .HasForeignKey<InteractiveElement>(ie => ie.QuizId);

            modelBuilder.Entity<Quiz>()
                .HasOne(q => q.Question)
                .WithOne()
                .HasForeignKey<Quiz>(q => q.QuestionId);

            modelBuilder.Entity<Question>()
                .HasOne(q => q.CorrectAnswer)
                .WithOne()
                .HasForeignKey<Question>(q => q.AnswerId);
        }
    }
}
