using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Resident;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Params.Resident;
using BenDing.Domain.Models.Params.Web;
using BenDing.Domain.Xml;
using BenDing.Repository.Interfaces.Web;
using Newtonsoft.Json;

namespace BenDing.Repository.Providers.Web
{
    public class ResidentMedicalInsuranceRepository : IResidentMedicalInsuranceRepository
    {
        private IDataBaseHelpRepository _baseHelpRepository;
        private IBaseSqlServerRepository _baseSqlServerRepository;
        private IWebBasicRepository _webServiceBasic;

        public ResidentMedicalInsuranceRepository(
            IDataBaseHelpRepository iDataBaseHelpRepository,
            IWebBasicRepository webBasicRepository,
            IBaseSqlServerRepository baseSqlServerRepository
            )
        {
            _baseHelpRepository = iDataBaseHelpRepository;
            _webServiceBasic = webBasicRepository;
            _baseSqlServerRepository = baseSqlServerRepository;
        }
        public async Task Login(QueryHospitalOperatorParam param)
        {
             await Task.Run(async () =>
             {
                 var userInfo = await _baseSqlServerRepository.QueryHospitalOperator(param);

                var result = MedicalInsuranceDll.ConnectAppServer_cxjb(userInfo.MedicalInsuranceAccount, userInfo.MedicalInsurancePwd);

                 if (result == 1)
                 {
                   var  data = XmlHelp.DeSerializerModel(new IniDto());

                 }
             
             });
        }
        /// <summary>
        /// 获取个人基础资料
        /// </summary>
        /// <param name="param"></param>
        public async Task<ResidentUserInfoDto> GetUserInfo(ResidentUserInfoParam param)
        {
            return await Task.Run(async () =>
            {
                var data = new ResidentUserInfoDto();

                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDll.CallService_cxjb("CXJB001");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new ResidentUserInfoDto());

                    }
                    else
                    {
                        throw new Exception("居民个人基础资料执行失败!!!");
                    }

                }



                return data;
            });
        }

        /// <summary>
        /// 入院登记
        /// </summary>
        /// <returns></returns>
        public async Task HospitalizationRegister(ResidentHospitalizationRegisterParam param, UserInfoDto user)
        {
            await Task.Run(async () =>
            {
                var paramIni = param;
                paramIni.Operators = BitConverter.ToInt64(Guid.Parse(param.Operators).ToByteArray(), 0).ToString();
                paramIni.InpatientDepartmentCode = BitConverter.ToInt64(Guid.Parse(param.InpatientDepartmentCode).ToByteArray(), 0).ToString();
                paramIni.HospitalizationNo = BitConverter.ToInt64(Guid.Parse(param.BusinessId).ToByteArray(), 0).ToString();
                paramIni.AdmissionDate = Convert.ToDateTime(param.AdmissionDate).ToString("yyyyMMdd");
                var xmlStr = XmlHelp.SaveXml(paramIni);
                if (xmlStr)
                {
                    var saveData = new MedicalInsuranceDto
                    {
                        AdmissionInfoJson = JsonConvert.SerializeObject(param),
                        HisHospitalizationId = param.HospitalizationNo,
                        Id = Guid.NewGuid(),
                    };
                    //保存医保信息
                    await _baseHelpRepository.SaveMedicalInsurance(user, saveData);
                   
                    int result = MedicalInsuranceDll.CallService_cxjb("CXJB002");
                    if (result == 1)
                    {
                        var data = XmlHelp.DeSerializerModel(new ResidentHospitalizationRegisterDto());
                        saveData.MedicalInsuranceHospitalizationNo = data.MedicalInsuranceInpatientNo;
                        saveData.MedicalInsuranceYearBalance = Convert.ToDecimal(data.MedicalInsuranceYearBalance);
                        //更新医保信息
                  
                        var strXmlIntoParam = XmlSerializeHelper.XmlSerialize(paramIni);
                        var strXmlBackParam = XmlSerializeHelper.XmlBackParam();
                        var saveXmlData = new SaveXmlData();
                        saveXmlData.OrganizationCode = user.OrganizationCode;
                        saveXmlData.AuthCode = user.AuthCode;
                        saveXmlData.BusinessId = param.BusinessId;
                        saveXmlData.TransactionId = saveData.Id.ToString("N");
                        saveXmlData.MedicalInsuranceBackNum = "CXJB002";
                        saveXmlData.BackParam = CommonHelp.EncodeBase64("utf-8", strXmlIntoParam);
                        saveXmlData.IntoParam = CommonHelp.EncodeBase64("utf-8", strXmlBackParam);
                        saveXmlData.MedicalInsuranceCode = "21";
                        saveXmlData.UserId = user.UserId;
                        await _webServiceBasic.HIS_InterfaceListAsync("38", JsonConvert.SerializeObject(saveXmlData), param.UserId);
                        await _baseHelpRepository.SaveMedicalInsurance(user, saveData);
                    }
                    else
                    {
                        throw new Exception("居民入院登记执行失败!!!");
                    }

                }


            });

        }
        /// <summary>
        /// 修改入院登记
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task HospitalizationModify(HospitalizationModifyParam param, UserInfoDto user)
        {
            await Task.Run(async () =>
           {
               var xmlStr = XmlHelp.SaveXml(param);
               if (xmlStr)
               {
                   int result = MedicalInsuranceDll.CallService_cxjb("CXJB003");
                   if (result == 1)
                   {
                       var strXmlBackParam = XmlSerializeHelper.XmlBackParam();
                       var strXmlIntoParam = XmlSerializeHelper.XmlSerialize(param);
                       var saveXmlData = new SaveXmlData();
                       saveXmlData.OrganizationCode = user.OrganizationCode;
                       saveXmlData.AuthCode = user.AuthCode;
                       saveXmlData.BusinessId = param.BusinessId;
                       saveXmlData.TransactionId = param.TransactionId.ToString("N");
                       saveXmlData.MedicalInsuranceBackNum = "CXJB002";
                       saveXmlData.BackParam = CommonHelp.EncodeBase64("utf-8", strXmlIntoParam);
                       saveXmlData.IntoParam = CommonHelp.EncodeBase64("utf-8", strXmlBackParam);
                       saveXmlData.MedicalInsuranceCode = "21";
                       saveXmlData.UserId = user.UserId;
                       await _webServiceBasic.HIS_InterfaceListAsync("38", JsonConvert.SerializeObject(saveXmlData), param.UserId);
                   }

               }
           });



        }
        /// <summary>
        /// 项目下载
        /// </summary>
        public async Task<ResidentProjectDownloadDto> ProjectDownload(ResidentProjectDownloadParam param)
        {
            return await Task.Run(async () =>
            {
                var data = new ResidentProjectDownloadDto();

                string strXml = XmlHelp.DeSerializerModelStr("ROWDATA");

                data = XmlHelp.DeSerializer<ResidentProjectDownloadDto>(strXml);
                // data = XmlHelp.DeSerializerModel(new ResidentProjectDownloadDto());
                //var xmlStr = XmlHelp.SaveXml(param);
                //if (xmlStr)
                //{
                //    int result = MedicalInsuranceDll.CallService_cxjb("CXJB019");
                //    if (result == 1)
                //    {
                //        data = XmlHelp.DeSerializerModel(new ResidentProjectDownloadDto());

                //    }

                //}
                //else
                //{
                //    throw new Exception("居民项目下载执行失败!!!");
                //}

                return data;
            });

        }
    }
}
