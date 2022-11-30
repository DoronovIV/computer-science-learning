﻿using ReversedService.Model.Entities;

namespace ReversedService.Model.Configs
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> userBuilder)
        {
            userBuilder.HasKey(u => u.Id);

            userBuilder.Property(u => u.PublicId).IsRequired();
            userBuilder.Property(u => u.CurrentNickname).IsRequired();

            userBuilder.HasMany(u => u.ChatList).WithMany(c => c.UserList);
        }

    }
}