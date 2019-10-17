using System;
using BenDing.Service;
using BenDingActive.Model;
using BenDingActive.Model.Dto;
using BenDingActive.Model.Dto.DifferentPlaces;
using BenDingActive.Model.Dto.Single;
using BenDingActive.Model.Params;
using BenDingActive.Model.Params.DifferentPlaces;
using BenDingActive.Model.Params.Single;
using BenDingActive.Xml;
using Newtonsoft.Json;


namespace BenDingActive.Service
{
    public class MedicalInsuranceDifferentPlacesService
    {
        /// <summary>
        /// 获取个人基础资料
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        public string DifferentPlacesGetUserInfo(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new UserInfoDifferentDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<UserInfoDifferentParam>(param);
                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDifferentPlaces.yyjk_call("YYJK001");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new UserInfoDifferentDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception(data.PO_MSG);
                        }


                    }

                    {
                        throw new Exception("请检查医保网络是否正常!!!");
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
        /// 入院登记
        /// </summary>
        /// <returns></returns>
        public string DifferentPlacesHospitalizationRegister(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new HospitalizationRegisterDifferentDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<HospitalizationRegisterDifferentParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDifferentPlaces.yyjk_call("YYJK003");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new HospitalizationRegisterDifferentDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
                        }


                    }
                    {
                        throw new Exception("请检查医保网络是否正常!!!");
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
        /// 取消入院登记
        /// </summary>
        /// <returns></returns>
        public string DifferentPlacesHospitalizationCancel(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new DifferentPlacesHospitalizationCancelDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<DifferentPlacesHospitalizationCancelParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = WorkersMedicalInsurance.CallService_cxjb("YYJK004");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new DifferentPlacesHospitalizationCancelDto());
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
                    {
                        throw new Exception("请检查医保网络是否正常!!!");
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
        public string DifferentPlacesHospitalizationModify(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new HospitalizationModifyDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<HospitalizationModifyParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDifferentPlaces.yyjk_call("YYJK005");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new HospitalizationModifyDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
                        }


                    }
                    {
                        throw new Exception("请检查医保网络是否正常!!!");
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
        /// 出院办理
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public string DifferentLeaveHospital(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new DifferentLeaveHospitalDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<DifferentLeaveHospitalParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDifferentPlaces.yyjk_call("YYJK006");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new DifferentLeaveHospitalDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
                        }


                    }
                    {
                        throw new Exception("请检查医保网络是否正常!!!");
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
        /// 出院办理回退
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public string DifferentLeaveHospitalReturn(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new DifferentLeaveHospitalReturnDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<DifferentLeaveHospitalReturnParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDifferentPlaces.yyjk_call("YYJK007");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new DifferentLeaveHospitalReturnDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
                        }


                    }
                    {
                        throw new Exception("请检查医保网络是否正常!!!");
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
        public string DifferentPlacesPrescriptionUpload(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new DifferentPlacesPrescriptionUploadDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<DifferentPlacesPrescriptionUploadParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDifferentPlaces.yyjk_call("YYJK008");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new DifferentPlacesPrescriptionUploadDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
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
                    ResultData = Logs.ToJson(data)

                });

            }

            return JsonConvert.SerializeObject(resultData);


        }
        /// <summary>
        /// 处方删除
        /// </summary>
        public string DifferentPlacesPrescriptionDelete(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new DifferentPlacesPrescriptionDeleteDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<PrescriptionDeleteParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDifferentPlaces.yyjk_call("YYJK009");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new DifferentPlacesPrescriptionDeleteDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
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
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        public string DifferentPlacesPrescriptionSplitLine(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new DifferentPlacesPrescriptionSplitLineDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<DifferentPlacesPrescriptionSplitLineParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDifferentPlaces.yyjk_call("YYJK010");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new DifferentPlacesPrescriptionSplitLineDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
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
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        ///// <summary>
        ///// 处方明细查询
        ///// </summary>
        //public string DifferentPlacesPrescriptionDetailQuery(string param, HisBaseParam baseParam)
        //{
        //    var resultData = new ApiJsonResultData { Code = true };

        //    try
        //    {
        //        var paramIni = JsonConvert.DeserializeObject<PrescriptionDetailQueryParam>(param);

        //        var xmlStr = XmlHelp.SaveXml(param);
        //        if (xmlStr)
        //        {
        //            int result = MedicalInsuranceDifferentPlaces.yyjk_call("YYJK010");
        //            if (result == 1)
        //            {
        //                var validData = XmlHelp.ValidXml("CFMX");
        //                if (validData.PO_FHZ == "1")
        //                {
        //                    if (validData.IsRows)
        //                    {
        //                        var data = XmlHelp.DeSerializerModel(new PrescriptionDetailQueryRow());
        //                        resultData.Data = JsonConvert.SerializeObject(data);
        //                    }
        //                    else
        //                    {
        //                        var data = XmlHelp.DeSerializerModel(new PrescriptionDetailQueryDto());
        //                        resultData.Data = JsonConvert.SerializeObject(data);
        //                    }


        //                }
        //                else
        //                {
        //                    throw new Exception( validData.PO_MSG);
        //                }
        //            }

        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        resultData.Code = false;
        //        resultData.Message = e.Message;
        //        Logs.LogWrite(new LogParam()
        //        {
        //            Msg = e.Message,
        //         OperatorCode ="",
        //            Params = Logs.ToJson(param),
        //        });

        //    }
        //    return JsonConvert.SerializeObject(resultData);
        //}
        /// <summary>
        /// 费用预结算
        /// </summary>
        public string DifferentPlacesCostPreSettlement(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new DifferentPlacesCostPreSettlementDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<DifferentPlacesCostPreSettlementParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDifferentPlaces.yyjk_call("YYJK013");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new DifferentPlacesCostPreSettlementDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
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
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 费用结算
        /// </summary>
        public string DifferentPlacesCostSettlement(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new DifferentPlacesCostSettlementDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<DifferentPlacesCostSettlementParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDifferentPlaces.yyjk_call("YYJK011");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new DifferentPlacesCostSettlementDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
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
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 入院结算取消
        /// </summary>
        public string DifferentPlacesHospitalizedSettlementCancel(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new DifferentPlacesHospitalizedSettlementCancelDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<DifferentPlacesHospitalizedSettlementCancelParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDifferentPlaces.yyjk_call("YYJK012");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new DifferentPlacesHospitalizedSettlementCancelDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
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
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 费用结算查询
        /// </summary>
        public string DifferentPlacesCostSettlementQuery(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new CostSettlementQueryDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<CostSettlementQueryParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDifferentPlaces.yyjk_call("CXJB012");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new CostSettlementQueryDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
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
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 异地划卡管理
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public string DifferentPlacesCardManagement(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new DifferentPlacesCardManagementDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<DifferentPlacesCardManagementParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDifferentPlaces.yyjk_call("YYJK014");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new DifferentPlacesCardManagementDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
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
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 医嘱上传
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public string DifferentPlacesDoctorsAdviceUpload(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new DifferentPlacesDoctorsAdviceUploadDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<DifferentPlacesDoctorsAdviceUploadParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDifferentPlaces.yyjk_call("YYJK016");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new DifferentPlacesDoctorsAdviceUploadDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
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
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 医嘱取消
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public string DifferentPlacesDoctorsAdviceCancel(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new DifferentPlacesDoctorsAdviceUploadDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<DifferentPlacesDoctorsAdviceCancelParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDifferentPlaces.yyjk_call("ydzy07");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new DifferentPlacesDoctorsAdviceUploadDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
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
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 病历上传
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public string DifferentPlacesMedicalRecordUpload(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new DifferentPlacesMedicalRecordUploadDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<DifferentPlacesMedicalRecordUploadParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDifferentPlaces.yyjk_call("YYJK018");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new DifferentPlacesMedicalRecordUploadDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
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
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 病历查询
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public string DifferentPlacesMedicalRecordQuery(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new DifferentPlacesMedicalRecordQueryDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<DifferentPlacesMedicalRecordQueryParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDifferentPlaces.yyjk_call("YYJK019");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new DifferentPlacesMedicalRecordQueryDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
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
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 病历删除
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam">Back</param>
        /// <returns></returns>
        public string DifferentPlacesMedicalRecordDelete(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new DifferentPlacesMedicalRecordQueryDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<DifferentPlacesMedicalRecordDeleteParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDifferentPlaces.yyjk_call("YYJK020");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new DifferentPlacesMedicalRecordQueryDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
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
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 是否可回退查询
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public string DifferentPlacesIsBackQuery(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new DifferentPlacesMedicalRecordQueryDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<DifferentPlacesIsBackQueryParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDifferentPlaces.yyjk_call("YYJK021");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new DifferentPlacesMedicalRecordQueryDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
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
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 住院报销待遇审核情况查询
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public string DifferentPlacesHospitalizationExamineQuery(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new DifferentPlacesHospitalizationExamineQueryDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<DifferentPlacesHospitalizationExamineQueryParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDifferentPlaces.yyjk_call("YYJK022");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new DifferentPlacesHospitalizationExamineQueryDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
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
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 医疗机构月结算申请 
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public string MedicalInstitutionsMonthlySettlement(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new MedicalInstitutionsMonthlySettlementDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<MedicalInstitutionsMonthlySettlementParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDifferentPlaces.yyjk_call("YYJK023");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new MedicalInstitutionsMonthlySettlementDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
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
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 医疗机构月结算申请回退
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public string MedicalInstitutionsMonthlySettlementCancel(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new MedicalInstitutionsMonthlySettlementCancelDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<MedicalInstitutionsMonthlySettlementCancelParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDifferentPlaces.yyjk_call("YYJK024");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new MedicalInstitutionsMonthlySettlementCancelDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
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
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 结算打印
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public string DifferentPlacesSettlementPrint(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new DifferentPlacesSettlementPrintDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<DifferentPlacesSettlementPrintParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDifferentPlaces.yyjk_call("YYJK026");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new DifferentPlacesSettlementPrintDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
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
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 门诊模拟结算
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public string OutpatientSimulatedSettlement(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new OutpatientSimulatedSettlementDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<OutpatientSimulatedSettlementParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDifferentPlaces.yyjk_call("YYJK025");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new OutpatientSimulatedSettlementDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
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
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 门诊结算
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public string OutpatientSettlement(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new OutpatientSimulatedSettlementDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<OutpatientSettlementParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDifferentPlaces.yyjk_call("YYJK027");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new OutpatientSimulatedSettlementDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
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
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 门诊结算回退
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public string OutpatientSettlementCancel(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new OutpatientSimulatedSettlementDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<OutpatientSettlementCancelParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDifferentPlaces.yyjk_call("YYJK028");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new OutpatientSimulatedSettlementDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
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
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 冲正交易
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public string DifferentPlacesRushTransaction(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new DifferentPlacesRushTransactionDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<DifferentPlacesRushTransactionParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDifferentPlaces.yyjk_call("YYJK029");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new DifferentPlacesRushTransactionDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
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
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 卡鉴权交易
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public string DifferentPlacesCardAuthenticationTransaction(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new DifferentPlacesCardAuthenticationTransactionDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<DifferentPlacesCardAuthenticationTransactionParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDifferentPlaces.yyjk_call("YYJK030");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new DifferentPlacesCardAuthenticationTransactionDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
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
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 科室信息上传
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public string DifferentPlacesDepartmentInfoUpload(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new DifferentPlacesDepartmentInfoUploadDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<DifferentPlacesDepartmentInfoUploadParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDifferentPlaces.yyjk_call("YYJK033");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new DifferentPlacesDepartmentInfoUploadDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
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
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
        /// <summary>
        /// 医生信息上传
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public string DifferentPlacesDoctorInfoUpload(string param, HisBaseParam baseParam)
        {
            var resultData = new ApiJsonResultDatas { Code = true };
            var data = new DifferentPlacesDoctorInfoUploadDto();
            try
            {
                var paramIni = JsonConvert.DeserializeObject<DifferentPlacesDoctorInfoUploadParam>(param);

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDifferentPlaces.yyjk_call("YYJK034");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new DifferentPlacesDoctorInfoUploadDto());
                        if (data.PO_FHZ == "1")
                        {
                            resultData.Data = JsonConvert.SerializeObject(data);
                        }
                        else
                        {
                            throw new Exception( data.PO_MSG);
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
                    ResultData = Logs.ToJson(data)

                });

            }
            return JsonConvert.SerializeObject(resultData);
        }
    }

}
