﻿// <auto-generated />
using System;
using ControleVendas.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace controle_vendas.Migrations
{
    [DbContext(typeof(AppDbConnectionContext))]
    [Migration("20250216020538_AlterTableColumnPedidoId")]
    partial class AlterTableColumnPedidoId
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ControleVendas.Modules.Categoria.Models.Entity.CategoriaEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)")
                        .HasColumnName("nome");

                    b.HasKey("Id");

                    b.HasIndex("Nome")
                        .IsUnique();

                    b.ToTable("categorias");
                });

            modelBuilder.Entity("ControleVendas.Modules.Cliente.Models.Entity.ClienteEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean")
                        .HasColumnName("ativo");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(180)
                        .HasColumnType("character varying(180)")
                        .HasColumnName("nome");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("telefone");

                    b.HasKey("Id");

                    b.HasIndex("Telefone")
                        .IsUnique();

                    b.ToTable("clientes");
                });

            modelBuilder.Entity("ControleVendas.Modules.Fornecedor.Models.Entity.FornecedorEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)")
                        .HasColumnName("nome");

                    b.HasKey("Id");

                    b.HasIndex("Nome")
                        .IsUnique();

                    b.ToTable("fornecedores");
                });

            modelBuilder.Entity("ControleVendas.Modules.ItemPedido.models.Entity.ItemPedidoEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("LucroItem")
                        .HasColumnType("numeric")
                        .HasColumnName("lucro_item");

                    b.Property<int?>("PedidoEntityId")
                        .HasColumnType("integer");

                    b.Property<int>("PedidoId")
                        .HasColumnType("integer")
                        .HasColumnName("pedido_id");

                    b.Property<decimal>("PrecoTotal")
                        .HasColumnType("numeric")
                        .HasColumnName("preco_total");

                    b.Property<decimal>("PrecoUnitario")
                        .HasColumnType("numeric")
                        .HasColumnName("preco_unitario");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("integer")
                        .HasColumnName("produto_id");

                    b.Property<int>("Quantidade")
                        .HasColumnType("integer")
                        .HasColumnName("quantidade");

                    b.HasKey("Id");

                    b.HasIndex("PedidoEntityId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("itens_pedidos");
                });

            modelBuilder.Entity("ControleVendas.Modules.Pedido.Models.Entity.PedidoEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClienteId")
                        .HasColumnType("integer")
                        .HasColumnName("cliente_id");

                    b.Property<DateTime>("DataVenda")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("data_venda");

                    b.Property<decimal>("Desconto")
                        .HasColumnType("numeric")
                        .HasColumnName("desconto");

                    b.Property<string>("FormaPagamento")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("forma_pagamento");

                    b.Property<int>("NumeroParcelas")
                        .HasColumnType("integer")
                        .HasColumnName("numero_parcelas");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.Property<decimal>("ValorPago")
                        .HasColumnType("numeric")
                        .HasColumnName("valor_pago");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("numeric")
                        .HasColumnName("valor_total");

                    b.Property<string>("VendedorId")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("vendedor_id");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("VendedorId");

                    b.ToTable("pedidos");
                });

            modelBuilder.Entity("ControleVendas.Modules.Produto.Models.Entity.ProdutoEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoriaId")
                        .HasColumnType("integer")
                        .HasColumnName("categoria_id");

                    b.Property<DateTime>("DataCadastro")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("data_cadastro");

                    b.Property<string>("Descricao")
                        .HasMaxLength(280)
                        .HasColumnType("character varying(280)")
                        .HasColumnName("descricao");

                    b.Property<int>("Estoque")
                        .HasColumnType("integer")
                        .HasColumnName("estoque");

                    b.Property<int>("FornecedorId")
                        .HasColumnType("integer")
                        .HasColumnName("fornecedor_id");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)")
                        .HasColumnName("nome");

                    b.Property<decimal>("ValorCompra")
                        .HasColumnType("numeric")
                        .HasColumnName("valor_compra");

                    b.Property<decimal>("ValorVenda")
                        .HasColumnType("numeric")
                        .HasColumnName("valor_venda");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("FornecedorId");

                    b.HasIndex("Nome")
                        .IsUnique();

                    b.ToTable("produtos");
                });

            modelBuilder.Entity("ControleVendas.Modules.User.Models.Entity.ApplicationUserEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("RefreshToken")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ControleVendas.Modules.ItemPedido.models.Entity.ItemPedidoEntity", b =>
                {
                    b.HasOne("ControleVendas.Modules.Pedido.Models.Entity.PedidoEntity", null)
                        .WithMany("Itens")
                        .HasForeignKey("PedidoEntityId");

                    b.HasOne("ControleVendas.Modules.Produto.Models.Entity.ProdutoEntity", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("ControleVendas.Modules.Pedido.Models.Entity.PedidoEntity", b =>
                {
                    b.HasOne("ControleVendas.Modules.Cliente.Models.Entity.ClienteEntity", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ControleVendas.Modules.User.Models.Entity.ApplicationUserEntity", "Vendedor")
                        .WithMany()
                        .HasForeignKey("VendedorId");

                    b.Navigation("Cliente");

                    b.Navigation("Vendedor");
                });

            modelBuilder.Entity("ControleVendas.Modules.Produto.Models.Entity.ProdutoEntity", b =>
                {
                    b.HasOne("ControleVendas.Modules.Categoria.Models.Entity.CategoriaEntity", "Categoria")
                        .WithMany("Produtos")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ControleVendas.Modules.Fornecedor.Models.Entity.FornecedorEntity", "Fornecedor")
                        .WithMany()
                        .HasForeignKey("FornecedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("Fornecedor");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ControleVendas.Modules.User.Models.Entity.ApplicationUserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ControleVendas.Modules.User.Models.Entity.ApplicationUserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ControleVendas.Modules.User.Models.Entity.ApplicationUserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ControleVendas.Modules.User.Models.Entity.ApplicationUserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ControleVendas.Modules.Categoria.Models.Entity.CategoriaEntity", b =>
                {
                    b.Navigation("Produtos");
                });

            modelBuilder.Entity("ControleVendas.Modules.Pedido.Models.Entity.PedidoEntity", b =>
                {
                    b.Navigation("Itens");
                });
#pragma warning restore 612, 618
        }
    }
}
