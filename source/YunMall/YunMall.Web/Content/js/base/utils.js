window.utils = {
    response: {
        isError: function (data) {
            return data === null || data.Code === null || data.Code === 1;
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