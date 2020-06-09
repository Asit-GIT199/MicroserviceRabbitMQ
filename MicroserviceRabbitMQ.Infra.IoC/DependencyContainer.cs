using MediatR;
using MicroserviceRabbitMQ.Banking.Application.Interfaces;
using MicroserviceRabbitMQ.Banking.Application.Services;
using MicroserviceRabbitMQ.Banking.Data.Context;
using MicroserviceRabbitMQ.Banking.Data.Repository;
using MicroserviceRabbitMQ.Banking.Domain.CommandHandlers;
using MicroserviceRabbitMQ.Banking.Domain.Commands;
using MicroserviceRabbitMQ.Banking.Domain.Interfaces;
using MicroserviceRabbitMQ.Domain.Core.Bus;
using MicroserviceRabbitMQ.Infra.Bus;
using MicroserviceRabbitMQ.Transfer.Application.Interfaces;
using MicroserviceRabbitMQ.Transfer.Application.Services;
using MicroserviceRabbitMQ.Transfer.Data.Context;
using MicroserviceRabbitMQ.Transfer.Data.Repository;
using MicroserviceRabbitMQ.Transfer.Domain.EventHandlers;
using MicroserviceRabbitMQ.Transfer.Domain.Events;
using MicroserviceRabbitMQ.Transfer.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MicroserviceRabbitMQ.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Domain Bus
            //services.AddTransient<IEventBus, RabbitMQBus>();

            //Domain Bus
            services.AddSingleton<IEventBus, RabbitMQBus>(sp =>
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new RabbitMQBus(sp.GetService<IMediator>(), scopeFactory);
            });

            //Subscriptions
            services.AddTransient<TransferEventHandler>();

            //Domain Events
            services.AddTransient<IEventHandler<TransferCreatedEvent>, TransferEventHandler>();

            //Domain Banking Commands
            services.AddTransient<IRequestHandler<CreateTransferCommand, bool>, TransferCommandHandler>();


            //Application Services for Banking
            services.AddTransient<IAccountService, AccountService>();
            //Application Services for Transfer
            services.AddTransient<ITransferService, TransferService>();

            //Data for Banking
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<BankingDbContext>();
            //Data for Transfer
            services.AddTransient<ITransferRepository, TransferRepository>();            
            services.AddTransient<TransferDbContext>();
        }
    }
}
