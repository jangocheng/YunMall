window.utils = {
    response: {
        isError: function (data) {
            return data == null || data.error == null || data.error == 1;
        },
        isErrorByCode: function (data) {
            return data == null || data.code == null || data.code == 500 || data.code == 300;
        },
        isException: function (data) {
            return data != null && data.code != null && data.code == 400 ;
        }
    },
    date: {
        timestampConvert: function (ts) {
            var date = new Date(ts);
            Y = date.getFullYear() + '-';
            M = (date.getMonth()+1 < 10 ? '0'+(date.getMonth()+1) : date.getMonth()+1) + '-';
            D = date.getDate() + ' ';
            h = date.getHours() + ':';
            m = date.getMinutes() + ':';
            s = date.getSeconds();
            return Y+M+D+h+m+s;
        }
    }
}