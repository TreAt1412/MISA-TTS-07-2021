using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        IEmployeeRepository _employeeRepository;
        ServiceResult _serviceResult;

        public EmployeeService(IEmployeeRepository employeeRepository, IBaseRepository<Employee> baseRepository) : base(baseRepository)
        {
            _serviceResult = new ServiceResult();
            _employeeRepository = employeeRepository;
        }

        public override ServiceResult Add(Employee entity)
        {
            try
            {
                //validate định dạng mã nv
                var propValue = entity.GetType().GetProperty("EmployeeCode").GetValue(entity);
                                             
                var employeeCodeFormat = @"^(NV-\d+)$"; 
                var isMatch = Regex.IsMatch(propValue.ToString(), employeeCodeFormat, RegexOptions.IgnoreCase);
                if (!isMatch)
                {
                    _serviceResult.Messenger = Properties.ResourceVN.EmployeeCode;
                    _serviceResult.isValid = false;
                    return _serviceResult;
                }
                    
                return base.Add(entity);
            }
            catch(Exception)
            {
                _serviceResult.isValid = false;
                _serviceResult.Messenger = Properties.ResourceVN.Error_Message_UserVN;
                return _serviceResult;
            }
                  
        }
    }

}
