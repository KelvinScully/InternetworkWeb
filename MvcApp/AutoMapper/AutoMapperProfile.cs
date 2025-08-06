using AutoMapper;
using DataAccessLayer.Objects.Account;
using DataAccessLayer.Objects.Inventory;
using MvcApp.Areas.Account.Model;
using MvcApp.Areas.Inventory.Models;

namespace MvcApp.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // CreateMap<object, object>().ReverseMap();
            // Two Way Maps
            CreateMap<ItemModel, ItemApo>().ReverseMap();
            CreateMap<ItemCategoryModel, ItemCategoryApo>().ReverseMap();
            CreateMap<ItemLocationModel, ItemLocationApo>().ReverseMap();
            CreateMap<ItemStatusModel, ItemStatusApo>().ReverseMap();
            CreateMap<UserModel, UserApo>().ReverseMap();

            // CreateMap<From, To>();
            // One Way Maps
            CreateMap<LoginRegisterModel, UserModel>();
        }
    }
}
