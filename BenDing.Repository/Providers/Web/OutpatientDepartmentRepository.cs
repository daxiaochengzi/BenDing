using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.JsonEntity;
using BenDing.Domain.Models.Dto.OutpatientDepartment;
using BenDing.Domain.Models.Dto.Resident;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.HisXml;
using BenDing.Domain.Models.Params.OutpatientDepartment;
using BenDing.Domain.Models.Params.SystemManage;
using BenDing.Domain.Models.Params.Web;
using BenDing.Domain.Xml;
using BenDing.Repository.Interfaces.Web;
using Newtonsoft.Json;
using NFine.Application.BenDingManage;
using NFine.Domain._03_Entity.BenDingManage;

namespace BenDing.Repository.Providers.Web
{
    public class OutpatientDepartmentRepository : IOutpatientDepartmentRepository
    {
        private readonly MonthlyHospitalizationBase _monthlyHospitalizationBase;
        public OutpatientDepartmentRepository()
        {
            _monthlyHospitalizationBase = new MonthlyHospitalizationBase();
            
        }

        /// <summary>
        /// 门诊费用录入
        /// </summary>
        public OutpatientDepartmentCostInputDto OutpatientDepartmentCostInput(OutpatientDepartmentCostInputParam param)
        {
            OutpatientDepartmentCostInputDto resultData = null;
            var xmlStr = XmlHelp.SaveXml(param);
            if (!xmlStr) throw new Exception("门诊费用录入保存参数出错");
            var result = MedicalInsuranceDll.CallService_cxjb("TPYP301");
            if (result != 1) throw new Exception("门诊费用录入执行出错");
            resultData = XmlHelp.DeSerializerModel(new OutpatientDepartmentCostInputDto(), true);
            return resultData;
        }

        /// <summary>
        /// 门诊费取消
        /// </summary>
        public void CancelOutpatientDepartmentCost(CancelOutpatientDepartmentCostParam param)
        {
            var xmlStr = XmlHelp.SaveXml(param);
            if (!xmlStr) throw new Exception("门诊费取消保存参数出错");
            var result = MedicalInsuranceDll.CallService_cxjb("TPYP302");
            if (result != 1) throw new Exception("门诊费取消执行出错");
            XmlHelp.DeSerializerModel(new IniDto(), true);

        }

        /// <summary>
        /// 查询门诊费
        /// </summary>
        public QueryOutpatientDepartmentCostjsonDto QueryOutpatientDepartmentCost(QueryOutpatientDepartmentCostParam param)
        {

            QueryOutpatientDepartmentCostjsonDto resultData = null;
            var xmlStr = XmlHelp.SaveXml(param);
            if (!xmlStr) throw new Exception("查询门诊费保存参数出错");
            var result = MedicalInsuranceDll.CallService_cxjb("TPYP303");
            if (result != 1) throw new Exception("门诊费用查询执行出错");
            var data = XmlHelp.DeSerializerModel(new Domain.Models.Dto.OutpatientDepartment.QueryOutpatientDepartmentCostDto(),true) ;
           if (data==null) throw new Exception("门诊费用查询出错");
            resultData = AutoMapper.Mapper.Map<QueryOutpatientDepartmentCostjsonDto>(data);
            //QueryOutpatientDepartmentCostDto


            return resultData;
        }
        /// <summary>
        /// 门诊月结汇总
        /// </summary>
        /// <param name="param"></param>
        public MonthlyHospitalizationDto MonthlyHospitalization(MonthlyHospitalizationParam param)
        {

            var xmlStr = XmlHelp.SaveXml(param.Participation);
            MonthlyHospitalizationDto data = null;
            if (!xmlStr) throw new Exception("门诊月结汇总保存参数出错");
            var result = MedicalInsuranceDll.CallService_cxjb("TPYP214");
            if (result != 1) throw new Exception("门诊月结汇总执行出错");
            data = XmlHelp.DeSerializerModel(new MonthlyHospitalizationDto(), true);
            var insertParam = new MonthlyHospitalizationEntity()
            {
                Amount = data.ReimbursementAllAmount,
                Id = Guid.NewGuid(),
                DocumentNo = data.DocumentNo,
                PeopleNum = data.ReimbursementPeopleNum,
                PeopleType = param.Participation.PeopleType,
                SummaryType = param.Participation.SummaryType,
                StartTime = param.Participation.StartTime,
                EndTime = param.Participation.EndTime,
            };
            _monthlyHospitalizationBase.Insert(insertParam, param.User);

            return data;
        }
        /// <summary>
        /// 取消门诊月结汇总
        /// </summary>
        /// <param name="param"></param>
        public void CancelMonthlyHospitalization(CancelMonthlyHospitalizationParam param)
        {
            var xmlStr = XmlHelp.SaveXml(param.Participation);
            if (!xmlStr) throw new Exception("取消门诊月结汇总保存参数出错");
            var result = MedicalInsuranceDll.CallService_cxjb("TPYP215");
            if (result != 1) throw new Exception("取消门诊月结汇总执行出错");
             XmlHelp.DeSerializerModel(new IniDto(), true);
            var monthlyHospitalization = _monthlyHospitalizationBase.GetForm(param.Id);
            if (monthlyHospitalization != null)
            {
                monthlyHospitalization.IsRevoke = true;
                //更新月结状态
                _monthlyHospitalizationBase.Modify(monthlyHospitalization, param.User, param.Id);
            }


        }
    }
}
