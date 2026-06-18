using ComponentesComputadoras.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComponentesComputadoras.Datos
{
    using ComponentesComputadoras.Entities.MicrosoftIdentity;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class DbDataAccess : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbDataAccess(DbContextOptions<DbDataAccess> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<TipoProducto> TiposProductos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<SocioNegocio> SociosNegocio { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<VentaDetalle> VentaDetalles { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<CompraDetalle> CompraDetalles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CompraDetalle>()
                .Property(cd => cd.PrecioUnitario)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasPrecision(18, 2);

            modelBuilder.Entity<VentaDetalle>()
                .Property(vd => vd.PrecioUnitario)
                .HasPrecision(18, 2);

            
            modelBuilder.Entity<CompraDetalle>()
     .HasOne(cd => cd.Compra)
     .WithMany(c => c.Detalles)
     .HasForeignKey(cd => cd.CompraId)
     .OnDelete(DeleteBehavior.Cascade);   

            modelBuilder.Entity<CompraDetalle>()
                .HasOne(cd => cd.Producto)
                .WithMany()
                .HasForeignKey(cd => cd.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);  


            modelBuilder.Entity<VentaDetalle>()
     .HasOne(vd => vd.Venta)
     .WithMany(v => v.Detalles)
     .HasForeignKey(vd => vd.VentaId)
     .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VentaDetalle>()
                .HasOne(vd => vd.Producto)
                .WithMany()
                .HasForeignKey(vd => vd.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);


            // Relaciones adicionales de Producto
            modelBuilder.Entity<Producto>()
     .HasOne(p => p.Proveedor)
     .WithMany(pr => pr.Productos) 
     .HasForeignKey(p => p.ProveedorId)
     .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Producto>()
                .HasOne(p => p.TipoProducto)
                .WithMany(tp => tp.Productos) 
                .HasForeignKey(p => p.TipoProductoId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
