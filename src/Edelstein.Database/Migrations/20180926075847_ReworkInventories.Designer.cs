﻿// <auto-generated />
using System;
using Edelstein.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Edelstein.Database.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20180926075847_ReworkInventories")]
    partial class ReworkInventories
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Edelstein.Database.Entities.Account", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password")
                        .HasMaxLength(128);

                    b.Property<string>("SPW")
                        .HasMaxLength(128);

                    b.Property<int>("State");

                    b.Property<string>("Username")
                        .HasMaxLength(13);

                    b.HasKey("ID");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Edelstein.Database.Entities.AccountData", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AccountID");

                    b.Property<int>("SlotCount");

                    b.Property<byte>("WorldID");

                    b.HasKey("ID");

                    b.HasIndex("AccountID");

                    b.ToTable("AccountData");
                });

            modelBuilder.Entity("Edelstein.Database.Entities.Character", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<short>("AP");

                    b.Property<int?>("AccountID");

                    b.Property<short>("DEX");

                    b.Property<int>("EXP");

                    b.Property<int>("Face");

                    b.Property<int>("FieldID");

                    b.Property<byte>("FieldPortal");

                    b.Property<byte>("Gender");

                    b.Property<int>("HP");

                    b.Property<int>("Hair");

                    b.Property<short>("INT");

                    b.Property<short>("Job");

                    b.Property<short>("LUK");

                    b.Property<byte>("Level");

                    b.Property<int>("MP");

                    b.Property<int>("MaxHP");

                    b.Property<int>("MaxMP");

                    b.Property<int>("Money");

                    b.Property<string>("Name")
                        .HasMaxLength(13);

                    b.Property<short>("POP");

                    b.Property<int>("PlayTime");

                    b.Property<short>("SP");

                    b.Property<short>("STR");

                    b.Property<byte>("Skin");

                    b.Property<short>("SubJob");

                    b.Property<int>("TempEXP");

                    b.Property<byte>("WorldID");

                    b.HasKey("ID");

                    b.HasIndex("AccountID");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("Edelstein.Database.Entities.FunctionKey", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Action");

                    b.Property<int?>("CharacterID");

                    b.Property<int>("Key");

                    b.Property<byte>("Type");

                    b.HasKey("ID");

                    b.HasIndex("CharacterID");

                    b.ToTable("FunctionKey");
                });

            modelBuilder.Entity("Edelstein.Database.Entities.Inventory.ItemInventory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CharacterID");

                    b.Property<byte>("SlotMax");

                    b.Property<byte>("Type");

                    b.HasKey("ID");

                    b.HasIndex("CharacterID");

                    b.ToTable("ItemInventory");
                });

            modelBuilder.Entity("Edelstein.Database.Entities.Inventory.ItemSlot", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateExpire");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int?>("ItemInventoryID");

                    b.Property<short>("Slot");

                    b.Property<int>("TemplateID");

                    b.HasKey("ID");

                    b.HasIndex("ItemInventoryID");

                    b.ToTable("ItemSlot");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ItemSlot");
                });

            modelBuilder.Entity("Edelstein.Database.Entities.Inventory.ItemSlotBundle", b =>
                {
                    b.HasBaseType("Edelstein.Database.Entities.Inventory.ItemSlot");

                    b.Property<short>("Attribute");

                    b.Property<short>("Number");

                    b.Property<string>("Title")
                        .HasMaxLength(13);

                    b.ToTable("ItemSlotBundle");

                    b.HasDiscriminator().HasValue("ItemSlotBundle");
                });

            modelBuilder.Entity("Edelstein.Database.Entities.Inventory.ItemSlotEquip", b =>
                {
                    b.HasBaseType("Edelstein.Database.Entities.Inventory.ItemSlot");

                    b.Property<short>("ACC");

                    b.Property<short>("Attribute")
                        .HasColumnName("ItemSlotEquip_Attribute");

                    b.Property<byte>("CHUC");

                    b.Property<byte>("CUC");

                    b.Property<short>("Craft");

                    b.Property<short>("DEX");

                    b.Property<int>("Durability");

                    b.Property<short>("EVA");

                    b.Property<int>("EXP");

                    b.Property<byte>("Grade");

                    b.Property<short>("INT");

                    b.Property<int>("IUC");

                    b.Property<short>("Jump");

                    b.Property<short>("LUK");

                    b.Property<byte>("Level");

                    b.Property<byte>("LevelUpType");

                    b.Property<short>("MAD");

                    b.Property<short>("MDD");

                    b.Property<short>("MaxHP");

                    b.Property<short>("MaxMP");

                    b.Property<short>("Option1");

                    b.Property<short>("Option2");

                    b.Property<short>("Option3");

                    b.Property<short>("PAD");

                    b.Property<short>("PDD");

                    b.Property<byte>("RUC");

                    b.Property<short>("STR");

                    b.Property<short>("Socket1");

                    b.Property<short>("Socket2");

                    b.Property<short>("Speed");

                    b.Property<string>("Title")
                        .HasColumnName("ItemSlotEquip_Title");

                    b.ToTable("ItemSlotEquip");

                    b.HasDiscriminator().HasValue("ItemSlotEquip");
                });

            modelBuilder.Entity("Edelstein.Database.Entities.AccountData", b =>
                {
                    b.HasOne("Edelstein.Database.Entities.Account")
                        .WithMany("Data")
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Edelstein.Database.Entities.Character", b =>
                {
                    b.HasOne("Edelstein.Database.Entities.Account", "Account")
                        .WithMany("Characters")
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Edelstein.Database.Entities.FunctionKey", b =>
                {
                    b.HasOne("Edelstein.Database.Entities.Character")
                        .WithMany("FunctionKeys")
                        .HasForeignKey("CharacterID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Edelstein.Database.Entities.Inventory.ItemInventory", b =>
                {
                    b.HasOne("Edelstein.Database.Entities.Character")
                        .WithMany("Inventories")
                        .HasForeignKey("CharacterID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Edelstein.Database.Entities.Inventory.ItemSlot", b =>
                {
                    b.HasOne("Edelstein.Database.Entities.Inventory.ItemInventory")
                        .WithMany("Items")
                        .HasForeignKey("ItemInventoryID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
