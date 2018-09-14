$(function() {
    layui.use(['form', 'laydate'], function () {
        var form = layui.form,
            laydate = layui.laydate;
        form.render();
        laydate.render({
            elem: "#date"
        });
    });
})


/*!左侧菜单 韦德 2018年8月3日22:13:06*/
$(function () {
    layui.use(['layer', 'vip_nav'], function () {
        // 操作对象
        var layer = layui.layer
            , vipNav = layui.vip_nav
            , $ = layui.jquery;

        // 顶部左侧菜单生成 [请求地址,过滤ID,是否展开,携带参数]
        //vipNav.top_left('./json/nav_top_left.json','side-top-left',false);
        // 主体菜单生成 [请求地址,过滤ID,是否展开,携带参数]
        var dynamicNavigationUrl = localStorage.getItem("dynamicNavigationUrl");
        vipNav.main(dynamicNavigationUrl, 'side-main', true);
    });
})

