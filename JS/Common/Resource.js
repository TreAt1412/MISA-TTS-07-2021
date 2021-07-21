/**
 * Resource dùng chung toàn chương trình
 * Dvanh 21/7/2021
 */
var Resource = Resource || {}

/**
 * Các kiểu dữ liệu của column trong grid
 * Dvanh 21/7/2021
 */
Resource.DataTypeColumn = {
    Number: "Number",
    Date: "Date",
    Enum: "Enum"
};

/**
 * Các method khi gọi ajax
 * Dvanh 21/7/2021
 */
Resource.Method = {
    Get: "Get",
    Post: "Post",
    Put: "Put",
    Delete: "Delete"
}

/**
 * Giới tính
 * Dvanh 21/7/2021
 */
Resource.Gender = {
    Female: "Nữ",
    Male: "Nam",
    Other: "Khác"
}

/**
 * Vị trí
 * Dvanh 21/7/2021
 */
Enumeration.Position = {
    Fresher: "Fresher", // fresher
    Junior: "Junior", // giám đốc
    Senior: "Senior", //Senior
}

/**
 * Các commandType của toolbar
 * Dvanh 21/7/2021
 */
Resource.CommandType = {
    Add: "Add",
    Edit: "Edit",
    Delete: "Delete",
    Refresh: "Refresh",
}

/**
 * Các commandType của form
 * Ngọc 18/7/2021
 */
Resource.CommandForm = {
    Save: "Save",
    Cancel: "Cancel",
    Confirm: "Confirm"
}