﻿
var baseInfo = {
    HospitalInfo: {
        "Account": null,//账户
        "Pwd": null,  // 密码
        "OperatorId": null,   //操作人员
        "InsuranceType": null, // 险种类型,
        "IdentityMark": null,    //身份标志   身份证号或个人编号
        "AfferentSign": null// 传入标志 1为公民身份号码 2为个人编号,
    },
    Inpatient: {
        "PersonalCoding": null ,//个人编码
        "PatientName": null,  // 病人姓名
        "PatientSex": null,   //性别
        "Birthday": null,    //出生日期
        "InsuranceType": null, // 险种类型,
        "IdCardNo": null ,// 身份证,
        "ResidentInsuranceBalance": null,//居民医保账户余额
        "WorkersInsuranceBalance": null,//职工医保账户余额
        "MentorBalance": null, //门特余额
        "OverallPaymentBalance": null //统筹支付余额

    }
};

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
        iniJs("#SettleData").empty();
        iniJs("#SettleData").append(html);
    }

}

function getHospitalInfo(getHospitalInfoParam) {
    
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
            
            console.log(data);
            if (data.Success === false) {
                var errData = data.Message;
                msgError(errData);
            } else {

                baseInfo.HospitalInfo["Account"] = data.Data.MedicalInsuranceAccount;
                baseInfo.HospitalInfo["Pwd"] = data.Data.MedicalInsurancePwd;
                baseInfo.HospitalInfo["OperatorId"] = iniJs("#empid").val();
                getHospitalInfoParam();
            
            }
        }

    });

}

function getInpatientInfo(getInpatientInfoBack)
{
    debugger;
    baseInfo.HospitalInfo["AfferentSign"] = iniJs("#AfferentSign").val();
    baseInfo.HospitalInfo["IdentityMark"] = iniJs("#IdentityMark").val();
    var activeX = document.getElementById("CSharpActiveX");
    var activeData = activeX.OutpatientMethods(JSON.stringify(baseInfo.HospitalInfo), JSON.stringify(baseInfo.HospitalInfo),"GetUserInfo");
        var activeJsonData = JSON.parse(activeData);
        if (activeJsonData.Success === false) {
            msgError(activeJsonData.Message);
        } else {
            //病人信息赋值
            var activeJsonInfo = JSON.parse(activeJsonData.Data);
            baseInfo.Inpatient["PersonalCoding"] = activeJsonInfo.PersonalCoding;
            baseInfo.Inpatient["PatientName"] = activeJsonInfo.PatientName;
            baseInfo.Inpatient["PatientSex"] = activeJsonInfo.PatientSex;
            baseInfo.Inpatient["Birthday"] = activeJsonInfo.Birthday;
            baseInfo.Inpatient["InsuranceType"] = activeJsonInfo.InsuranceType;
            baseInfo.Inpatient["IdCardNo"] = activeJsonInfo.IdCardNo;
            baseInfo.Inpatient["ResidentInsuranceBalance"] = activeJsonInfo.ResidentInsuranceBalance;
            baseInfo.Inpatient["WorkersInsuranceBalance"] = activeJsonInfo.WorkersInsuranceBalance;
            baseInfo.Inpatient["MentorBalance"] = activeJsonInfo.MentorBalance;
            baseInfo.Inpatient["OverallPaymentBalance"] = activeJsonInfo.OverallPaymentBalance;
            getInpatientInfoBack();
        }
}

function msgSuccess(successData) {
    iniMsg.alert(successData, { icon: 6, shade: 0.1, skin: 'layui-layer-molv', title: '温馨提示' });
}
function msgError(errData) {
    iniMsg.alert(errData, { skin: 'layui-layer-molv', icon: 5, title: '错误提示' });
}
