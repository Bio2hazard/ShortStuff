using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ShortStuff.Domain.Entities;

namespace ShortStuff.Repository
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<Data.Entities.User, User>().MaxDepth(1).ForMember(u => u.Followers, opt =>
            {
                opt.MapFrom(x => x.Followers);
                opt.ExplicitExpansion();
            }).ForMember(u => u.Messages, opt =>
            {
                opt.MapFrom(u => u.Messages);
                opt.ExplicitExpansion();
            }).ForMember(u => u.Favorites, opt =>
            {
                opt.MapFrom(u => u.Favorites);
                opt.ExplicitExpansion();
            }).ForMember(u => u.Notifications, opt =>
            {
                opt.MapFrom(u => u.Notifications);
                opt.ExplicitExpansion();
            }).ForMember(u => u.SubscribedTopics, opt =>
            {
                opt.MapFrom(u => u.SubscribedTopics);
                opt.ExplicitExpansion();
            });
            Mapper.CreateMap<Data.Entities.Message, Message>();
            Mapper.CreateMap<Data.Entities.Echo, Echo>();
            Mapper.CreateMap<Data.Entities.Notification, Notification>();
            Mapper.CreateMap<Data.Entities.Topic, Topic>();

            try
            {
                Mapper.AssertConfigurationIsValid();
            }
            catch (AutoMapperConfigurationException ex)
            {
                throw ex;
            }
            

        }
    }
}
