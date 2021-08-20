using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Services
{
    public interface IBaseService<MISAEntity>
    {
        #region Methods
        /// <summary>
        /// Nghiệp vụ thêm mới khách hàng 
        /// Author: Dvanh 13/8/2021
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>ServiceResult: Kết quả xử lý nghiệp vụ</returns>
        ServiceResult Add(MISAEntity entity);

        /// <summary>
        /// Nghiệp vụ sửa thông tin khách hàng 
        /// Author: Dvanh 13/8/2021
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>ServiceResult: Kết quả xử lý nghiệp vụ</returns>
        ServiceResult Edit(MISAEntity entity, Guid entityId);
        #endregion

    }
}
