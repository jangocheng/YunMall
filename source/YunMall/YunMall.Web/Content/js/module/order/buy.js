function findProduct(arr, id) {
    for (var i = 0; i < arr.length; i++) {
        if (arr[i].Pid === id) {
            return arr[i];
        }
    }
}

layui.use(['laypage', 'layer'],
    function () {
        var laypage = layui.laypage, layer = layui.layer;

        //测试数据
        var nData = JSON.parse($("#products").val());
        var data = [];
        var productMaps = JSON.parse($("#productMaps").val());

        for (var j = 0; j < productMaps.length; j++) {
            var key = productMaps[j];
            var product = findProduct(nData, key.Pid);
            if (product != null) {
                product.Count = key.Count;
                data.push(product);
            }
        }  

        //调用分页
        laypage.render({
            elem: 'page'
            , count: data.length
            , limit: 3
            , jump: function (obj) {
                jumpPage(obj);
            }
        });

        $("#goPay").click(function() {
            layer.prompt({ title: '请输入支付密码', formType: 1 },
                function (pass, index) {

                    $.post("/order/buy/payment",
                        {
                            type: 0,
                            security: pass,
                            products: 
                        });

                    layer.close(index);
                });
        });


        // 选中支付类型
        $(".payment li").click(function () {
            $(".payment li").removeClass("payment-selected");
            $(this).addClass("payment-selected");
            $("#tradeType").val($(this).data("id"));
        });

        function jumpPage(obj) {
            //模拟渲染
            drawTable(obj);
             
            var total = 0;
            for (var i = 0; i < data.length; i++) {
                total += data[i].Amount * data[i].Count;
            }
            $("#total").text(total);

            $(".num").keydown(function () {
                var ignores = [8, 18, 144, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105]; 
                for (var i = 0; i < ignores.length; i++) {
                    if (event.keyCode === ignores[i]) {
                        return true;
                    }
                }
                return /[\d]/.test(String.fromCharCode(event.keyCode));
            });
            $(".num").blur(function () {
                var amount = $(this).parent().prev().find("span").data("amount");
                var pid = $(this).parent().prev().find("span").data("pid");
                var count = $(this).val();
                if (count === null || count.length <= 0) count = 1;
                if (count * amount <= 0) return;
                $(this).parent().prev().find("span").text(count * parseFloat(amount));


                for (var i = 0; i < data.length; i++) {
                    if (pid === data[i].Pid) {
                        data[i].Amount = count * parseFloat(amount);
                        data[i].Count = count;
                    };
                }

                total = 0;
                for (var i = 0; i < data.length; i++) {
                    total += data[i].Amount * data[i].Count;
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
                str += '      <img src="' + item.MainImage + '" />';
                str += "</td>";
                str += "<td>";
                if (item.ProductName.length > 10) {
                    str += '  <a href="javascript:void(0)" class="title " data-pid="' + item.Pid + '"  title="' +
                        item.ProductName +
                        '">' +
                        item.ProductName.substr(0, 10).trim() +
                        '...</a>';
                } else {
                    str += '  <a href="javascript:void(0)" class="title "  data-pid="' + item.Pid + '"  title="' +
                        item.ProductName +
                        '">' +
                        item.ProductName +
                        '</a>';
                }
                str += "</td>";
                str += "<td>";
                str += '  <span class="price"  data-pid="' + item.Pid + '" data-amount="' + item.Amount + '">' + item.Amount + '</span>';
                str += "</td>";
                str += "<td>";
                str +=
                    '  <input class="num" type="number" name="name" value="' + item.Count + '"/>';
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