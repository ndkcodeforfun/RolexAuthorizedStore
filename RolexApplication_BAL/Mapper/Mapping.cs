using AutoMapper;
using RolexApplication_BAL.ModelView;
using RolexApplication_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolexApplication_BAL.Mapper
{
    public class Mapping : Profile
    {
        public Mapping() {
            CreateMap<Product, ProductVIew>().ReverseMap();
            CreateMap<Category, CategoryView>().ReverseMap();
        }
    }
}
