﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Quiron.Data.Context;
using Quiron.Service.AutoMapper;
using QuironNUnitTest.Infra;

namespace QuironNUnitTest.Comun
{
    class UtilitariosTest
    {
        private static QuironContext quironContext;

        public static QuironContext GetContext()
        {
            if (quironContext == null)
            {
                DbContextOptions<QuironContext> dbContextOptions = new DbContextOptionsBuilder<QuironContext>()
                    .UseSqlServer("Data Source=FTS-DEV-GTF-03\\SQLEXPRESS;Initial Catalog=Quiron;Persist Security Info=True;User ID=gm;Password=1234")
                    .Options;

                quironContext = new QuironContext(dbContextOptions);
                QuironDBInitializer quironDBInitializer = new QuironDBInitializer();
                quironDBInitializer.Seed(quironContext);
            }

            return quironContext;
        }

        public static IMapper GetMapper()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapping()));
            return mapperConfiguration.CreateMapper();
        }
    }
}