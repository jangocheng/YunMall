var route = "/product/manage";
var service;
var tableIndex;
(function () {
    service = initService(route);

    // 加载数据表
    initDataTable(route + "/getProducts", function (form, table, layer, vipTable, tableIns) {

    },function (table, res, curr, count) {
        // 监听工具条
        table.on('tool(my-data-table)', function(obj){
            var data = obj.data; //获得当前行数据
            var layEvent = obj.event; //获得 lay-event 对应的值（也可以是表头的 event 参数对应的值）
            var tr = obj.tr; //获得当前行 tr 的DOM对象
            if(layEvent === 'reset'){ //重置密码
                layer.prompt({title: '输入新密码', formType: 0}, function(pass, index){
                    data.password = pass;
                    service.updatePassword(data, function (data) {
                        if(utils.response.isErrorByCode(data)) return layer.msg("操作失败");
                        if(utils.response.isException(data)) return layer.msg(data.msg);
                        tableIndex.reload();
                        layer.msg("操作成功");
                    })
                    layer.close(index);
                });

            }else if(layEvent === 'disable'){ //冻结
                layer.confirm('您确定要冻结此用户账户吗？', {
                    btn: ['冻结','取消'] //按钮
                }, function(){
                    data.isEnable = 0;
                    service.updateEnable(data, function (data) {
                        if(utils.response.isErrorByCode(data)) return layer.msg("操作失败");
                        if(utils.response.isException(data)) return layer.msg(data.msg);
                        tableIndex.reload();
                        layer.msg("操作成功");
                    })
                });
            }else if(layEvent === 'del'){ //删除
                layer.confirm('您确定要删除此用户账户吗？', {
                    btn: ['删除','取消'] //按钮
                }, function(){
                    data.isEnable = 0;
                    data.isDelete = 1;
                    service.updateAvailability(data, function (data) {
                        if(utils.response.isErrorByCode(data)) return layer.msg("操作失败");
                        if(utils.response.isException(data)) return layer.msg(data.msg);
                        tableIndex.reload();
                        layer.msg("操作成功");
                    })
                });
            }else if(layEvent === 'enable'){ //解冻
                layer.confirm('您确定要解冻此用户账户吗？', {
                    btn: ['解冻','取消'] //按钮
                }, function(){
                    data.isEnable = 1;
                    service.updateEnable(data, function (data) {
                        if(utils.response.isErrorByCode(data)) return layer.msg("操作失败");
                        if(utils.response.isException(data)) return layer.msg(data.msg);
                        tableIndex.reload();
                        layer.msg("操作成功");
                    })
                });
            } else if(layEvent === 'renew'){ //删除
                layer.confirm('您确定要恢复此用户账户吗？', {
                    btn: ['恢复','取消'] //按钮
                }, function(){
                    data.isEnable = 1;
                    data.isDelete = 0;
                    service.updateAvailability(data, function (data) {
                        if(utils.response.isErrorByCode(data)) return layer.msg("操作失败");
                        if(utils.response.isException(data)) return layer.msg(data.msg);
                        tableIndex.reload();
                        layer.msg("操作成功");
                    })
                });
            } else if(layEvent === 'setService'){ //删除
                layer.confirm('确定要提升为客服账号？', {
                    btn: ['确定','取消'] //按钮
                }, function(){
                    service.changeRole({
                        "userId": data.userId,
                        "roleName": "STAFF"
                    }, function (data) {
                        if(utils.response.isErrorByCode(data)) return layer.msg("操作失败");
                        if(utils.response.isException(data)) return layer.msg(data.msg);
                        tableIndex.reload();
                        layer.msg("操作成功");
                    })
                });
            } else if(layEvent === 'setAdmin'){ //删除
                layer.confirm('确定要提升为管理员账号？', {
                    btn: ['确定','取消'] //按钮
                }, function(){
                    data.isEnable = 1;
                    data.isDelete = 0;
                    service.changeRole({
                        "userId": data.userId,
                        "roleName": "ADMIN"
                    }, function (data) {
                        if(utils.response.isErrorByCode(data)) return layer.msg("操作失败");
                        if(utils.response.isException(data)) return layer.msg(data.msg);
                        tableIndex.reload();
                        layer.msg("操作成功");
                    })
                });
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
         * 修改密码 韦德 2018年8月30日14:58:13
         * @param param
         * @param callback
         */
        updatePassword: function (param, callback) {
            param.addDate = utils.date.timestampConvert(param.addDate);
            if(param.updateDate != null) param.updateDate = utils.date.timestampConvert(param.updateDate);
            $.post(r + "/updatePassword", param, function (data) {
                callback(data);
            });
        },
        /**
         * 删除用户 韦德 2018年8月30日14:58:13
         * @param param
         * @param callback
         */
        updateAvailability: function (param, callback) {
            param.addDate = utils.date.timestampConvert(param.addDate);
            if(param.updateDate != null) param.updateDate = utils.date.timestampConvert(param.updateDate);
            $.post(r + "/updateAvailability", param, function (data) {
                callback(data);
            });
        },
        /**
         * 冻结用户 韦德 2018年8月30日14:58:13
         * @param param
         * @param callback
         */
        updateEnable: function (param, callback) {
            param.addDate = utils.date.timestampConvert(param.addDate);
            if(param.updateDate != null) param.updateDate = utils.date.timestampConvert(param.updateDate);
            $.post(r + "/updateEnable", param, function (data) {
                callback(data);
            });
        },
        /**
         * 设置权限 韦德 2018年9月1日02:47:07
         * @param param
         * @param callback
         */
        changeRole: function (param, callback) {
            $.post(r + "/changeRole", param, function (data) {
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
        {type: "numbers", fixed: 'left'}
        , {field: 'Pid', title: '商品id', width: 80, sort: true}
        , {field: 'ProductName', title: '商品名称', width: 180}
        , { field: 'Amount', title: '价格', width: 180, templet: function (d) {
            return "<span style='color: #c2330f;'>" + d.Amount + "</span>";
            }}
        , {
            field: 'Description', title: '简述', width: 150, templet: function (d) {
                return (d.Description != null) ? d.Description.substr(0, 10) + "..." : "";
            }
        }
        , {field: 'Status', title: '状态', width: 120, sort:true,  templet: function (d) {
            var state = "未上架";
            if (d.Status == 1){
                    state = "销售中";
            } else if (d.Status == 1){
                    state = "已下架";
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
        , {fixed: 'right',title: '操作', width: 480, align: 'center', toolbar: "#barOption"}
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