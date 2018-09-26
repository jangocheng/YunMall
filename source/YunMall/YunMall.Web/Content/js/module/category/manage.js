var route = "/category/manage";
var service;
var tableIndex;
(function() {
    service = initService(route);

    // 加载数据表
    layui.use(['treetable', 'form'],
        function() {

            service.getCategorys({},
                function(data) {
                    if (utils.response.isList(data)) {
                        if (utils.response.isEmpty(data)) {
                            return layer.msg("加载经营类目列表失败");
                        }
                    } else if (utils.response.isError(data)) {
                        return layer.msg("加载经营类目失败");
                    }


                    // 数据格式转换
                    var nData = new Array();

                    for (var i = 0; i < data.data.length; i++) {
                        var item = data.data[i];
                        if (item.ParentName == null) item.ParentName = "无";
                        nData.push({
                            "id": item.Cid,
                            "pid": item.ParentId,
                            "title": item.CategoryName,
                            "parentName": item.ParentName,
                            "isLeaf": item.IsLeaf,
                            "isEnable": item.IsEnable,
                            "isDelete": item.IsDelete
                        });
                    }

                    var o = layui.$, treetable = layui.treetable;
                    treetable.render({
                        elem: '.test-tree-table',
                        data: nData,
                        field: 'title',
                        cols: [
                            {
                                field: 'id',
                                title: '类目ID',
                                width: '10%',
                            },
                            {
                                field: 'title',
                                title: '类目名称',
                                width: '40%',
                            },
                            {
                                field: 'parentName',
                                title: '父级类目',
                                width: '30%',
                            },
                            {
                                field: 'actions',
                                title: '操作',
                                width: '30%'
                            },
                        ],
                        call: function (data) {
                            if (data.isLeaf === true) {
                                return '<a class="layui-btn layui-btn layui-btn-sm" lay-filter="edit">编辑</a>';
                            } else {
                                var html =
                                    '<a class="layui-btn layui-btn layui-btn-sm layui-btn-danger" lay-filter="add" href="1.html">添加</a>';
                                html +=
                                    '<a class="layui-btn layui-btn layui-btn-sm " lay-filter="edit">编辑</a>';
                                return html;
                            }
                        }
                    });

                    treetable.on('treetable(test1)',
                        function(data) {
                            console.dir(o(data.elem).html());
                        });

                    treetable.on('treetable(add)',
                        function(data) {
                            console.dir(data);
                        });

                    treetable.on('treetable(edit)',
                        function(data) {
                            console.dir(data);
                        });

                    o('.up-all').click(function() {
                        treetable.all('up');
                    });

                    o('.down-all').click(function() {
                        treetable.all('down');
                    });


                });// get end

        });// use end 
})();

/**
 * 加载模块
 * @param r
 * @returns
 */
function initService(r) {
    return {
        /**
         * 加载数据表 韦德 2018年9月26日10:00:37 
         * @param param
         * @param callback
         */
        getCategorys: function (param, callback) {
            $.get(r + "/getCategorys", param, function (data) {
                callback(data);
            });
        }
    }
}
