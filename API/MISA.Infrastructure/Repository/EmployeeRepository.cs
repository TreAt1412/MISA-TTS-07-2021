using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        #region DECLARE
        IDbConnection _dbConnection;
        public readonly string _connectionString;
        #endregion

        #region Constructor
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {
            _connectionString = configuration.GetConnectionString("CukCukDatabase");
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
        /// Created By: Dvanh
        /// 19/8/2021
        public List<Employee> Pagination(int? pagesize, int? pagenumber, string searchContent, Guid? positionId, Guid? departmentId)
        {
            _dbConnection = new MySqlConnection(_connectionString);

            var positionIdString = (positionId == null) ? string.Empty : positionId.ToString();
            var departmentIdString = (departmentId == null) ? string.Empty : departmentId.ToString();

            DynamicParameters parameters = new DynamicParameters();                   

            var sqlCommand = $"SELECT * FROM Employee Where 1=1 ";
            //$"(EmployeeCode LIKE @searchContent OR FullName LIKE @searchContent OR PhoneNumber LIKE @searchContent) " +
            //$"AND positionId LIKE @positionId " +
            //$"AND departmentId LIKE @departmentId " +
            //$"LIMIT @limit OFFSET @offset";
            //Thay đổi query nếu có lọc theo từ khóa
            if (!string.IsNullOrEmpty(searchContent))
            {
                parameters.Add("@searchContent", $"%{searchContent}%");
                sqlCommand = " AND (EmployeeCode LIKE @searchContent OR FullName LIKE @searchContent OR PhoneNumber LIKE @searchContent)";             
            }

            /// Thay đổi query nếu có lọc theo phòng ban
            if (!string.IsNullOrEmpty(departmentIdString))
            {
                parameters.Add("@departmentId", $"%{departmentIdString}%");
                sqlCommand += $"AND departmentId LIKE @departmentId ";
            }

            /// Thay đổi query nếu có lọc theo vị trí
            if (!string.IsNullOrEmpty(positionIdString))
            {
                parameters.Add("@positionId", $"%{positionIdString}%");
                sqlCommand += $"AND positionId LIKE @positionId ";
            }

            /// Thay đổi query nếu có phân trang
            if (pagenumber> 0 && pagesize > 0)
            {
                parameters.Add("@limit", pagesize);
                parameters.Add("@offset", (pagenumber - 1) * pagesize);
                sqlCommand += $"LIMIT @limit OFFSET @offset ";
            }

            var employees = _dbConnection.Query<Employee>(sqlCommand, param: parameters);
            return employees.ToList();
        }

        public string GetNewCode()
        {
            _dbConnection = new MySqlConnection(_connectionString);

            string sqlCommand = "SELECT EmployeeCode FROM Employee ORDER BY EmployeeCode DESC LIMIT 1";
            var employeeCode = _dbConnection.QueryFirstOrDefault<string>(sqlCommand);

            int currentMax = 0;
         
            int codeValue = int.Parse(employeeCode.ToString().Split("-")[1]);
            if (currentMax < codeValue)
            {
                currentMax = codeValue;
            }

            string newEmployeeCode = "NV-" + (currentMax + 1);        
            return newEmployeeCode;
        }
        #endregion
    }
}
