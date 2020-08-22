﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nexo.Data;

namespace Nexo.Migrations
{
    [DbContext(typeof(SystemContext))]
    [Migration("20200822005416_SextoCreate")]
    partial class SextoCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Nexo.Models.FornecedorModel", b =>
                {
                    b.Property<int>("ID_Fornecedor")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo");

                    b.Property<string>("CNPJ")
                        .IsRequired();

                    b.Property<DateTime>("Data_Cadastro");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.Property<int>("Status");

                    b.Property<int>("StatusId");

                    b.HasKey("ID_Fornecedor");

                    b.ToTable("Fornecedores");
                });

            modelBuilder.Entity("Nexo.Models.ProdutoModel", b =>
                {
                    b.Property<int>("Id_Produto")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Data_Cadastro");

                    b.Property<int>("ID_Fornecedor");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.Property<decimal>("Valor");

                    b.HasKey("Id_Produto");

                    b.HasIndex("ID_Fornecedor");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("Nexo.Models.ProdutoModel", b =>
                {
                    b.HasOne("Nexo.Models.FornecedorModel", "Fornecedor")
                        .WithMany("Produtos")
                        .HasForeignKey("ID_Fornecedor")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
