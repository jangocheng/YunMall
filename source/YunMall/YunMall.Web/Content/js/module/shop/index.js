layui.use(['laypage', 'layer'],
    function () {
        var laypage = layui.laypage, layer = layui.layer;

        //测试数据
        var data = [
            {
                pid: 1,
                productName: "中秋节月饼中秋节月饼中秋节月饼",
                amount: 128,
                count: 1,
                mainImage: "/Content/upload_image/20180921/636731695907172444.jpg"
            },
            {
                pid: 2,
                productName: "中秋节月饼(广州)",
                amount: 128,
                count: 1,
                mainImage: "/Content/upload_image/20180921/636731695907172444.jpg"
            },
            {
                pid: 3,
                productName: "中秋节月饼(广州)",
                amount: 128,
                count: 1,
                mainImage: "/Content/upload_image/20180921/636731695907172444.jpg"
            },
            {
                pid: 4,
                productName: "中秋节月饼(广州)",
                amount: 128,
                count: 1,
                mainImage: "/Content/upload_image/20180921/636731695907172444.jpg"
            },
            {
                pid: 5,
                productName: "中秋节月饼(广州)",
                amount: 128,
                count: 1,
                mainImage: "/Content/upload_image/20180921/636731695907172444.jpg"
            },
            {
                pid: 6,
                productName: "中秋节月饼(广州)",
                amount: 128,
                count: 1,
                mainImage: "/Content/upload_image/20180921/636731695907172444.jpg"
            },
            {
                pid: 7,
                productName: "中秋节月饼(广州)",
                amount: 128,
                count: 1,
                mainImage: "/Content/upload_image/20180921/636731695907172444.jpg"
            }
        ];


        // 购物车
        var shopCarData = []; 

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
                str += '      <img style="" src="' + item.mainImage + '" />';
                str += "</td>";
                str += "<td>";
                str += '  <a href="#" class="title">' + item.productName + '</a>';
                str += "</td>";
                str += "<td>";
                str += '  <span class="price" style="font-size: 20px"  data-pid="' + item.pid + '" data-amount="' + item.amount + '">' + item.amount + '</span>';
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
                var name = $(this).parent().prev().prev().text();
                var pid = $(this).parent().prev().find("span").data("pid");
                shopCarData.push({
                    pid: pid,
                    pname: name,
                });
                $("#shopCarCount").text(shopCarData.length);
                $(this).animate({ width: "90px", height: "35px" }, 300, null, function () {
                    $(this).animate({ width: "100px", height: "30px" }, 300);

                    if ($("#shopCar").find("li").length < 3) {
                        var shortName = name;
                        if (name.length > 15) {
                            shortName = name.substr(0, 15);
                        }
                        list += "<li title=" + name + ">" + shortName + "</li>";
                        $("#shopCar").html(list);
                    }
                });
            });
        }
    });