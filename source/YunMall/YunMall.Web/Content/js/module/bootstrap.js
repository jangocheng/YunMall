$(function() {
    var $toReg = $("#toReg"),
        $toLogin = $("#toLogin"),
        $login = $("#login"),
        $reg = $("#reg"),
        $loginBtn = $("#loginBtn"),
        $regBtn = $("#regBtn");


    // 去注册页面
    $toReg.click(function() {
        $login.slideUp(500);
        $reg.slideDown(500);
    });

    // 去登录页面
    $toLogin.click(function() {
        $reg.slideUp(500);
        $login.slideDown(500);
    });


    // 读取上次登录的用户名并填充
    readLocalStorage();

    // 回车提交验证
    $("input[name='password']").keydown(function(e) {
        if (e.keyCode === "13") {
            $("#login").submit();
        }
    });


    // 表单验证 
    layui.use(['form'], function () {
        var form = layui.form,
            layer = layui.layer;

        //自定义验证规则
        form.verify({
            username: function (value) {
                if (value.length < 5) {
                    return '用户名至少是5位字母/数字';
                }
            }
            , password: [/(.+){6,12}$/, '密码必须6到12位']
        });

        // 登录验证
        form.on('submit(login)', function (data) {
            var username = $("input[name='username']").val(),
                password = $("input[name='password']").val();
            loginRequest(username, password);
            return false; //阻止表单跳转。如果需要表单跳转，去掉这段即可。
        });
        form.render();
    });
});


function readLocalStorage() {
    var storage = localStorage.getItem("login");
    if (storage === null || storage === "undefined") return;
    $("input[name='username']").eq(0).val(storage);
}


function loginRequest(username, password) {

    $.post("./login",
        {
            "username": username,
            "password": password
        },
        function(data) {
            if (data === null) return layer.msg("登录失败");
            if (data.Code === 1) return layer.open({ title: "sorry", content: data.Msg});

            var msg = "";
            switch (data.Msg) {
                case "L00000":
                    localStorage.setItem("login", username);
                    localStorage.setItem("dynamicNavigationUrl", "/content/json/nav_main.json");
                    localStorage.skin = 2;
                    location.href = "/management/index";
                    return;
                case "L00001":
                    msg = "登录异常";
                    break;
                case "L00002":
                    msg = "用户不存在";
                    break;
                case "L00003":
                    msg = "密码错误";
                    break;
                case "L00004":
                    msg = "冻结/禁止登录";
                    break;
                case "L00005":
                    msg = "账号或密码填写格式不正确";
                    break;
            }

            layer.msg(msg);
        });

    
}