using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MISA.Core.Entities;
using System.Data;
using MySqlConnector;
using Dapper;
using System.Text.RegularExpressions;
using MISA.Core.Interfaces.Services;
using MISA.Core.Interfaces.Repository;
using System.IO;
using OfficeOpenXml;
using System.Threading;
using MISA.Core.CommonFunction;

namespace MISA.CukCuk.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : BaseEntityController<Customer>
    {
        #region DECLARE
        //IDepartmentService _departmentService;
        IBaseRepository<Customer> _baseRepository;
        IBaseService<Customer> _baseService;
        ICustomerService _customerService;
        #endregion

        #region Constructor
        public CustomersController(ICustomerService customerService,IBaseService<Customer> baseService, IBaseRepository<Customer> baseRepository) : base(baseService, baseRepository)
        {
            _baseService = baseService;
            _baseRepository = baseRepository;
            _customerService = customerService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Nhập khẩu dữ liệu từ excel
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        /// Created By : Dvanh 19/8/2021
        [HttpPost("Import")]
        public IActionResult Import(IFormFile formFile, CancellationToken cancellationToken)
        {
            try
            {
                var serviceResult = _customerService.Import(formFile, cancellationToken);

                if (serviceResult.isValid)
                {
                    return StatusCode(200, serviceResult.Data);
                }
                else
                {
                    var errorObj = new
                    {                    
                        userMsg = serviceResult.Messenger
                    };
                    return BadRequest(errorObj);
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Properties.Resource.Error_Message_UserVN,
                    errorCode = "misa-001",
                    moreInfo = @"https:/openapi.misa.com.vn/errorcode/misa-001",
                    traceId = ""
                };
                return StatusCode(500, errorObj);
            }
        }
        #endregion
    }
}
