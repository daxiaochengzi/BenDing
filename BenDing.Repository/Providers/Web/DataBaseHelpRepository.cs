using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Resident;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Enums;
using BenDing.Domain.Models.Params.Resident;
using BenDing.Domain.Models.Params.UI;
using BenDing.Domain.Models.Params.Web;
using BenDing.Domain.Xml;
using BenDing.Repository.Interfaces.Web;
using Dapper;
using NFine.Code;

namespace BenDing.Repository.Providers.Web
{
    public class DataBaseHelpRepository : IDataBaseHelpRepository
    {
        private IBaseSqlServerRepository _baseSqlServerRepository;
        private ISystemManageRepository _iSystemManageRepository;
        private string _connectionString;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="conStr"></param>   
        public DataBaseHelpRepository(IBaseSqlServerRepository iBaseSqlServerRepository, ISystemManageRepository ImanageRepository)
        {
            _baseSqlServerRepository = iBaseSqlServerRepository;
            _iSystemManageRepository = ImanageRepository;
            string conStr = ConfigurationManager.ConnectionStrings["NFineDbContext"].ToString();
            _connectionString = !string.IsNullOrWhiteSpace(conStr) ? conStr : throw new ArgumentNullException(nameof(conStr));

        }
        /// <summary>
        /// 更新医疗机构
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task ChangeOrg(UserInfoDto userInfo, List<OrgDto> param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {

                _sqlConnection.Open();
                if (param.Any())
                {
                    IDbTransaction transaction = _sqlConnection.BeginTransaction();
                    try
                    {

                        string strSql = $"update [dbo].[HospitalOrganization] set DeleteTime=GETDATE(),IsDelete=1,DeleteUserId='{userInfo.UserId}'";

                        if (param.Any())
                        {

                            await _sqlConnection.ExecuteAsync(strSql, null, transaction);

                            string insterCount = null;
                            foreach (var itmes in param)
                            {
                                string insterSql = $@"
                                insert into [dbo].[HospitalOrganization](id,HospitalId,HospitalName,HospitalAddr,ContactPhone,PostalCode,ContactPerson,CreateTime,IsDelete,DeleteTime,CreateUserId)
                                values('{Guid.NewGuid()}','{itmes.Id}','{itmes.HospitalName}','{itmes.HospitalAddr}','{itmes.ContactPhone}','{itmes.PostalCode}','{itmes.ContactPerson}',
                                    GETDATE(),0,null,'{userInfo.UserId}');";
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
        public async Task AddCatalog(UserInfoDto userInfo, List<CatalogDto> param, CatalogTypeEnum type)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                try
                {
                    if (param.Any())
                    {
                        string insterCount = null;
                        foreach (var itmes in param)
                        {
                            string insterSql = $@"
                                    insert into [dbo].[HospitalThreeCatalogue]([id],[DirectoryCode],[DirectoryName],[MnemonicCode],[DirectoryCategoryCode],[DirectoryCategoryName],[Unit],[Specification],[formulation],
                                    [ManufacturerName],[remark],DirectoryCreateTime,CreateTime,IsDelete,CreateUserId,FixedEncoding)
                                    values('{Guid.NewGuid()}','{itmes.DirectoryCode}','{itmes.DirectoryName}','{itmes.MnemonicCode}',{Convert.ToInt16(type)},'{itmes.DirectoryCategoryName}','{itmes.Unit}','{itmes.Specification}','{itmes.Formulation}',
                                   '{itmes.ManufacturerName}','{itmes.Remark}', '{itmes.DirectoryCreateTime}',GETDATE(),0,'{userInfo.UserId}','{ BitConverter.ToInt64(Guid.Parse(itmes.DirectoryCode).ToByteArray(), 0)}');";
                            insterCount += insterSql;
                        }
                        await _sqlConnection.ExecuteAsync(insterCount, null);

                    }
                    _sqlConnection.Close();
                }
                catch (Exception e)
                {

                    _sqlConnection.Close();
                    throw new Exception(e.Message);
                }

            }


        }
        /// <summary>
        /// 基层端三大目录查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<Dictionary<int, List<QueryCatalogDto>>> QueryCatalog(QueryCatalogUiParam param)
        {
            var dataList = new List<QueryCatalogDto>();
            var resultData = new Dictionary<int, List<QueryCatalogDto>>();
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                string querySql = @"
                             select Id, DirectoryCode ,DirectoryName,MnemonicCode,DirectoryCategoryName,Unit,Specification
                             ,Formulation,ManufacturerName,Remark,DirectoryCreateTime from [dbo].[HospitalThreeCatalogue] where IsDelete=0";
                string countSql = @"select count(*) from [dbo].[HospitalThreeCatalogue] where IsDelete=0";
                string whereSql = "";
                if (!string.IsNullOrWhiteSpace(param.DirectoryCategoryCode))
                {
                    whereSql += $" and DirectoryCategoryCode='{param.DirectoryCategoryCode}'";
                }
                if (!string.IsNullOrWhiteSpace(param.DirectoryCode))
                {
                    whereSql += $" and DirectoryCode='{param.DirectoryCode}'";
                }
                if (!string.IsNullOrWhiteSpace(param.DirectoryName))
                {
                    whereSql += "  and DirectoryName like '" + param.DirectoryName + "%'";
                }
                if (param.Limit != 0 && param.Page > 0)
                {
                    var skipCount = param.Limit * (param.Page - 1);
                    querySql += whereSql + " order by CreateTime desc OFFSET " + skipCount + " ROWS FETCH NEXT " + param.Limit + " ROWS ONLY;";
                }
                string executeSql = countSql + whereSql + ";" + querySql;

                var result = await _sqlConnection.QueryMultipleAsync(executeSql);

                int totalPageCount = result.Read<int>().FirstOrDefault();
                dataList = (from t in result.Read<QueryCatalogDto>()

                            select t).ToList();

                resultData.Add(totalPageCount, dataList);
                _sqlConnection.Close();

            }


            return resultData;

        }
        /// <summary>
        /// 医保中心端
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<Dictionary<int, List<ResidentProjectDownloadRow>>> QueryProjectDownload(QueryProjectUiParam param)
        {
            var dataList = new List<ResidentProjectDownloadRow>();
            var resultData = new Dictionary<int, List<ResidentProjectDownloadRow>>();
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                string querySql = @"
                             select [Id],[ProjectCode],[ProjectName],[ProjectCodeType],Unit,MnemonicCode,Formulation,ProjectLevel,
                             Manufacturer,QuasiFontSize,Specification,Remark,NewCodeMark,NewUpdateTime from [dbo].[MedicalInsuranceProject] 
                             where  IsDelete=0";
                string countSql = @"select count(*) from [dbo].[MedicalInsuranceProject] where  IsDelete=0";
                string whereSql = "";
                if (!string.IsNullOrWhiteSpace(param.ProjectCodeType))
                {   //西药
                    if (param.ProjectCodeType == "1")
                    {
                        whereSql += $" and ProjectCodeType in('11','91','92')";
                    }//中药
                    if (param.ProjectCodeType == "0")
                    {
                        whereSql += $" and ProjectCodeType in('12','13','91','92')";
                    }//耗材
                    if (param.ProjectCodeType == "3")
                    {
                        whereSql += $" and ProjectCodeType in('41','81','91','92')";
                    }

                    if (param.ProjectCodeType == "2")
                    {
                        whereSql += $" and ProjectCodeType not in('11','12','13','41','81','91','92')";
                    }
                }
                if (!string.IsNullOrWhiteSpace(param.ProjectCode))
                {
                    whereSql += $" and ProjectCode='{param.ProjectCode}'";
                }
                if (!string.IsNullOrWhiteSpace(param.ProjectName))
                {
                    whereSql += "  and ProjectName like '" + param.ProjectName + "%'";
                }
                if (param.Limit != 0 && param.Page > 0)
                {
                    var skipCount = param.Limit * (param.Page - 1);
                    querySql += whereSql + " order by CreateTime desc OFFSET " + skipCount + " ROWS FETCH NEXT " + param.Limit + " ROWS ONLY;";
                }
                string executeSql = countSql + whereSql + ";" + querySql;

                var result = await _sqlConnection.QueryMultipleAsync(executeSql);

                int totalPageCount = result.Read<int>().FirstOrDefault();
                dataList = (from t in result.Read<ResidentProjectDownloadRow>()
                            select t).ToList();
                resultData.Add(totalPageCount, dataList);
                _sqlConnection.Close();

            }


            return resultData;

        }
        /// <summary>
        /// 删除三大目录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<int> DeleteCatalog(UserInfoDto user, int param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                string strSql = $" update [dbo].[HospitalThreeCatalogue] set IsDelete=1 ,DeleteUserId='{user.UserId}',DeleteTime=GETDATE()  where DirectoryCategoryCode='{param.ToString()}'";
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
                string strSql = $"select MAX(DirectoryCreateTime) from [dbo].[HospitalThreeCatalogue] where IsDelete=0 and DirectoryCategoryCode={num} ";
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
                string strSql = $"select MAX(CreateTime) from [dbo].[ICD10] where IsDelete=0 ";
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
                    var catalogDtoIdList = param.Select(c => c.DiseaseId).ToList();
                    var ids = ListToStr(catalogDtoIdList);
                    string sqlstr = $"select DiseaseCoding  from [dbo].[ICD10]  where DiseaseCoding  in({ids}) and IsDelete=0";
                    var idListNew = await _sqlConnection.QueryAsync<string>(sqlstr);
                    //排除已有项目
                    paramNew = idListNew.Any() == true ? param.Where(c => !idListNew.Contains(c.DiseaseId)).ToList()
                        : param;
                    string insterCount = null;

                    if (paramNew.Any())
                    {
                        foreach (var itmes in paramNew)
                        {

                            string insterSql = $@"
                                        insert into [dbo].[ICD10]([id],[DiseaseCoding],[DiseaseName],[MnemonicCode],[Remark],[DiseaseId],
                                          Icd10CreateTime, CreateTime,CreateUserId,IsDelete)
                                        values('{Guid.NewGuid()}','{itmes.DiseaseCoding}','{itmes.DiseaseName}','{itmes.MnemonicCode}','{itmes.Remark}','{itmes.DiseaseId}','{itmes.Icd10CreateTime}',
                                        GETDATE(),'{user.UserId}',0);";

                            insterCount += insterSql;
                        }
                        await _sqlConnection.ExecuteAsync(insterCount);
                    }



                }


                _sqlConnection.Close();


            }
        }
        /// <summary>
        /// ICD10查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<List<QueryICD10InfoDto>> QueryICD10(QueryICD10UiParam param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                //排除已有项目
                var resultData = new List<QueryICD10InfoDto>();
                _sqlConnection.Open();

                string strSql = $"select  [id],[DiseaseCoding],[DiseaseName] ,[MnemonicCode],[Remark] ,DiseaseId from [dbo].[ICD10]  where IsDelete=0";
                string regexstr = @"[\u4e00-\u9fa5]";

                if (Regex.IsMatch(param.Search, regexstr))
                {
                    strSql += " and DiseaseName like '" + param.Search + "%'";
                }
                else
                {
                    strSql += " and MnemonicCode like '" + param.Search + "%'";
                }

                var data = await _sqlConnection.QueryAsync<QueryICD10InfoDto>(strSql);

                if (data != null && data.Any())
                {
                    resultData = data.ToList();
                }


                _sqlConnection.Close();

                return resultData;


            }
        }
        /// <summary>
        /// 获取门诊病人信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task GetOutpatientPerson(UserInfoDto user, List<OutpatientInfoDto> param)
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
                            $@"update [dbo].[outpatient] set  [IsDelete] =1 ,DeleteTime=GETDATE(),DeleteUserId='{user.UserId}' where [IsDelete]=0 and [businessid] in(" +
                            ywId + ") and [outpatientnumber] in(" + outpatientNum + ")";
                        var num = await _sqlConnection.ExecuteAsync(strSql, null, transaction);
                        string insertSql = "";
                        foreach (var item in param)
                        {
                            string str = $@"INSERT INTO [dbo].[outpatient](
                               id, [patientname],[idcardno],[patientsex],[businessid],[outpatientnumber],[visitdate]
                               ,[departmentname],[department_id],[diagnosticdoctor],[diagnosticdisease_code],[diagnosticdiseasename]
                               ,[diseasedesc],[operator] ,[medicaltreatmenttotalcost],[remark],[reception_status]
                               ,[createtime],[IsDelete],[DeleteTime],organizationcode,CreateUserId)
                               VALUES('{Guid.NewGuid()}','{item.姓名}','{item.身份证号码}','{item.性别}','{item.业务ID}','{item.门诊号}','{item.就诊日期}'
                                     ,'{item.科室}','{item.科室编码}','{item.诊断医生}','{item.诊断疾病编码}','{item.诊断疾病名称}'
                                     ,'{item.主要病情描述}','{item.经办人}','{item.就诊总费用}','{item.备注}','{item.接诊状态}'
                                     ,GETDATE(),0,null,'{user.OrganizationCode}','{user.UserId}'
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
        public async Task GetOutpatientDetailPerson(UserInfoDto user, List<OutpatientDetailDto> param)
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
                            $@"update [dbo].[outpatientfee] set  [IsDelete] =1 ,DeleteTime=GETDATE(),DeleteUserId='{user.UserId}' where [IsDelete]=0 
                                and [outpatientnumber] in(" + outpatientNum + ")";
                        var num = await _sqlConnection.ExecuteAsync(strSql, null, transaction);
                        string insertSql = "";
                        foreach (var item in param)
                        {
                            string str = $@"INSERT INTO [dbo].[outpatientfee](
                                id,[outpatientnumber] ,[costdetailid] ,[costitemname],[cost_item_code] ,[costitemcategoryname] ,[costitemcategorycode]
                               ,[unit] ,[formulation] ,[specification] ,[unitprice],[quantity],[amount] ,[dosage] ,[usage] ,[medicatedays]
		                       ,[hospitalpricingunit] ,[isimporteddrugs] ,[drugproducingarea] ,[recipecode],[costdocumenttype] ,[billdepartment]
			                   ,[billdepartmentid] ,[billdoctorname],[billdoctorid] ,[billtime] ,[operatedepartmentname],[operatedepartmentid]
                               ,[operatedoctorname] ,[operate_doctor_id],[operatetime] ,[prescriptiondoctor] ,[operator],[practicedoctornumber]
                               ,[costwriteoffid],[organizationcode],[organizationname] ,[createtime] ,[IsDelete],[DeleteTime],CreateUserId)
                           VALUES('{Guid.NewGuid()}','{item.门诊号}','{item.费用明细ID}','{item.项目名称}','{item.项目编码}','{item.项目类别名称}','{item.项目类别编码}'
                                 ,'{item.单位}','{item.剂型}','{item.规格}',{item.单价},{item.数量},{item.金额},'{item.用量}','{item.用法}','{item.用药天数}',
                                 '{item.医院计价单位}','{item.是否进口药品}','{item.药品产地}','{item.处方号}','{item.费用单据类型}','{item.开单科室名称}'
                                 ,'{item.开单科室编码}','{item.开单医生姓名}','{item.开单医生编码}','{item.开单时间}','{item.执行科室名称}','{item.执行科室编码}'
                                 ,'{item.执行医生姓名}','{item.执行医生编码}','{item.执行时间}','{item.处方医师}','{item.经办人}','{item.执业医师证号}'
                                 ,'{item.费用冲销ID}','{item.机构编码}','{item.机构名称}',GETDATE(),0,null,'{user.UserId}'
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

                        var outpatientNum = ListToStr(param.Select(c => c.CostDetailId).ToList());
                        var paramFirst = param.FirstOrDefault();
                        string strSql =
                            $@" select [CostDetailId],[DataSort] from [dbo].[HospitalizationFee] where [HospitalizationNo]={paramFirst.HospitalizationNo}
                                 and [CostDetailId] in({outpatientNum})";
                        var data = await _sqlConnection.QueryAsync<InpatientInfoDetailQueryDto>(strSql);
                        int sort = 0;
                        List<InpatientInfoDetailDto> paramNew;
                        if (data.Any())
                        {    //获取最大排序号
                            sort = data.Select(c => c.DataSort).Max();
                            var costDetailIdList = data.Select(c => c.CostDetailId).ToList();
                            //排除已包含的明细id
                            paramNew = param.Where(c => !costDetailIdList.Contains(c.CostDetailId)).ToList();
                        }
                        else
                        {
                            paramNew = param.OrderBy(d => d.BillTime).ToList();
                        }

                        string insertSql = "";

                        foreach (var item in paramNew)
                        {
                            sort++;
                            var businessTime = item.BillTime.Substring(0, 10) + " 00:00:00.000";
                            string str = $@"INSERT INTO [dbo].[HospitalizationFee](
                               id,[HospitalizationNo] ,[CostDetailId] ,[CostItemName],[CostItemCode] ,[CostItemCategoryName] ,[CostItemCategoryCode]
                               ,[Unit] ,[Formulation] ,[Specification] ,[UnitPrice],[Quantity],[Amount] ,[Dosage] ,[Usage] ,[MedicateDays]
		                       ,[HospitalPricingUnit] ,[IsImportedDrugs] ,[DrugProducingArea] ,[RecipeCode]  ,[CostDocumentType] ,[BillDepartment]
			                   ,[BillDepartmentId] ,[BillDoctorName],[BillDoctorId] ,[BillTime] ,[OperateDepartmentName],[OperateDepartmentId]
                               ,[OperateDoctorName] ,[OperateDoctorId],[OperateTime] ,[PrescriptionDoctor] ,[Operators],[PracticeDoctorNumber]
                               ,[CostWriteOffId],[OrganizationCode],[OrganizationName] ,[CreateTime] ,[IsDelete],[DeleteTime],CreateUserId,DataSort,UploadMark,RecipeCodeFixedEncoding,BillDoctorIdFixedEncoding,BusinessTime)
                           VALUES('{Guid.NewGuid()}','{item.HospitalizationNo}','{item.CostDetailId}','{item.CostItemName}','{item.CostItemCode}','{item.CostItemCategoryName}','{item.CostItemCategoryCode}'
                                 ,'{item.Unit}','{item.Formulation}','{item.Specification}',{item.UnitPrice},{item.Quantity},{item.Amount},'{item.Dosage}','{item.Usage}','{item.MedicateDays}',
                                 '{item.HospitalPricingUnit}','{item.IsImportedDrugs}','{item.DrugProducingArea}','{item.RecipeCode}','{item.CostDocumentType}','{item.BillDepartment}'
                                 ,'{item.BillDepartmentId}','{item.BillDoctorName}','{item.BillDoctorId}','{item.BillTime}','{item.OperateDepartmentName}','{item.OperateDepartmentId}'
                                 ,'{item.OperateDoctorName}','{item.OperateDoctorId}','{item.OperateTime}','{item.PrescriptionDoctor}','{item.Operators}','{item.PracticeDoctorNumber}'
                                 ,'{item.CostWriteOffId}','{item.OrganizationCode}','{item.OrganizationName}',GETDATE(),0,null,'{user.UserId}',{sort},0,'{CommonHelp.GuidToStr(item.RecipeCode)}','{CommonHelp.GuidToStr(item.BillDoctorId)}','{businessTime}'
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
        /// <summary>
        /// 住院清单查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<Dictionary<int, List<QueryHospitalizationFeeDto>>> QueryHospitalizationFee(QueryHospitalizationFeeUiParam param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                var dataList = new List<QueryHospitalizationFeeDto>();
                var dataListNew = new List<QueryHospitalizationFeeDto>();
                var resultData = new Dictionary<int, List<QueryHospitalizationFeeDto>>();
                _sqlConnection.Open();

                string querySql = $@"
                             select Id,[CostItemCode] as DirectoryCode,[CostItemName] as DirectoryName,[CostItemCategoryCode] as DirectoryCategoryCode,
                             Quantity,UnitPrice,Amount,BillTime,RecipeCode,BillDepartment,OperateDoctorName,OrganizationCode from [dbo].[HospitalizationFee] 
                             where HospitalizationNo=(select top 1 a.HospitalizationNo from [dbo].[Inpatient] as a where a.[BusinessId]='{param.BusinessId}')";
                string countSql = $@"select COUNT(*) from [dbo].[HospitalizationFee] 
                              where HospitalizationNo=(select top 1 a.HospitalizationNo from [dbo].[Inpatient] as a where a.[BusinessId]='{param.BusinessId}')";
                string whereSql = "";
                if (param.Limit != 0 && param.Page > 0)
                {
                    var skipCount = param.Limit * (param.Page - 1);
                    querySql += whereSql + " order by CreateTime desc OFFSET " + skipCount + " ROWS FETCH NEXT " + param.Limit + " ROWS ONLY;";
                }
                string executeSql = countSql + whereSql + ";" + querySql;
                var result = await _sqlConnection.QueryMultipleAsync(executeSql);
                int totalPageCount = result.Read<int>().FirstOrDefault();
                dataList = (from t in result.Read<QueryHospitalizationFeeDto>()
                            select t).ToList();
                if (dataList.Any())
                {
                    var organizationCode = dataList.Select(d => d.OrganizationCode).FirstOrDefault();
                    var directoryCodeList = dataList.Select(c => c.DirectoryCode).ToList();
                    var queryPairCodeParam = new QueryMedicalInsurancePairCodeParam()
                    {
                        DirectoryCodeList = directoryCodeList,
                        OrganizationCode = organizationCode
                    };
                    //获取医保对码数据
                    var pairCodeData = await _baseSqlServerRepository.QueryMedicalInsurancePairCode(queryPairCodeParam);
                    //获取医院登记
                    var gradeData = await _iSystemManageRepository.QueryHospitalOrganizationGrade(organizationCode);
                    //获取入院病人登记信息
                    var residentInfoData = await _baseSqlServerRepository.QueryMedicalInsuranceResidentInfo(
                             new QueryMedicalInsuranceResidentInfoParam() { BusinessId = param.BusinessId });
                    if (pairCodeData != null)
                    {
                        foreach (var c in dataList)
                        {
                            var itemPairCode = pairCodeData
                                   .FirstOrDefault(d => d.DirectoryCode == c.DirectoryCode);
                            var item = new QueryHospitalizationFeeDto
                            {
                                Id = c.Id,
                                Amount = c.Amount,
                                BillTime = c.BillTime,
                                BillDepartment = c.BillDepartment,
                                DirectoryCode = c.DirectoryCode,
                                DirectoryName = c.DirectoryName,
                                DirectoryCategoryCode = c.DirectoryCategoryCode,
                                UnitPrice = c.UnitPrice,
                                UploadUserName = c.UploadUserName,
                                Quantity = c.Quantity,
                                RecipeCode = c.RecipeCode,
                                Specification = c.Specification,
                                OperateDoctorName = c.OperateDoctorName,
                                UploadMark = c.UploadMark,
                                AdjustmentDifferenceValue = c.AdjustmentDifferenceValue,
                                BlockPrice = itemPairCode != null ? GetBlockPrice(itemPairCode, gradeData) : 0,
                                ProjectCode = itemPairCode?.ProjectCode,
                                ProjectCodeType = itemPairCode?.ProjectCodeType,
                                ProjectLevel = itemPairCode?.ProjectCodeType,
                                SelfPayProportion= (itemPairCode != null && residentInfoData != null)?GetSelfPayProportion(itemPairCode, residentInfoData) :0,
                                UploadAmount=c.UploadAmount
                            };
                            dataListNew.Add(item);

                        }




                    }
                }
                {
                    dataListNew = dataList;
                }
                _sqlConnection.Close();
                resultData.Add(totalPageCount, dataListNew);
                return resultData;

            }
        }

        public async Task<int> UpdateInpatientInfoDetail(UpdateInpatientInfoDetail param)
        {
            int count = 0;
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                //update [dbo].[住院费用] set [DataState]=1,update_time=GETDATE(),[UpdateUserId]= where [费用明细ID]='' and [机构编码]=''
                _sqlConnection.Open();
                count = await _sqlConnection.ExecuteAsync("");
                _sqlConnection.Close();
            }

            return count;
        }
        /// <summary>
        /// 获取住院病人
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task GetInpatientInfo(UserInfoDto user, List<InpatientInfoDto> param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {

                _sqlConnection.Open();
                if (param.Any())
                {
                  

                        var outpatientNum = ListToStr(param.Select(c => c.HospitalizationNo).ToList());
                        var businessId = ListToStr(param.Select(c => c.BusinessId).ToList());
                        string strSql =
                            $@"update  [dbo].[inpatient] set  [IsDelete] =1 ,DeleteTime=GETDATE(),DeleteUserId='{user.UserId}' where [IsDelete]=0  and [hospitalizationno] in ({outpatientNum})
                                 and [businessid] in({businessId})"; 
                        var num = await _sqlConnection.ExecuteAsync(strSql, null);
                        string insertSql = "";
                        foreach (var item in param)
                        {
                            string str = $@"
                                INSERT INTO [dbo].[inpatient]
                                           (id,[HospitalName] ,[AdmissionDate]  ,[LeaveHospitalDate] ,[HospitalizationNo] ,[BusinessId] ,[PatientName] ,[IdCardNo]
                                           ,[PatientSex],[Birthday] ,[ContactName],[ContactPhone] ,[FamilyAddress] ,[InDepartmentName] ,[InDepartmentId]
                                           ,[AdmissionDiagnosticDoctor] ,[AdmissionBed] ,[AdmissionMainDiagnosis] ,[AdmissionMainDiagnosisIcd10] ,[AdmissionSecondaryDiagnosis] ,[AdmissionSecondaryDiagnosisIcd10]
                                           ,[AdmissionWard] ,[AdmissionOperator] ,[AdmissionOperateTime] ,[HospitalizationTotalCost] ,[Remark] ,[LeaveDepartmentName] ,[LeaveDepartmentId] 
		                                   ,[LeaveHospitalWard] ,[LeaveHospitalBed]  ,[LeaveHospitalMainDiagnosis] ,[LeaveHospitalMainDiagnosisIcd10] ,[LeaveHospitalSecondaryDiagnosis] ,[LeaveHospitalSecondaryDiagnosisIcd10]
                                           ,[InpatientHospitalState] ,[AdmissionDiagnosticDoctorId] ,[AdmissionBedId]  ,[AdmissionWardId],[LeaveHospitalBedId] ,[LeaveHospitalWardId]
                                           ,[CreateTime]  ,[IsDelete] ,[DeleteTime],OrganizationCode,CreateUserId,FixedEncoding)
                                     VALUES ('{Guid.NewGuid()}','{item.HospitalName}','{item.AdmissionDate}','{item.LeaveHospitalDate}','{item.HospitalizationNo}','{item.BusinessId}','{item.PatientName}','{item.IdCardNo}',
                                             '{item.PatientSex}','{item.Birthday}','{item.ContactName}','{item.ContactPhone}','{item.FamilyAddress}','{item.InDepartmentName}','{item.InDepartmentId}',
                                             '{item.AdmissionDiagnosticDoctor}','{item.AdmissionBed}','{item.AdmissionMainDiagnosis}','{item.AdmissionMainDiagnosisIcd10}','{item.AdmissionSecondaryDiagnosis}','{item.AdmissionSecondaryDiagnosisIcd10}',
                                             '{item.AdmissionWard}','{item.AdmissionOperator}','{item.AdmissionOperateTime}',{Convert.ToDecimal(item.HospitalizationTotalCost)},'{item.Remark}','{item.LeaveDepartmentName}','{item.LeaveDepartmentId}',
                                             '{item.LeaveHospitalWard}','{item.LeaveHospitalBed}','{item.LeaveHospitalMainDiagnosis}','{item.LeaveHospitalMainDiagnosisIcd10}','{item.LeaveHospitalSecondaryDiagnosis}','{item.LeaveHospitalSecondaryDiagnosisIcd10}',
                                             '{item.InpatientHospitalState}','{item.AdmissionDiagnosticDoctorId}','{item.AdmissionBedId}','{item.AdmissionWardId}','{item.LeaveHospitalBedId}','{item.LeaveHospitalWardId}',
                                               GETDATE(),0,null,'{user.OrganizationCode}','{user.UserId}','{BitConverter.ToInt64(Guid.Parse(item.BusinessId).ToByteArray(), 0)}'
                                              );";
                            insertSql += str;
                        }
                        var nums = await _sqlConnection.ExecuteAsync(insertSql, null);
                       
                    
                   
                }
                _sqlConnection.Close();
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

                               FROM [dbo].[住院病人] where IsDelete=0 and 业务ID='{param.BusinessId}' and   OrgCode='{param.InstitutionalNumber}'
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
                string strSql = $"select  COUNT(*) from [dbo].[住院医保信息] where [业务ID]={param} and IsDelete=0";
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
        public async Task SaveMedicalInsurance(UserInfoDto user, MedicalInsuranceDto param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                string insertSql = null;
                if (!string.IsNullOrWhiteSpace(param.MedicalInsuranceHospitalizationNo)==true && param.IsModify == false)
                {
                    insertSql = $@"update [dbo].[MedicalInsurance] set MedicalInsuranceYearBalance={param.MedicalInsuranceYearBalance},
                    MedicalInsuranceHospitalizationNo='{param.MedicalInsuranceHospitalizationNo}',IsDelete=0,InsuranceType='{param.InsuranceType}'
                    where [Id]='{param.Id}'";
                }
                else if (param.IsModify)
                {
                    insertSql = $@"update [dbo].[MedicalInsurance] set [MedicalInsuranceYearBalance]=0,
                    AdmissionInfoJson='{param.AdmissionInfoJson}',[IsDelete]=0,
                    where [Id]='{param.Id}' and OrganizationCode='{user.OrganizationCode}'";
                }
                else
                {
                    insertSql = $@"INSERT INTO [dbo].[MedicalInsurance]([Id],[HisHospitalizationId],[InsuranceNo],[MedicalInsuranceYearBalance]
                               ,[AdmissionInfoJson],[ReimbursementExpenses] ,[SelfPayFee],[OtherInfo] 
		                       ,[CreateTime],[IsDelete] ,OrganizationCode,CreateUserId,OrganizationName,InsuranceType)
                           VALUES('{Guid.NewGuid()}', '{param.HisHospitalizationId}','{param.InsuranceNo}', {param.MedicalInsuranceYearBalance},'{param.AdmissionInfoJson}',
                                 {param.ReimbursementExpenses},{param.SelfPayFee},'{param.OtherInfo}',
                                GETDATE(),1,'{user.OrganizationCode}','{user.UserId}','{user.OrganizationName }',{param.InsuranceType});";
                    insertSql = $"delete [dbo].[MedicalInsurance] where [HisHospitalizationId]='{param.HisHospitalizationId}';" + insertSql;

                }
                var log = LogFactory.GetLogger("ini".GetType().ToString());
                log.Debug(insertSql);
                await _sqlConnection.ExecuteAsync(insertSql);


            }
        }
        public async Task<QueryMedicalInsuranceDto> QueryMedicalInsurance(UserInfoDto user, string businessId)
        {
            var resultData = new QueryMedicalInsuranceDto();
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                string querySql = $@"select a.[Id],a.[AdmissionInfoJson],a.HisHospitalizationId,a.MedicalInsuranceHospitalizationNo,a.InsuranceType from [dbo].[MedicalInsurance] as a
                                inner join [dbo].[inpatient] as b on
                                a.HisHospitalizationId=b.FixedEncoding
                                where a.IsDelete=0 and b.IsDelete=0
                                and b.BusinessId='{businessId}' and a.OrganizationCode='{user.OrganizationCode}'";
                var data = await _sqlConnection.QueryFirstOrDefaultAsync<QueryMedicalInsuranceDto>(querySql);
                if (data != null) resultData = data;
                _sqlConnection.Close();
                return resultData;

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
                        $@"update MedicalInsuranceDataAll set DeleteTime=GETDATE(),DeleteUserId='{param.CreateUserId}' where  DeleteTime is  null and DataId='{param.DataId}' and BusinessId='{param.BusinessId}'";
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
                _sqlConnection.Close();
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
                      ,[DeleteTime]
                      ,[OrgCode]
                      ,[DeleteUserId]
                  FROM [dbo].[MedicalInsuranceDataAll] where DataId='{param.DataId}' and  DataType='{param.DataType}' and OrgCode='{param.OrgCode}' and BusinessId='{param.BusinessId}' and  DeleteTime is  null";
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
        public async Task<Int32> DeleteMedicalInsurance(UserInfoDto user, string param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                int result = 0;
                _sqlConnection.Open();
                if (param.Any())
                {

                    string insertSql = $@"update [dbo].[住院医保信息] set IsDelete=1,DeleteTime=GETDATE(),DeleteUserId='{user.UserId}' where  [业务ID]={param}";
                    result = await _sqlConnection.ExecuteAsync(insertSql);
                }
                return result;
            }
        }
        public async Task<Int32> InformationInfoSave(UserInfoDto user, List<InformationDto> param, InformationParam info)
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

                        var outpatientNum = ListToStr(param.Select(c => c.DirectoryCode).ToList());
                        string strSql =
                            $@"update [dbo].[HospitalGeneralCatalog] set  [IsDelete] =1 ,DeleteTime=GETDATE(),DeleteUserId='{user.UserId}' where [IsDelete]=0 
                                and [DirectoryCode] in(" + outpatientNum + ")";
                        await _sqlConnection.ExecuteAsync(strSql, null, transaction);
                        string insertSql = "";
                        int mund = 0;
                        foreach (var item in param)
                        {
                            mund++;
                            string str = $@"INSERT INTO [dbo].[HospitalGeneralCatalog]
                                   (id,DirectoryType,[OrganizationCode],[DirectoryCode],[DirectoryName]
                                   ,[MnemonicCode],[DirectoryCategoryName],[Remark] ,[CreateTime]
		                            ,[IsDelete],[DeleteTime],CreateUserId)
                             VALUES ('{Guid.NewGuid()}','{info.DirectoryType}','{info.OrganizationCode}','{item.DirectoryCode}','{item.DirectoryName}',
                                     '{item.MnemonicCode}','{item.DirectoryCategoryName}','{item.Remark}',GETDATE(),
                                       0, null,'{user.UserId}');";
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
        /// <summary>
        /// 医保项目下载
        /// </summary>
        /// <param name="user"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<Int32> ProjectDownload(UserInfoDto user, List<ResidentProjectDownloadRowDataRowDto> param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                int result = 0;

                _sqlConnection.Open();
                if (param.Any())
                {
                    var projectCodeList = ListToStr(param.Select(c => c.ProjectCode).ToList());
                    //string deleteSql = "delete [dbo].[MedicalInsuranceProject] where ProjectCode in (" + projectCodeList + ")";
                    //await _sqlConnection.ExecuteAsync(deleteSql);


                    //20191024023636736DateTime.Now.ToString("yyyyMMddhhmmssfff")
                    
                    string insertSql = null;
                    foreach (var item in param)
                    {
                        var projectName = FilteSqlStr(item.ProjectName);
                         insertSql += $@"INSERT INTO [dbo].[MedicalInsuranceProject]
                           (id,[ProjectCode],[ProjectName] ,[ProjectCodeType] ,[ProjectLevel],[WorkersSelfPayProportion]
                           ,[Unit],[MnemonicCode] ,[Formulation],[ResidentSelfPayProportion],[RestrictionSign]
                           ,[ZeroBlock],[OneBlock],[TwoBlock],[ThreeBlock],[FourBlock],[EffectiveSign],[ResidentOutpatientSign]
                           ,[ResidentOutpatientBlock],[Manufacturer] ,[QuasiFontSize] ,[Specification],[Remark],[NewCodeMark]
                           ,[NewUpdateTime],[StartTime] ,[EndTime],[LimitPaymentScope],[CreateTime],[CreateUserId]
                           )
                          VALUES('{Guid.NewGuid()}','{item.ProjectCode}','{projectName}','{item.ProjectCodeType}','{item.ProjectLevel}',{CommonHelp.ValueToDecimal(item.WorkersSelfPayProportion)}
                                  ,'{item.Unit}','{item.MnemonicCode}', '{item.Formulation}',{CommonHelp.ValueToDecimal(item.ResidentSelfPayProportion)},'{item.RestrictionSign}'
                                  ,{CommonHelp.ValueToDecimal(item.ZeroBlock)},{CommonHelp.ValueToDecimal(item.OneBlock)},{CommonHelp.ValueToDecimal(item.TwoBlock)},{CommonHelp.ValueToDecimal(item.ThreeBlock)},{CommonHelp.ValueToDecimal(item.FourBlock)},'{item.EffectiveSign}','{item.ResidentOutpatientSign}'
                                  ,{CommonHelp.ValueToDecimal(item.ResidentOutpatientBlock)},'{item.Manufacturer}','{item.QuasiFontSize}','{item.Specification}','{item.Remark}','{item.NewCodeMark}'
                                  ,'{ DateTime.ParseExact(item.NewUpdateTime, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyy-MM-dd HH:mm:ss")}',
                                  NULL,NULL,'{item.LimitPaymentScope}',GETDATE(),'{user.UserId}'
                               );";
                    }
                    result = await _sqlConnection.ExecuteAsync(insertSql);
                }
                return result;
            }
        }
        /// <summary>
        /// 获取更新最新时间
        /// </summary>
        /// <param name="user"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<Int32> ProjectDownloadTimeMax(UserInfoDto user, List<ResidentProjectDownloadRowDataRowDto> param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                int result = 0;
                _sqlConnection.Open();
                if (param.Any())
                {
                    string insertSql = null;
                    //foreach (var item in param)
                    //{
                    //    insertSql += $@"INSERT INTO [dbo].[medical_insurance_project]
                    //       (id,[project_code],[project_name] ,[project_code_type] ,[project_level],[workers_self_pay_proportion]
                    //       ,[unit],[mnemonic_code] ,[formulation],[resident_self_pay_proportion],[restriction_sign]
                    //       ,[zero_block],[one_block],[two_block],[three_block],[four_block],[effective_sign],[resident_outpatient_sign]
                    //       ,[resident_outpatient_block],[manufacturer] ,[quasi_font_size] ,[specification],[remark],[new_code_mark]
                    //       ,[new_update_time],[start_time] ,[end_time],[limit_payment_scope],[create_time],[CreateUserId]
                    //       )
                    //      VALUES('{Guid.NewGuid()}','{item.AKE001}','{item.AKE002}','{item.AKA063}','{item.AKA065}','{item.AKA069}'
                    //              ,'{item.AKA067}','{item.AKA020}', '{item.AKA070}','{item.CKE899}','{item.AKA036}'
                    //              ,'{item.CKA599}','{item.CKA578}','{item.CKA579}','{item.CKA580}','{item.CKA560}','{item.AAE100}','{item.CKE889}'
                    //              ,'{item.CKA601}','{item.AKA098}','{item.CKA603}','{item.AKA074}','{item.AAE013}','{item.CKE897}'
                    //              ,'{item.AAE036}','{item.AAE030}','{item.AAE031}','{item.CKE599}',GETDATE(),'{user.职员ID}'
                    //           );";
                    //}


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
        /// <summary>  
        /// 根据GUID获取19位的唯一数字序列  
        /// </summary>  
        /// <returns></returns>  
        private long GuidToLongID()
        {
            //byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        }

        private decimal GetBlockPrice(QueryMedicalInsurancePairCodeDto param, OrganizationGrade grade)
        {
            decimal resultData = 0;
            if (grade == OrganizationGrade.二级乙等以下) resultData = param.ZeroBlock;
            if (grade == OrganizationGrade.二级乙等) resultData = param.OneBlock;
            if (grade == OrganizationGrade.二级甲等) resultData = param.TwoBlock;
            if (grade == OrganizationGrade.三级乙等) resultData = param.ThreeBlock;
            if (grade == OrganizationGrade.三级甲等) resultData = param.FourBlock;

            return resultData;
        }

        private decimal GetSelfPayProportion(QueryMedicalInsurancePairCodeDto param, MedicalInsuranceResidentInfoDto residentInfo)
        {
            decimal resultData = 0;
            //居民
            if (residentInfo.InsuranceType == "342") resultData = param.ResidentSelfPayProportion;
            //职工
            if (residentInfo.InsuranceType == "310") resultData = param.WorkersSelfPayProportion;
            return resultData;
        }
        public  string FilteSqlStr(string Str)
        {
           
            Str = Str.Replace("'", "");
            Str = Str.Replace("\"", "");
            Str = Str.Replace("&", "&amp");
            Str = Str.Replace("<", "&lt");
            Str = Str.Replace(">", "&gt");

            Str = Str.Replace("delete", "");
            Str = Str.Replace("update", "");
            Str = Str.Replace("insert", "");

            return Str;
        }

        
    }
}
