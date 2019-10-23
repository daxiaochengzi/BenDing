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

namespace BenDing.Repository.Providers.Web
{
   public class ResidentMedicalInsuranceRepository: IResidentMedicalInsuranceRepository
    {/// <summary>
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
        public async Task<ResidentHospitalizationRegisterDto> HospitalizationRegister(ResidentHospitalizationRegisterParam param)
        {
            return await Task.Run(async () =>
            {
                var data = new ResidentHospitalizationRegisterDto();
                var xmlStr = XmlHelp.SaveXml(param);
                if (xmlStr)
                {
                    int result = MedicalInsuranceDll.CallService_cxjb("CXJB002");
                    if (result == 1)
                    {
                        data = XmlHelp.DeSerializerModel(new ResidentHospitalizationRegisterDto());

                    }
                    else
                    {
                        throw new Exception("居民入院登记执行失败!!!");
                    }

                }
                return data;

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
                
                string strXml= XmlHelp.DeSerializerModelStr("ROWDATA");
               
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
