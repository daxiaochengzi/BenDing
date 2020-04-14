using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.JsonEntity;
using BenDing.Domain.Models.Dto.OutpatientDepartment;
using BenDing.Domain.Models.Dto.Resident;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Dto.Workers;
using BenDing.Domain.Models.HisXml;
using BenDing.Domain.Models.Params.OutpatientDepartment;
using BenDing.Domain.Models.Params.SystemManage;
using BenDing.Domain.Models.Params.Web;
using BenDing.Domain.Xml;
using BenDing.Repository.Interfaces.Web;
using Newtonsoft.Json;


namespace BenDing.Repository.Providers.Web
{
    public class OutpatientDepartmentRepository : IOutpatientDepartmentRepository
    {
    

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
            var iniData = XmlHelp.DeSerializerModel(new OutpatientDepartmentCostInputJsonDto(), true);
             resultData = AutoMapper.Mapper.Map<OutpatientDepartmentCostInputDto>(iniData);
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
        }
        /// <summary>
        /// 门诊计划生育预结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public WorkerHospitalizationPreSettlementDto OutpatientPlanBirthPreSettlement(OutpatientPlanBirthPreSettlementParam param)
        {
            WorkerHospitalizationPreSettlementDto data = null;
            var xmlStr = XmlHelp.SaveXml(param);
            if (!xmlStr) throw new Exception("门诊计划生育预结算保存参数出错");
            int result = MedicalInsuranceDll.CallService_cxjb("SYBX004");
            if (result != 1) throw new Exception("门诊计划生育预结算执行出错!!!");
            var dataIni = XmlHelp.DeSerializerModel(new WorkerBirthPreSettlementJsonDto(), true);
            if (dataIni != null) data = AutoMapper.Mapper.Map<WorkerHospitalizationPreSettlementDto>(dataIni);
            return data;
        }
        /// <summary>
        /// 门诊计划生育结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public WorkerHospitalizationPreSettlementDto OutpatientPlanBirthSettlement(OutpatientPlanBirthSettlementParam param)
        {
            WorkerHospitalizationPreSettlementDto data = null;
            var xmlStr = XmlHelp.SaveXml(param);
            if (!xmlStr) throw new Exception("门诊计划生育结算保存参数出错");
            int result = MedicalInsuranceDll.CallService_cxjb("SYBX005");
            if (result != 1) throw new Exception("门诊计划生育结算执行出错!!!");
            var dataIni = XmlHelp.DeSerializerModel(new WorkerBirthPreSettlementJsonDto(), true);
            if (dataIni != null) data = AutoMapper.Mapper.Map<WorkerHospitalizationPreSettlementDto>(dataIni);
            return data;
        }
        /// <summary>
        /// 门诊计划生育结算取消
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public void OutpatientPlanBirthSettlementCancel(OutpatientPlanBirthSettlementCancelParam param)
        {
            WorkerHospitalizationPreSettlementDto data = null;
            var xmlStr = XmlHelp.SaveXml(param);
            if (!xmlStr) throw new Exception("门诊计划生育结算取消保存参数出错");
            int result = MedicalInsuranceDll.CallService_cxjb("SYBX006");
            if (result != 1) throw new Exception("门诊计划生育结算取消执行出错!!!");
            var dataIni = XmlHelp.DeSerializerModel(new IniDto(), true);
           
           
        }
        /// <summary>
        /// 门诊计划生育结算查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public WorkerHospitalizationPreSettlementDto OutpatientPlanBirthSettlementQuery(OutpatientPlanBirthSettlementQueryParam param)
        {
            WorkerHospitalizationPreSettlementDto data = null;
            var xmlStr = XmlHelp.SaveXml(param);
            if (!xmlStr) throw new Exception("门诊计划生育结算查询保存参数出错");
            int result = MedicalInsuranceDll.CallService_cxjb("SYBX007");
            if (result != 1) throw new Exception("门诊计划生育结算查询执行出错!!!");
            var dataIni = XmlHelp.DeSerializerModel(new WorkerBirthPreSettlementJsonDto(), true);
            if (dataIni != null) data = AutoMapper.Mapper.Map<WorkerHospitalizationPreSettlementDto>(dataIni);
            return data;
        }
    }
}
