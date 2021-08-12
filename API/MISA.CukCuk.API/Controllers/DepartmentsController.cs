using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.API.Model;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllDepartment()
        {
            //Truy cập vào database:
            // 1.Khai báo đối tượng
            var connectionString = "Host = 47.241.69.179;" +
                "Database = MISA.CukCuk_Demo_NVMANH;" +
                "User Id = dev;" +
                "Password = 12345678";

            // 2.Khởi tạo đối tượng kết nối với database
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            // 3.Lấy dữ liệu
            var sqlCommand = "SELECT * FROM Department";
            var departments = dbConnection.Query<object>(sqlCommand);
            // Trả về cho client

            var response = StatusCode(200, departments);
            return response;
        }


        [HttpGet("{departmentId}")]
        public IActionResult GetDepartmentById(Guid departmentId)
        {
            //Truy cập vào database:
            // 1.Khai báo đối tượng
            var connectionString = "Host = 47.241.69.179;" +
                "Database = MISA.CukCuk_Demo_NVMANH;" +
                "User Id = dev;" +
                "Password = 12345678";
            // 2.Khởi tạo đối tượng kết nối với database
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            // 3.Lấy dữ liệu
            var sqlCommand = $"SELECT * FROM Department WHERE departmentId = @DepartmentIdParam";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@DepartmentIdParam", departmentId);
            var department = dbConnection.QueryFirstOrDefault<object>(sqlCommand, param: parameters);
            // Trả về cho client

            var response = StatusCode(200, department);
            return response;
        }

        [HttpPost]
        public IActionResult InsertDepartment([FromBody] Department department)
        {
            department.DepartmentId = Guid.NewGuid();
            //Truy cập vào database:
            // 1.Khai báo đối tượng
            var connectionString = "Host = 47.241.69.179;" +
                "Database = MISA.CukCuk_Demo_NVMANH;" +
                "User Id = dev;" +
                "Password = 12345678";
            // 2.Khởi tạo đối tượng kết nối với database
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            //khai báo dynamicParam:
            var dynamicParam = new DynamicParameters();

            // 3.Thêm dữ liệu vào database
            var columnsName = string.Empty;
            var columnsParam = string.Empty;

            //Đọc từng property của object:
            var properties = department.GetType().GetProperties();


            //Duyệt từng property:
            foreach (var prop in properties)
            {
                //lấy tên của prop:
                var propName = prop.Name;

                //Lấy value của prop:
                var propValue = prop.GetValue(department);

                //Lấy kiểu dữ liệu của prop:
                var propType = prop.PropertyType;

                //thêm param tương ứng với mỗi property của đối tượng
                dynamicParam.Add($"@{propName}", propValue);

                columnsName += $"{propName},";
                columnsParam += $"@{propName},";
            }
            columnsName = columnsName.Remove(columnsName.Length - 1, 1);
            columnsParam = columnsParam.Remove(columnsParam.Length - 1, 1);
            var sqlCommand = $"INSERT INTO Department({columnsName}) VALUES({columnsParam}) ";

            var rowEffects = dbConnection.Execute(sqlCommand, param: dynamicParam);
            // Trả về cho client

            var response = StatusCode(200, rowEffects);
            return response;
        }

        [HttpPut("{departmentId}")]
        public IActionResult UpdateDepartment(Guid departmentId, Department department)
        {
            //Truy cập vào database:
            // 1.Khai báo đối tượng
            var connectionString = "Host = 47.241.69.179;" +
                "Database = MISA.CukCuk_Demo_NVMANH;" +
                "User Id = dev;" +
                "Password = 12345678";
            // 2.Khởi tạo đối tượng kết nối với database
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            //khai báo dynamicParam:
            var dynamicParam = new DynamicParameters();

            // 3.Thêm dữ liệu vào database
            var columnsName = string.Empty;

            //Đọc từng property của object:
            var properties = department.GetType().GetProperties();

            //Duyệt từng property:
            foreach (var prop in properties)
            {
                //lấy tên của prop:
                var propName = prop.Name;

                //Lấy value của prop:
                var propValue = prop.GetValue(department);

                //Lấy kiểu dữ liệu của prop:
                var propType = prop.PropertyType;

                //thêm param tương ứng với mỗi property của đối tượng
                dynamicParam.Add($"@{propName}", propValue);

                columnsName += $"{propName} = @{propName},";

            }
            columnsName = columnsName.Remove(columnsName.Length - 1, 1);

            var sqlCommand = $"UPDATE Department SET {columnsName} WHERE departmentId = @DepartmentIdParam ";

            dynamicParam.Add("@DepartmentIdParam", departmentId);
            var rowEffects = dbConnection.Execute(sqlCommand, param: dynamicParam);

            // Trả về cho client
            var response = StatusCode(200, rowEffects);
            return response;
        }

        [HttpDelete("{departmentId}")]
        public IActionResult DeleteDepartment(Guid departmentId)
        {
            //Truy cập vào database:
            // 1.Khai báo đối tượng
            var connectionString = "Host = 47.241.69.179;" +
                "Database = MISA.CukCuk_Demo_NVMANH;" +
                "User Id = dev;" +
                "Password = 12345678";
            // 2.Khởi tạo đối tượng kết nối với database
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            // 3.Lấy dữ liệu
            var sqlCommand = $"DELETE FROM Department WHERE DepartmentId = @DepartmentIdParam";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@DepartmentIdParam", departmentId);
            var rowEffects = dbConnection.Execute(sqlCommand, param: parameters);

            // 4.Trả về cho client
            var response = StatusCode(200, rowEffects);
            return response;
        }
    }
}
