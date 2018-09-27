/*!财务模块-交易清单 韦德 2018年8月5日22:50:57*/
var route = "./pay";
var service;
var tableIndex;
(function () {
    service = initService(route);
})()

/**
 * 加载模块
 * @param r
 * @returns
 */
function initService(r) {
    return {
        /**
         * 充值 2018年8月7日02:57:49
         * @param callback
         */
        recharge: function (param, callback) {
            $.get(route + "/toRecharge",param, function (data) {
                callback(data);
            })
        },
        /**
         * 获取签名
         */
        getSign: function (param) {
            var pubKey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDYa5wMSo1iyXu0nrNizOnVkKFpREm1sgaY1sJu/be3tf//DRwoolEpwBXch9Y11OBrIItd0e1TXVoZvFiRboachimheGWJFl+Xxj8UrQFWz+tuqB5q9YNvwJVAX7WKKDHenwuXTTt/+jZI8mhRQPhBLdGM6hYCRloo87vOJPh0KQIDAQAB";
            var encrypt = new JSEncrypt();
            encrypt.setPublicKey(pubKey);
            return encrypt.encrypt(param);
        },
        /**
         * 无流水充值
         * @param param
         * @param callback
         */
        changeBalance: function (param, callback) {
            $.get(route + "/changeBalance",param, function (data) {
                callback(data);
            })
        }
    }
}
