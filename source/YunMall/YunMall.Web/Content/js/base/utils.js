window.utils = {
    response: {
        isError: function (data) {
            return data === null || data.Code === null || data.Code === 1;
        },
        isList: function(data) {
            return data.data != null;
        },
        isEmpty: function(data) {
            return data.data == null || data.data.length <= 0;
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
        },
        dateConvert: function (cellval) {
            if (cellval.indexOf("-") != -1) return "-";

            var date = new Date(parseInt(cellval.replace("/Date(", "").replace(")/", ""), 10));

            var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;

            var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();


            var h = date.getHours() + ':',

            m = date.getMinutes() + ':',

            s = date.getSeconds();

            return date.getFullYear() + "-" + month + "-" + currentDate + " " + h + m + s;
        }
    }
}