using MISA.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class Employee : BaseEntity
    {
        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>
        [MISARequired ("Mã nhân viên")]
        public string EmployeeCode { get; set; }
        /// <summary>
        /// Họ và đệm
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Tên
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Tên đầy đủ
        /// </summary>
        [MISARequired ("Họ và tên")]
        public string FullName { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public int? Gender { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Số cmt
        /// </summary>
        [MISARequired ("Số CMND / CCCD")]
        [MISAIsNumber ("Số CMND / CCCD")]
        public string IdentityNumber { get; set; }

        /// <summary>
        /// Nơi cấp cmt
        /// </summary>
        public string IdentityPlace { get; set; }

        /// <summary>
        /// Ngày cấp cmt
        /// </summary>
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [MISARequired ("Email")]
        [MISAEmail]
        public string Email { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        [MISARequired ("Số điện thoại")]
        [MISAPhoneNumber]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Lương tháng
        /// </summary>
        [MISAIsNumber ("Lương")]
        public string Salary { get; set; }

        /// <summary>
        /// Khóa ngoại
        /// </summary>
        public Guid? PositionId { get; set; }

        /// <summary>
        /// Khóa ngoại
        /// </summary>
        public Guid? DepartmentId { get; set; }

        #endregion
    }
}
