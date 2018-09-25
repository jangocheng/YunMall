var route = "/product/manage";
var service;
var tableIndex;
(function () {
    service = initService(route);

    // 加载数据表
    initDataTable(route + "/getProducts", function (form, table, layer, vipTable, tableIns) {

    }, function (table, res, curr, count) {  
        // 监听工具条
        table.on('tool(my-data-table)', function(obj){
            var data = obj.data; //获得当前行数据
            var layEvent = obj.event; //获得 lay-event 对应的值（也可以是表头的 event 参数对应的值）
            var tr = obj.tr; //获得当前行 tr 的DOM对象
            if (layEvent === 'putaway') { //上架
                layer.confirm('您确定要上架此商品吗？', {
                    btn: ['确定', '取消'] //按钮
                }, function () {
                    service.putaway({
                        productId: data.Pid
                    }, function (data) {
                        if (utils.response.isError(data)) return layer.msg(data.Msg);
                        tableIndex.reload();
                        layer.msg("操作成功");
                    });
                });
            } else if (layEvent === 'unShelve') { //下架
                layer.confirm('您确定要下架此商品吗？', {
                    btn: ['确定', '取消'] //按钮
                }, function () {
                    service.unShelve({
                        productId: data.Pid
                    }, function (data) {
                        if (utils.response.isError(data)) return layer.msg(data.Msg);
                        tableIndex.reload();
                        layer.msg("操作成功");
                    });
                });
            } else if (layEvent === 'edit') {
                location.href = "/product/publish?productId=" + data.Pid
            }

        });

    });
})()

/**
 * 加载模块
 * @param r
 * @returns
 */
function initService(r) {
    return {
        /**
         * 上架商品 韦德 2018年9月24日17:10:27
         * @param param
         * @param callback
         */
        putaway: function (param, callback) {
            param.addTime = utils.date.timestampConvert(param.addTime);
            if (param.editTime != null) param.editTime = utils.date.timestampConvert(param.editTime);
            $.post(r + "/putaway", param, function (data) {
                callback(data);
            });
        },
        /**
         * 下架商品 韦德 2018年9月24日17:10:34
         * @param param
         * @param callback
         */
        unShelve: function (param, callback) {
            param.addTime = utils.date.timestampConvert(param.addTime);
            if (param.editTime != null) param.editTime = utils.date.timestampConvert(param.editTime);
            $.post(r + "/unShelve", param, function (data) {
                callback(data);
            });
        }
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
        $tradeDateBeginInput = $("input[name='trade_date_begin']"),
        $tradeDateEndInput = $("input[name='trade_date_end']");

    var cols = getTableColumns();

    // 注册查询事件
    $queryButton.click(function () {
        $queryButton.attr("disabled",true);
        var condition = $queryCondition.val();
        if(condition.indexOf("+") != -1) condition = condition.replace("+", "[add]");
        if(condition.indexOf("-") != -1) condition = condition.replace("-", "[reduce]");
        var param =  "?condition=" + encodeURI(condition);
        /*param += "&state=" + $tradeTypeInput.val();*/
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
            , url: url
            , height: 720    //容器高度
            , cols: cols
            , id: 'my-data-table' 
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


        // 批量上架 
        $("#batch-putaway").on("click",
            function() {
                var checkStatus = table.checkStatus('my-data-table');
                if (checkStatus.data.length > 0) {
                    var arr = checkStatus.data;
                    var pids = "";
                    for (var i = 0; i < arr.length; i++) {
                        pids += arr[i].Pid + ",";
                    }
                    pids = pids.substr(0, pids.length - 1);
                    layer.confirm('您确定要批量上架这些商品吗？', {
                        btn: ['确定', '取消'] //按钮
                    }, function () {
                        service.putaway({
                            productId: pids
                        }, function (data) {
                            if (utils.response.isError(data)) return layer.msg(data.Msg);
                            tableIndex.reload();
                            layer.msg("操作成功");
                        });
                    });
                }

            });



        // 批量下架
        $("#batch-unShelve").on("click",
            function () {
                var checkStatus = table.checkStatus('my-data-table');
                if (checkStatus.data.length > 0) {
                    var arr = checkStatus.data;
                    var pids = "";
                    for (var i = 0; i < arr.length; i++) {
                        pids += arr[i].Pid + ",";
                    }
                    pids = pids.substr(0, pids.length - 1);
                    layer.confirm('您确定要批量下架这些商品吗？', {
                        btn: ['确定', '取消'] //按钮
                    }, function () {
                        service.unShelve({
                            productId: pids
                        }, function (data) {
                            if (utils.response.isError(data)) return layer.msg(data.Msg);
                            tableIndex.reload();
                            layer.msg("操作成功");
                        });
                    });
                }

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
        { type: 'checkbox', fixed: 'left' }
        , {field: 'Pid', title: '商品id', width: 80, sort: true}
        , {field: 'ProductName', title: '商品名称', width: 180}
        , { field: 'Amount', title: '价格', width: 180, templet: function (d) {
            return "<span style='color: #c2330f;'>" + d.Amount + "</span>";
            }}
        , {
            field: 'Description', title: '简述', width: 260, templet: function (d) {
                return (d.Description != null) ? d.Description.substr(0, 50) + "..." : "";
            }
        }
        , {
            field: 'Status', title: '状态', width: 120, sort: true, templet: function (d) {
                 
                var state = ""; 
                switch (d.Status) {
                    case 0:
                        state = "未上架"; 
                        break;
                    case 1:
                        state = "已上架"; 
                        break;
                    case 2:
                        state = "已下架"; 
                        break;
                }
                 
                return state;
            }}
        , {
            field: 'AddTime', title: '发布时间', width: 180, sort: true, templet: function (d) {
                return d.AddTime == null ? '' : utils.date.dateConvert(d.AddTime);
                return d.AddTime;
            }}
        , {
            field: 'EditTime', title: '最后编辑时间', width: 180, sort: true, templet: function (d) {
                return d.EditTime == null ? '' : utils.date.dateConvert(d.EditTime);
            }}
        , { fixed: 'right', title: '操作', width: 160, align: 'center', templet: function(d) {
            var html = "";

            switch (d.Status) {
            case 0: // 未上架
                    html += '<a name="item-view" id="product-push" class="layui-btn layui-btn layui-btn-sm layui-btn-danger" lay-event="putaway">上架</a>';
                break;
            case 1: // 已上架
                html += '<a name="item-edit" id="product-remove" class="layui-btn layui-btn layui-btn-sm layui-btn-warm" lay-event="unShelve">下架</a>';
                break;
            case 2: // 已下架
                    html += '<a name="item-view" id="product-push" class="layui-btn layui-btn layui-btn-sm layui-btn-danger" lay-event="unShelve">上架</a>';
                break;
            }

            html += '<a name="item-edit" class="layui-btn layui-btn layui-btn-sm layui-btn-normal" lay-event="edit">编辑</a>';

            return html;
        } }
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
        , url: url
        , height: 720    //容器高度
        , cols: cols
        , id: id 
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