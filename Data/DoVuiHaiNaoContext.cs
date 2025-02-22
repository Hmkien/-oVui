using DoVuiHaiNao.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DoVuiHaiNao.Data;

public class DoVuiHaiNaoContext : IdentityDbContext<ApplicationUser>
{
    public DoVuiHaiNaoContext(DbContextOptions<DoVuiHaiNaoContext> options)
        : base(options)
    { }
    public DbSet<EntityBase> Entities { get; set; }
    public DbSet<Quizz> Quizzs { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Result> Results { get; set; }
    public DbSet<DanhMuc> Categories { get; set; }
    public DbSet<MucDo> MucDos { get; set; }
    public DbSet<AppRole> AppRole { get; set; }
    public DbSet<ResultAnswer> ResultAnswer { get; set; }
    public DbSet<ApplicationUser> ApplicationUser { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

    }
}
