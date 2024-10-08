﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StockTrack_API.Data;

#nullable disable

namespace StockTrack_API.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StockTrack_API.Models.Entities.MovimentationEventEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Event")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MovimentationEventEntity");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Event = "ENTRY"
                        },
                        new
                        {
                            Id = 2,
                            Event = "EDIT"
                        },
                        new
                        {
                            Id = 3,
                            Event = "EXIT"
                        });
                });

            modelBuilder.Entity("StockTrack_API.Models.Entities.MovimentationTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MovimentationTypeEntity");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Type = "USER"
                        },
                        new
                        {
                            Id = 2,
                            Type = "AREA"
                        },
                        new
                        {
                            Id = 3,
                            Type = "WAREHOUSE"
                        },
                        new
                        {
                            Id = 4,
                            Type = "MATERIAL"
                        },
                        new
                        {
                            Id = 5,
                            Type = "LOAN"
                        },
                        new
                        {
                            Id = 6,
                            Type = "MAINTENANCE"
                        },
                        new
                        {
                            Id = 7,
                            Type = "GENERAL"
                        });
                });

            modelBuilder.Entity("StockTrack_API.Models.Entities.UserRoleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserRoleEntity");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Role = "USER"
                        },
                        new
                        {
                            Id = 2,
                            Role = "WAREHOUSEMAN"
                        },
                        new
                        {
                            Id = 3,
                            Role = "COORDINATOR"
                        },
                        new
                        {
                            Id = 4,
                            Role = "SUPPORT"
                        });
                });

            modelBuilder.Entity("StockTrack_API.Models.Interfaces.Area", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InstitutionId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("InstitutionId");

                    b.ToTable("ST_AREAS", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Active = true,
                            CreatedAt = new DateTime(2024, 10, 8, 3, 52, 18, 177, DateTimeKind.Utc).AddTicks(8198),
                            CreatedBy = "Admin",
                            Description = "Área de Testes",
                            InstitutionId = 1,
                            Name = "Teste",
                            UpdatedAt = new DateTime(2024, 10, 8, 3, 52, 18, 177, DateTimeKind.Utc).AddTicks(8199),
                            UpdatedBy = ""
                        });
                });

            modelBuilder.Entity("StockTrack_API.Models.Interfaces.Institution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccessCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Complement")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Neightboor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ST_INSTITUTIONS", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccessCode = "000",
                            CEP = "02110010",
                            City = "Sao Paulo",
                            Complement = "",
                            Name = "Servidor de testes",
                            Neightboor = "Vila Guilherme",
                            Nickname = "Testes",
                            State = "SP",
                            StreetName = "Rua Alcantara",
                            StreetNumber = "113"
                        },
                        new
                        {
                            Id = 2,
                            AccessCode = "064",
                            CEP = "02110010",
                            City = "Sao Paulo",
                            Complement = "",
                            Name = "Horácio Augusto da Silveira",
                            Neightboor = "Vila Guilherme",
                            Nickname = "ETEC Prof. Horácio",
                            State = "SP",
                            StreetName = "Rua Alcantara",
                            StreetNumber = "113"
                        });
                });

            modelBuilder.Entity("StockTrack_API.Models.Interfaces.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InstitutionId")
                        .HasColumnType("int");

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaterialType")
                        .HasColumnType("int");

                    b.Property<string>("Measure")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Quantity")
                        .HasColumnType("real");

                    b.Property<int>("RecordNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("InstitutionId");

                    b.ToTable("ST_MATERIALS", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Active = true,
                            CreatedAt = new DateTime(2024, 10, 8, 3, 52, 18, 177, DateTimeKind.Utc).AddTicks(8292),
                            CreatedBy = "",
                            Description = "Notebook ThinkPad",
                            ImageURL = "",
                            InstitutionId = 1,
                            Manufacturer = "ThinkPad",
                            MaterialType = 0,
                            Measure = "UN",
                            Name = "Notebook",
                            Quantity = 3f,
                            RecordNumber = 123456,
                            UpdatedAt = new DateTime(2024, 10, 8, 3, 52, 18, 177, DateTimeKind.Utc).AddTicks(8292),
                            UpdatedBy = ""
                        });
                });

            modelBuilder.Entity("StockTrack_API.Models.Interfaces.MaterialWarehouses", b =>
                {
                    b.Property<int>("MaterialId")
                        .HasColumnType("int");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("int");

                    b.HasKey("MaterialId", "WarehouseId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("ST_MATERIAL_WAREHOUSES", (string)null);
                });

            modelBuilder.Entity("StockTrack_API.Models.Interfaces.Movimentation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AreaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Event")
                        .HasColumnType("int");

                    b.Property<int>("InstitutionId")
                        .HasColumnType("int");

                    b.Property<int?>("MaterialId")
                        .HasColumnType("int");

                    b.Property<string>("MovimentationBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Quantity")
                        .HasColumnType("real");

                    b.Property<int>("Reason")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("WarehouseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.HasIndex("MaterialId");

                    b.HasIndex("UserId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("ST_MOVIMENTATIONS", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AreaId = 1,
                            Date = new DateTime(2024, 10, 8, 0, 52, 18, 177, DateTimeKind.Local).AddTicks(8316),
                            Description = "Adição de área \"Teste\"",
                            Event = 1,
                            InstitutionId = 1,
                            MovimentationBy = "Admin",
                            Name = "Área Teste",
                            Quantity = 1f,
                            Reason = 1,
                            Type = 2
                        });
                });

            modelBuilder.Entity("StockTrack_API.Models.Interfaces.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("AccessDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhotoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ST_USERS", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Active = true,
                            CreatedAt = new DateTime(2024, 10, 8, 0, 52, 18, 177, DateTimeKind.Local).AddTicks(8001),
                            Email = "admin@stocktrack.com",
                            Name = "Admin",
                            PasswordHash = new byte[] { 209, 100, 90, 65, 197, 0, 83, 167, 139, 52, 107, 131, 132, 167, 45, 109, 125, 14, 203, 48, 140, 130, 199, 109, 37, 38, 74, 0, 135, 28, 172, 102, 198, 29, 126, 230, 29, 145, 145, 229, 215, 7, 102, 160, 111, 2, 8, 155, 145, 89, 171, 84, 192, 132, 142, 14, 235, 141, 207, 223, 32, 144, 167, 244 },
                            PasswordSalt = new byte[] { 105, 66, 151, 21, 154, 231, 69, 218, 194, 160, 206, 65, 1, 228, 194, 250, 236, 93, 6, 155, 51, 247, 236, 200, 136, 151, 87, 243, 29, 186, 137, 246, 91, 158, 3, 31, 165, 242, 194, 110, 180, 47, 3, 209, 161, 54, 178, 87, 212, 95, 104, 3, 86, 119, 12, 181, 118, 233, 109, 183, 118, 115, 158, 132, 75, 80, 110, 142, 237, 158, 192, 21, 111, 55, 170, 97, 155, 168, 112, 47, 4, 64, 14, 108, 219, 69, 37, 19, 94, 211, 236, 120, 121, 238, 30, 248, 193, 166, 221, 53, 197, 253, 41, 231, 93, 44, 136, 57, 80, 50, 145, 135, 56, 234, 113, 203, 152, 224, 138, 61, 40, 72, 158, 64, 211, 50, 111, 182 },
                            PhotoUrl = "https://imgur.com/mOXzZLE.png"
                        });
                });

            modelBuilder.Entity("StockTrack_API.Models.Interfaces.UserInstitution", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("InstitutionId")
                        .HasColumnType("int");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.HasKey("UserId", "InstitutionId");

                    b.HasIndex("InstitutionId");

                    b.ToTable("ST_USER_INSTITUTIONS");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            InstitutionId = 1,
                            UserRole = 4
                        },
                        new
                        {
                            UserId = 1,
                            InstitutionId = 2,
                            UserRole = 3
                        });
                });

            modelBuilder.Entity("StockTrack_API.Models.Interfaces.Warehouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("AreaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InstitutionId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.HasIndex("InstitutionId");

                    b.ToTable("ST_WAREHOUSES", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Active = true,
                            AreaId = 1,
                            CreatedAt = new DateTime(2024, 10, 8, 3, 52, 18, 177, DateTimeKind.Utc).AddTicks(8254),
                            CreatedBy = "",
                            Description = "Almoxarifado de informática",
                            InstitutionId = 1,
                            Name = "Informática",
                            UpdatedAt = new DateTime(2024, 10, 8, 3, 52, 18, 177, DateTimeKind.Utc).AddTicks(8254),
                            UpdatedBy = ""
                        });
                });

            modelBuilder.Entity("StockTrack_API.Models.Interfaces.WarehouseUsers", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "WarehouseId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("ST_WAREHOUSE_USERS", (string)null);
                });

            modelBuilder.Entity("StockTrack_API.Models.MovimentationReasonEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MovimentationReasonEntity");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Reason = "INSERTION"
                        },
                        new
                        {
                            Id = 2,
                            Reason = "EDIT"
                        },
                        new
                        {
                            Id = 3,
                            Reason = "RETURNFROMLOAN"
                        },
                        new
                        {
                            Id = 4,
                            Reason = "RETURNFROMMAINTENANCE"
                        },
                        new
                        {
                            Id = 5,
                            Reason = "DISPOSAL"
                        },
                        new
                        {
                            Id = 6,
                            Reason = "LOAN"
                        },
                        new
                        {
                            Id = 7,
                            Reason = "SENTTOMAINTENANCE"
                        },
                        new
                        {
                            Id = 8,
                            Reason = "REMOVED"
                        },
                        new
                        {
                            Id = 9,
                            Reason = "OTHER"
                        });
                });

            modelBuilder.Entity("StockTrack_API.Models.Interfaces.Area", b =>
                {
                    b.HasOne("StockTrack_API.Models.Interfaces.Institution", "Institution")
                        .WithMany("Areas")
                        .HasForeignKey("InstitutionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Institution");
                });

            modelBuilder.Entity("StockTrack_API.Models.Interfaces.Material", b =>
                {
                    b.HasOne("StockTrack_API.Models.Interfaces.Institution", "Institution")
                        .WithMany()
                        .HasForeignKey("InstitutionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Institution");
                });

            modelBuilder.Entity("StockTrack_API.Models.Interfaces.MaterialWarehouses", b =>
                {
                    b.HasOne("StockTrack_API.Models.Interfaces.Material", "Material")
                        .WithMany("MaterialWarehouses")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("StockTrack_API.Models.Interfaces.Warehouse", "Warehouse")
                        .WithMany("MaterialWarehouses")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Material");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("StockTrack_API.Models.Interfaces.Movimentation", b =>
                {
                    b.HasOne("StockTrack_API.Models.Interfaces.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId");

                    b.HasOne("StockTrack_API.Models.Interfaces.Material", "Material")
                        .WithMany()
                        .HasForeignKey("MaterialId");

                    b.HasOne("StockTrack_API.Models.Interfaces.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.HasOne("StockTrack_API.Models.Interfaces.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId");

                    b.Navigation("Area");

                    b.Navigation("Material");

                    b.Navigation("User");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("StockTrack_API.Models.Interfaces.UserInstitution", b =>
                {
                    b.HasOne("StockTrack_API.Models.Interfaces.Institution", "Institution")
                        .WithMany("Users")
                        .HasForeignKey("InstitutionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StockTrack_API.Models.Interfaces.User", "User")
                        .WithMany("UserInstitutions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Institution");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StockTrack_API.Models.Interfaces.Warehouse", b =>
                {
                    b.HasOne("StockTrack_API.Models.Interfaces.Area", "Area")
                        .WithMany("Warehouses")
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("StockTrack_API.Models.Interfaces.Institution", "Institution")
                        .WithMany()
                        .HasForeignKey("InstitutionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Area");

                    b.Navigation("Institution");
                });

            modelBuilder.Entity("StockTrack_API.Models.Interfaces.WarehouseUsers", b =>
                {
                    b.HasOne("StockTrack_API.Models.Interfaces.User", "User")
                        .WithMany("WarehouseUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("StockTrack_API.Models.Interfaces.Warehouse", "Warehouse")
                        .WithMany("Warehousemans")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("StockTrack_API.Models.Interfaces.Area", b =>
                {
                    b.Navigation("Warehouses");
                });

            modelBuilder.Entity("StockTrack_API.Models.Interfaces.Institution", b =>
                {
                    b.Navigation("Areas");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("StockTrack_API.Models.Interfaces.Material", b =>
                {
                    b.Navigation("MaterialWarehouses");
                });

            modelBuilder.Entity("StockTrack_API.Models.Interfaces.User", b =>
                {
                    b.Navigation("UserInstitutions");

                    b.Navigation("WarehouseUsers");
                });

            modelBuilder.Entity("StockTrack_API.Models.Interfaces.Warehouse", b =>
                {
                    b.Navigation("MaterialWarehouses");

                    b.Navigation("Warehousemans");
                });
#pragma warning restore 612, 618
        }
    }
}
