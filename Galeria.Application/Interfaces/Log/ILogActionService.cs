﻿using Galeria.Application.Interfaces.Base;
using Galeria.Domain.DTO.Logs;
using Galeria.Domain.Entities.Logs;

namespace Galeria.Application.Interfaces.Logs
{
    public interface ILogActionService : IServiceBase<LogAction, LogActionDTO>
    {
    }
}
