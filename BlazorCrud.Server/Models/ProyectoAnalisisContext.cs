using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Models;

public partial class ProyectoAnalisisContext : DbContext
{
    public ProyectoAnalisisContext()
    {
    }

    public ProyectoAnalisisContext(DbContextOptions<ProyectoAnalisisContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BitacoraAcceso> BitacoraAccesos { get; set; }

    public virtual DbSet<DocumentoPersona> DocumentoPersonas { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<EstadoCivil> EstadoCivils { get; set; }

    public virtual DbSet<FechaActiva> FechaActivas { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Modulo> Modulos { get; set; }

    public virtual DbSet<MovimientoCuentum> MovimientoCuenta { get; set; }

    public virtual DbSet<Opcion> Opcions { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleOpcion> RoleOpcions { get; set; }

    public virtual DbSet<SaldoCuentum> SaldoCuenta { get; set; }

    public virtual DbSet<StatusCuentum> StatusCuenta { get; set; }

    public virtual DbSet<StatusUsuario> StatusUsuarios { get; set; }

    public virtual DbSet<Sucursal> Sucursals { get; set; }

    public virtual DbSet<TipoAcceso> TipoAccesos { get; set; }

    public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; }

    public virtual DbSet<TipoMovimientoCxc> TipoMovimientoCxcs { get; set; }

    public virtual DbSet<TipoSaldoCuentum> TipoSaldoCuenta { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioPreguntum> UsuarioPregunta { get; set; }

    public virtual DbSet<UsuarioRole> UsuarioRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BitacoraAcceso>(entity =>
        {
            entity.HasKey(e => e.IdBitacoraAcceso).HasName("PK__BITACORA__C22A371A13C0BDC3");

            entity.ToTable("BITACORA_ACCESO");

            entity.Property(e => e.Accion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Browser)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DireccionIp)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Dispositivo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaAcceso).HasColumnType("datetime");
            entity.Property(e => e.HttpUserAgent)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IdUsuario)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Sesion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SistemaOperativo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdTipoAccesoNavigation).WithMany(p => p.BitacoraAccesos)
                .HasForeignKey(d => d.IdTipoAcceso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BITACORA___IdTip__03F0984C");
        });

        modelBuilder.Entity<DocumentoPersona>(entity =>
        {
            entity.HasKey(e => new { e.IdTipoDocumento, e.IdPersona }).HasName("PK__DOCUMENT__E85FBE058136FE18");

            entity.ToTable("DOCUMENTO_PERSONA");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.NoDocumento)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.DocumentoPersonas)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DOCUMENTO__IdPer__1F98B2C1");

            entity.HasOne(d => d.IdTipoDocumentoNavigation).WithMany(p => p.DocumentoPersonas)
                .HasForeignKey(d => d.IdTipoDocumento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DOCUMENTO__IdTip__1EA48E88");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.IdEmpresa).HasName("PK__EMPRESA__5EF4033E5C4C60FE");

            entity.ToTable("EMPRESA");

            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Nit)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstadoCivil>(entity =>
        {
            entity.HasKey(e => e.IdEstadoCivil).HasName("PK__ESTADO_C__889DE1B29ED23AFE");

            entity.ToTable("ESTADO_CIVIL");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FechaActiva>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("FECHA_ACTIVA");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.IdGenero).HasName("PK__GENERO__0F834988AC5994D4");

            entity.ToTable("GENERO");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.IdMenu).HasName("PK__MENU__4D7EA8E131A6D5FA");

            entity.ToTable("MENU");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdModuloNavigation).WithMany(p => p.Menus)
                .HasForeignKey(d => d.IdModulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MENU__IdModulo__76969D2E");
        });

        modelBuilder.Entity<Modulo>(entity =>
        {
            entity.HasKey(e => e.IdModulo).HasName("PK__MODULO__D9F153150D657CB1");

            entity.ToTable("MODULO");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MovimientoCuentum>(entity =>
        {
            entity.HasKey(e => e.IdMovimientoCuenta).HasName("PK__MOVIMIEN__191CB46B751FF0C2");

            entity.ToTable("MOVIMIENTO_CUENTA");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaMovimiento).HasColumnType("datetime");
            entity.Property(e => e.IdTipoMovimientoCxc).HasColumnName("IdTipoMovimientoCXC");
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ValorMovimiento).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ValorMovimientoPagado).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdSaldoCuentaNavigation).WithMany(p => p.MovimientoCuenta)
                .HasForeignKey(d => d.IdSaldoCuenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MOVIMIENT__IdSal__2CF2ADDF");

            entity.HasOne(d => d.IdTipoMovimientoCxcNavigation).WithMany(p => p.MovimientoCuenta)
                .HasForeignKey(d => d.IdTipoMovimientoCxc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MOVIMIENT__IdTip__2DE6D218");
        });

        modelBuilder.Entity<Opcion>(entity =>
        {
            entity.HasKey(e => e.IdOpcion).HasName("PK__OPCION__4F238858665509E6");

            entity.ToTable("OPCION");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Pagina)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdMenuNavigation).WithMany(p => p.Opcions)
                .HasForeignKey(d => d.IdMenu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OPCION__IdMenu__7B5B524B");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__PERSONA__2EC8D2AC74AD94E4");

            entity.ToTable("PERSONA");

            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEstadoCivilNavigation).WithMany(p => p.Personas)
                .HasForeignKey(d => d.IdEstadoCivil)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PERSONA__IdEstad__1BC821DD");

            entity.HasOne(d => d.IdGeneroNavigation).WithMany(p => p.Personas)
                .HasForeignKey(d => d.IdGenero)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PERSONA__IdGener__1AD3FDA4");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PK__ROLE__B436905412389A57");

            entity.ToTable("ROLE");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RoleOpcion>(entity =>
        {
            entity.HasKey(e => new { e.IdRole, e.IdOpcion }).HasName("PK__ROLE_OPC__20C4A8D1DBCEBF68");

            entity.ToTable("ROLE_OPCION");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdOpcionNavigation).WithMany(p => p.RoleOpcions)
                .HasForeignKey(d => d.IdOpcion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ROLE_OPCI__IdOpc__7F2BE32F");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.RoleOpcions)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ROLE_OPCI__IdRol__7E37BEF6");
        });

        modelBuilder.Entity<SaldoCuentum>(entity =>
        {
            entity.HasKey(e => e.IdSaldoCuenta).HasName("PK__SALDO_CU__7F709A4ADACDB9DF");

            entity.ToTable("SALDO_CUENTA");

            entity.Property(e => e.Creditos).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Debitos).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.SaldoAnterior).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.SaldoCuenta)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SALDO_CUE__IdPer__2645B050");

            entity.HasOne(d => d.IdStatusCuentaNavigation).WithMany(p => p.SaldoCuenta)
                .HasForeignKey(d => d.IdStatusCuenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SALDO_CUE__IdSta__2739D489");

            entity.HasOne(d => d.IdTipoSaldoCuentaNavigation).WithMany(p => p.SaldoCuenta)
                .HasForeignKey(d => d.IdTipoSaldoCuenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SALDO_CUE__IdTip__282DF8C2");
        });

        modelBuilder.Entity<StatusCuentum>(entity =>
        {
            entity.HasKey(e => e.IdStatusCuenta).HasName("PK__STATUS_C__2CABBC3FAC226AD1");

            entity.ToTable("STATUS_CUENTA");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StatusUsuario>(entity =>
        {
            entity.HasKey(e => e.IdStatusUsuario).HasName("PK__STATUS_U__26C64044C9F59573");

            entity.ToTable("STATUS_USUARIO");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.HasKey(e => e.IdSucursal).HasName("PK__SUCURSAL__BFB6CD996BCF2F3D");

            entity.ToTable("SUCURSAL");

            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Sucursals)
                .HasForeignKey(d => d.IdEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SUCURSAL__IdEmpr__5EBF139D");
        });

        modelBuilder.Entity<TipoAcceso>(entity =>
        {
            entity.HasKey(e => e.IdTipoAcceso).HasName("PK__TIPO_ACC__F55E50EC6982DEBC");

            entity.ToTable("TIPO_ACCESO");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoDocumento>(entity =>
        {
            entity.HasKey(e => e.IdTipoDocumento).HasName("PK__TIPO_DOC__3AB3332FC48FD175");

            entity.ToTable("TIPO_DOCUMENTO");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoMovimientoCxc>(entity =>
        {
            entity.HasKey(e => e.IdTipoMovimientoCxc).HasName("PK__TIPO_MOV__98F2895FD621FC2A");

            entity.ToTable("TIPO_MOVIMIENTO_CXC");

            entity.Property(e => e.IdTipoMovimientoCxc).HasColumnName("IdTipoMovimientoCXC");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoSaldoCuentum>(entity =>
        {
            entity.HasKey(e => e.IdTipoSaldoCuenta).HasName("PK__TIPO_SAL__0EE0B2EC3667F644");

            entity.ToTable("TIPO_SALDO_CUENTA");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__USUARIO__5B65BF9775F0A40A");

            entity.ToTable("USUARIO");

            entity.Property(e => e.IdUsuario)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SesionActual)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TelefonoMovil)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.UltimaFechaCambioPassword).HasColumnType("datetime");
            entity.Property(e => e.UltimaFechaIngreso).HasColumnType("datetime");
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdGeneroNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdGenero)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__USUARIO__IdGener__68487DD7");

            entity.HasOne(d => d.IdStatusUsuarioNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdStatusUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__USUARIO__IdStatu__6754599E");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdSucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__USUARIO__IdSucur__693CA210");
        });

        modelBuilder.Entity<UsuarioPreguntum>(entity =>
        {
            entity.HasKey(e => e.IdPregunta).HasName("PK__USUARIO___754EC09E09C600EA");

            entity.ToTable("USUARIO_PREGUNTA");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.IdUsuario)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Pregunta)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Respuesta)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuarioPregunta)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__USUARIO_P__IdUsu__6C190EBB");
        });

        modelBuilder.Entity<UsuarioRole>(entity =>
        {
            entity.HasKey(e => new { e.IdUsuario, e.IdRole }).HasName("PK__USUARIO___0026D69249808086");

            entity.ToTable("USUARIO_ROLE");

            entity.Property(e => e.IdUsuario)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.UsuarioRoles)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__USUARIO_R__IdRol__71D1E811");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuarioRoles)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__USUARIO_R__IdUsu__70DDC3D8");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
