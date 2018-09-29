layui.use(['laypage', 'layer'],
    function () {
        var laypage = layui.laypage, layer = layui.layer;

        //测试数据
        var data = [
            {
                pid: 1,
                productName: "中秋节月饼(广州)",
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


        //调用分页
        laypage.render({
            elem: 'page'
            , count: data.length
            , limit: 3
            , jump: function (obj) {
                jumpPage(obj);
            }
        });

        $("#goPay").click(function () {
            layer.prompt({ title: '请输入支付密码', formType: 1 }, function (pass, index) {
                layer.close(index);
                /*layer.prompt({title: '随便写点啥，并确认', formType: 2}, function(text, index){
                    layer.close(index);
                    layer.msg('演示完毕！您的口令：'+ pass +'<br>您最后写下了：'+text);
                });*/
            });
        })

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
                str += '      <img src="' + item.mainImage + '" />';
                str += "</td>";
                str += "<td>";
                str += '  <a href="#" class="title">' + item.productName + '</a>';
                str += "</td>";
                str += "<td>";
                str += '  <span class="price"  data-pid="' + item.pid + '" data-amount="' + item.amount + '">' + item.amount + '</span>';
                str += "</td>";
                str += "<td>";
                str +=
                    '  <input class="num" type="number" name="name" value="' + item.count + '"/>';
                str += "</td>";
                str += "<td>";
                str += '  <button class="button button-red" name="remove">移除</button>';
                str += "</td>";
                str += "</tr>";
                str += "</tr>";
                arr.push(str);
            });
            $("tbody").html(arr.join(''));
        }

    });