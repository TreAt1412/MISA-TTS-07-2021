loadData();
//Load dữ liệu cho vị trí
getPosition();

//Load dữ liệu cho phòng ban
getDepartment();
let employees = [];
/**
 * Event hiện pop up 
 * Focus vào ô mã nhân viên và thêm 1 mã nhân viên mới
 * Thêm mới dữ liệu
 * Dvanh 21/7/2021
 */
$(".content-header #btnAdd").click(function() {
    $("#popup").show();
    $(".wrapper").addClass("fade");
    $.ajax({
        url: "http://cukcuk.manhnv.net/v1/Employees/NewEmployeeCode",
        method: "GET",
    }).done(res => {
        $("#txtEmployeeCode").val(res);
        $("#txtEmployeeCode").focus();
    }).fail(res => {
        alert("Không lấy được mã nhân viên");
        $("#popup").hide();
        $(".wrapper").removeClass("fade");
    })

    // //Load dữ liệu cho vị trí
    // getPosition();

    // //Load dữ liệu cho phòng ban
    // getDepartment();

    //Hàm reset các trường
    resetPopup();

    $('#btnSave').click(function() {
        if (isRequiredLack(".info") == 0)
            return;
        const employeeCode = $('#txtEmployeeCode').val();
        const fullName = $('#txtFullName').val();
        const dateOfBirth = $('#txtDateOfBirth').val();
        const gender = $('#txtGender').attr("Value");
        const identityNumber = $('#txtIdentityNumber').val();
        const identityDate = $('#txtIdentityDate').val();
        const identityPlace = $('#txtIdentityPlace').val();
        const email = $('#txtEmail').val();
        if (emailValidate('#txtEmail', email) == 0) {

            return;
        }
        const phoneNumber = $('#txtPhoneNumber').val();
        const positionId = $('#txtPositionName').attr("Value");
        const departmentId = $('#txtDepartmentName').attr("Value");
        const personalTaxCode = $('#txtPersonalTaxCode').val();
        const salary = CommonFn.formatNumber($('#txtSalary').val());
        const joinDate = $('#txtJoinDate').val();
        const workStatus = $('#txtWorkStatus').attr("Value");

        let employee = {
            "employeeCode": employeeCode,
            "fullName": fullName,
            "gender": gender,
            "dateOfBirth": dateOfBirth,
            "phoneNumber": phoneNumber,
            "email": email,
            "identityNumber": identityNumber,
            "identityDate": identityDate,
            "identityPlace": identityPlace,
            "joinDate": joinDate,
            "departmentId": departmentId,
            "positionId": positionId,
            "workStatus": workStatus,
            "personalTaxCode": personalTaxCode,
            "salary": salary
        }


        $.ajax({
            url: "http://cukcuk.manhnv.net/v1/Employees",
            method: "POST",
            data: JSON.stringify(employee),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
        }).done(res => {
            alert("Thêm mới thành công");
            loadData();
            $("#popup").hide();
            $(".wrapper").removeClass("fade");

        }).fail(function(res) {
            console.log(res);
            alert("thêm mới thất bại")
        });
    })
});

/**
 * Hàm reset các trường
 * Dvanh 21/7/2021
 */
function resetPopup() {
    $('input').css("border", "1px solid #bbbbbb");
    $('input').val("");
    $('input').removeClass("");
}
/**
 * Hàm lấy dữ liệu vị trí cho dropdown
 * Dvanh 21/7/2021
 */
function getPosition() {
    $.ajax({
        url: "http://cukcuk.manhnv.net/v1/Positions",
        method: "GET",
    }).done(res => {
        res.forEach(position => {
            const positionName = position['PositionName'];
            const positionId = position['PositionId'];
            let dropdown = $(".dropdown.dd-Position");

            let item = `<div class="dropdown-item">
            <div class="dropdown-icon"></div>
            <div class="dropdown-text" Value = "${positionId}">${positionName}</div>
            </div>`
            dropdown.append(item);
        })

    })
};

/**
 * Hàm lấy dữ liệu phòng ban cho dropdown
 * Dvanh 21/7/2021
 */
function getDepartment() {
    $.ajax({
        url: "http://cukcuk.manhnv.net/api/Department",
        method: "GET",
    }).done(res => {
        res.forEach(department => {
            const departmentName = department['DepartmentName'];
            const departmentId = department['DepartmentId'];
            let dropdown = $(".dropdown.dd-Department");
            let item = `<div class="dropdown-item">
            <div class="dropdown-icon"></div>
            <div class="dropdown-text" Value = "${departmentId}">${departmentName}</div>
            </div>`
            dropdown.append(item);
        })

    })
};

/**
 * Event ẩn pop up 
 * Dvanh 21/7/2021
 */
$(".head-close, .button.cancel").click(function() {
    $("#popup").hide();
    $(".wrapper").removeClass("fade");
    $(".X").attr("style", "visibility: hidden;")
})

/**
 * reload lại trang
 * Dvanh 21/7/2021
 */
$(".refresh").click(function() {
    location.reload();
})

/**
 * Load ảnh từ máy lên form thêm mới
 * Dvanh 21/7/2021
 */
$('.image').click(function() {
    $('#myFile').trigger('click');
})

$('#myFile').click(function(e) {
    $('#myFile').change(function(e) {
        var img = URL.createObjectURL(e.target.files[0]);
        $('.image').css("background-image", `url(${img})`);
        $('.image').css("background-repeat", `none`);
    })
})

/**
 * Hàm lấy all nhân viên từ API
 * Dvanh 21/7/2021
 */
function loadData() {
    $.ajax({
        url: "http://cukcuk.manhnv.net/v1/Employees",
        method: "GET",
        success: function(response) {
            employees = response;
            updateEmployeeNumber(employees.length);
            let tbody = $("table tbody");

            employees.forEach(function(employee, index) {
                let EmployeeCode = employee.EmployeeCode,
                    FullName = employee.FullName,
                    GenderName = employee.GenderName,
                    PhoneNumber = employee.PhoneNumber,
                    Email = employee.Email,
                    PositionName = employee.PositionName,
                    DepartmentName = employee.DepartmentName,
                    Salary = CommonFn.formatMoney(employee.Salary),
                    WorkStatus = employee.WorkStatus,
                    DateOfBirth = CommonFn.formatDate(employee.DateOfBirth);

                let trHTML = `<tr Value = ${employee.EmployeeId}>
                        <td>${EmployeeCode}</td>
                        <td>${FullName}</td>
                        <td>${GenderName}</td>
                        <td >${DateOfBirth}</td>
                        <td>${PhoneNumber}</td>
                        <td>${Email}</td>
                        <td>${PositionName}</td>
                        <td>${DepartmentName}</td>
                        <td >${Salary}</td>
                        <td>${WorkStatus}</td>
                        </tr>`

                tbody.append(trHTML);
            })

        },
        error: function(errormessage) {
            console.log(errormessage.responseText);
        }

    })
}

/**
 * hàm validate dữ liệu (nhập các trường bắt buộc)
 * Dvanh 21/7/2021
 */
$('input[required]').blur(function() {
    let me = $(this)
    let value = me.val();
    if (value == '') {
        me.css('border', '1px solid red');
        me.attr('title', 'Thông tin này bắt buộc nhập');
    } else {
        me.css('border', '1px solid #bbbbbb');
        me.removeAttr('tittle')
    }
    me.focus(function() {
        me.css("border", "1px solid #01B075");
    })
})


/**
 * Hàm bấp đúp vào tr để lên form sửa 
 * Dvanh 21/7/2021
 */

$("tbody").on("dblclick", "tr", function() {
    let me = $(this),
        employeeId = me.attr("Value");

    // //Load dữ liệu cho vị trí
    // getPosition();

    // //Load dữ liệu cho phòng ban
    // getDepartment();
    $("#popup").show();
    for (let i = 0; i < employees.length; i++) {
        let employee = employees[i];
        if (employeeId == employee["EmployeeId"]) {
            $('#txtEmployeeCode').val(employee.EmployeeCode);
            $('#txtFullName').val(employee.FullName);
            $('#txtDateOfBirth').val(employee.DateOfBirth);
            $('#txtGender').attr("Value", `${employee.Gender}`);
            $('#txtIdentityNumber').val(employee.IdentityNumber);
            $('#txtIdentityDate').val(employee.IdentityDate);
            $('#txtIdentityPlace').val(employee.IdentityPlace);
            $('#txtEmail').val(employee.Email);
            $('#txtPhoneNumber').val(employee.PhoneNumber);
            $('#txtPositionName').attr("Value", `${employee.PositionId}`);
            $('#txtPositionName').text(`${employee.PositionName}`);
            $('#txtDepartmentName').attr("Value", `${employee.DepartmentId}`);
            $('#txtDepartmentName').text(`${employee.DepartmentName}`);
            $('#txtPersonalTaxCode').val(employee.PersonalTaxCode);
            $('#txtSalary').val(CommonFn.formatMoney(employee.Salary));
            $('#txtJoinDate').val(employee.JoinDate);
            $('#txtWorkStatus').attr("Value", `${employee.WorkStatus}`);
        }
    }
    $('#btnSave').click(function() {
        const employeeCode = $('#txtEmployeeCode').val();
        const fullName = $('#txtFullName').val();
        const dateOfBirth = $('#txtDateOfBirth').val();
        const gender = $('#txtGender').attr("Value");
        const identityNumber = $('#txtIdentityNumber').val();
        const identityDate = $('#txtIdentityDate').val();
        const identityPlace = $('#txtIdentityPlace').val();
        const email = $('#txtEmail').val();
        const phoneNumber = $('#txtPhoneNumber').val();
        const positionId = $('#txtPositionName').attr("Value");
        const departmentId = $('#txtDepartmentName').attr("Value");
        const personalTaxCode = $('#txtPersonalTaxCode').val();
        const salary = CommonFn.formatNumber($('#txtSalary').val());
        const joinDate = $('#txtJoinDate').val();
        const workStatus = $('#txtWorkStatus').attr("Value");

        let employeeEdit = {
            "employeeCode": employeeCode,
            "fullName": fullName,
            "gender": gender,
            "dateOfBirth": dateOfBirth,
            "phoneNumber": phoneNumber,
            "email": email,
            "identityNumber": identityNumber,
            "identityDate": identityDate,
            "identityPlace": identityPlace,
            "joinDate": joinDate,
            "departmentId": departmentId,
            "positionId": positionId,
            "workStatus": workStatus,
            "personalTaxCode": personalTaxCode,
            "salary": salary
        }
        $.ajax({
            url: `http://cukcuk.manhnv.net/v1/Employees/${employeeId}`,
            method: "PUT",
            data: JSON.stringify(employeeEdit),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
        }).done(res => {
            alert("Sửa thành công");
            loadData();
            $("#popup").hide();
            $(".wrapper").removeClass("fade");

        }).fail(function(res) {
            console.log(res);
        });
    })
})

/**
 *Check định dạng email 
 *Dvanh 21/7/2021
 * @param(txt: tên input, email:string email)
 */

function emailValidate(txt, email) {
    if (CommonFn.isEmailAddress(email) == true) {
        return 1;
    } else {
        $(txt).focus();
        $(txt).css("border", "1px solid #red");
        $(txt).attr("title", "Không đúng định dạng email")
        return 0;
    }
}

/**
 * function check rỗng
 * Dvanh 21/7/2021
 * @param(div: tên div cần check)
 */

function isRequiredLack(div) {
    const inputs = $(div + " input[required]");

    for (i = 0; i < inputs.length; i++) {
        input = $("#" + inputs[i].id);
        console.log(input.val());
        if (input.val() == "") {
            $('input').css("border", "1px solid #bbbbbb");
            input.css("border", "1px solid #red !important");
            input.focus();
            input.attr("title", "Trường này cần nhập!");
            return 0;
        }
    }
    return 1;
}