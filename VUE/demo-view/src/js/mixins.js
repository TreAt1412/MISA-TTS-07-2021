export const CommonFn = {
    methods: {
        /**
         * Hàm format số tiền
         * Dvanh 18/7/2021
         */
        formatMoney(money) {
            if (money && !isNaN(money)) {
                return money.toString().replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1.");
            } else {
                return money;
            }
        },

        /**
         * Hàm kiểm tra chuổi chỉ chứa số
         * Dvanh 24/07/2021
         */
        onlyNumber(number) {
            let part = /[^0-9]/g,
                res = number.match(part);
            if (!res) {
                return true;
            }
            return false;
        },

        /**
         * Hàm format số tiền về số bth
         * Dvanh 18/7/2021
         */
        formatNumber(money) {
            let salary = money.replaceAll(".", "");
            return salary;
        },

        /**
         * Format ngày tháng
         * Dvanh 18/7/2021
         */
        formatDate(dateSrc) {
            let date = new Date(dateSrc),
                year = date.getFullYear().toString(),
                month = (date.getMonth() + 1).toString().padStart(2, '0'),
                day = date.getDate().toString().padStart(2, '0');

            return `${day}/${month}/${year}`;
        },

        /**
         * Kiểm tra xem có phải dạng date không
         * Dvanh 18/7/2021
         */
        isDateFormat(date) {
            let regex = new RegExp("([0-9]{4}[-](0[1-9]|1[0-2])[-]([0-2]{1}[0-9]{1}|3[0-1]{1})|([0-2]{1}[0-9]{1}|3[0-1]{1})[/](0[1-9]|1[0-2])[/][0-9]{4})");
            return regex.test(date);
        },

        /**
         * Chuyển đổi ngày tháng
         * Dvanh 18/7/2021
         */
        convertDate(dateSrc) {
            let dateString = "";
            if (dateSrc) {
                let date = new Date(dateSrc),
                    year = date.getFullYear().toString(),
                    month = (date.getMonth() + 1).toString().padStart(2, '0'),
                    day = date.getDate().toString().padStart(2, '0');

                dateString = `${year}-${month}-${day}`;
            } else {
                dateString = "123";
            }
            return dateString;
        },

    },
}