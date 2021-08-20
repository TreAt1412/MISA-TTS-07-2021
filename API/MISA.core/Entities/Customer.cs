using MISA.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{ 
    public class Customer : BaseEntity
    {
        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>
        [MISARequired]
        public string CustomerCode { get; set; }
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
        [MISARequired]
        public string FullName { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
       
        public int? Gender { get; set; }

        /// <summary>
        /// Thẻ thành viên
        /// </summary>
        public string MemberCardCode { get; set; }

        /// <summary>
        /// Nhóm khách hàng
        /// </summary>
        [MISANotMap]
        public string CustomerGroupName { get; set; }


        /// <summary>
        /// Số điện thoại
        /// </summary>
        [MISARequired]
        [MISAPhoneNumber]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Tên công ty
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Mã số thuế cty
        /// </summary>
        [MISAIsNumber]
        public string CompanyTaxCode { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [MISARequired]
        [MISAEmail]
        public string Email { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Các lối khi duyệt từ excel
        /// </summary>
        [MISANotMap]
        public List<string> ImportError { get; set; } = new List<string>();

        #endregion

        
    }
    
}
