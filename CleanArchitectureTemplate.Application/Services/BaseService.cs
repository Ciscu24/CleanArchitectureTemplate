using CleanArchitectureTemplate.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureTemplate.Application.Services
{
    public abstract class BaseService<T> where T : class
    {
        protected readonly internal IUnitOfWork _unitOfWork;
        protected readonly internal ILogger<T> _logger;

        protected BaseService(IUnitOfWork unitOfWork, ILogger<T> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
    }
}
