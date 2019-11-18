'use strict';
window.SwaggerTranslator = {
    _words: [],

    translate: function () {
        var $this = this;
        $('[data-sw-translate]').each(function () {
            $(this).html($this._tryTranslate($(this).html()));
            $(this).val($this._tryTranslate($(this).val()));
            $(this).attr('title', $this._tryTranslate($(this).attr('title')));
        });
    },

    setControllerSummary: function () {

        try {
            console.log($("#input_baseUrl").val());
            $.ajax({
                type: "get",
                async: true,
                url: $("#input_baseUrl").val(),
                dataType: "json",
                success: function (data) {

                    var summaryDict = data.ControllerDesc;
                    console.log(summaryDict);
                    var id, controllerName, strSummary;
                    $("#resources_container .resource").each(function (i, item) {
                        id = $(item).attr("id");
                        if (id) {
                            controllerName = id.substring(9);
                            try {
                                strSummary = summaryDict[controllerName];
                                if (strSummary) {
                                    $(item).children(".heading").children(".options").first().prepend('<li class="controller-summary" style="color:green;" title="' + strSummary + '">' + strSummary + '</li>');
                                }
                            } catch (e) {
                                console.log(e);
                            }
                        }
                    });
                }
            });
        } catch (e) {
            console.log(e);
        }
    },
    _tryTranslate: function (word) {
        return this._words[$.trim(word)] !== undefined ? this._words[$.trim(word)] : word;
    },

    learn: function (wordsMap) {
        this._words = wordsMap;
    }
};


/* jshint quotmark: double */
window.SwaggerTranslator.learn({
    "Warning: Deprecated": "���棺�ѹ�ʱ",
    "Implementation Notes": "ʵ�ֱ�ע",
    "Response Class": "��Ӧ��",
    "Status": "״̬",
    "Parameters": "����",
    "Parameter": "����",
    "Value": "ֵ",
    "Description": "����",
    "Parameter Type": "��������",
    "Data Type": "��������",
    "Response Messages": "��Ӧ��Ϣ",
    "HTTP Status Code": "HTTP״̬��",
    "Reason": "ԭ��",
    "Response Model": "��Ӧģ��",
    "Request URL": "����URL",
    "Response Body": "��Ӧ��",
    "Response Code": "��Ӧ��",
    "Response Headers": "��Ӧͷ",
    "Hide Response": "������Ӧ",
    "Headers": "ͷ",
    "Try it out!": "��һ�£�",
    "Show/Hide": "��ʾ/����",
    "List Operations": "��ʾ����",
    "Expand Operations": "չ������",
    "Raw": "ԭʼ",
    "can't parse JSON.  Raw result": "�޷�����JSON. ԭʼ���",
    "Model Schema": "ģ�ͼܹ�",
    "Model": "ģ��",
    "apply": "Ӧ��",
    "Username": "�û���",
    "Password": "����",
    "Terms of service": "��������",
    "Created by": "������",
    "See more at": "�鿴���ࣺ",
    "Contact the developer": "��ϵ������",
    "api version": "api�汾",
    "Response Content Type": "��ӦContent Type",
    "fetching resource": "���ڻ�ȡ��Դ",
    "fetching resource list": "���ڻ�ȡ��Դ�б�",
    "Explore": "���",
    "Show Swagger Petstore Example Apis": "��ʾ Swagger Petstore ʾ�� Apis",
    "Can't read from server.  It may not have the appropriate access-control-origin settings.": "�޷��ӷ�������ȡ������û����ȷ����access-control-origin��",
    "Please specify the protocol for": "��ָ��Э�飺",
    "Can't read swagger JSON from": "�޷���ȡswagger JSON��",
    "Finished Loading Resource Information. Rendering Swagger UI": "�Ѽ�����Դ��Ϣ��������ȾSwagger UI",
    "Unable to read api": "�޷���ȡapi",
    "from path": "��·��",
    "server returned": "����������"
});
$(function () {
    window.SwaggerTranslator.translate();
    window.SwaggerTranslator.setControllerSummary();
});