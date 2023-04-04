﻿using Library.DataTable.TableCakeCustom;
using Library.DomainData.DataEF;
using Library.InterfaceRepository.Interface.CakeCustom_Repo;
using Library.Repository.Generic;

namespace Library.Repository.Repository
{
    public class CustomCakeRepository : GenericRepository<T_CustomCake>, ICustomCakeRepository
    {
        public CustomCakeRepository(ContextDBCakeOk context) : base(context)
        {

        }
    }
}
