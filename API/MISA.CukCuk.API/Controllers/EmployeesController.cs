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
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using System.Threading;
using System.IO;
using OfficeOpenXml;

namespace MISA.CukCuk.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : BaseEntityController<Employee>
    {
        #region DECLARE
        IEmployeeRepository _employeeRepository;
        IEmployeeService _employeeService;
        IBaseRepository<Employee> _baseRepository;
        IBaseService<Employee> _baseService;
        #endregion

        #region Constructor
        public EmployeesController(IBaseService<Employee> baseService, IBaseRepository<Employee> baseRepository, IEmployeeRepository employeeRepository, IEmployeeService employeeService) : base(baseService, baseRepository)
        {
            _baseService = baseService;
            _baseRepository = baseRepository;
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Phân trang nhân viên
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="pagenumber"></param>
        /// <param name="searchContent"></param>
        /// <param name="positionId"></param>
        /// <param name="departmentId"></param>
        /// <returns>Danh sách nhân viên</returns>
        /// Created By: Dvanh 19/8/2021
        [HttpGet("paging")]
        public  IActionResult Pagination(int? pagesize, int? pagenumber, string searchContent, Guid? positionId, Guid? departmentId)
        {
            try
            {
                var employees = _employeeRepository.Pagination(pagesize, pagenumber, searchContent, positionId, departmentId);
                // Trả về cho client
                return Ok(employees);
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

        /// <summary>
        /// Hàm lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên</returns>
        /// Created By: Dvanh 19/8/2021
        [HttpGet("NewEmployeeCode")]
        public IActionResult getNewCode()
        {
            try
            {
                var newEmployeeCode = _employeeRepository.GetNewCode();
                // Trả về cho client
                return Ok(newEmployeeCode);
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

        public override IActionResult InsertEntity(Employee entity)
        {
            try
            {
                var serviceResult = _employeeService.Add(entity);

                // Trả về cho client
                if (serviceResult.isValid == true)
                {
                    return StatusCode(201, serviceResult.Data);
                }
                else
                {
                    var errorObj = new
                    {
                        userMsg = serviceResult.Messenger,
                        errorCode = "misa-001",
                        moreInfo = @"https:/openapi.misa.com.vn/errorcode/misa-001",
                        traceId = ""
                    };
                    return StatusCode(400, errorObj);
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = MISA.Core.Properties.ResourceVN.Error_Message_UserVN,
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


