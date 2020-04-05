

//按钮状态
function buttonStatus(buttonId, status) {
    //取消禁用
    if (status === true) {
        iniJs("#" + buttonId).attr("class", "layui-btn layui-btn-radius");
        iniJs("#" + buttonId).removeAttr("disabled");
    }//禁用
    else {
        iniJs("#" + buttonId).attr("class", "layui-btn layui-btn-disabled layui-btn-radius");
        iniJs("#" + buttonId).attr("disabled", 'disabled');
    }
}
//获取结算返回值
function settlementData(data) {
    var html = "";
    for (var i in data) {
        if (data.hasOwnProperty(i)) {

            html += '<div class="layui-inline">';
            html += '     <label class="layui-form-label">' + data[i].Name + '</label>';
            html += '     <div class="layui-input-inline">';
            html += '     <input type="text" name="AdmissionDiagnosticDoctor" autocomplete="off"   value="' + data[i].Value + '" disabled class="layui-input layui-disabled">';
            html += '     </div>';
            html += '</div>';
        }
    }
    if (html !== "") {
        iniJs("#SettleData").append(html);
    }

}
