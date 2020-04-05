
    //var benDingJs = {};
    //benDingJs.test=function(parameters) {
    //    console.log("123999");
    //    iniPageData();
    //}

//按钮状态
    function buttonStatus(buttonId, status) {
        //取消禁用
        if (status === true) {
            $("#" + buttonId).attr("class", "layui-btn layui-btn-radius");

        }//禁用
        else {
            $("#" + buttonId).attr("class", "layui-btn layui-btn-disabled layui-btn-radius");

        }

    }