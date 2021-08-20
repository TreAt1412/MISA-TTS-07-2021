using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Repository
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {   
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
        public List<Employee> Pagination(int? pagesize, int? pagenumber, string searchContent, Guid? positionId, Guid? departmentId);

        public string GetNewCode();
    }
}
