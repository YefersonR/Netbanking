﻿// <auto-generated />
using System;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(NetBankingContext))]
    partial class NetBankingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Core.Domain.Entities.Beneficiary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("AccountBeneficiary")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserCardCredit")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AccountBeneficiary");

                    b.HasIndex("UserCardCredit");

                    b.ToTable("Beneficiaries");
                });

            modelBuilder.Entity("Core.Domain.Entities.CreditCard", b =>
                {
                    b.Property<Guid>("CardNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Debt")
                        .HasColumnType("real");

                    b.Property<float>("Limit")
                        .HasColumnType("real");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CardNumber");

                    b.HasIndex("AccountUser");

                    b.ToTable("CreditCards");
                });

            modelBuilder.Entity("Core.Domain.Entities.Loans", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("AccountUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountUser");

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("Core.Domain.Entities.SavingsAccount", b =>
                {
                    b.Property<Guid>("AccountNumber")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AccountNumber");

                    b.ToTable("SavingsAccounts");
                });

            modelBuilder.Entity("Core.Domain.Entities.Transations", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("AccountNumber")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AccountToPayAccountNumber")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("NumberAccountToPay")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserCardCredit")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AccountNumber");

                    b.HasIndex("AccountToPayAccountNumber");

                    b.HasIndex("UserCardCredit");

                    b.ToTable("Transations");
                });

            modelBuilder.Entity("Core.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("CardCredit")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Identification")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PrincipalSavingAccountAccountNumber")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SavingAccount")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CardCredit");

                    b.HasIndex("PrincipalSavingAccountAccountNumber");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Core.Domain.Entities.Beneficiary", b =>
                {
                    b.HasOne("Core.Domain.Entities.SavingsAccount", "BeneficiaryUser")
                        .WithMany("Beneficiaries")
                        .HasForeignKey("AccountBeneficiary")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.Entities.User", "User")
                        .WithMany("Beneficiary")
                        .HasForeignKey("UserCardCredit");

                    b.Navigation("BeneficiaryUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Domain.Entities.CreditCard", b =>
                {
                    b.HasOne("Core.Domain.Entities.User", "User")
                        .WithMany("CreditCard")
                        .HasForeignKey("AccountUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Domain.Entities.Loans", b =>
                {
                    b.HasOne("Core.Domain.Entities.User", "User")
                        .WithMany("Loans")
                        .HasForeignKey("AccountUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Domain.Entities.SavingsAccount", b =>
                {
                    b.HasOne("Core.Domain.Entities.User", "User")
                        .WithMany("SavingsAccount")
                        .HasForeignKey("AccountNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Domain.Entities.Transations", b =>
                {
                    b.HasOne("Core.Domain.Entities.SavingsAccount", "User")
                        .WithMany("Transations")
                        .HasForeignKey("AccountNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.Entities.SavingsAccount", "AccountToPay")
                        .WithMany()
                        .HasForeignKey("AccountToPayAccountNumber");

                    b.HasOne("Core.Domain.Entities.User", null)
                        .WithMany("Transations")
                        .HasForeignKey("UserCardCredit");

                    b.Navigation("AccountToPay");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Domain.Entities.User", b =>
                {
                    b.HasOne("Core.Domain.Entities.SavingsAccount", "PrincipalSavingAccount")
                        .WithMany()
                        .HasForeignKey("PrincipalSavingAccountAccountNumber");

                    b.Navigation("PrincipalSavingAccount");
                });

            modelBuilder.Entity("Core.Domain.Entities.SavingsAccount", b =>
                {
                    b.Navigation("Beneficiaries");

                    b.Navigation("Transations");
                });

            modelBuilder.Entity("Core.Domain.Entities.User", b =>
                {
                    b.Navigation("Beneficiary");

                    b.Navigation("CreditCard");

                    b.Navigation("Loans");

                    b.Navigation("SavingsAccount");

                    b.Navigation("Transations");
                });
#pragma warning restore 612, 618
        }
    }
}
