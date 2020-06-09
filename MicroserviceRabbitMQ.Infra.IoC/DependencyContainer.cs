using MediatR;
using MicroserviceRabbitMQ.Banking.Application.Interfaces;
using MicroserviceRabbitMQ.Banking.Application.Services;
using MicroserviceRabbitMQ.Banking.Data.Context;
using MicroserviceRabbitMQ.Banking.Data.Repository;
using MicroserviceRabbitMQ.Banking.Domain.CommandHandlers;
using MicroserviceRabbitMQ.Banking.Domain.Commands;
using MicroserviceRabbitMQ.Banking.Domain.Events;
using MicroserviceRabbitMQ.Banking.Domain.Interfaces;
using MicroserviceRabbitMQ.Domain.Core.Bus;
using MicroserviceRabbitMQ.Infra.Bus;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroserviceRabbitMQ.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Domain Bus
            services.AddTransient<IEventBus, RabbitMQBus>();

            //Subscriptions
            //services.AddTransient<TransferEventHandler>();

            ////Domain Events
            //services.AddTransient<IEventHandler<TransferCreatedEvent>, TransferEventHandler>();

            //Domain Banking Commands
            services.AddTransient<IRequestHandler<CreateTransferCommand, bool>, TransferCommandHandler>();


            //Application Services
            services.AddTransient<IAccountService, AccountService>();
            //services.AddTransient<ITransferService, TransferService>();

            //Data
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<BankingDbContext>();
        }
    }
}
