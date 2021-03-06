﻿using MicroserviceRabbitMQ.Transfer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroserviceRabbitMQ.Transfer.Domain.Interfaces
{
    public interface ITransferRepository
    {
        IEnumerable<TransferLog> GetTransferLogs();
        void Add(TransferLog transferLog);
    }
}
