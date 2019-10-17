using BenDingActive.Model.Dto;
using BenDingActive.Model.Params;
using BenDingActive.Xml;
using System;
using BenDing.Service;
using BenDingActive.Model;
using BenDingActive.Model.Dto.Single;
using BenDingActive.Model.Params.Single;
using Newtonsoft.Json;

namespace BenDingActive.Service
{//WorkersMedicalInsurance
    public class SingleResidentMedicalInsuranceService
    {
        
        /// <summary>
        /// 入院登记
        /// </summary>
        /// <returns></returns>
        public string SingleHospitalizationRegister(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new SingleHospitalizationRegisterDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<SingleHospitalizationRegisterParam>(param);
                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("TPYP201");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new SingleHospitalizationRegisterDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
                        }
                        Logs.LogWrite(new LogParam()
                        {
                            Msg = data.PO_MSG,
                         OperatorCode ="",
                            Params = Logs.ToJson(param),
                            ResultData = Logs.ToJson(data)

                        });

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
                 OperatorCode ="",
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
        public string SingleHospitalizationModify(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new SingleHospitalizationModifyDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<SingleHospitalizationModifyParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("TPYP202");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new SingleHospitalizationModifyDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
                        }

                        Logs.LogWrite(new LogParam()
                        {
                            Msg = data.PO_MSG,
                         OperatorCode ="",
                            Params = Logs.ToJson(param),
                            ResultData = Logs.ToJson(data)

                        });
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
                 OperatorCode ="",
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 取消入院
        /// </summary>
        /// <returns></returns>
        public string SingleHospitalizationCancel(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new SingleHospitalizationCancelDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<SingleHospitalizationCancelParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("TPYP203");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new SingleHospitalizationCancelDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
                        }
                        Logs.LogWrite(new LogParam()
                        {
                            Msg = data.PO_MSG,
                         OperatorCode ="",
                            Params = Logs.ToJson(param),
                            ResultData = Logs.ToJson(data)

                        });

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
                 OperatorCode ="",
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
        public string SinglePrescriptionUpload(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new SinglePrescriptionUploadDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<SinglePrescriptionUploadParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("TPYP007");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new SinglePrescriptionUploadDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
                        }
                        Logs.LogWrite(new LogParam()
                        {
                            Msg = data.PO_MSG,
                         OperatorCode ="",
                            Params = Logs.ToJson(param),
                            ResultData = Logs.ToJson(data)

                        });

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
                 OperatorCode ="",
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 处方删除
        /// </summary>
        public string SinglePrescriptionDelete(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new SinglePrescriptionDeleteDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<SinglePrescriptionDeleteParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("TPYP008");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new SinglePrescriptionDeleteDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
                        }

                        Logs.LogWrite(new LogParam()
                        {
                            Msg = data.PO_MSG,
                         OperatorCode ="",
                            Params = Logs.ToJson(param),
                            ResultData = Logs.ToJson(data)

                        });
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
                 OperatorCode ="",
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 处方明细查询
        /// </summary>
        public string SinglePrescriptionDetailQuery(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            
            try
            {
                var paramIni = JsonConvert.DeserializeObject<SinglePrescriptionDetailQueryParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("TPYP009");
                    if (result == 1)
                    {
                        var dataValid = XmlHelp.ValidXml("PI_CFMX");
                        if (dataValid.PO_FHZ == "1")
                        {
                            if (dataValid.IsRows)
                            {
                                var data = XmlHelp.DeSerializerModel(
                                    new PrescriptionDetailQueryRowList());
                                resultData.Data = JsonConvert.SerializeObject(data);
                                Logs.LogWrite(new LogParam()
                                {
                                   
                                 OperatorCode ="",
                                    Params = Logs.ToJson(param),
                                    ResultData = Logs.ToJson(data)

                                });
                            }
                            else
                            {
                                var data = XmlHelp.DeSerializerModel(
                                    new SinglePrescriptionDetailQueryDto());
                                resultData.Data = JsonConvert.SerializeObject(data);
                                Logs.LogWrite(new LogParam()
                                {

                                 OperatorCode ="",
                                    Params = Logs.ToJson(param),
                                    ResultData = Logs.ToJson(data)

                                });
                            }
                           

                        }
                        else
                        {
                            throw new Exception( dataValid.PO_MSG);
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
                 OperatorCode ="",
                    Params = Logs.ToJson(param),
                   

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 单病种出院结算
        /// </summary>
        public string SingleLeaveHospitalSettlement(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new SingleLeaveHospitalSettlementDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<SingleLeaveHospitalSettlementParam >(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("TPYP210");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new SingleLeaveHospitalSettlementDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
                        }

                        Logs.LogWrite(new LogParam()
                        {
                            Msg = data.PO_MSG,
                         OperatorCode ="",
                            Params = Logs.ToJson(param),
                            ResultData = Logs.ToJson(data)

                        });
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
                 OperatorCode ="",
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 单病种出院结算取消
        /// </summary>
        public string SingleLeaveHospitalSettlementCancel(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new SinglePreMonthSettlementCancelDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<SingleLeaveHospitalSettlementCancelParam>(param);
                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("TPYP211");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new SinglePreMonthSettlementCancelDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
                        }
                        Logs.LogWrite(new LogParam()
                        {
                            Msg = data.PO_MSG,
                         OperatorCode ="",
                            Params = Logs.ToJson(param),
                            ResultData = Logs.ToJson(data)

                        });
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
                 OperatorCode ="",
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 单病种预结算
        /// </summary>
        public string SinglePreSettlement(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new SinglePreMonthSettlementDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<SinglePreSettlementParam>(param);
                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("TPYP212");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new SinglePreMonthSettlementDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
                        }
                        Logs.LogWrite(new LogParam()
                        {
                            Msg = data.PO_MSG,
                         OperatorCode ="",
                            Params = Logs.ToJson(param),
                            ResultData = Logs.ToJson(data)

                        });
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
                 OperatorCode ="",
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 单病种预结算查询
        /// </summary>
        public string SinglePreSettlementQuery(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new SinglePreSettlementQueryDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<SinglePreSettlementQueryParam>(param);
                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("TPYP213");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new SinglePreSettlementQueryDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
                        }
                    }
                    Logs.LogWrite(new LogParam()
                    {
                        Msg = data.PO_MSG,
                     OperatorCode ="",
                        Params = Logs.ToJson(param),
                        ResultData = Logs.ToJson(data)

                    });

                }

            }
            catch (Exception e)
            {
                resultData.Code = false;
                resultData.Message = e.Message;
                Logs.LogWrite(new LogParam()
                {
                    Msg = e.Message,
                 OperatorCode ="",
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        ///单病种 精神病住院月结汇总查询
        /// </summary>
        public string SingleMonthSettlement(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new SinglePreMonthSettlementDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<SingleMonthSettlementParam>(param);
                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("TPYP214");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new SinglePreMonthSettlementDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception(data.PO_MSG);
                        }
                        Logs.LogWrite(new LogParam()
                        {
                            Msg = data.PO_MSG,
                         OperatorCode ="",
                            Params = Logs.ToJson(param),
                            ResultData = Logs.ToJson(data)

                        });
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
                 OperatorCode ="",
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        ///单病种 精神病月结汇总取消
        /// </summary>
        public string SingleMonthSettlementCancel(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new SinglePreMonthSettlementCancelDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<SingleMonthSettlementCancelParam>(param);
                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("TPYP214");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new SinglePreMonthSettlementCancelDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception(data.PO_MSG);
                        }
                        Logs.LogWrite(new LogParam()
                        {
                            Msg = data.PO_MSG,
                         OperatorCode ="",
                            Params = Logs.ToJson(param),
                            ResultData = Logs.ToJson(data)

                        });
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
                 OperatorCode ="",
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        ///2.2.24.门诊诊查费录入及结算
        /// </summary>
        public string OutpatientConsultationFeeSettlement(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new OutpatientConsultationFeeSettlementDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<OutpatientConsultationFeeSettlementParam>(param);
                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("TPYP301");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new OutpatientConsultationFeeSettlementDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception(data.PO_MSG);
                        }
                        Logs.LogWrite(new LogParam()
                        {
                            Msg = data.PO_MSG,
                         OperatorCode ="",
                            Params = Logs.ToJson(param),
                            ResultData = Logs.ToJson(data)

                        });
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
                 OperatorCode ="",
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        ///2.2.25.门诊诊查费取消(挂号取消)
        /// </summary>
        public string OutpatientConsultationFeeCancel(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new OutpatientConsultationFeeCancelDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<OutpatientConsultationFeeCancelParam>(param);
                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("TPYP302");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new OutpatientConsultationFeeCancelDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception(data.PO_MSG);
                        }
                        Logs.LogWrite(new LogParam()
                        {
                            Msg = data.PO_MSG,
                         OperatorCode ="",
                            Params = Logs.ToJson(param),
                            ResultData = Logs.ToJson(data)

                        });
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
                 OperatorCode ="",
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        ///2.2.26.门诊诊查费查询
        /// </summary>
        public string OutpatientConsultationFeeQuery(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new OutpatientConsultationFeeQueryDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<OutpatientConsultationFeeQueryParam>(param);
                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("TPYP303");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new OutpatientConsultationFeeQueryDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception(data.PO_MSG);
                        }
                        Logs.LogWrite(new LogParam()
                        {
                            Msg = data.PO_MSG,
                         OperatorCode ="",
                            Params = Logs.ToJson(param),
                            ResultData = Logs.ToJson(data)

                        });
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
                 OperatorCode ="",
                    Params = Logs.ToJson(param),
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        
    }
}
