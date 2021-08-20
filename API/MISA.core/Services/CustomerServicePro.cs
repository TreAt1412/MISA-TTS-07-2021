using Microsoft.AspNetCore.Http;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class CustomerServicePro : BaseService<Customer>, ICustomerService
    {
        ServiceResult _serviceResult;

        public CustomerServicePro(IBaseRepository<Customer> baseRepository) : base(baseRepository)
        {
            _serviceResult = new ServiceResult();
            //_customerRepository = customerRepository;
        }

        public ServiceResult Import(IFormFile formFile, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
