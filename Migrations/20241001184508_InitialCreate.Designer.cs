﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StockTrack_API.Data;

#nullable disable

namespace StockTrack_API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20241001184508_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                            Event = "Area"
                        },
                        new
                        {
                            Id = 2,
                            Event = "Warehouse"
                        },
                        new
                        {
                            Id = 3,
                            Event = "Material"
                        },
                        new
                        {
                            Id = 4,
                            Event = "Loan"
                        },
                        new
                        {
                            Id = 5,
                            Event = "Maintenance"
                        },
                        new
                        {
                            Id = 6,
                            Event = "General"
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
                            Type = "ENTRY"
                        },
                        new
                        {
                            Id = 2,
                            Type = "EDIT"
                        },
                        new
                        {
                            Id = 3,
                            Type = "EXIT"
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
                            CreatedAt = new DateTime(2024, 10, 1, 18, 45, 8, 385, DateTimeKind.Utc).AddTicks(3671),
                            CreatedBy = "Admin",
                            Description = "Área de Testes",
                            InstitutionId = 1,
                            Name = "Teste",
                            UpdatedAt = new DateTime(2024, 10, 1, 18, 45, 8, 385, DateTimeKind.Utc).AddTicks(3672),
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
                            AccessCode = "",
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
                            Id = 64,
                            AccessCode = "",
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
                            CreatedAt = new DateTime(2024, 10, 1, 18, 45, 8, 385, DateTimeKind.Utc).AddTicks(3713),
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
                            UpdatedAt = new DateTime(2024, 10, 1, 18, 45, 8, 385, DateTimeKind.Utc).AddTicks(3714),
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
                            Date = new DateTime(2024, 10, 1, 15, 45, 8, 385, DateTimeKind.Local).AddTicks(3736),
                            Description = "Adição de área \"Teste\"",
                            Event = 1,
                            InstitutionId = 1,
                            MovimentationBy = "Admin",
                            Name = "Área Teste",
                            Quantity = 1f,
                            Reason = 1,
                            Type = 1
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
                            CreatedAt = new DateTime(2024, 10, 1, 15, 45, 8, 385, DateTimeKind.Local).AddTicks(3466),
                            Email = "admin@stocktrack.com",
                            Name = "Admin",
                            PasswordHash = new byte[] { 227, 146, 207, 225, 136, 153, 44, 27, 230, 115, 198, 188, 159, 42, 84, 162, 57, 42, 56, 45, 0, 153, 55, 73, 219, 133, 170, 55, 105, 214, 208, 1, 146, 176, 179, 161, 51, 138, 187, 133, 157, 54, 188, 20, 27, 209, 254, 253, 0, 204, 242, 213, 144, 175, 158, 51, 71, 223, 247, 165, 206, 173, 149, 70 },
                            PasswordSalt = new byte[] { 156, 194, 69, 168, 40, 97, 150, 216, 62, 177, 244, 190, 15, 22, 18, 159, 20, 88, 157, 75, 13, 83, 14, 196, 64, 8, 26, 119, 120, 91, 182, 244, 148, 188, 121, 202, 121, 207, 76, 203, 15, 109, 76, 134, 213, 24, 216, 123, 27, 45, 89, 6, 158, 191, 49, 97, 137, 234, 239, 25, 144, 58, 232, 217, 68, 248, 214, 123, 158, 5, 207, 100, 170, 101, 140, 106, 153, 116, 160, 21, 135, 32, 209, 189, 106, 148, 171, 156, 20, 147, 231, 83, 159, 62, 134, 81, 49, 153, 233, 212, 183, 11, 121, 54, 167, 132, 156, 75, 11, 161, 44, 2, 195, 83, 130, 24, 36, 113, 164, 72, 233, 211, 14, 197, 23, 82, 112, 98 },
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
                            InstitutionId = 64,
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
                            CreatedAt = new DateTime(2024, 10, 1, 18, 45, 8, 385, DateTimeKind.Utc).AddTicks(3688),
                            CreatedBy = "",
                            Description = "Almoxarifado de informática",
                            InstitutionId = 1,
                            Name = "Informática",
                            UpdatedAt = new DateTime(2024, 10, 1, 18, 45, 8, 385, DateTimeKind.Utc).AddTicks(3689),
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
                            Reason = "Insertion"
                        },
                        new
                        {
                            Id = 2,
                            Reason = "Edit"
                        },
                        new
                        {
                            Id = 3,
                            Reason = "ReturnFromLoan"
                        },
                        new
                        {
                            Id = 4,
                            Reason = "ReturnFromMaintenance"
                        },
                        new
                        {
                            Id = 5,
                            Reason = "Disposal"
                        },
                        new
                        {
                            Id = 6,
                            Reason = "Loan"
                        },
                        new
                        {
                            Id = 7,
                            Reason = "SentToMaintenance"
                        },
                        new
                        {
                            Id = 8,
                            Reason = "Removed"
                        },
                        new
                        {
                            Id = 9,
                            Reason = "Other"
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
                        .WithMany("UserInstitutions")
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
                        .WithMany("WarehouseUsers")
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

                    b.Navigation("UserInstitutions");
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

                    b.Navigation("WarehouseUsers");
                });
#pragma warning restore 612, 618
        }
    }
}