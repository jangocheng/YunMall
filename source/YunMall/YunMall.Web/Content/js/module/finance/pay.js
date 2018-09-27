var route = "./pays";
var service;
var tableIndex;
(function() {
    service = initService(route);

    // 获取系统账户信息
    service.getSystemAccount(function(data) {
        if (utils.response.isError(data)) return layer.msg("查询失败");
        var obj = JSON.parse(data.Msg);
        $("#sys_username").text(obj.account);
        $("#sys_balance").text(obj.amount);
        $("#income-amount").text(obj.incomeAmount);
        $("#expend-amount").text(obj.expendAmount);
    });

    // 加载数据表
    initDataTable(route + "/getPayLimit",
        function(form, table, layer, vipTable, tableIns) {

        },
        function(table, res, curr, count) {

        });
})();

/**
 * 加载模块
 * @param r
 * @returns
 */
function initService(r) {
    return {
        /**
         * 获取系统账户信息 2018年8月7日00:23:26
         * @param callback
         */
        getSystemAccount: function (callback) {
            $.get(route + "/GetSystemAccount",
                function(data) {
                    callback(data);
                });
        },
    }
}

/**
 * 加载数据表
 * @param url
 * @param callback
 * @param loadDone
 */
function initDataTable(url, callback, loadDone) {
    var $queryButton = $("#my-data-table-query"),
        $queryCondition = $("#my-data-table-condition"),
        $tradeTypeInput = $("select[name='trade_type']"),
        $filterTypeInput = $("select[name='filter_type']"),
        $tradeDateBeginInput = $("input[name='trade_date_begin']"),
        $tradeDateEndInput = $("input[name='trade_date_end']");

    var cols = getTableColumns();

    // 注册查询事件
    $queryButton.click(function () {
        $queryButton.attr("disabled",true);

        var param =  "?condition=" + $queryCondition.val();
        param += "&tradeType=" + $tradeTypeInput.val();
        param += "&type=" + $filterTypeInput.val();
        param += "&beginTime=" + $tradeDateBeginInput.val();
        param += "&endTime=" + $tradeDateEndInput.val();

        loadTable(tableIndex,"my-data-table", "#my-data-table", cols, url + param, function (res, curr, count) {
            $queryButton.removeAttr("disabled");
        });
    })

    layui.use(['table', 'form', 'layer', 'vip_table', 'layedit', 'tree','element'], function () {
        // 操作对象
        var form = layui.form
            , table = layui.table
            , layer = layui.layer
            , vipTable = layui.vip_table
            , $ = layui.jquery
            , layedit = layui.layedit
            , element = layui.element;

        // 表格渲染
        tableIndex = table.render({
            elem: '#my-data-table'                  //指定原始表格元素选择器（推荐id选择器）
            , height: 480    //容器高度
            , cols: cols
            , id: 'my-data-table'
            , url: url
            , method: 'get'
            , totalRow: true
            , page: true
            , limits: [30, 60, 90, 150, 300]
            , limit: 30 //默认采用30
            , loading: true
            , even: true
            , done: function (res, curr, count) {
                loadDone(table, res, curr, count);
            }
        });

        // 刷新
        $('#btn-refresh-my-data-table').on('click', function () {
            tableIndex.reload();
        });

        // you code ...
        callback(form, table, layer, vipTable, tableIndex);
    });
}

/**
 * 获取表格列属性
 * @returns {*[]}
 */
function getTableColumns() {
    return [[
        {type: "numbers"}
        , {field: 'PayId', title: 'ID', width: 80, sort: true, unresize: true, totalRowText: '合计'}
        , {field: 'SystemRecordId', title: '流水号', width: 220}
        , {field: 'ChannelRecordId', title: '交易号', width: 280, templet: function (d) {
                return d.ChannelRecordId == null ? "站内交易" : d.ChannelRecordId;
            }}
        , {field: 'AddTime', title: '交易日', width: 240, templet: function (d) {
                return utils.date.dateConvert(d.AddTime);
            }}
        , {field: 'FromName', title: '甲方', width: 150}
        , {field: 'ToName', title: '乙方', width: 150}
        , {field: 'ChannelName', title: '渠道名称', width: 120}
        , {field: 'ToAccountTime', title: '渠道到账时间', width: 240, templet: function (d) {
                return d.ToAccountTime == null ? '-' : utils.date.dateConvert(d.ToAccountTime);
            }}
        , {field: 'ProductName', title: '商品名称', width: 120}
        , {field: 'TradeName', title: '交易类型', width: 120}
        , {field: 'Amount', title: '交易总额', width: 120, align: "center", totalRow: true}
        , {field: 'Status', title: '状态', width: 120, templet: function (d) {
            if (d.Status == 0) {
                    return "正常";
                }else if(d.Status == 1){
                    return "退款";
                }else{
                    return "未知";
                }
            }}
        , {field: 'Remark', title: '摘要', width: 120}
    ]];
}

/**
 * 加载表格数据
 * @param tableIns
 * @param id
 * @param elem
 * @param cols
 * @param url
 * @param loadDone
 */
function loadTable(index,id,elem,cols,url,loadDone) {
    index.reload({
        elem: elem
        , height: 480    //容器高度
        , cols: cols
        , id: id
        , url: url
        , method: 'get'
        , page: true
        , limits: [30, 60, 90, 150, 300]
        , limit: 30 //默认采用30
        , loading: true
        , even: true
        , done: function (res, curr, count) {
            resetPager();
            loadDone(res, curr, count);
        }
    });
}

function resetPager() {
    $(".layui-table-body.layui-table-main").each(function (i, o) {
        $(o).height(395);
    });
}
