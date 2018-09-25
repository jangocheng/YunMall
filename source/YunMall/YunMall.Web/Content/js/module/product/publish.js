$(function () {

    // 实时计算真实利润
    $('input[name="price"]').eq(0).bind('input propertychange', function () {
        var returnRate = $("#returnRate").val();
        var realPrice = parseFloat($(this).val()) - (parseFloat($(this).val()) * parseFloat(returnRate) / 100);
        $("label[name='realAmount']").text("￥" + realPrice);
    });


    // 表单验证 
    layui.use(['form','upload'], function () {
        var form = layui.form,
            layer = layui.layer,
            upload = layui.upload;

        // 表单疑问提醒
        var tipsIndex = 0;
        $("i[class~='tips[rate]']").hover(function () {
            tipsIndex = layer.tips('定价 - (定价 * 平台返利百分比) = 实收利润', this,
                {
                    tips: [2, '#3D6DDE'],
                    area: ["auto","auto"]
                });
            },
            function () {
                layer.close(tipsIndex);
            });

        //自定义验证规则
        form.verify({
            productName: function (value) {
                if (value.length < 4) {
                    return '商品名称至少为4个字符';
                }
                if (value.length > 255) {
                    return '商品名称最多255个字符';
                }
            }
            , price: [/(^[1-9]([0-9]+)?(\.[0-9]{1,2})?$)|(^(0){1}$)|(^[0-9]\.[0-9]([0-9])?$)/, '请输入正确的价格']
            , description: function (value) {
                if (value.length > 255) {
                    return '商品描述最多255个字符';
                }
            }
        });

        // 发布验证
        form.on('submit(publish)', function (data) {
            var productName = $("input[name='productName']").val(),
                price = $("input[name='price']").val(),
                description = $("textarea[name='description").val(),
                $type = $("input[name='type']"),
                categoryId = $("select[name='category']").val(),
                mainImage = $("#mainImage").val();


            if (productName === null || productName === undefined || productName.length <= 0) {
                layer.msg("请输入商品名称");
                return false;
            }

            if (price === null || price === undefined || price.length <= 0) {
                layer.msg("请输入商品价格");
                return false;
            }

            var type = 0;
            if ($type.eq(0).next().hasClass("layui-unselect")) type = 0;
            if ($type.eq(1).next().hasClass("layui-unselect")) type = 1;
            if ($type.eq(0).next().hasClass("layui-unselect") && $type.eq(1).next().hasClass("layui-unselect")) type = 2;


            

            $.post("./publish/add",
                {
                    "pid": $("#pid").val() === null ? null : $("#pid").val(),
                    "productName": productName,
                    "price": parseFloat(price),
                    "categoryId": categoryId,
                    "type": parseInt(type),
                    "description": description,
                    "mainImage": mainImage
                },
                function(data) {
                    if (utils.response.isError(data)) return data.Msg === null ? layer.msg("发布失败") : layer.msg(data.Msg);
                    location.href = "/product/manage";
                });
            return false; //阻止表单跳转。如果需要表单跳转，去掉这段即可。
        });

        //拖拽上传
        upload.render({
            elem: '#mainImageUploader'
            , url: '/file/upload?dir=upload_image'
            , done: function (data) {
                if (utils.response.isError(data)) return data.Msg === null ? layer.msg("上传失败") : layer.msg(data.Msg);
                $("#mainImage").val(data.Msg);
                layer.msg("上传成功");

                $("#mainImageUploader").hide();
                $("#productImage img").attr("src", data.Msg);
                $("#productImage").show();
            }
        });

        form.render();
    });

    // 重新上传
    $("#productImage").click(function() {
        $("#mainImageUploader").show();
        $("#productImage").hide();
    });
})