using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Resident;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Params.Resident;
using BenDing.Domain.Xml;
using BenDing.Repository.Interfaces.Web;
using Newtonsoft.Json;

namespace BenDing.Repository.Providers.Web
{
    public class ResidentMedicalInsuranceRepository : IResidentMedicalInsuranceRepository
    {
        private IDataBaseHelpRepository _baseHelpRepository;
      

        public ResidentMedicalInsuranceRepository(
            IDataBaseHelpRepository iDataBaseHelpRepository
            )
        {
            _baseHelpRepository = iDataBaseHelpRepository;
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
                        HisHospitalizationId = param.HospitalizationNo
                    };
                    //保存医保信息
                    await _baseHelpRepository.SaveMedicalInsurance(user, saveData);
                    //var data = XmlHelp.DeSerializerModel(new ResidentHospitalizationRegisterDto());
                    //saveData.MedicalInsuranceHospitalizationNo = "888";
                    //saveData.MedicalInsuranceYearBalance = 777;
                    ////更新医保信息
                    //await _baseHelpRepository.SaveMedicalInsurance(user, saveData);
                    int result = MedicalInsuranceDll.CallService_cxjb("CXJB002");
                    if (result == 1)
                    {
                        var data = XmlHelp.DeSerializerModel(new ResidentHospitalizationRegisterDto());
                        saveData.MedicalInsuranceHospitalizationNo = data.MedicalInsuranceInpatientNo;
                        saveData.MedicalInsuranceYearBalance = Convert.ToDecimal(data.MedicalInsuranceYearBalance);
                        //更新医保信息
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
        public async Task HospitalizationModify(HospitalizationModifyParam param)
        {
            await Task.Run(async () =>
           {
               var xmlStr = XmlHelp.SaveXml(param);
               if (xmlStr)
               {
                   int result = MedicalInsuranceDll.CallService_cxjb("CXJB003");
                   if (result != 1)
                   {
                       throw new Exception("居民修改入院登记失败!!!");
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
