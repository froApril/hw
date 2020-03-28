using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services
{
    /// <summary>
    /// Based on AutoMapper: https://github.com/AutoMapper/AutoMapper/wiki/Getting-started
    /// See above link if help is needed
    /// </summary>
    public class MessageTypeConverter
    {
        private static MessageTypeConverter sMessageTypeConverter = new MessageTypeConverter();

        public static MessageTypeConverter Instance
        {
            get
            {
                return sMessageTypeConverter;
            }
        }



        public MessageTypeConverter()
        {
            InitializeExternalToInternalMappings();
            InitializeInternalToExternalMappings();
        }

        private void InitializeInternalToExternalMappings()
        {
            AutoMapper.Mapper.CreateMap<BookStore.Business.Entities.Media,
                BookStore.Services.MessageTypes.Media>().ForMember(
                    dest => dest.StockCount, opts => opts.MapFrom( src => src.Stocks.Quantity));

            AutoMapper.Mapper.CreateMap<BookStore.Business.Entities.Order,
                BookStore.Services.MessageTypes.Order>();

            AutoMapper.Mapper.CreateMap<BookStore.Business.Entities.OrderItem,
                BookStore.Services.MessageTypes.OrderItem>();

            AutoMapper.Mapper.CreateMap<BookStore.Business.Entities.User,
                 BookStore.Services.MessageTypes.User>();

            AutoMapper.Mapper.CreateMap<BookStore.Business.Entities.LoginCredential,
                BookStore.Services.MessageTypes.LoginCredential>();

            

        }

        public void InitializeExternalToInternalMappings()
        {
            AutoMapper.Mapper.CreateMap<BookStore.Services.MessageTypes.Media,
                BookStore.Business.Entities.Media>();

            AutoMapper.Mapper.CreateMap<BookStore.Services.MessageTypes.Order,
               BookStore.Business.Entities.Order>();

            AutoMapper.Mapper.CreateMap<BookStore.Services.MessageTypes.OrderItem,
                BookStore.Business.Entities.OrderItem>();

            AutoMapper.Mapper.CreateMap<BookStore.Services.MessageTypes.User,
                 BookStore.Business.Entities.User>();

            AutoMapper.Mapper.CreateMap<BookStore.Services.MessageTypes.LoginCredential,
                BookStore.Business.Entities.LoginCredential>();

            



        }

        public Destination Convert<Source, Destination>(Source s)
        {
            var result = AutoMapper.Mapper.Map<Source, Destination>(s);
            return result;
        }
    }
}
