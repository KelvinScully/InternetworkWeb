using AutoMapper;
using DataAccessLayer.Objects.Account;
using DataAccessLayer.Objects.Inventory;
using MvcApp.areas.Account.Model;
using MvcApp.areas.Inventory.Models;

namespace MvcApp.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // CreateMap<object, object>().ReverseMap();
            // Two Way Maps
            CreateMap<ItemStatusModel, ItemStatusApo>().ReverseMap();
            CreateMap<UserModel, UserApo>().ReverseMap();

            // CreateMap<From, To>();
            // One Way Maps
            CreateMap<LoginRegisterModel, UserModel>();
        }
    }
}
