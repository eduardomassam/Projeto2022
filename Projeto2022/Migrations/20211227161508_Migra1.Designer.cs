﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Projeto2022;

#nullable disable

namespace Projeto2022.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20211227161508_Migra1")]
    partial class Migra1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Projeto.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeID"), 1L, 1);

                    b.Property<string>("DasID")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("varchar(90)");

                    b.Property<string>("defeitos")
                        .IsRequired()
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("qualidades")
                        .IsRequired()
                        .HasColumnType("varchar(1000)");

                    b.HasKey("EmployeeID");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Projeto.Models.Skill", b =>
                {
                    b.Property<int>("SkillID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SkillID"), 1L, 1);

                    b.Property<string>("SkillName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("controlaSkill")
                        .HasColumnType("int");

                    b.HasKey("SkillID");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("Projeto.Models.SkillsFuncionario", b =>
                {
                    b.Property<int>("idSkillFuncionario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idSkillFuncionario"), 1L, 1);

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<int>("SkillID")
                        .HasColumnType("int");

                    b.Property<string>("observacoes")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("tempExp")
                        .IsRequired()
                        .HasColumnType("varchar(2)");

                    b.HasKey("idSkillFuncionario");

                    b.HasIndex("EmployeeID");

                    b.HasIndex("SkillID");

                    b.ToTable("SkillsFuncionarios");
                });

            modelBuilder.Entity("Projeto.Models.Usuario", b =>
                {
                    b.Property<int>("idLogin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idLogin"), 1L, 1);

                    b.Property<int>("controlaSkill")
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("usuario")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("idLogin");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Projeto.Models.SkillsFuncionario", b =>
                {
                    b.HasOne("Projeto.Models.Employee", "fk_idFuncionario")
                        .WithMany()
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Projeto.Models.Skill", "fk_idSkill")
                        .WithMany()
                        .HasForeignKey("SkillID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("fk_idFuncionario");

                    b.Navigation("fk_idSkill");
                });
#pragma warning restore 612, 618
        }
    }
}
