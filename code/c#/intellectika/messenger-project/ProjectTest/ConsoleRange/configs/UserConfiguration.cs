﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetworkingAuxiliaryLibrary.Objects.Entities;

namespace MyApp
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> userBuilder)
        {
            userBuilder.HasKey(u => u.Id);

            userBuilder.Property(u => u.PublicId).IsRequired();
            userBuilder.Property(u => u.CurrentNickname).IsRequired();
            userBuilder.Property(u => u.Login).IsRequired();

            userBuilder.HasMany(u => u.ChatList).WithMany(c => c.UserList);
        }

    }
}