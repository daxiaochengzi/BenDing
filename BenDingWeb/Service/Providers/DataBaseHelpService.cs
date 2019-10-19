﻿using System;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BenDingWeb.Models.Dto;
using MedicalInsurance.Domain.Models.Enums;
using BenDingWeb.Models.Params;
using BenDingWeb.Models.Params.UI;
using BenDingWeb.Service.Interfaces;
using Dapper;
using System.Configuration;

namespace BenDingWeb.Service.Providers
{
    public class DataBaseHelpService : IDataBaseHelpService
    {
        private string _connectionString;
       
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="conStr"></param> 
        public DataBaseHelpService()
        {
          string conStr =  ConfigurationManager.ConnectionStrings["NFineDbContext"].ToString();
            _connectionString=  !string.IsNullOrWhiteSpace(conStr) ? conStr : throw new ArgumentNullException(nameof(conStr));

        }
        /// <summary>
        /// 更新医疗机构
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task ChangeOrg(UserInfoDto userInfo,List<OrgDto> param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {

                _sqlConnection.Open();
                if (param.Any())
                {
                    IDbTransaction transaction = _sqlConnection.BeginTransaction();
                    try
                    {
                        
                        string strSql = $"update [dbo].[hospital_organization] set delete_time=GETDATE(),is_delete=1,delete_user_id='{userInfo.职员ID}'";
                    
                        if (param.Any())
                        {

                            await _sqlConnection.ExecuteAsync(strSql, null, transaction);
                         
                            string insterCount = null;
                            foreach (var itmes in param)
                            {
                                string insterSql = $@"
                                insert into [dbo].[hospital_organization](hospital_id,hospital_name,hospital_addr,contact_phone,postal_code,contact_person,create_time,is_delete,delete_time,create_user_id)
                                values('{itmes.Id}','{itmes.医院名称}','{itmes.地址}','{itmes.联系电话}','{itmes.邮政编码}','{itmes.联系人}',
                                    GETDATE(),0,null,'{userInfo.职员ID}');";
                                insterCount += insterSql;
                            }
                            await _sqlConnection.ExecuteAsync(insterCount, null, transaction);
                            transaction.Commit();

                        }
                       
                    }
                    catch (Exception exception)
                    {

                        transaction.Rollback();
                        throw new Exception(exception.Message);

                    }
                }
                //var sqlData = await _sqlConnection.QueryAsync<string>("usp_webHealth_NJ_all", new { SPName = name, Params = strParams },
                //    commandType: CommandType.StoredProcedure);
                //string strSqlData = sqlData.FirstOrDefault();
                _sqlConnection.Close();


            }


        }
        /// <summary>
        /// 添加三大目录
        /// </summary>
        /// <param name="param"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task AddCatalog(UserInfoDto userInfo,List<CatalogDto> param, CatalogTypeEnum type)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                //排除已有项目

                _sqlConnection.Open();
                if (param.Any())
                {
                        var paramNew = new List<CatalogDto>();
                       //获取唯一编码
                        var catalogDtoIdList = param.Select(c => c.目录编码).ToList();
                        var ids = ListToStr(catalogDtoIdList);
                        string sqlstr = $"select directory_code  from [dbo].[hospital_three_catalogue]  where directory_code in({ids})";
                        var idListNew = await _sqlConnection.QueryAsync<string>(sqlstr);
                    //排除已有项目
                         paramNew = idListNew.Any() == true ? param.Where(c => !idListNew.Contains(c.目录编码)).ToList() 
                                    : param;
                        string insterCount = null;
                        if (paramNew.Any())
                        {
                            foreach (var itmes in paramNew)
                            {
                                string insterSql = $@"
                                        insert into [dbo].[hospital_three_catalogue]([directory_code],[directory_name],[mnemonic_code],[directory_category_code],[directory_category_name],[unit],[specification],[formulation],
                                        [manufacturer_name],[remark],create_time,is_delete,delete_time,create_user_id,delete_user_id)
                                        values('{itmes.目录编码}','{itmes.目录名称}','{itmes.助记码}',{Convert.ToInt16(type)},'{itmes.目录类别名称}','{itmes.单位}','{itmes.规格}',
                                        '{itmes.剂型}', '{itmes.生产厂家名称}','{itmes.备注}', '{itmes.创建时间}',0,null,'{userInfo.职员ID}',null);";
                                        insterCount += insterSql;
                            }
                            await _sqlConnection.ExecuteAsync(insterCount);
                        }

                    

                }


                _sqlConnection.Close();


            }


        }
        /// <summary>
        /// 删除三大目录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<int> DeleteCatalog(UserInfoDto user,
            int param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                string strSql = $"delete [dbo].[hospital_three_catalogue] where [directory_code]= {param}";
                var num = await _sqlConnection.ExecuteAsync(strSql);
                _sqlConnection.Close();
                return num;
            }
        }
        /// <summary>
        /// 获取三大项目最新时间
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public async Task<string> GetTime(int num)
        {
            string result = null;
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                string strSql = $"select MAX(create_time) from [dbo].[hospital_three_catalogue] where is_delete=0 and directory_code={num} ";
                var timeMax = await _sqlConnection.QueryFirstAsync<string>(strSql);

                result = timeMax;
                _sqlConnection.Close();
            }

            return result;

        }
        /// <summary>
        /// ICD10获取最新时间
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetICD10Time()
        {
            string result = null;
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                string strSql = $"select MAX(create_time) from [dbo].[ICD10] where is_delete=0 ";
                var timeMax = await _sqlConnection.QueryFirstAsync<string>(strSql);

                result = timeMax;
                _sqlConnection.Close();
            }

            return result;

        }
        /// <summary>
        /// 添加ICD10
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task AddICD10(List<ICD10InfoDto> param, UserInfoDto user)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                //排除已有项目

                _sqlConnection.Open();
                if (param.Any())
                {
                    var paramNew = new List<ICD10InfoDto>();
                    //获取唯一编码
                    var catalogDtoIdList = param.Select(c => c.疾病ID).ToList();
                    var ids = ListToStr(catalogDtoIdList);
                    string sqlstr = $"select disease_coding  from [dbo].[ICD10]  where disease_coding in({ids})";
                    var idListNew = await _sqlConnection.QueryAsync<string>(sqlstr);
                    //排除已有项目
                    paramNew = idListNew.Any() == true ? param.Where(c => !idListNew.Contains(c.疾病ID)).ToList()
                        : param;
                    string insterCount = null;
               
                    if (paramNew.Any())
                    {
                        foreach (var itmes in paramNew)
                        {
                        
                            string insterSql = $@"
                                        insert into [dbo].[ICD10]([disease_coding],[disease_name],[mnemonic_code],[remark],[disease_id],
                                          Icd10_create_time, create_time,create_user_id)
                                        values('{itmes.疾病编码}','{itmes.病种名称}','{itmes.助记码}','{itmes.备注}','{itmes.疾病ID}','{itmes.创建时间}',
                                        GETDATE(),'{user.职员ID}');";
                          
                                insterCount += insterSql;
                        }
                        await _sqlConnection.ExecuteAsync(insterCount);
                    }



                }


                _sqlConnection.Close();


            }
        }
        /// <summary>
        /// 获取门诊病人信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task GetOutpatientPerson(UserInfoDto user,List<OutpatientInfoDto> param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {

                _sqlConnection.Open();
                if (param.Any())
                {
                    IDbTransaction transaction = _sqlConnection.BeginTransaction();
                    try
                    {
                        var ywId = ListToStr(param.Select(c => c.业务ID).ToList());
                        var outpatientNum = ListToStr(param.Select(c => c.门诊号).ToList());
                        string strSql =
                            $@"update [dbo].[outpatient] set  [is_delete] =1 ,delete_time=GETDATE(),delete_user_id='{user.职员ID}' where [is_delete]=0 and [business_id] in(" +
                            ywId + ") and [outpatient_number] in(" + outpatientNum + ")";
                        var num = await _sqlConnection.ExecuteAsync(strSql, null, transaction);
                        string insertSql = "";
                        foreach (var item in param)
                        {
                            string str = $@"INSERT INTO [dbo].[outpatient](
                                [patient_name],[id_card_no],[patient_sex],[business_id],[outpatient_number],[visit_date]
                               ,[department_name],[department_id],[diagnostic_doctor],[diagnostic_disease_code],[diagnostic_disease_name]
                               ,[disease_desc],[operator] ,[medical_treatment_total_cost],[remark],[reception_status]
                               ,[create_time],[is_delete],[delete_time],organization_code,create_user_id)
                               VALUES('{item.姓名}','{item.身份证号码}','{item.性别}','{item.业务ID}','{item.门诊号}','{item.就诊日期}'
                                     ,'{item.科室}','{item.科室编码}','{item.诊断医生}','{item.诊断疾病编码}','{item.诊断疾病名称}'
                                     ,'{item.主要病情描述}','{item.经办人}','{item.就诊总费用}','{item.备注}','{item.接诊状态}'
                                     ,GETDATE(),0,null,'{user.机构编码}','{user.职员ID}'
                                );";
                            insertSql += str;

                        }
                     
                        var nums = await _sqlConnection.ExecuteAsync(insertSql, null, transaction);
                        transaction.Commit();
                    }
                    catch (Exception exception)
                    {

                        transaction.Rollback();
                        throw new Exception(exception.Message);
                    }
                }

            }


        }
        /// <summary>
        /// 获取门诊病人明细
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task GetOutpatientDetailPerson(UserInfoDto user,List<OutpatientDetailDto> param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {

                _sqlConnection.Open();
                if (param.Any())
                {
                    IDbTransaction transaction = _sqlConnection.BeginTransaction();
                    try
                    {

                        var outpatientNum = ListToStr(param.Select(c => c.门诊号).ToList());
                        string strSql =
                            $@"update [dbo].[outpatient_fee] set  [is_delete] =1 ,delete_time=GETDATE(),delete_user_id='{user.职员ID}' where [is_delete]=0 
                                and [outpatient_number] in(" + outpatientNum + ")";
                        var num = await _sqlConnection.ExecuteAsync(strSql, null, transaction);
                        string insertSql = "";
                        foreach (var item in param)   
                        {
                            string str = $@"INSERT INTO [dbo].[outpatient_fee](
                               [outpatient_number] ,[cost_detail_id] ,[cost_item_name],[cost_item_code] ,[cost_item_category_name] ,[cost_item_category_code]
                               ,[unit] ,[formulation] ,[specification] ,[unit_price],[quantity],[amount] ,[dosage] ,[usage] ,[medicate_days]
		                       ,[hospital_pricing_unit] ,[is_imported_drugs] ,[drug_producing_area] ,[recipe_code],[cost_document_type] ,[bill_department]
			                   ,[bill_department_id] ,[bill_doctor_name],[bill_doctor_id] ,[bill_time] ,[operate_department_name],[operate_department_id]
                               ,[operate_doctor_name] ,[operate_doctor_id],[operate_time] ,[prescription_doctor] ,[operator],[practice_doctor_number]
                               ,[cost_write_off_id],[organization_code],[organization_name] ,[create_time] ,[is_delete],[delete_time],create_user_id)
                           VALUES('{item.门诊号}','{item.费用明细ID}','{item.项目名称}','{item.项目编码}','{item.项目类别名称}','{item.项目类别编码}'
                                 ,'{item.单位}','{item.剂型}','{item.规格}',{item.单价},{item.数量},{item.金额},'{item.用量}','{item.用法}','{item.用药天数}',
                                 '{item.医院计价单位}','{item.是否进口药品}','{item.药品产地}','{item.处方号}','{item.费用单据类型}','{item.开单科室名称}'
                                 ,'{item.开单科室编码}','{item.开单医生姓名}','{item.开单医生编码}','{item.开单时间}','{item.执行科室名称}','{item.执行科室编码}'
                                 ,'{item.执行医生姓名}','{item.执行医生编码}','{item.执行时间}','{item.处方医师}','{item.经办人}','{item.执业医师证号}'
                                 ,'{item.费用冲销ID}','{item.机构编码}','{item.机构名称}',GETDATE(),0,null,'{user.职员ID}'
                                 );";
                            insertSql += str;
                          


                        }
                        var nums = await _sqlConnection.ExecuteAsync(insertSql, null, transaction);
                        transaction.Commit();
                    }
                    catch (Exception exception)
                    {
                        transaction.Rollback();
                        throw new Exception(exception.Message);
                    }
                }

            }
        }
        /// <summary>
        /// 保存住院病人明细
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task GetInpatientInfoDetailDto(UserInfoDto user, List<InpatientInfoDetailDto> param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {

                _sqlConnection.Open();
             
                if (param.Any())       
                {
                    
                    try
                    {
                       
                        var outpatientNum = ListToStr(param.Select(c =>c.费用明细ID).ToList());
                        var paramFirst = param.FirstOrDefault();
                        string strSql =
                            $@" select [cost_detail_id],[data_sort] from [dbo].[hospitalization_fee] where [hospitalization_no]={paramFirst.住院号}
                                 and [cost_detail_id] in({outpatientNum})";
                        var data = await _sqlConnection.QueryAsync<InpatientInfoDetailQueryDto>(strSql);
                        int sort = 0;
                        List<InpatientInfoDetailDto> paramNew;
                        if (data.Any())
                        {    //获取最大排序号
                            sort = data.Select(c => c.data_sort).Max();
                            var costDetailIdList= data.Select(c => c.cost_detail_id).ToList();
                            //排除已包含的明细id
                            paramNew = param.Where(c=> !costDetailIdList.Contains(c.费用明细ID)).ToList();
                        }
                        else
                        {
                            paramNew = param;
                        }

                        string insertSql = "";
                      
                        foreach (var item in paramNew)
                        {
                            sort++;
                            string str = $@"INSERT INTO [dbo].[hospitalization_fee](
                               [hospitalization_no] ,[cost_detail_id] ,[cost_item_name],[cost_item_code] ,[cost_item_category_name] ,[cost_item_category_code]
                               ,[unit] ,[formulation] ,[specification] ,[unit_price],[quantity],[amount] ,[dosage] ,[usage] ,[medicate_days]
		                       ,[hospital_pricing_unit] ,[is_imported_drugs] ,[drug_producing_area] ,[recipe_code]  ,[cost_document_type] ,[bill_department]
			                   ,[bill_department_id] ,[bill_doctor_name],[bill_doctor_id] ,[bill_time] ,[operate_department_name],[operate_department_id]
                               ,[operate_doctor_name] ,[operate_doctor_id],[operate_time] ,[prescription_doctor] ,[operator],[practice_doctor_number]
                               ,[cost_write_off_id],[organization_code],[organization_name] ,[create_time] ,[is_delete],[delete_time],create_user_id,data_sort,upload_mark)
                           VALUES('{item.住院号}','{item.费用明细ID}','{item.项目名称}','{item.项目编码}','{item.项目类别名称}','{item.项目类别编码}'
                                 ,'{item.单位}','{item.剂型}','{item.规格}',{item.单价},{item.数量},{item.金额},'{item.用量}','{item.用法}','{item.用药天数}',
                                 '{item.医院计价单位}','{item.是否进口药品}','{item.药品产地}','{item.处方号}','{item.费用单据类型}','{item.开单科室名称}'
                                 ,'{item.开单科室编码}','{item.开单医生姓名}','{item.开单医生编码}','{item.开单时间}','{item.执行科室名称}','{item.执行科室编码}'
                                 ,'{item.执行医生姓名}','{item.执行医生编码}','{item.执行时间}','{item.处方医师}','{item.经办人}','{item.执业医师证号}'
                                 ,'{item.费用冲销ID}','{item.机构编码}','{item.机构名称}',GETDATE(),0,null,'{user.职员ID}',{sort},0
                                 );";
                            insertSql += str;
                        }

                        if (paramNew.Count > 0)
                        {
                            var nums = await _sqlConnection.ExecuteAsync(insertSql, null);
                           
                            
                        }

                    }
                    catch (Exception exception)
                    {
                       
                        throw new Exception(exception.Message);
                    }
                }
               

            }
        }
        public async Task<int> UpdateInpatientInfoDetail(UpdateInpatientInfoDetail param)
        {
            int count = 0;
                using (var _sqlConnection = new SqlConnection(_connectionString))
                {
                //update [dbo].[住院费用] set [DataState]=1,update_time=GETDATE(),[UpdateUserId]= where [费用明细ID]='' and [机构编码]=''
                _sqlConnection.Open();
                    count= await _sqlConnection.ExecuteAsync("");
                    _sqlConnection.Close();
                }

            return count;
        }
        /// <summary>
        /// 获取住院病人
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task GetInpatientInfo(UserInfoDto user,List<InpatientInfoDto> param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {

                _sqlConnection.Open();
                if (param.Any())
                {
                    IDbTransaction transaction = _sqlConnection.BeginTransaction();
                    try
                    {

                        var outpatientNum = ListToStr(param.Select(c => c.住院号).ToList());
                        var businessId = ListToStr(param.Select(c => c.业务ID).ToList());
                        string strSql =
                            $@"update  [dbo].[inpatient] set  [is_delete] =1 ,delete_time=GETDATE(),delete_user_id='{user.职员ID}' where [is_delete]=0  and [hospitalization_no] in ({outpatientNum})
                                 and [business_id] in({businessId})";
                        var num = await _sqlConnection.ExecuteAsync(strSql, null, transaction);
                        string insertSql = "";
                        foreach (var item in param)
                        {
                            string str = $@"
                                INSERT INTO [dbo].[inpatient]
                                           ([hospital_name] ,[admission_date]  ,[leave_hospital_date] ,[hospitalization_no] ,[business_id] ,[patient_name] ,[id_card_no]
                                           ,[patient_sex],[birthday] ,[contact_name],[contact_phone] ,[family_address] ,[in_department_name] ,[in_department_id]
                                           ,[admission_diagnostic_doctor] ,[admission_bed] ,[admission_main_diagnosis] ,[admission_main_diagnosis_icd10] ,[admission_secondary_diagnosis] ,[admission_secondary_diagnosis_icd10]
                                           ,[admission_ward] ,[admission_operator] ,[admission_operate_time] ,[hospitalization_total_cost] ,[remark] ,[leave_department_name] ,[leave_department_id] 
		                                   ,[leave_hospital_ward] ,[leave_hospital_bed]  ,[leave_hospital_main_diagnosis] ,[leave_hospital_main_diagnosis_icd10] ,[leave_hospital_secondary_diagnosis] ,[leave_hospital_secondary_diagnosis_icd10]
                                           ,[inpatient_hospital_state] ,[admission_diagnostic_doctor_id] ,[admission_bed_id]  ,[admission_ward_id],[leave_hospital_bed_id] ,[leave_hospital_ward_id]
                                           ,[create_time]  ,[is_delete] ,[delete_time],organization_code,create_user_id)
                                     VALUES ('{item.医院名称}','{item.入院日期}','{item.出院日期}','{item.住院号}','{item.业务ID}','{item.姓名}','{item.身份证号}',
                                             '{item.性别}','{item.出生日期}','{item.联系人姓名}','{item.联系电话}','{item.家庭地址}','{item.入院科室}','{item.入院科室编码}',
                                             '{item.入院诊断医生}','{item.入院床位}','{item.入院主诊断}','{item.入院主诊断ICD10}','{item.入院次诊断}','{item.入院次诊断ICD10}',
                                             '{item.入院病区}','{item.入院经办人}','{item.入院经办时间}','{item.住院总费用}','{item.备注}','{item.出院科室}','{item.出院科室编码}',
                                             '{item.出院病区}','{item.出院床位}','{item.出院主诊断}','{item.出院主诊断ICD10}','{item.出院次诊断}','{item.出院主诊断ICD10}',
                                             '{item.在院状态}','{item.入院诊断医生编码}','{item.入院床位编码}','{item.入院病区编码}','{item.出院床位编码}','{item.出院病区编码}',
                                               GETDATE(),0,null,'{user.机构编码}','{user.职员ID}'
                                              );";
                            insertSql += str;
                        }
                        var nums = await _sqlConnection.ExecuteAsync(insertSql, null, transaction);
                        transaction.Commit();
                        transaction.Dispose();
                    }
                    catch (Exception exception)
                    {
                        transaction.Dispose();
                        transaction.Rollback();
                        throw new Exception(exception.Message);
                    }
                }

            }
        }
        /// <summary>
        /// 住院病人查询
        /// </summary>
        /// <returns></returns>
        public async Task<QueryInpatientInfoDto> QueryInpatientInfo(QueryInpatientInfoParam param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {

                _sqlConnection.Open();
                string strSql = $@"SELECT top 1 [Id]
                                  ,[医院名称]
                                  ,[入院日期]
                                  ,[出院日期]
                                  ,[住院号]
                                  ,[业务ID]
                                  ,[姓名]
                                  ,[身份证号]
                                  ,[性别]
                                  ,[出生日期]
                                  ,[联系人姓名]
                                  ,[联系电话]
                                  ,[家庭地址]
                                  ,[入院科室]
                                  ,[入院科室编码]
                                  ,[入院诊断医生]
                                  ,[入院床位]
                                  ,[入院主诊断]
                                  ,[入院主诊断ICD10]
                                  ,[入院次诊断]
                                  ,[入院次诊断ICD10]
                                  ,[入院病区]
                                  ,[入院经办人]
                                  ,[入院经办时间]
                                  ,[住院总费用]
                                  ,[备注]
                                  ,[出院科室]
                                  ,[出院科室编码]
                                  ,[出院病区]
                                  ,[出院床位]
                                  ,[出院主诊断]
                                  ,[出院主诊断ICD10]
                                  ,[出院次诊断]
                                  ,[出院次诊断ICD10]
                                  ,[在院状态]
                                  ,[入院诊断医生编码]
                                  ,[入院床位编码]
                                  ,[入院病区编码]
                                  ,[出院床位编码]
                                  ,[出院病区编码]

                               FROM [dbo].[住院病人] where is_delete=0 and 业务ID='{param.BusinessId}' and   OrgCode='{param.InstitutionalNumber}'
                                   ";

                var data = await _sqlConnection.QueryFirstAsync<QueryInpatientInfoDto>(strSql);

                _sqlConnection.Close();
                return data;
            }
        }
        /// <summary>
        /// 医保信息查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<Int32> QueryMedicalInsurance(string param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                string strSql = $"select  COUNT(*) from [dbo].[住院医保信息] where [业务ID]={param} and is_delete=0";
                var counts = await _sqlConnection.ExecuteAsync(strSql);
                _sqlConnection.Close();
                return counts;

            }
        }
        /// <summary>
        /// 医保信息保存
        /// </summary>
        /// <param name="user"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task MedicalInsurance(UserInfoDto user, List<MedicalInsuranceDto> param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {

                _sqlConnection.Open();
                if (param.Any())
                {
                    string insertSql = "";
                    foreach (var item in param)
                    {
                        string str = $@"INSERT INTO [dbo].[住院医保信息]([住院Id],[业务ID],[医保卡号]
                               ,[医保总费用],[报账费用] ,[自付费用],[其他信息] 
		                       ,[create_time],[update_time] ,[is_delete] ,[delete_time],OrgCode,CreateUserId)
                           VALUES(
                                 {item.业务ID},{item.业务ID}, {item.医保卡号},{item.医保总费用},
                                 {item.报账费用},{item.自付费用}, {item.其他信息},
                                GETDATE(),GETDATE(),0,null,'{user.机构编码}','{user.职员ID}'
                                 );";
                        insertSql += str;

                    }
                    var nums = await _sqlConnection.ExecuteAsync(insertSql);
                }

            }
        }
        /// <summary>
        ///  医保反馈数据保存
        /// </summary>
        /// <param name="user"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task SaveMedicalInsuranceDataAll(MedicalInsuranceDataAllParam param)
        {


            using (var _sqlConnection = new SqlConnection(_connectionString))
            {

                _sqlConnection.Open();

                IDbTransaction transaction = _sqlConnection.BeginTransaction();
                try
                {
                    
                    string strSql =
                        $@"update MedicalInsuranceDataAll set delete_time=GETDATE(),delete_user_id='{param.CreateUserId}' where  delete_time is  null and DataId='{param.DataId}' and BusinessId='{param.BusinessId}'";
                    var num = await _sqlConnection.ExecuteAsync(strSql, null, transaction);
                    string insertSql = $@"INSERT INTO [dbo].[MedicalInsuranceDataAll]
                   ([DataAllId]
                   ,[ParticipationJson]
                   ,[ResultDataJson]
                   ,[DataType]
                   ,[DataId]
                   ,[Remark]
                   ,[CreateUserId]
                   ,[create_time]
                   ,BusinessId
                   ,HisMedicalInsuranceId
                   ,OrgCode
                   ,IDCard
                    )
                VALUES ('{param.DataAllId}','{param.ParticipationJson}','{param.ResultDataJson}','{param.DataType}','{param.DataId}'
                        , '{param.Remark}','{param.CreateUserId}', GETDATE(),'{param.BusinessId}'
                        ,'{param.HisMedicalInsuranceId}','{param.OrgCode}','{param.IdCard}')";
                    var nums = await _sqlConnection.ExecuteAsync(insertSql, null, transaction);
                    transaction.Commit();
                }
                catch (Exception exception)
                {

                    transaction.Rollback();
                    throw new Exception(exception.Message);
                }

            }

        }
        /// <summary>
        ///  医保反馈数据查询
        /// </summary>
        /// <param name="user"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<MedicalInsuranceDataAllDto> SaveMedicalInsuranceDataAllQuery(MedicalInsuranceDataAllParamUIQueryParam param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                var resultData = new MedicalInsuranceDataAllDto();
                _sqlConnection.Open();
                string strSql = $@"
                SELECT [DataAllId]
                      ,[ParticipationJson]
                      ,[ResultDataJson]
                      ,[DataType]
                      ,[DataId]
                      ,[HisMedicalInsuranceId]
                      ,[BusinessId]
                      ,[Remark]
                      ,[CreateUserId]
                      ,[create_time]
                      ,[delete_time]
                      ,[OrgCode]
                      ,[delete_user_id]
                  FROM [dbo].[MedicalInsuranceDataAll] where DataId='{param.DataId}' and  DataType='{param.DataType}' and OrgCode='{param.OrgCode}' and BusinessId='{param.BusinessId}' and  delete_time is  null";
                 var data = await _sqlConnection.QueryFirstOrDefaultAsync<MedicalInsuranceDataAllDto>(strSql);
                 return data ?? resultData;


            }

            
        }
        /// <summary>
        /// 业务ID
        /// </summary>
        /// <param name="user"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<Int32> DeleteMedicalInsurance(UserInfoDto user,string param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                int result = 0;
                _sqlConnection.Open();
                if (param.Any())
                {

                    string insertSql = $@"update [dbo].[住院医保信息] set is_delete=1,delete_time=GETDATE(),delete_user_id='{user.职员ID}' where  [业务ID]={param}";
                    result = await _sqlConnection.ExecuteAsync(insertSql);
                }
                return result;
            }
        }
        public async Task<Int32> InformationInfoSave(UserInfoDto user,List<InformationDto> param, InformationParam info)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                Int32 counts = 0;
                _sqlConnection.Open();
                if (param.Any())
                {
                    IDbTransaction transaction = _sqlConnection.BeginTransaction();
                    try
                    {

                        var outpatientNum = ListToStr(param.Select(c => c.目录编码).ToList());
                        string strSql =
                            $@"update [dbo].[comprehensive_catalogue] set  [is_delete] =1 ,delete_time=GETDATE(),delete_user_id='{user.职员ID}' where [is_delete]=0 
                                and [directory_code] in(" + outpatientNum + ")";
                        await _sqlConnection.ExecuteAsync(strSql, null, transaction);
                        string insertSql = "";
                        int mund = 0;
                        foreach (var item in param)
                        {
                            mund++;
                            string str = $@"INSERT INTO [dbo].[comprehensive_catalogue]
                                   ([directory_type] ,[organization_code],[directory_code],[directory_name]
                                   ,[mnemonic_code],[directory_category_name],[remark] ,[create_time]
		                            ,[is_delete],[delete_time],create_user_id)
                             VALUES ({info.目录类型},'{info.机构编码}','{item.目录编码}','{item.目录名称}',
                                     '{item.助记码}','{item.目录类别名称}','{item.备注}',GETDATE(),
                                       0, null,'{user.职员ID}');";
                            insertSql += str;

                        }

                        counts = await _sqlConnection.ExecuteAsync(insertSql, null, transaction);
                        transaction.Commit();
                    }
                    catch (Exception)
                    {

                        transaction.Rollback();
                        throw;
                    }
                    _sqlConnection.Close();
                }

                return counts;
            }
        }
        public async Task<Int32> PairCode(UserInfoDto user,List<PairCodeDto> param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                int result = 0;
                
                _sqlConnection.Open();
                if (param.Any())
                {
                    string insertSql =null;
                    foreach (var item in param)
                    {
                         insertSql += $@"INSERT INTO [dbo].[社保对码]
                           ([居民普通门诊报销标志],[居民普通门诊报销限价] ,[操作人员姓名] ,[版本],[状态]
                            [社保目录类别],[社保目录ID] ,[社保目录编码],[社保目录名称]
                           ,[拼音],[剂型],[规格],[单位],[生产厂家],[收费级别],[准字号]
                           ,[新码标志],[限制用药标志] ,[限制支付范围] ,[职工自付比例],[居民自付比例]
                           ,[备注],[create_time])
                          VALUES(  {Convert.ToInt16(item.CKE889)},{Convert.ToDecimal(item.CKA601)},'{user.职员ID}','y',{Convert.ToInt16(item.AAE100)}
                                  ,{Convert.ToInt16(item.AKA063)},'{DateTime.Now.ToString("yyyymmddhhmmssfff")}', '{item.AKE001}','{item.AKE002}'
                                  ,'{item.AKA020}','{item.AKA070}','{item.AKA074}','{item.AKA067}','{item.AKA098}','{item.AKA065}','{item.CKA603}'
                                  ,{Convert.ToInt16(item.CKE897)},{Convert.ToInt16(item.AKA036)},'{item.CKE599}','{item.AKA069}','{item.CKE899}',
                                  ,'{item.AAE013}',GETDATE()
                               )";
                    }

                    
                    result = await _sqlConnection.ExecuteAsync(insertSql);
                }
                return result;
            }
        }
        private string ListToStr(List<string> param)
        {
            string result = null;
            if (param.Any())
            {
                foreach (var item in param)
                {
                    result += "'" + item + "'" + ",";
                }

            }
            return result?.Substring(0, result.Length - 1);
        }
    }

}
