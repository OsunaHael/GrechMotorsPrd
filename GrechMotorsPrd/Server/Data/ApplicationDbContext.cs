using GrechMotorsPrd.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace GrechMotorsPrd.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor que acepta DbContextOptions y lo pasa al constructor base
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // Aquí defines tus DbSets
        public DbSet<UserModel> users { get; set; }
        public DbSet<UnitModel> units { get; set; }
        public DbSet<FurnitureModel> furnitures { get; set; }
        public DbSet<PieceModel> pieces { get; set; }
        public DbSet<FurniturePieceModel> furniturespieces { get; set; }
        public DbSet<UnitFurnitureModel> unitsfurnitures { get; set; }
        public DbSet<UserPieceModel> userspieces { get; set; }
        public DbSet<UserFurnitureModel> usersfurnitures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Llamamos al método base para mantener el comportamiento predeterminado
            base.OnModelCreating(modelBuilder);

            // Configuramos las relaciones de clave externa

            // Relación uno a muchos entre UserModel y UnitModel
            modelBuilder.Entity<UnitModel>()
                .HasOne(m => m.User)
                .WithMany(u => u.Unit) // Propiedad de navegación en UserModel
                .HasForeignKey(m => m.user_id);
            // Relación muchos a muchos entre UserModel y PieceModel a través de una tabla intermedia
            modelBuilder.Entity<UserPieceModel>()
                .HasKey(up => new { up.user_id, up.piece_id });

            modelBuilder.Entity<UserPieceModel>()
                .HasOne(up => up.User)
                .WithMany(u => u.UserPieces)
                .HasForeignKey(up => up.user_id);

            modelBuilder.Entity<UserPieceModel>()
                .HasOne(up => up.Piece)
                .WithMany(p => p.UserPieces)
                .HasForeignKey(up => up.piece_id);

            // Relación muchos a muchos entre UserModel y FurnitureModel a través de una tabla intermedia
            modelBuilder.Entity<UserFurnitureModel>()
                .HasKey(uf => new { uf.user_id, uf.furniture_id });

            modelBuilder.Entity<UserFurnitureModel>()
                .HasOne(uf => uf.User)
                .WithMany(u => u.UserFurnitures)
                .HasForeignKey(uf => uf.user_id);

            modelBuilder.Entity<UserFurnitureModel>()
                .HasOne(uf => uf.Furniture)
                .WithMany(f => f.UserFurnitures)
                .HasForeignKey(uf => uf.furniture_id);

            // Relación muchos a muchos entre FurnitureModel y PieceModel a través de FurniturePieceModel
            modelBuilder.Entity<FurniturePieceModel>()
                .HasKey(fp => new { fp.furniture_id, fp.piece_id });

            modelBuilder.Entity<FurniturePieceModel>()
                .HasOne(fp => fp.Furniture)
                .WithMany(f => f.FurniturePieces)
                .HasForeignKey(fp => fp.furniture_id);

            modelBuilder.Entity<FurniturePieceModel>()
                .HasOne(fp => fp.Piece)
                .WithMany(p => p.FurniturePieces)
                .HasForeignKey(fp => fp.piece_id);

            // Relación muchos a muchos entre UnitModel y FurnitureModel a través de UnitFurnitureModel
            modelBuilder.Entity<UnitFurnitureModel>()
                .HasKey(uf => new { uf.unit_id, uf.furniture_id });

            modelBuilder.Entity<UnitFurnitureModel>()
                .HasOne(uf => uf.Unit)
                .WithMany(u => u.UnitFurnitures)
                .HasForeignKey(uf => uf.unit_id);

            modelBuilder.Entity<UnitFurnitureModel>()
                .HasOne(uf => uf.Furniture)
                .WithMany(f => f.UnitFurnitures)
                .HasForeignKey(uf => uf.furniture_id);
        }
    }
}