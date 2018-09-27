var route = "./accounts";
var service;
var tableIndex;
(function () {
    service = initService(route);

    // 加载数据表
    initDataTable(route + "/GetAccountsLimit", function (form, table, layer, vipTable, tableIns) {

    }, function (table, res, curr, count) {

    });
})()

/**
 * 加载模块
 * @param r
 * @returns
 */
function initService(r) {
    return {

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
        $queryButton.attr("disabled", true);
        var condition = $queryCondition.val();
        if (condition.indexOf("+") != -1) condition = condition.replace("+", "[add]");
        if (condition.indexOf("-") != -1) condition = condition.replace("-", "[reduce]");
        var param = "?condition=" + encodeURI(condition);
        param += "&type=" + $tradeTypeInput.val();
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
            , height: 720    //容器高度
            , cols: cols
            , id: 'my-data-table'
            , url: url
            , method: 'get'
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
        , {field: 'AccountsId', title: 'ID', width: 80, sort: true}
        , {field: 'PayId', title: '流水号', width: 300}
        , {field: 'TradeAccountName', title: '用户名', width: 150}
        , {field: 'AddTime', title: '创建时间', width: 240, templet: function (d) {
                return utils.date.dateConvert(d.AddTime);
            }}
        , {field: 'AccountsType', title: '交易类型', width: 120, templet: function (d) {
                return d.AccountsType == 1 ? "增" : "减";
            }}
        , {field: 'Amount', title: '交易额', width: 120, templet: function (d) {
                if(d.accountsType == 1){
                    return "<span style='color: #2fc253;font-size: 15px;'>+" + d.Amount + "</span>";
                }else{
                    return "<span style='color: #c2330f;font-size: 15px;'>-" + d.Amount + "</span>";
                }
                return "<span>" + d.Amount + "</span>";
            }}
        , {field: 'BeforeBalance', title: '变动前余额', width: 120, align: "center"}
        , {field: 'AfterBalance', title: '变动后余额', width: 120, align: "center"}
        , {field: 'Remark', title: '摘要', width: 240}
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
        , height: 720    //容器高度
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
        $(o).height(640);
    });
}
