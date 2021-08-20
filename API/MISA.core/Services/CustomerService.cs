using Microsoft.AspNetCore.Http;
using MISA.Core.Entities;
using MISA.Core.CommonFunction;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        #region DECLARE
        ICustomerRepository _customerRepository;
        ServiceResult _serviceResult;
        #endregion

        #region Constructor
        public CustomerService(ICustomerRepository customerRepository, IBaseRepository<Customer> baseRepository) : base(baseRepository)
        {
            _serviceResult = new ServiceResult();
            _customerRepository = customerRepository;
        }
        #endregion

        #region methods

        public ServiceResult Import(IFormFile formFile, CancellationToken cancellationToken)
        {

            var customers = new List<Customer>();
            // Check file có hợp lệ không
            if (formFile == null)
            {
                _serviceResult.Messenger = Properties.ResourceVN.Form_File_IsNot_Null;
                _serviceResult.isValid = false;
                return _serviceResult;
            }
            // Check độ lớn file (Giới hàn 100MB)

            // Thực hiện đọc dữ liệu
            using (var stream = new MemoryStream())
            {
                formFile.CopyToAsync(stream, cancellationToken);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 3; row <= rowCount; row++)
                    {
                        var CustomerCode = worksheet.Cells[row, 1].Value;
                        var FullName = worksheet.Cells[row, 2].Value;
                        var MemberCardCode = worksheet.Cells[row, 3].Value;
                        var CustomerGroupName = worksheet.Cells[row, 4].Value;
                        var PhoneNumber = worksheet.Cells[row, 5].Value;
                        var DateOfBirth = worksheet.Cells[row, 6].Value;
                        var CompanyName = worksheet.Cells[row, 7].Value;
                        var CompanyTaxCode = worksheet.Cells[row, 8].Value;
                        var Email = worksheet.Cells[row, 9].Value;
                        var Address = worksheet.Cells[row, 10].Value;

                        var customer = new Customer
                        {
                            CustomerCode = (CustomerCode != null) ? CustomerCode.ToString().Trim() : string.Empty,
                            FullName = (FullName != null) ? FullName.ToString().Trim() : string.Empty,
                            MemberCardCode = (MemberCardCode != null) ? MemberCardCode.ToString().Trim() : string.Empty,
                            CustomerGroupName = (CustomerGroupName != null) ? CustomerGroupName.ToString().Trim() : string.Empty,
                            PhoneNumber = (PhoneNumber != null) ? PhoneNumber.ToString().Trim() : string.Empty,
                            DateOfBirth = (DateOfBirth != null) ? CommonFn.ConvertToYMD(DateOfBirth.ToString().Trim()) : null,
                            CompanyName = (CompanyName != null) ? CompanyName.ToString().Trim() : string.Empty,
                            CompanyTaxCode = (CompanyTaxCode != null) ? CompanyTaxCode.ToString().Trim() : string.Empty,
                            Email = (Email != null) ? Email.ToString().Trim() : string.Empty,
                            Address = (Address != null) ? Address.ToString().Trim() : string.Empty
                        };

                        if (!base.validateRequired(customer))
                        {
                            customer.ImportError.Add(Properties.ResourceVN.Empty_Field);
                        }

                        if (!base.checkedCodeExist(CustomerCode.ToString(), Guid.Empty))
                        {
                            customer.ImportError.Add(Properties.ResourceVN.Duplicate_Code);
                        }

                        if (!base.validateEmail(customer))
                        {
                            customer.ImportError.Add(Properties.ResourceVN.Error_Email);
                        }

                        if (!base.validatePhoneNumber(customer))
                        {
                            customer.ImportError.Add(Properties.ResourceVN.Error_PhoneNumber);
                        }

                        if (!base.validateIsNumber(customer))
                        {
                            customer.ImportError.Add(Properties.ResourceVN.Contain_Numbers_Only);
                        }

                        if (customer.ImportError.Count() == 0)
                        {
                            _serviceResult.Data = _customerRepository.Add(customer);
                        }
                    }                 
                }
            }
            return _serviceResult;
        }
        #endregion
    }
}

