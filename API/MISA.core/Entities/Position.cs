using MISA.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class Position : BaseEntity
    {
        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid PositionId { get; set; }

        /// <summary>
        /// Mã vị trí
        /// </summary>
        [MISARequired("Mã vị trí")]
        public string PositionCode { get; set; }

        /// <summary>
        /// Tên vị trí
        /// </summary>
        [MISARequired("Tên vị trí")]
        public string PositionName { get; set; }
        #endregion
    }
}
