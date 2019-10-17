using BenDingActive.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Service;
using BenDingActive.Model.Dto;
using BenDingActive.Model.Params;
using Newtonsoft.Json;

namespace BenDingActive.Service
{
  public  class ResidentMedicalInsuranceService
    {
        /// <summary>
        /// 获取个人基础资料
        /// </summary>
        /// <param name="param"></param>
       
        public string GetUserInfo(string param)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new IniDto();
            try
            {
                
                var dataIni = WorkersMedicalInsurance.ConnectAppServer_cxjb("cpq2677", "888888");
                if (dataIni == 1)
                {
                    // var paramIni= JsonConvert.DeserializeObject<UserInfoParam>(param);  
                    var xmlStr = XmlHelp.SaveXml(param);
                    if (xmlStr)
                    {
                        int result = WorkersMedicalInsurance.CallService_cxjb("CXJB001");
                        if (result == 1)
                        {
                            data = XmlHelp.DeSerializerModels(new UserInfoDto());
                            if (data.PO_FHZ == "1")
                            {
                                resultData.Data = JsonConvert.SerializeObject(data);
                            }
                            else
                            {
                                throw new Exception(data.PO_MSG);
                            }


                        }
                        else
                        {
                            throw new Exception("系统登录失败!!!");
                        }

                    }

                }


            }
            catch (Exception e)
            {
                resultData.Code = false;
                resultData.Message = e.Message;
                Logs.LogWrite(new LogParam()
                {
                    Msg = e.Message,

                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data)

                });

            }

            return JsonConvert.SerializeObject(resultData);

        }
        /// <summary>
        /// 入院登记
        /// </summary>
        /// <returns></returns>
        public string HospitalizationRegister(string param)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new IniDto();
            try
            {
                //var paramIni = JsonConvert.DeserializeObject<HospitalizationRegisterParam>(param);

                var xmlStr =  XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("CXJB002");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModels(new HospitalizationRegisterDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception(data.PO_MSG);
                        }


                    }

                }

            }
            catch (Exception e)
            {
                resultData.Code = false;
                resultData.Message = e.Message;
                Logs.LogWrite(new LogParam()
                {
                    Msg = e.Message,
                    OperatorCode = "",
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 住院资料修改
        /// </summary>
        /// <returns></returns>
        public string HospitalizationModify(string param)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new IniDto();
            try
            {
               // var paramIni = JsonConvert.DeserializeObject<HospitalizationModifyParam>(param);

                var xmlStr =  XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("CXJB003");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModels(new HospitalizationModifyDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception(data.PO_MSG);
                        }


                    }

                }

            }
            catch (Exception e)
            {
                resultData.Code = false;
                resultData.Message = e.Message;
                Logs.LogWrite(new LogParam()
                {
                    Msg = e.Message,
                    OperatorCode = "",
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);

        }
        /// <summary>
        /// 处方上传
        /// </summary>
        /// <returns></returns>
        public string PrescriptionUpload(string param)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new IniDto();
            try
            {
                //var paramIni = JsonConvert.DeserializeObject<PrescriptionUploadParam>(param);

                var xmlStr =  XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("CXJB004");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModels(new PrescriptionUploadDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception(data.PO_MSG);
                        }


                    }

                }

            }
            catch (Exception e)
            {
                resultData.Code = false;
                resultData.Message = e.Message;
                Logs.LogWrite(new LogParam()
                {
                    Msg = e.Message,
                    OperatorCode = "",
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data)

                });

            }

            return  JsonConvert.SerializeObject(resultData);


        }
        /// <summary>
        /// 处方删除
        /// </summary>
        public string PrescriptionDelete(string param)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new IniDto();
            try
            {
               // var paramIni = JsonConvert.DeserializeObject<PrescriptionDeleteParam>(param);

                var xmlStr =  XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("CXJB005");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModels(new PrescriptionDeleteDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception(data.PO_MSG);
                        }


                    }

                }

            }
            catch (Exception e)
            {
                resultData.Code = false;
                resultData.Message = e.Message;
                Logs.LogWrite(new LogParam()
                {
                    Msg = e.Message,
                    OperatorCode = "",
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 处方明细查询
        /// </summary>
        public string PrescriptionDetailQuery(string param)
        {
            var resultData = new ApiJsonResultDatas { Code = true };

            try
            {
                //var paramIni = JsonConvert.DeserializeObject<PrescriptionDetailQueryParam>(param);

                var xmlStr =  XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("CXJB006");
                    if (result == 1)
                    {
                        var validData = XmlHelp.ValidXml("CFMX");
                        if (validData.PO_FHZ == "1")
                        {
                            if (validData.IsRows)
                            {
                                var data = XmlHelp.DeSerializerModels(new PrescriptionDetailQueryRow());
                                resultData.Data = JsonConvert.SerializeObject(data);
                            }
                            else
                            {
                                var data = XmlHelp.DeSerializerModels(new PrescriptionDetailQueryDto());
                                resultData.Data = JsonConvert.SerializeObject(data);
                            }


                        }
                        else
                        {
                            throw new Exception(validData.PO_MSG);
                        }
                    }

                }

            }
            catch (Exception e)
            {
                resultData.Code = false;
                resultData.Message = e.Message;
                Logs.LogWrite(new LogParam()
                {
                    Msg = e.Message,
                    OperatorCode = "",
                    Params = Logs.ToJson(param),
                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 批次确认
        /// </summary>
        public string BatchConfirmation(string param)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new IniDto();
            try
            {
               // var paramIni = JsonConvert.DeserializeObject<BatchConfirmationParam>(param);
                var xmlStr =  XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("CXJB007");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModels(new BatchConfirmationDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception(data.PO_MSG);
                        }


                    }

                }

            }
            catch (Exception e)
            {
                resultData.Code = false;
                resultData.Message = e.Message;
                Logs.LogWrite(new LogParam()
                {
                    Msg = e.Message,
                    OperatorCode = "",
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);

        }
        /// <summary>
        /// 批次未确认
        /// </summary>
        public string BatchUnConfirmation(string param)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new IniDto();
            try
            {
              //  var paramIni = JsonConvert.DeserializeObject<BatchUnConfirmationParam>(param);

                var xmlStr =  XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("CXJB008");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModels(new BatchUnConfirmationDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception(data.PO_MSG);
                        }


                    }

                }

            }
            catch (Exception e)
            {
                resultData.Code = false;
                resultData.Message = e.Message;
                Logs.LogWrite(new LogParam()
                {
                    Msg = e.Message,
                    OperatorCode = "",
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 费用预结算
        /// </summary>
        public string CostPreSettlement(string param)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new IniDto();
            try
            {
                //var paramIni = JsonConvert.DeserializeObject<CostPreSettlementParam>(param);

                var xmlStr =  XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("CXJB009");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModels(new CostPreSettlementDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception(data.PO_MSG);
                        }


                    }

                }

            }
            catch (Exception e)
            {
                resultData.Code = false;
                resultData.Message = e.Message;
                Logs.LogWrite(new LogParam()
                {
                    Msg = e.Message,
                    OperatorCode = "",
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 费用结算
        /// </summary>
        public string CostSettlement(string param)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new IniDto();
            try
            {
              //  var paramIni = JsonConvert.DeserializeObject<CostSettlementParam>(param);

                var xmlStr =  XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("CXJB010");
                    if (result == 1)
                    {
                      
                       
                           data = XmlHelp.DeSerializerModels(new CostSettlementDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception(data.PO_MSG);
                        }


                    }

                }

            }
            catch (Exception e)
            {
                resultData.Code = false;
                resultData.Message = e.Message;
                Logs.LogWrite(new LogParam()
                {
                    Msg = e.Message,
                    OperatorCode = "",
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 入院结算取消
        /// </summary>
        public string HospitalizedSettlementCancel(string param)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new IniDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<HospitalizedSettlementCancelParam>(param);

                var xmlStr =  XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("CXJB011");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModels(new HospitalizedSettlementCancelDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception(data.PO_MSG);
                        }

                    }

                }

            }
            catch (Exception e)
            {
                resultData.Code = false;
                resultData.Message = e.Message;
                Logs.LogWrite(new LogParam()
                {
                    Msg = e.Message,
                    OperatorCode = "",
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 费用结算查询
        /// </summary>
        public string CostSettlementQuery(string param)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new IniDto();
            try
            {
                //var paramIni = JsonConvert.DeserializeObject<CostSettlementQueryParam>(param);

                var xmlStr =  XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("CXJB012");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModels(new CostSettlementQueryDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception(data.PO_MSG);
                        }


                    }

                }

            }
            catch (Exception e)
            {
                resultData.Code = false;
                resultData.Message = e.Message;
                Logs.LogWrite(new LogParam()
                {
                    Msg = e.Message,
                    OperatorCode = "",
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 项目下载
        /// </summary>
        public string ProjectDownload(string param)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new IniDto();
            try
            {
               // var paramIni = JsonConvert.DeserializeObject<ProjectDownloadParam>(param);

                var xmlStr =  XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("CXJB019");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModels(new ProjectDownloadDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception(data.PO_MSG);
                        }


                    }

                }

            }
            catch (Exception e)
            {
                resultData.Code = false;
                resultData.Message = e.Message;
                Logs.LogWrite(new LogParam()
                {
                    Msg = e.Message,
                    OperatorCode = "",
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data)

                });

            }

            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 特殊疾病认定
        /// </summary>
        public string IdentificationSpecialDiseases(string param)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new IniDto();
            try
            {
              //  var paramIni = JsonConvert.DeserializeObject<IdentificationSpecialDiseasesParam>(param);

                var xmlStr =  XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("CXJB013");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModels(new IdentificationSpecialDiseasesDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception(data.PO_MSG);
                        }


                    }

                }

            }
            catch (Exception e)
            {
                resultData.Code = false;
                resultData.Message = e.Message;
                Logs.LogWrite(new LogParam()
                {
                    Msg = e.Message,
                    OperatorCode = "",
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 特殊疾病认定取消
        /// </summary>
        public string IdentificationSpecialDiseasesCancel(string param)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new IniDto();
            try
            {
                //var paramIni = JsonConvert.DeserializeObject<IdentificationSpecialDiseasesCancelParam>(param);

                var xmlStr =  XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("MTBX002");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModels(new IdentificationSpecialCancelDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception(data.PO_MSG);
                        }


                    }

                }

            }
            catch (Exception e)
            {
                resultData.Code = false;
                resultData.Message = e.Message;
                Logs.LogWrite(new LogParam()
                {
                    Msg = e.Message,
                    OperatorCode = "",
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 特殊疾病查询
        /// </summary>
        public string IdentificationSpecialQuery(string param)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            try
            {
               // var paramIni = JsonConvert.DeserializeObject<IdentificationSpecialDiseasesQueryParam>(param);

                var xmlStr =  XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("MTBX003");
                    if (result == 1)
                    {

                        var dataValid = XmlHelp.ValidXml("PO_RDXX");
                        //if (dataValid.PO_FHZ == "1")
                        //{
                        //    var data = XmlHelp.IdentificationSpecialQueryDeSerializerModels(
                        //        new IdentificationSpecialQueryDto());
                        //    resultData.Data = JsonConvert.SerializeObject(data);
                        //}
                        //else
                        //{
                        //    throw new Exception(dataValid.PO_MSG);
                        //}


                    }

                }

            }
            catch (Exception e)
            {
                resultData.Code = false;
                resultData.Message = e.Message;
                Logs.LogWrite(new LogParam()
                {
                    Msg = e.Message,
                    OperatorCode = "",
                    Params = Logs.ToJson(param),


                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        public string IdentificationSpecialHospitalQuery(string param)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new IniDto();
            try
            {
               // var paramIni = JsonConvert.DeserializeObject<IdentificationSpecialHospitalQueryParam>(param);

                var xmlStr =  XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("MTBX004");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModels(new IdentificationSpecialHospitalQueryDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception(data.PO_MSG);
                        }


                    }

                }

            }
            catch (Exception e)
            {
                resultData.Code = false;
                resultData.Message = e.Message;
                Logs.LogWrite(new LogParam()
                {
                    Msg = e.Message,
                    OperatorCode = "",
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 特殊疾病申报变更
        /// </summary>
        public string IdentificationSpecialChange(string param)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new IniDto();
            try
            {
               // var paramIni = JsonConvert.DeserializeObject<IdentificationSpecialChangParam>(param);

                var xmlStr =  XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("MTBX005");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModels(new IdentificationSpecialChangeDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception(data.PO_MSG);
                        }


                    }

                }

            }
            catch (Exception e)
            {
                resultData.Code = false;
                resultData.Message = e.Message;
                Logs.LogWrite(new LogParam()
                {
                    Msg = e.Message,
                    OperatorCode = "",
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 特殊疾病预结算
        /// </summary>
        public string IdentificationSpecialSettlement(string param)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new IniDto();
            try
            {
              //  var paramIni = JsonConvert.DeserializeObject<IdentificationSpecialSettlementParam>(param);

                var xmlStr =  XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("MTBX012");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModels(new IdentificationSpecialSettlementDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception(data.PO_MSG);
                        }


                    }

                }

            }
            catch (Exception e)
            {
                resultData.Code = false;
                resultData.Message = e.Message;
                Logs.LogWrite(new LogParam()
                {
                    Msg = e.Message,
                    OperatorCode = "",
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 特殊疾病报销
        /// </summary>
        public string IdentificationSpecialReimbursement(string param)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new IniDto();
            try
            {
               // var paramIni = JsonConvert.DeserializeObject<IdentificationSpecialSettlementParam>(param);

                var xmlStr =  XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("MTBX006");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModels(new IdentificationSpecialReimbursementDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception(data.PO_MSG);
                        }


                    }

                }

            }
            catch (Exception e)
            {
                resultData.Code = false;
                resultData.Message = e.Message;
                Logs.LogWrite(new LogParam()
                {
                    Msg = e.Message,
                    OperatorCode = "",
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 特殊疾病报销取消
        /// </summary>
        public string IdentificationSpecialCancel(string param)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new IniDto();
            try
            {
             //   var paramIni = JsonConvert.DeserializeObject<IdentificationSpecialCancelParam>(param);

                var xmlStr =  XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("MTBX007");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModels(new IdentificationSpecialCancelDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception(data.PO_MSG);
                        }


                    }

                }

            }
            catch (Exception e)
            {
                resultData.Code = false;
                resultData.Message = e.Message;
                Logs.LogWrite(new LogParam()
                {
                    Msg = e.Message,
                    OperatorCode = "",
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 特殊疾病报销查询
        /// </summary>
        public string IdentificationSpecialReimbursementQuery(string param)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            try
            {
               // var paramIni = JsonConvert.DeserializeObject<IdentificationSpecialReimbursementQueryParam>(param);

                var xmlStr =  XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("MTBX008");
                    if (result == 1)
                    {

                        var dataValid = XmlHelp.ValidXml("PO_JSXX");
                        if (dataValid.PO_FHZ == "1")
                        {
                            if (dataValid.IsRows)
                            {
                                var data = XmlHelp.DeSerializerModels(
                                   new IdentificationSpecialReimbursementQueryListDto());
                                resultData.Data = JsonConvert.SerializeObject(data);
                            }
                            else
                            {
                                var data = XmlHelp.DeSerializerModels(
                                    new IdentificationSpecialReimbursementQueryDto());
                                resultData.Data = JsonConvert.SerializeObject(data);
                            }
                        }
                        else
                        {
                            throw new Exception(dataValid.PO_MSG);
                        }


                    }

                }

            }
            catch (Exception e)
            {
                resultData.Code = false;
                resultData.Message = e.Message;
                Logs.LogWrite(new LogParam()
                {
                    Msg = e.Message,
                    OperatorCode = "",
                    Params = Logs.ToJson(param),


                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 特殊疾病报销汇总
        /// </summary>
        public string IdentificationSpecialReimbursementAll(string param)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new IniDto();
            try
            {
                //var paramIni = JsonConvert.DeserializeObject<IdentificationSpecialReimbursementAllParam>(param);

                var xmlStr =  XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("MTBX009");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModels(new IdentificationSpecialReimbursementAllDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception(data.PO_MSG);
                        }


                    }

                }

            }
            catch (Exception e)
            {
                resultData.Code = false;
                resultData.Message = e.Message;
                Logs.LogWrite(new LogParam()
                {
                    Msg = e.Message,
                    OperatorCode = "",
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 特殊疾病报销汇总取消
        /// </summary>
        public string IdentificationSpecialReimbursementAllCancel(string param)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new IniDto();
            try
            {
               // var paramIni = JsonConvert.DeserializeObject<IdentificationSpecialReimbursementAlllCancelParam>(param);

                var xmlStr =  XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("MTBX010");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModels(new IdentificationSpecialReimbursementAlllCancelDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception(data.PO_MSG);
                        }


                    }

                }

            }
            catch (Exception e)
            {
                resultData.Code = false;
                resultData.Message = e.Message;
                Logs.LogWrite(new LogParam()
                {
                    Msg = e.Message,
                    OperatorCode = "",
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 特殊疾病下载
        /// </summary>
        public string IdentificationSpecialDownload(string param)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new IniDto();
            try
            {
               // var paramIni = JsonConvert.DeserializeObject<IdentificationSpecialDownloadParam>(param);

                var xmlStr =  XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("MTBX010");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModels(new IdentificationSpecialDownloadDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception(data.PO_MSG);
                        }


                    }

                }

            }
            catch (Exception e)
            {
                resultData.Code = false;
                resultData.Message = e.Message;
                Logs.LogWrite(new LogParam()
                {
                    Msg = e.Message,
                    OperatorCode = "",
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }

    }
}
