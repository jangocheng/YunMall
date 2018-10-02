// 购物车
window.shopCarData = []; 

layui.use(['laypage', 'layer'],
    function () {
        var laypage = layui.laypage, layer = layui.layer;

        //测试数据
        var data = JSON.parse($("#products").val());

        //调用分页
        laypage.render({
            elem: 'page'
            , count: data.length
            , limit: 6
            , jump: function (obj) {
                jumpPage(obj);
            }
        });

        function jumpPage(obj) {
            //模拟渲染
            drawTable(obj);


            var total = 0;
            for (var i = 0; i < data.length; i++) {
                total += data[i].amount;
            }
            $("#total").text(total);

            $(".num").keydown(function () {
                var ignores = [8, 18, 144, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105];
                console.log(event.keyCode);
                for (var i = 0; i < ignores.length; i++) {
                    if (event.keyCode == ignores[i]) {
                        return true;
                    }
                }
                return /[\d]/.test(String.fromCharCode(event.keyCode));
            });
            $(".num").blur(function () {
                var amount = $(this).parent().prev().find("span").data("amount");
                var pid = $(this).parent().prev().find("span").data("pid");
                var count = $(this).val();
                $(this).parent().prev().find("span").text(count * parseFloat(amount));


                for (var i = 0; i < data.length; i++) {
                    if (pid === data[i].pid) {
                        data[i].amount = count * parseFloat(amount);
                        data[i].count = count;
                    };
                }

                total = 0;
                for (var i = 0; i < data.length; i++) {
                    total += data[i].amount;
                }
                $("#total").text(total);
            });

            $("button[name='remove']").click(function () {
                var pid = $(this).parent().prev().find("span").data("pid");
                $(this).parent().parent().hide(500, function () {
                    var index = 0;
                    for (var i = 0; i < data.length; i++) {
                        if (pid === data[i].pid) {
                            index = i;
                        };
                    }
                    data.splice(index, 1);

                    laypage.render({
                        elem: 'page'
                        , count: data.length
                        , limit: 3
                        , jump: function (obj) {
                            jumpPage(obj);
                        }
                    });
                });

            });
        }

        function drawTable(obj) {
            var arr = []
                , thisData = data.concat().splice(obj.curr * obj.limit - obj.limit, obj.limit);
            layui.each(thisData, function (index, item) {
                var str = "";
                str += "<tr>";
                str += "  <td>";
                str += '      <img style="" src="' + item.MainImage + '" />';
                str += "</td>";
                str += "<td>";
                if (item.ProductName.length > 30) {
                    str += '  <a href="javascript:void(0)" class="title preview" data-pid="' + item.Pid + '"  title="' +
                        item.ProductName +
                        '">' +
                        item.ProductName.substr(0, 30).trim() +
                        '...</a>';
                } else {
                    str += '  <a href="javascript:void(0)" class="title preview"  data-pid="' + item.Pid + '"  title="' +
                        item.ProductName +
                        '">' +
                        item.ProductName + 
                        '</a>';
                }
                str += "</td>";
                str += "<td>";
                str += '  <span class="price" style="font-size: 20px"  data-pid="' + item.Pid + '" data-amount="' + item.Amount + '">' + item.Amount + '</span>';
                str += "</td>";
                str += "<td>";
                str +=
                    '  <button class="button button-blue" name="addShopCar">加入购物车</button>';
                str += "</td>";
                str += "</tr>";
                str += "</tr>";
                arr.push(str);
            });

            if (thisData.length <= 0) {
                $(".table-product")
                    .html(
                        '<div style="width: 100%; margin: 0 auto; text-align: center;line-height: 300px">  <img style="width:360px" src="/content/images/empty.png" /> </div>');
            } else {
                $("tbody").html(arr.join(''));
            }

            var list = "";

            // 添加到购物车
            $("button[name='addShopCar']").click(function () {
                var title = $(this).parent().parent().find("td").eq(1).find("a").attr("title");
                var name = $(this).parent().prev().prev().text();
                var pid = $(this).parent().prev().find("span").data("pid");
                shopCarData.push(pid);
                $("#shopCarCount").text(shopCarData.length);
                $(this).animate({ width: "90px", height: "35px" }, 300, null, function () {
                    $(this).animate({ width: "100px", height: "30px" }, 300);

                    if ($("#shopCar").find("li").length < 3) {
                        var shortName = name;
                        if (name.length > 15) {
                            shortName = name.substr(0, 15);
                        }
                        list += "<li title='" + title + "'>" + shortName + "</li>";
                        $("#shopCar").html(list);
                    }
                });
            });
             

            // 预览商品信息
            $(".preview").bind("click", function() {
                $.get('/shop/preview',
                    {
                        pid: $(this).data("pid")
                    }, function (str) {
                        layer.open({
                            area: ["800px","650px"],
                            title: "商品详情",
                            type: 1,
                            shadeClose : true,
                            content: str //注意，如果str是object，那么需要字符拼接。
                        });
                    });
            });

            // 去结算
            $("button[name='goPayment']").bind("click", function() {
                var href = '/order/buy?products=';
                if (shopCarData.length === 0 ) {
                    // 请选择商品
                    return layer.msg("请您至少选中一件商品");
                } else if (shopCarData.length != 0) {
                    href += shopCarData.join(",");
                } else {
                    return layer.msg("未知错误");
                }
                location.href = href;
            });
        }


    });


