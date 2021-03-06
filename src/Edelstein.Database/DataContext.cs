using System.Linq;
using Edelstein.Database.Entities;
using Edelstein.Database.Entities.Inventory;
using Edelstein.Database.Entities.Shop;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Edelstein.Database
{
    public class DataContext : DbContext
    {
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Character> Characters { get; set; }

        public virtual DbSet<NPCShop> NPCShops { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemSlotEquip>().HasBaseType<ItemSlot>();
            modelBuilder.Entity<ItemSlotBundle>().HasBaseType<ItemSlot>();
            modelBuilder.Entity<ItemSlotPet>().HasBaseType<ItemSlot>();

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Data)
                .WithOne(a => a.Account)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<AccountData>()
                .HasMany(a => a.Characters)
                .WithOne(c => c.Data)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Character>()
                .HasMany(c => c.FunctionKeys)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Character>()
                .HasMany(c => c.QuickslotKeys)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Character>()
                .HasMany(c => c.Macros)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Character>()
                .HasMany(c => c.Inventories)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ItemInventory>()
                .HasMany(i => i.Items)
                .WithOne(i => i.ItemInventory)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<NPCShop>()
                .HasMany(s => s.Items)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }

        public void InsertUpdateOrDeleteGraph(object entity)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            if (entity is Character character)
            {
                var existing = Characters
                    .AsNoTracking()
                    .Include(c => c.FunctionKeys)
                    .Include(c => c.QuickslotKeys)
                    .Include(c => c.Macros)
                    .Include(c => c.Inventories)
                    .ThenInclude(c => c.Items)
                    .Include(c => c.SkillRecords)
                    .FirstOrDefault(c => c.ID == character.ID);

                if (existing != null)
                {
                    foreach (var functionKey in existing.FunctionKeys)
                    {
                        if (character.FunctionKeys.All(f => f.ID != functionKey.ID))
                            Entry(functionKey).State = EntityState.Deleted;
                    }
                    
                    foreach (var quickslotKey in existing.QuickslotKeys)
                    {
                        if (character.QuickslotKeys.All(f => f.ID != quickslotKey.ID))
                            Entry(quickslotKey).State = EntityState.Deleted;
                    }

                    foreach (var macro in existing.Macros)
                    {
                        if (character.Macros.All(f => f.ID != macro.ID))
                            Entry(macro).State = EntityState.Deleted;
                    }

                    var existingItems = existing.Inventories.SelectMany(i => i.Items).ToList();
                    var currentItems = character.Inventories.SelectMany(i => i.Items).ToList();

                    foreach (var existingItem in existingItems)
                    {
                        if (currentItems.All(i => i.ID != existingItem.ID))
                            Entry(existingItem).State = EntityState.Deleted;
                    }

                    foreach (var skillRecord in existing.SkillRecords)
                    {
                        if (character.SkillRecords.All(s => s.ID != skillRecord.ID))
                            Entry(skillRecord).State = EntityState.Deleted;
                    }
                }
            }
        }

        public override EntityEntry<TEntity> Update<TEntity>(TEntity entity)
        {
            InsertUpdateOrDeleteGraph(entity);
            return base.Update(entity);
        }
    }
}