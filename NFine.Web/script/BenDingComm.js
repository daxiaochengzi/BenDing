

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

function GetHospitalInfo(GetHospitalInfoParam) {
    
    var params = {
        "TransKey": iniJs("#transkey").val() /*医保交易码*/,
        "BusinessId": iniJs("#bid").val() /*当前住院记录的业务ID*/,
        "UserId": iniJs("#empid").val() /*授权操作人的ID*/
    }
    iniJs.ajax({
        type: 'get',
        url: hostNew + '/GetHospitalInfo',
        data: params,
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.Success === false) {
                var errData = data.Message;
                MsgError(errData);
                
            } else {
                
                var paramBase =
                {
                    "Account": data.Data.MedicalInsuranceAccount,
                    "Pwd": data.Data.MedicalInsurancePwd,
                    "OperatorId": iniJs("#empid").val() /*授权操作人的ID*/
                };
                GetHospitalInfoParam(paramBase);
            
            }
        }

    });

}

function MsgSuccess(successData) {
    iniMsg.alert(successData, { icon: 6, shade: 0.1, skin: 'layui-layer-molv', title: '温馨提示' });
}
function MsgError(errData) {
    iniMsg.alert(errData, { skin: 'layui-layer-molv', icon: 5, title: '错误提示' });
}
