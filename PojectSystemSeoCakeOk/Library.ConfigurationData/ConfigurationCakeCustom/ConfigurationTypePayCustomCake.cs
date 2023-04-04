﻿using Library.DataTable.TableCakeCustom;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ConfigurationData.ConfigurationCakeCustom
{
    public class ConfigurationTypePayCustomCake : IEntityTypeConfiguration<T_TypePayCustomCake>
    {
        public void Configure(EntityTypeBuilder<T_TypePayCustomCake> builder)
        {
            builder.ToTable("T_TypePayCustomCakes");
            builder.HasKey(x => x.IDTypePay);
            builder.Property(x => x.IDTypePay).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(250).HasColumnType("nvarchar(250)");
            builder.Property(x => x.CreateDate).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.Status).IsRequired();
        }
    }
}
