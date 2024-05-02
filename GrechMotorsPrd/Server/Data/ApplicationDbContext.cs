// Importamos los modelos necesarios y el espacio de nombres de Entity Framework Core
using GrechMotorsPrd.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GrechMotorsPrd.Server.Data
{
    // Definición de la clase ApplicationDbContext que hereda de DbContext
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        // Constructor que acepta DbContextOptions y lo pasa al constructor base
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // Aquí se definen los DbSets para cada modelo que se va a mapear a la base de datos
        public DbSet<UserModel> users { get; set; } // DbSet para el modelo UserModel
        public DbSet<UnitModel> units { get; set; } // DbSet para el modelo UnitModel
        public DbSet<FurnitureModel> furnitures { get; set; } // DbSet para el modelo FurnitureModel
        public DbSet<PieceModel> pieces { get; set; } // DbSet para el modelo PieceModel
        public DbSet<FurniturePieceModel> furniturespieces { get; set; } // DbSet para el modelo FurniturePieceModel
        public DbSet<UnitFurnitureModel> unitsfurnitures { get; set; } // DbSet para el modelo UnitFurnitureModel
        public DbSet<UserPieceModel> userspieces { get; set; } // DbSet para el modelo UserPieceModel
        public DbSet<UserFurnitureModel> usersfurnitures { get; set; } // DbSet para el modelo UserFurnitureModel
        public DbSet<UnitPieceCodeModel> unitspiecescodes { get; set; } // DbSet para el modelo UnitsPiecesCodesModel
        public DbSet<UnitFurnitureCodeModel> unitsfurniturescodes { get; set; } // DbSet para el modelo UnitsFurnituresCodesModel
        public DbSet<PieceStatusHistory> piecestatushistories { get; set; } // DbSet para el modelo PieceStatusHistory
        public DbSet<FurnitureStatusHistory> furniturestatushistories { get; set; } // DbSet para el modelo FurnitureStatusHistory

        // Método para configurar las relaciones de las entidades en la base de datos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Llamamos al método base para mantener el comportamiento predeterminado
            base.OnModelCreating(modelBuilder);

            // Configuramos las relaciones de clave externa para las entidades

            // Relación uno a muchos entre UserModel y UnitModel
            modelBuilder.Entity<UnitModel>()
                .HasOne(m => m.User) // Una unidad tiene un usuario
                .WithMany(u => u.Unit) // Un usuario tiene muchas unidades
                .HasForeignKey(m => m.user_id); // Clave foránea de la unidad es user_id

            // Relación muchos a muchos entre UserModel y PieceModel a través de una tabla intermedia
            modelBuilder.Entity<UserPieceModel>()
                .HasKey(up => new { up.user_id, up.piece_id }); // Definir clave compuesta

            modelBuilder.Entity<UserPieceModel>()
                .HasOne(up => up.User) // Una relación usuario tiene un usuario
                .WithMany(u => u.UserPieces) // Un usuario tiene muchas relaciones usuario-pieza
                .HasForeignKey(up => up.user_id); // Clave foránea de usuario es user_id

            modelBuilder.Entity<UserPieceModel>()
                .HasOne(up => up.Piece) // Una relación usuario tiene una pieza
                .WithMany(p => p.UserPieces) // Una pieza tiene muchas relaciones usuario-pieza
                .HasForeignKey(up => up.piece_id); // Clave foránea de pieza es piece_id

            // Relación muchos a muchos entre UserModel y FurnitureModel a través de una tabla intermedia
            modelBuilder.Entity<UserFurnitureModel>()
                .HasKey(uf => new { uf.user_id, uf.furniture_id }); // Definir clave compuesta

            modelBuilder.Entity<UserFurnitureModel>()
                .HasOne(uf => uf.User) // Una relación usuario tiene un usuario
                .WithMany(u => u.UserFurnitures) // Un usuario tiene muchas relaciones usuario-mueble
                .HasForeignKey(uf => uf.user_id); // Clave foránea de usuario es user_id

            modelBuilder.Entity<UserFurnitureModel>()
                .HasOne(uf => uf.Furniture) // Una relación usuario tiene un mueble
                .WithMany(f => f.UserFurnitures) // Un mueble tiene muchas relaciones usuario-mueble
                .HasForeignKey(uf => uf.furniture_id); // Clave foránea de mueble es furniture_id

            // Relación muchos a muchos entre FurnitureModel y PieceModel a través de FurniturePieceModel
            modelBuilder.Entity<FurniturePieceModel>()
                .HasKey(fp => new { fp.furniture_id, fp.piece_id }); // Definir clave compuesta

            modelBuilder.Entity<FurniturePieceModel>()
                .HasOne(fp => fp.Furniture) // Una relación mueble tiene un mueble
                .WithMany(f => f.FurniturePieces) // Un mueble tiene muchas relaciones mueble-pieza
                .HasForeignKey(fp => fp.furniture_id); // Clave foránea de mueble es furniture_id

            modelBuilder.Entity<FurniturePieceModel>()
                .HasOne(fp => fp.Piece) // Una relación mueble tiene una pieza
                .WithMany(p => p.FurniturePieces) // Una pieza tiene muchas relaciones mueble-pieza
                .HasForeignKey(fp => fp.piece_id); // Clave foránea de pieza es piece_id

            // Relación muchos a muchos entre UnitModel y FurnitureModel a través de UnitFurnitureModel
            modelBuilder.Entity<UnitFurnitureModel>()
                .HasKey(uf => new { uf.unit_id, uf.furniture_id }); // Definir clave compuesta

            modelBuilder.Entity<UnitFurnitureModel>()
                .HasOne(uf => uf.Unit) // Una relación unidad tiene una unidad
                .WithMany(u => u.UnitFurnitures) // Una unidad tiene muchas relaciones unidad-mueble
                .HasForeignKey(uf => uf.unit_id); // Clave foránea de unidad es unit_id

            modelBuilder.Entity<UnitFurnitureModel>()
                .HasOne(uf => uf.Furniture) // Una relación unidad tiene un mueble
                .WithMany(f => f.UnitFurnitures) // Un mueble tiene muchas relaciones unidad-mueble
                .HasForeignKey(uf => uf.furniture_id); // Clave foránea de mueble es furniture_id

            //Relacion muchos a muchos entre UnitModel y PieceModel a través de UnitPieceCodeModel
            modelBuilder.Entity<UnitPieceCodeModel>()
                .HasKey(upc => new { upc.unit_id, upc.piece_id }); // Definir clave compuesta

            modelBuilder.Entity<UnitPieceCodeModel>()
                .HasOne(upc => upc.Unit) // Una relación unidad tiene una unidad
                .WithMany(u => u.UnitPiecesCodes) // Una unidad tiene muchas relaciones unidad-pieza
                .HasForeignKey(upc => upc.unit_id); // Clave foránea de unidad es unit_id

            modelBuilder.Entity<UnitPieceCodeModel>()
                .HasOne(upc => upc.Piece) // Una relación unidad tiene una pieza
                .WithMany(p => p.UnitPiecesCodes) // Una pieza tiene muchas relaciones unidad-pieza
                .HasForeignKey(upc => upc.piece_id); // Clave foránea de pieza es piece_id

            //Relacion muchos a muchos entre UnitModel y FurnitureModel a través de UnitFurnitureCodeModel
            modelBuilder.Entity<UnitFurnitureCodeModel>()
                .HasKey(ufc => new { ufc.unit_id, ufc.furniture_id }); // Definir clave compuesta

            modelBuilder.Entity<UnitFurnitureCodeModel>()
                .HasOne(ufc => ufc.Unit) // Una relación unidad tiene una unidad
                .WithMany(u => u.UnitFurnituresCodes) // Una unidad tiene muchas relaciones unidad-mueble
                .HasForeignKey(ufc => ufc.unit_id); // Clave foránea de unidad es unit_id

            modelBuilder.Entity<UnitFurnitureCodeModel>()
                .HasOne(ufc => ufc.Furniture) // Una relación unidad tiene un mueble
                .WithMany(f => f.UnitFurnituresCodes) // Un mueble tiene muchas relaciones unidad-mueble
                .HasForeignKey(ufc => ufc.furniture_id); // Clave foránea de mueble es furniture_id

            // Relación muchos a muchos entre PieceStatusHistory y las otras entidades
            modelBuilder.Entity<PieceStatusHistory>()
                .HasKey(psh => psh.id); // Definir clave primaria

            modelBuilder.Entity<PieceStatusHistory>()
                .HasOne(psh => psh.Piece) // Una relación historial de estado de pieza tiene una pieza
                .WithMany(p => p.PieceStatusHistories) // Una pieza tiene muchos historiales de estado de pieza
                .HasForeignKey(psh => psh.piece_id); // Clave foránea de pieza es piece_id

            modelBuilder.Entity<PieceStatusHistory>()
                .HasOne(psh => psh.Furniture) // Una relación historial de estado de pieza tiene un mueble
                .WithMany(f => f.PieceStatusHistories) // Un mueble tiene muchos historiales de estado de pieza
                .HasForeignKey(psh => psh.furniture_id); // Clave foránea de mueble es furniture_id

            modelBuilder.Entity<PieceStatusHistory>()
                .HasOne(psh => psh.User) // Una relación historial de estado de pieza tiene un usuario
                .WithMany(u => u.PieceStatusHistories) // Un usuario tiene muchos historiales de estado de pieza
                .HasForeignKey(psh => psh.user_id); // Clave foránea de usuario es user_id

            modelBuilder.Entity<PieceStatusHistory>()
                .HasOne(psh => psh.Unit) // Una relación historial de estado de pieza tiene una unidad
                .WithMany(u => u.PieceStatusHistories) // Una unidad tiene muchos historiales de estado de pieza
                .HasForeignKey(psh => psh.unit_id); // Clave foránea de unidad es unit_id

            modelBuilder.Entity<FurnitureStatusHistory>()
                .HasKey(fsh => fsh.id); // Definir clave primaria

            modelBuilder.Entity<FurnitureStatusHistory>()
                .HasOne(fsh => fsh.Furniture) // Una relación historial de estado de mueble tiene un mueble
                .WithMany(f => f.FurnitureStatusHistories) // Un mueble tiene muchos historiales de estado de mueble
                .HasForeignKey(fsh => fsh.furniture_id); // Clave foránea de mueble es furniture_id

            modelBuilder.Entity<FurnitureStatusHistory>()
                .HasOne(fsh => fsh.User) // Una relación historial de estado de mueble tiene un usuario
                .WithMany(u => u.FurnitureStatusHistories) // Un usuario tiene muchos historiales de estado de mueble
                .HasForeignKey(fsh => fsh.user_id); // Clave foránea de usuario es user_id

            modelBuilder.Entity<FurnitureStatusHistory>()
                .HasOne(fsh => fsh.Unit) // Una relación historial de estado de mueble tiene una unidad
                .WithMany(u => u.FurnitureStatusHistories) // Una unidad tiene muchos historiales de estado de mueble
                .HasForeignKey(fsh => fsh.unit_id); // Clave foránea de unidad es unit_id
        }
    }
}