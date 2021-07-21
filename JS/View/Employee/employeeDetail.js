$("#txtSalary").on("input", function() {
    let me = $(this),
        val = me.val();
    // console.log(CommonFn.formatMoney(val));
    val = val.replaceAll(".", "");
    me.val(CommonFn.formatMoney(val));
});

// function removeDot(str, i) {
//     return str.split("").splice(i, 1).join("");
// }