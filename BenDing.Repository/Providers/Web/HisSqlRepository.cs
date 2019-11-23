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
    public class HisSqlRepository : IHisSqlRepository
    {
        private IMedicalInsuranceSqlRepository _baseSqlServerRepository;
        private ISystemManageRepository _iSystemManageRepository;
        private string _connectionString;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="conStr"></param>   
        public HisSqlRepository(IMedicalInsuranceSqlRepository iBaseSqlServerRepository, ISystemManageRepository ImanageRepository)
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
        public async Task<Dictionary<int,List<QueryCatalogDto>>> QueryCatalog(QueryCatalogUiParam param)
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
                    var ids = CommonHelp.ListToStr(catalogDtoIdList);
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
                if (string.IsNullOrWhiteSpace(param.DiseaseCoding))
                {
                    strSql += $" and DiseaseCoding = '{param.DiseaseCoding}'";
                }
                else
                {
                    if (Regex.IsMatch(param.Search, regexstr))
                    {
                        strSql += " and DiseaseName like '" + param.Search + "%'";
                    }
                    else
                    {
                        strSql += " and MnemonicCode like '" + param.Search + "%'";
                    }
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
                        var ywId = CommonHelp.ListToStr(param.Select(c => c.业务ID).ToList());
                        var outpatientNum = CommonHelp.ListToStr(param.Select(c => c.门诊号).ToList());
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

                        var outpatientNum = CommonHelp.ListToStr(param.Select(c => c.门诊号).ToList());
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
        public async Task SaveInpatientInfoDetail(UserInfoDto user, List<InpatientInfoDetailDto> param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {

                _sqlConnection.Open();

                if (param.Any())
                {

                    try
                    {

                        var outpatientNum = CommonHelp.ListToStr(param.Select(c => c.CostDetailId).ToList());
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
                               ,[CostWriteOffId],[OrganizationCode],[OrganizationName] ,[CreateTime] ,[IsDelete],[DeleteTime],CreateUserId
                               ,DataSort,UploadMark,RecipeCodeFixedEncoding,BillDoctorIdFixedEncoding,BusinessTime)
                           VALUES('{Guid.NewGuid()}','{item.HospitalizationNo}','{item.CostDetailId}','{item.CostItemName}','{item.CostItemCode}','{item.CostItemCategoryName}','{item.CostItemCategoryCode}'
                                 ,'{item.Unit}','{item.Formulation}','{item.Specification}',{item.UnitPrice},{item.Quantity},{item.Amount},'{item.Dosage}','{item.Usage}','{item.MedicateDays}',
                                 '{item.HospitalPricingUnit}','{item.IsImportedDrugs}','{item.DrugProducingArea}','{item.RecipeCode}','{item.CostDocumentType}','{item.BillDepartment}'
                                 ,'{item.BillDepartmentId}','{item.BillDoctorName}','{item.BillDoctorId}','{item.BillTime}','{item.OperateDepartmentName}','{item.OperateDepartmentId}'
                                 ,'{item.OperateDoctorName}','{item.OperateDoctorId}','{item.OperateTime}','{item.PrescriptionDoctor}','{item.Operators}','{item.PracticeDoctorNumber}'
                                 ,'{item.CostWriteOffId}','{item.OrganizationCode}','{item.OrganizationName}',GETDATE(),0,null,'{user.UserId}'
                                 ,{sort},0,'{CommonHelp.GuidToStr(item.RecipeCode)}','{CommonHelp.GuidToStr(item.BillDoctorId)}','{businessTime}'
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
                                SelfPayProportion = (itemPairCode != null && residentInfoData != null) ? GetSelfPayProportion(itemPairCode, residentInfoData) : 0,
                                UploadAmount = c.UploadAmount
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
        /// <summary>
        /// 保存住院病人
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task SaveInpatientInfo(UserInfoDto user, InpatientInfoDto param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {

                _sqlConnection.Open();
                string strSql =
                    $@"update  [dbo].[inpatient] set  [IsDelete] =1 ,DeleteTime=GETDATE(),DeleteUserId='{user.UserId}' where [IsDelete]=0  and [businessid] ='{param.BusinessId}';";
                string leaveHospitalDate = null;
                if (!string.IsNullOrWhiteSpace(param.LeaveHospitalDate))
                {
                    if (param.LeaveHospitalDate != "1900-01-01 00:00:00.000")
                    {
                        leaveHospitalDate = param.LeaveHospitalDate;
                    }
                }
                string insertSql = $@"
                                INSERT INTO [dbo].[inpatient]
                                           (id,[HospitalName] ,[AdmissionDate]  ,[LeaveHospitalDate] ,[HospitalizationNo] ,[BusinessId] ,[PatientName] ,[IdCardNo]
                                           ,[PatientSex],[Birthday] ,[ContactName],[ContactPhone] ,[FamilyAddress] ,[InDepartmentName] ,[InDepartmentId]
                                           ,[AdmissionDiagnosticDoctor] ,[AdmissionBed] ,[AdmissionMainDiagnosis] ,[AdmissionMainDiagnosisIcd10] ,[AdmissionSecondaryDiagnosis] ,[AdmissionSecondaryDiagnosisIcd10]
                                           ,[AdmissionWard] ,[AdmissionOperator] ,[AdmissionOperateTime] ,[HospitalizationTotalCost] ,[Remark] ,[LeaveDepartmentName] ,[LeaveDepartmentId] 
		                                   ,[LeaveHospitalWard] ,[LeaveHospitalBed]  ,[LeaveHospitalMainDiagnosis] ,[LeaveHospitalMainDiagnosisIcd10] ,[LeaveHospitalSecondaryDiagnosis] ,[LeaveHospitalSecondaryDiagnosisIcd10]
                                           ,[InpatientHospitalState] ,[AdmissionDiagnosticDoctorId] ,[AdmissionBedId]  ,[AdmissionWardId],[LeaveHospitalBedId] ,[LeaveHospitalWardId]
                                           ,[CreateTime]  ,[IsDelete] ,[DeleteTime],OrganizationCode,CreateUserId,FixedEncoding)
                                     VALUES ('{Guid.NewGuid()}','{param.HospitalName}','{param.AdmissionDate}','{leaveHospitalDate}','{param.HospitalizationNo}','{param.BusinessId}','{param.PatientName}','{param.IdCardNo}',
                                             '{param.PatientSex}','{param.Birthday}','{param.ContactName}','{param.ContactPhone}','{param.FamilyAddress}','{param.InDepartmentName}','{param.InDepartmentId}',
                                             '{param.AdmissionDiagnosticDoctor}','{param.AdmissionBed}','{param.AdmissionMainDiagnosis}','{param.AdmissionMainDiagnosisIcd10}','{param.AdmissionSecondaryDiagnosis}','{param.AdmissionSecondaryDiagnosisIcd10}',
                                             '{param.AdmissionWard}','{param.AdmissionOperator}','{param.AdmissionOperateTime}',{Convert.ToDecimal(param.HospitalizationTotalCost)},'{param.Remark}','{param.LeaveDepartmentName}','{param.LeaveDepartmentId}',
                                             '{param.LeaveHospitalWard}','{param.LeaveHospitalBed}','{param.LeaveHospitalMainDiagnosis}','{param.LeaveHospitalMainDiagnosisIcd10}','{param.LeaveHospitalSecondaryDiagnosis}','{param.LeaveHospitalSecondaryDiagnosisIcd10}',
                                             '{param.InpatientHospitalState}','{param.AdmissionDiagnosticDoctorId}','{param.AdmissionBedId}','{param.AdmissionWardId}','{param.LeaveHospitalBedId}','{param.LeaveHospitalWardId}',
                                               GETDATE(),0,null,'{user.OrganizationCode}','{user.UserId}','{BitConverter.ToInt64(Guid.Parse(param.BusinessId).ToByteArray(), 0)}'
                                              );";
                insertSql = insertSql + strSql;
                var nums = await _sqlConnection.ExecuteAsync(insertSql, null);
                _sqlConnection.Close();
            }



        }
        /// <summary>
        /// 住院病人查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<QueryInpatientInfoDto> QueryInpatientInfo(QueryInpatientInfoParam param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                var resultData = new QueryInpatientInfoDto();
                _sqlConnection.Open();
                string strSql = $"select top 1 * from [dbo].[Inpatient] where IsDelete=0 and BusinessId='{param.BusinessId}'";

                var data = await _sqlConnection.QueryAsync<QueryInpatientInfoDto>(strSql, null);
                if (data != null && data.Any()) resultData = data.FirstOrDefault();
                _sqlConnection.Close();
                return resultData;
            }



        }
        /// <summary>
        /// 获取所有未传费用的住院病人
        /// </summary>
        /// <returns></returns>
        public async Task<List<QueryAllHospitalizationPatientsDto>> QueryAllHospitalizationPatients(PrescriptionUploadAutomaticParam param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                var resultData = new List<QueryAllHospitalizationPatientsDto>();
                _sqlConnection.Open();
                string strSql = null;
                if (param.IsTodayUpload == true)
                {
                    string day = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000";
                    strSql = $@"
                            select a.OrganizationCode,a.HospitalizationNo,a.BusinessId,b.InsuranceType from [dbo].[Inpatient]  as a 
                            inner join [dbo].[MedicalInsurance] as b on b.HisHospitalizationId=a.FixedEncoding
                            where a.IsDelete=0 and b.IsDelete=0 and a.HospitalizationNo 
                            in(select HospitalizationNo from [dbo].[HospitalizationFee] where BusinessTime='{day}' and  Isdelete=0 and UploadMark=0 Group by HospitalizationNo)";
                }
                else
                {
                    strSql = $@"
                                select a.OrganizationCode,a.HospitalizationNo,a.BusinessId,b.InsuranceType from [dbo].[Inpatient]  as a 
                                inner join [dbo].[MedicalInsurance] as b on b.HisHospitalizationId=a.FixedEncoding
                                where a.IsDelete=0 and b.IsDelete=0 and a.HospitalizationNo 
                                in(select HospitalizationNo from [dbo].[HospitalizationFee] where Isdelete=0 and UploadMark=0 Group by HospitalizationNo) ";
                }
                var data = await _sqlConnection.QueryAsync<QueryAllHospitalizationPatientsDto>(strSql);

                if (data != null && data.Any())
                {
                    resultData = data.ToList();
                }
                _sqlConnection.Close();
                return resultData;
            }


        }
 
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
        /// <summary>
        /// 保存HIS系统中科室、医师、病区、床位的基本信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="param"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task<Int32> SaveInformationInfo(UserInfoDto user, List<InformationDto> param, InformationParam info)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                Int32 counts = 0;
                _sqlConnection.Open();
                if (param.Any())
                {
                    var outpatientNum = CommonHelp.ListToStr(param.Select(c => c.DirectoryCode).ToList());
                    string strSql =
                        $@"update [dbo].[HospitalGeneralCatalog] set  [IsDelete] =1 ,DeleteTime=GETDATE(),DeleteUserId='{user.UserId}' where [IsDelete]=0 
                                and [DirectoryCode] in(" + outpatientNum + ")";
                    await _sqlConnection.ExecuteAsync(strSql, null);
                    string insertSql = "";
                    int num = 0;
                    foreach (var item in param)
                    {
                        num++;
                        string str = $@"INSERT INTO [dbo].[HospitalGeneralCatalog]
                                   (id,DirectoryType,[OrganizationCode],[DirectoryCode],[DirectoryName]
                                   ,[MnemonicCode],[DirectoryCategoryName],[Remark] ,[CreateTime]
		                            ,[IsDelete],[DeleteTime],CreateUserId,FixedEncoding)
                             VALUES ('{Guid.NewGuid()}','{info.DirectoryType}','{info.OrganizationCode}','{item.DirectoryCode}','{item.DirectoryName}',
                                     '{item.MnemonicCode}','{item.DirectoryCategoryName}','{item.Remark}',GETDATE(),
                                       0, null,'{user.UserId}','{CommonHelp.GuidToStr(item.DirectoryCode)}');";
                        insertSql += str;

                    }

                    counts = await _sqlConnection.ExecuteAsync(strSql + insertSql, null);

                    _sqlConnection.Close();
                }

                return counts;
            }
        }
        /// <summary>
        /// 查询HIS系统中科室、医师、病区、床位的基本信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<List<QueryInformationInfoDto>> QueryInformationInfo(InformationParam param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                var resultData = new List<QueryInformationInfoDto>();
                _sqlConnection.Open();
                string sqlStr = @"select Id, [OrganizationCode],[DirectoryType], [DirectoryCode],[DirectoryName],[FixedEncoding],
                                [DirectoryCategoryName],[Remark] from [dbo].[HospitalGeneralCatalog] where IsDelete=0 ";
                if (!string.IsNullOrWhiteSpace(param.DirectoryName)) sqlStr += " and DirectoryName like '" + param.DirectoryName + "%'";
                if (!string.IsNullOrWhiteSpace(param.DirectoryType)) sqlStr += $" and DirectoryType='{param.DirectoryType}'";
                     var data = await _sqlConnection.QueryAsync<QueryInformationInfoDto>(sqlStr);
                if (data != null) resultData = data.ToList();
                _sqlConnection.Close();

                return resultData;
            }
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
      
    }
}
