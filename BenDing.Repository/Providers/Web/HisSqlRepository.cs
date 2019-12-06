using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using BenDing.Domain.Models.Dto.Resident;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Enums;
using BenDing.Domain.Models.Params;
using BenDing.Domain.Models.Params.Base;
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
        private readonly IMedicalInsuranceSqlRepository _baseSqlServerRepository;
        private readonly ISystemManageRepository _iSystemManageRepository;
        private readonly string _connectionString;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iBaseSqlServerRepository"></param>
        /// <param name="IsystemManageRepository"></param>

        public HisSqlRepository(IMedicalInsuranceSqlRepository iBaseSqlServerRepository, ISystemManageRepository IsystemManageRepository)
        {
            _baseSqlServerRepository = iBaseSqlServerRepository;
            _iSystemManageRepository = IsystemManageRepository;
            string conStr = ConfigurationManager.ConnectionStrings["NFineDbContext"].ToString();
            _connectionString = !string.IsNullOrWhiteSpace(conStr) ? conStr : throw new ArgumentNullException(nameof(conStr));

        }
        /// <summary>
        /// 更新医疗机构
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public void ChangeOrg(UserInfoDto userInfo, List<OrgDto> param)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {

                sqlConnection.Open();
                if (param.Any())
                {
                    IDbTransaction transaction = sqlConnection.BeginTransaction();
                    try
                    {

                        string strSql = $"update [dbo].[HospitalOrganization] set DeleteTime=getDate(),IsDelete=1,DeleteUserId='{userInfo.UserId}'";

                        if (param.Any())
                        {

                            sqlConnection.Execute(strSql, null, transaction);

                            string insterCount = null;
                            foreach (var itmes in param)
                            {
                                string insterSql = $@"
                                insert into [dbo].[HospitalOrganization](id,HospitalId,HospitalName,HospitalAddr,ContactPhone,PostalCode,ContactPerson,CreateTime,IsDelete,DeleteTime,CreateUserId)
                                values('{Guid.NewGuid()}','{itmes.Id}','{itmes.HospitalName}','{itmes.HospitalAddr}','{itmes.ContactPhone}','{itmes.PostalCode}','{itmes.ContactPerson}',
                                    getDate(),0,null,'{userInfo.UserId}');";
                                insterCount += insterSql;
                            }
                            sqlConnection.Execute(insterCount, null, transaction);
                            transaction.Commit();

                        }

                    }
                    catch (Exception exception)
                    {

                        transaction.Rollback();
                        throw new Exception(exception.Message);

                    }
                }

                sqlConnection.Close();


            }


        }
        /// <summary>
        /// 添加三大目录
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="param"></param>
        /// <param name="type"></param>
        public void AddCatalog(UserInfoDto userInfo, List<CatalogDto> param, CatalogTypeEnum type)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
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
                                   '{itmes.ManufacturerName}','{itmes.Remark}', '{itmes.DirectoryCreateTime}',getDate(),0,'{userInfo.UserId}','{ BitConverter.ToInt64(Guid.Parse(itmes.DirectoryCode).ToByteArray(), 0)}');";
                            insterCount += insterSql;
                        }
                        sqlConnection.Execute(insterCount);

                    }
                    sqlConnection.Close();
                }
                catch (Exception e)
                {

                    sqlConnection.Close();
                    throw new Exception(e.Message);
                }

            }


        }
        /// <summary>
        /// 基层端三大目录查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Dictionary<int, List<QueryCatalogDto>> QueryCatalog(QueryCatalogUiParam param)
        {
            List<QueryCatalogDto> dataList;
            var resultData = new Dictionary<int, List<QueryCatalogDto>>();
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
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

                var result = sqlConnection.QueryMultiple(executeSql);

                int totalPageCount = result.Read<int>().FirstOrDefault();
                dataList = (from t in result.Read<QueryCatalogDto>()

                            select t).ToList();

                resultData.Add(totalPageCount, dataList);
                sqlConnection.Close();

            }


            return resultData;

        }
        /// <summary>
        /// 删除三大目录
        /// </summary>
        /// <param name="user"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int DeleteCatalog(UserInfoDto user, int param)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string strSql = $" update [dbo].[HospitalThreeCatalogue] set IsDelete=1 ,DeleteUserId='{user.UserId}',DeleteTime=getDate()  where DirectoryCategoryCode='{param.ToString()}'";
                var num = sqlConnection.Execute(strSql);
                sqlConnection.Close();
                return num;
            }
        }
        /// <summary>
        /// 获取三大项目最新时间
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public string GetTime(int num)
        {
            string result;
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string strSql = $"select MAX(DirectoryCreateTime) from [dbo].[HospitalThreeCatalogue] where IsDelete=0 and DirectoryCategoryCode={num} ";
                var timeMax = sqlConnection.QueryFirst<string>(strSql);

                result = timeMax;
                sqlConnection.Close();
            }

            return result;

        }
        /// <summary>
        /// ICD10获取最新时间
        /// </summary>
        /// <returns></returns>
        public string GetICD10Time()
        {
            string result;
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string strSql = $"select MAX(CreateTime) from [dbo].[ICD10] where IsDelete=0 ";
                var timeMax = sqlConnection.QueryFirst<string>(strSql);

                result = timeMax;
                sqlConnection.Close();
            }

            return result;

        }
        /// <summary>
        /// 添加ICD10
        /// </summary>
        /// <param name="param"></param>
        /// <param name="user"></param>
        public void AddICD10(List<ICD10InfoDto> param, UserInfoDto user)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                //排除已有项目

                sqlConnection.Open();
                if (param.Any())
                {
                    //获取唯一编码
                    var catalogDtoIdList = param.Select(c => c.DiseaseId).ToList();
                    var ids = CommonHelp.ListToStr(catalogDtoIdList);
                    string sqlStr = $"select DiseaseCoding  from [dbo].[ICD10]  where DiseaseCoding  in({ids}) and IsDelete=0";
                    var idListNew = sqlConnection.Query<string>(sqlStr);
                    //排除已有项目 
                    var listNew = idListNew as string[] ?? idListNew.ToArray();
                    var paramNew = listNew.Any() ? param.Where(c => !listNew.Contains(c.DiseaseId)).ToList()
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
                                        getDate(),'{user.UserId}',0);";

                            insterCount += insterSql;
                        }
                        sqlConnection.Execute(insterCount);
                    }



                }


                sqlConnection.Close();


            }
        }
        /// <summary>
        /// ICD10查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<QueryICD10InfoDto> QueryICD10(QueryICD10UiParam param)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                //排除已有项目
                var resultData = new List<QueryICD10InfoDto>();
                sqlConnection.Open();

                string strSql = $"select  [id],[DiseaseCoding],[DiseaseName] ,[MnemonicCode],[Remark] ,DiseaseId from [dbo].[ICD10]  where IsDelete=0";
                string regexstr = @"[\u4e00-\u9fa5]";
                if (!string.IsNullOrWhiteSpace(param.DiseaseCoding))
                {
                    strSql += $" and DiseaseCoding ='{param.DiseaseCoding}'";
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



                var data = sqlConnection.Query<QueryICD10InfoDto>(strSql);

                if (data != null && data.Any())
                {
                    resultData = data.ToList();
                }


                sqlConnection.Close();

                return resultData;


            }
        }
        /// <summary>
        /// 保存门诊病人信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="param"></param>
        public void SaveOutpatient(UserInfoDto user, BaseOutpatientInfoDto param)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string sqlStr = $@"update [dbo].[outpatient] set  [IsDelete] =1 ,DeleteTime=getDate(),DeleteUserId='{user.UserId}' where [IsDelete]=0 and [businessid]='{param.BusinessId}';
                   INSERT INTO [dbo].[outpatient](
                   Id,[PatientName],[IdCardNo],[PatientSex],[BusinessId],[OutpatientNumber],[VisitDate]
                   ,[DepartmentId],[DepartmentName],[DiagnosticDoctor],[DiagnosticDiseaseCode],[DiagnosticDiseaseName]
                   ,[DiseaseDesc],[Operator] ,[MedicalTreatmentTotalCost],[Remark],[ReceptionStatus]
                   ,[CreateTime],[IsDelete],[DeleteTime],OrganizationCode,OrganizationName,CreateUserId)
                   VALUES('{Guid.NewGuid()}','{param.PatientName}','{param.IdCardNo}','{param.PatientSex}','{param.BusinessId}','{param.OutpatientNumber}','{param.VisitDate}'
                         ,'{param.DepartmentId}','{param.DepartmentName}','{param.DiagnosticDoctor}','{param.DiagnosticDiseaseCode}','{param.DiagnosticDiseaseName}'
                         ,'{param.DiseaseDesc}','{param.Operator}','{param.MedicalTreatmentTotalCost}','{param.Remark}','{param.ReceptionStatus}'
                         ,getDate(),0,null,'{user.OrganizationCode}','{user.OrganizationName}','{user.UserId}'
                    );";
                sqlConnection.Execute(sqlStr);
                sqlConnection.Close();
            }


        }
        /// <summary>
        /// 查询门诊病人信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public QueryOutpatientDto QueryOutpatient(QueryOutpatientParam param)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {

                sqlConnection.Open();
                string strSql = $"select top 1 * from [dbo].[Outpatient] where IsDelete=0 and BusinessId='{param.BusinessId}'";
                var data = sqlConnection.QueryFirstOrDefault<QueryOutpatientDto>(strSql);
                sqlConnection.Close();
                return data;
            }



        }
        /// <summary>
        /// 保存门诊病人明细
        /// </summary>
        /// <param name="user"></param>
        /// <param name="param"></param>
        public void SaveOutpatientDetail(UserInfoDto user, List<BaseOutpatientDetailDto> param)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {

                sqlConnection.Open();

                if (param.Any())
                {

                    var outpatientNum = CommonHelp.ListToStr(param.Select(c => c.CostDetailId).ToList());
                    var paramFirst = param.FirstOrDefault();
                    if (paramFirst != null)
                    {
                        string strSql =
                            $@" select [CostDetailId],[DataSort] from [dbo].[OutpatientFee] where [OutpatientNo]='{paramFirst.OutpatientNo}'
                                 and [CostDetailId] in({outpatientNum})";
                        var data = sqlConnection.Query<InpatientInfoDetailQueryDto>(strSql);
                        int sort = 0;
                        List<BaseOutpatientDetailDto> paramNew;
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
                            string str = $@"INSERT INTO [dbo].[OutpatientFee](
                               id,[OutpatientNo] ,[CostDetailId] ,[DirectoryName],[DirectoryCode] ,[DirectoryCategoryName] ,[DirectoryCategoryCode]
                               ,[Unit] ,[Formulation] ,[Specification] ,[UnitPrice],[Quantity],[Amount] ,[Dosage] ,[Usage] ,[MedicateDays]
		                       ,[HospitalPricingUnit] ,[IsImportedDrugs] ,[DrugProducingArea] ,[RecipeCode]  ,[CostDocumentType] ,[BillDepartment]
			                   ,[BillDepartmentId] ,[BillDoctorName],[BillDoctorId] ,[BillTime] ,[OperateDepartmentName],[OperateDepartmentId]
                               ,[OperateDoctorName] ,[OperateDoctorId],[OperateTime] ,[PrescriptionDoctor] ,[Operators],[PracticeDoctorNumber]
                               ,[CostWriteOffId],[OrganizationCode],[OrganizationName] ,[CreateTime] ,[IsDelete],[DeleteTime],CreateUserId
                               ,DataSort,UploadMark,RecipeCodeFixedEncoding,BillDoctorIdFixedEncoding,BusinessTime)
                           VALUES('{Guid.NewGuid()}','{item.OutpatientNo}','{item.CostDetailId}','{item.DirectoryName}','{item.DirectoryCode}','{item.DirectoryCategoryName}','{item.DirectoryCategoryCode}'
                                 ,'{item.Unit}','{item.Formulation}','{item.Specification}',{item.UnitPrice},{item.Quantity},{item.Amount},'{item.Dosage}','{item.Usage}','{item.MedicateDays}',
                                 '{item.HospitalPricingUnit}','{item.IsImportedDrugs}','{item.DrugProducingArea}','{item.RecipeCode}','{item.CostDocumentType}','{item.BillDepartment}'
                                 ,'{item.BillDepartmentId}','{item.BillDoctorName}','{item.BillDoctorId}','{item.BillTime}','{item.OperateDepartmentName}','{item.OperateDepartmentId}'
                                 ,'{item.OperateDoctorName}','{item.OperateDoctorId}','{item.OperateTime}','{item.PrescriptionDoctor}','{item.Operators}','{item.PracticeDoctorNumber}'
                                 ,'{item.CostWriteOffId}','{item.OrganizationCode}','{item.OrganizationName}',getDate(),0,null,'{user.UserId}'
                                 ,{sort},0,'{CommonHelp.GuidToStr(item.RecipeCode)}','{CommonHelp.GuidToStr(item.BillDoctorId)}','{businessTime}'
                                 );";
                            insertSql += str;
                        }

                        if (paramNew.Count > 0)
                        {
                            sqlConnection.Execute(insertSql);


                        }
                    }
                }
                sqlConnection.Close();

            }
        }

        /// <summary>
        /// 保存住院病人明细
        /// </summary>
        /// <param name="user"></param>
        /// <param name="param"></param>
        public void SaveInpatientInfoDetail(SaveInpatientInfoDetailParam param)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {

                sqlConnection.Open();

                if (param.DataList.Any())
                {
                    string insertSql = "";
                    try
                    {

                        var outpatientNum = CommonHelp.ListToStr(param.DataList.Select(c => c.DetailId).ToList());
                        var paramFirst = param.DataList.FirstOrDefault();
                        if (paramFirst != null)
                        {
                            string strSql =
                                $@" select [DetailId],[DataSort] from [dbo].[HospitalizationFee] where [HospitalizationId]='{param.HospitalizationId}'
                                 and [DetailId] in({outpatientNum})";
                            var data = sqlConnection.Query<InpatientInfoDetailQueryDto>(strSql);
                            int sort = 0;
                            List<InpatientInfoDetailDto> paramNew;
                            if (data.Any())
                            {    //获取最大排序号
                                sort = data.Select(c => c.DataSort).Max();
                                var costDetailIdList = data.Select(c => c.CostDetailId).ToList();
                                //排除已包含的明细id
                                paramNew = param.DataList.Where(c => !costDetailIdList.Contains(c.DetailId)).ToList();
                            }
                            else
                            {
                                paramNew = param.DataList.OrderBy(d => d.OperateTime).ToList();
                            }

                          

                            foreach (var item in paramNew)
                            {
                               
                                sort++;
                                var businessTime = item.OperateTime.Substring(0, 10) + " 00:00:00.000";
                                string str = $@"INSERT INTO [dbo].[HospitalizationFee]
                                           ([Id],[HospitalizationId],[DetailId],[DocumentNo],[BillDepartment] ,[DirectoryName],[DirectoryCode]
                                           ,[ProjectCode] ,[Formulation],[Specification],[UnitPrice],[Usage] ,[Quantity],[Amount],[DocumentType]
                                           ,[BillDepartmentId] ,[BillDoctorId] ,[BillDoctorName] ,[Dosage] ,[Unit]
                                           ,[OperateDepartmentName],[OperateDepartmentId],[OperateDoctorName],[OperateDoctorId] ,[DoorEmergencyFeeMark]
                                           ,[HospitalAuditMark],[OperateTime],[OutHospitalInspectMark] ,[OrganizationCode] ,[OrganizationName]
                                           ,[UploadMark] ,[DataSort] ,[AdjustmentDifferenceValue],[BusinessTime],[CreateTime],[CreateUserId])
                                           VALUES('{Guid.NewGuid()}','{param.HospitalizationId}','{item.DetailId}','{item.DocumentNo}','{item.BillDepartment}','{item.DirectoryName}','{item.DirectoryCode}',
                                                  '{item.ProjectCode}','{item.Formulation}','{item.Specification}',{item.UnitPrice},'{item.Usage}',{item.Quantity},{item.Amount},'{item.DocumentType}',
                                                  '{item.BillDepartmentId}','{item.BillDoctorId}','{item.BillDoctorName}','{item.Dosage}','{item.Unit}',
                                                  '{item.OperateDepartmentName}','{item.OperateDepartmentId}','{item.OperateDoctorName}','{item.OperateDoctorId}','{item.DoorEmergencyFeeMark}',
                                                  '{item.HospitalAuditMark}','{item.OperateTime}','{item.OutHospitalInspectMark}','{param.User.OrganizationCode}','{param.User.OrganizationName}',
                                                  0,{sort},0,'{businessTime}',getDate(),'{param.User.UserId}');";
                                insertSql += str;
                            }

                            if (paramNew.Count > 0)
                            {
                                sqlConnection.Execute(insertSql);


                            }
                        }
                    }
                    catch (Exception exception)
                    {
                       
                        var log = LogFactory.GetLogger("ini".GetType().ToString());
                        log.Debug(insertSql);
                        throw new Exception(exception.Message);
                    }
                }
                sqlConnection.Close();

            }
        }
        /// <summary>
        /// 住院病人明细查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<QueryInpatientInfoDetailDto> InpatientInfoDetailQuery(InpatientInfoDetailQueryParam param)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var resultData = new List<QueryInpatientInfoDetailDto>();
                sqlConnection.Open();

                string strSql = @"select (select  top 1 a.BusinessId from [dbo].[Inpatient] as a where a.HospitalizationNo=HospitalizationNo and IsDelete=0) as BusinessId,
                                  * from [dbo].[HospitalizationFee]  where  IsDelete=0 ";
                if (param.IdList != null && param.IdList.Any())
                {
                    var idlist = CommonHelp.ListToStr(param.IdList);
                    strSql += $@" and Id in({idlist})";
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(param.BusinessId))
                    {
                        strSql +=
                            $@" and HospitalizationNo =(select top 1 HospitalizationNo from [dbo].[Inpatient] where BusinessId='{param.BusinessId}' and IsDelete=0)";
                    }
                    else
                    {
                        throw new Exception("业务号不能为空!!!");
                    }
                }
                var data = sqlConnection.Query<QueryInpatientInfoDetailDto>(strSql);

                if (data != null && data.Any())
                {
                    resultData = data.ToList();
                }

                sqlConnection.Close();
                return resultData;
            }
        }
        /// <summary>
        /// 住院清单查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Dictionary<int, List<QueryHospitalizationFeeDto>> QueryHospitalizationFee(QueryHospitalizationFeeUiParam param)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                List<QueryHospitalizationFeeDto> dataList;
                var dataListNew = new List<QueryHospitalizationFeeDto>();
                var resultData = new Dictionary<int, List<QueryHospitalizationFeeDto>>();

                sqlConnection.Open();

                string querySql = $@"
                             select * from [dbo].[HospitalizationFee] 
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
                var result = sqlConnection.QueryMultiple(executeSql);
                int totalPageCount = result.Read<int>().FirstOrDefault();
                dataList = (from t in result.Read<QueryHospitalizationFeeDto>()
                            select new QueryHospitalizationFeeDto
                            {
                                Id = t.Id,
                                Amount = t.Amount,
                                BillTime = t.BillTime,
                                BillDepartment = t.BillDepartment,
                                DirectoryCode = t.DirectoryCode,
                                DirectoryName = t.DirectoryName,
                                DirectoryCategoryCode = t.DirectoryCategoryCode != null
                                            ? ((CatalogTypeEnum)Convert.ToInt32(t.DirectoryCategoryCode)).ToString()
                                            : t.DirectoryCategoryCode,
                                UnitPrice = t.UnitPrice,
                                UploadUserName = t.UploadUserName,
                                Quantity = t.Quantity,
                                RecipeCode = t.RecipeCode,
                                Specification = t.Specification,
                                OperateDoctorName = t.OperateDoctorName,
                                UploadMark = t.UploadMark,
                                AdjustmentDifferenceValue = t.AdjustmentDifferenceValue,
                                UploadAmount = t.UploadAmount,
                                UploadTime = t.UploadTime,
                                OrganizationCode = t.OrganizationCode
                            }
                             ).ToList();
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
                    var pairCodeData = _baseSqlServerRepository.QueryMedicalInsurancePairCode(queryPairCodeParam);
                    //获取医院登记
                    var gradeData = _iSystemManageRepository.QueryHospitalOrganizationGrade(organizationCode);
                    //获取入院病人登记信息
                    var residentInfoData = _baseSqlServerRepository.QueryMedicalInsuranceResidentInfo(
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
                                ProjectLevel = itemPairCode != null ? ((ProjectLevel)Convert.ToInt32(itemPairCode.ProjectLevel)).ToString() : null,
                                ProjectCodeType = itemPairCode != null ? ((ProjectCodeType)Convert.ToInt32(itemPairCode.ProjectCodeType)).ToString() : null,
                                SelfPayProportion = (residentInfoData != null && itemPairCode != null)
                                    ? GetSelfPayProportion(itemPairCode, residentInfoData)
                                    : 0,
                                UploadAmount = c.UploadAmount,
                                OrganizationCode = c.OrganizationCode,
                                UploadTime = c.UploadTime,
                            };
                            dataListNew.Add(item);
                        }
                    }
                    else
                    {
                        dataListNew = dataList;
                    }

                }

                sqlConnection.Close();
                resultData.Add(totalPageCount, dataListNew);
                return resultData;

            }
        }
        /// <summary>
        /// 保存住院病人
        /// </summary>
        /// <param name="user"></param>
        /// <param name="param"></param>
        public void SaveInpatientInfo(UserInfoDto user, InpatientInfoDto param)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {

                sqlConnection.Open();
                string strSql =
                    $@"update  [dbo].[inpatient] set  [IsDelete] =1 ,DeleteTime=getDate(),DeleteUserId='{user.UserId}' where [IsDelete]=0  and [businessid] ='{param.BusinessId}';";
                string leaveHospitalDate = null;
                if (!string.IsNullOrWhiteSpace(param.LeaveHospitalDate))
                {
                    if (param.LeaveHospitalDate != "1900-01-01 00:00:00.000")
                    {
                        leaveHospitalDate = param.LeaveHospitalDate;
                    }
                }
                string insertSql = $@"";
                insertSql = insertSql + strSql;
                sqlConnection.Execute(insertSql);
                sqlConnection.Close();
            }



        }
        /// <summary>
        /// 住院病人查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public QueryInpatientInfoDto QueryInpatientInfo(QueryInpatientInfoParam param)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {

                sqlConnection.Open();
                string strSql = $"select top 1 * from [dbo].[Inpatient] where IsDelete=0 and BusinessId='{param.BusinessId}'";
                var data = sqlConnection.QueryFirstOrDefault<QueryInpatientInfoDto>(strSql);
                sqlConnection.Close();
                return data;
            }



        }
        /// <summary>
        /// 获取所有未传费用的住院病人
        /// </summary>
        /// <returns></returns>
        public List<QueryAllHospitalizationPatientsDto> QueryAllHospitalizationPatients(PrescriptionUploadAutomaticParam param)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var resultData = new List<QueryAllHospitalizationPatientsDto>();
                sqlConnection.Open();
                string strSql;
                if (param.IsTodayUpload)
                {
                    string day = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000";
                    strSql = $@"
                            select a.OrganizationCode,a.HospitalizationNo,a.BusinessId,b.InsuranceType from [dbo].[Inpatient]  as a 
                            inner join [dbo].[MedicalInsurance] as b on b.HisHospitalizationId=a.FixedEncoding
                            where a.IsDelete=0 and b.IsDelete=0 and a.HospitalizationNo 
                            in(select HospitalizationNo from [dbo].[HospitalizationFee] where BusinessTime='{day}' and  IsDelete=0 and UploadMark=0 Group by HospitalizationNo)";
                }
                else
                {
                    strSql = @"
                                select a.OrganizationCode,a.HospitalizationNo,a.BusinessId,b.InsuranceType from [dbo].[Inpatient]  as a 
                                inner join [dbo].[MedicalInsurance] as b on b.HisHospitalizationId=a.FixedEncoding
                                where a.IsDelete=0 and b.IsDelete=0 and a.HospitalizationNo 
                                in(select HospitalizationNo from [dbo].[HospitalizationFee] where IsDelete=0 and UploadMark=0 Group by HospitalizationNo) ";
                }
                var data = sqlConnection.Query<QueryAllHospitalizationPatientsDto>(strSql);

                if (data != null && data.Any())
                {
                    resultData = data.ToList();
                }
                sqlConnection.Close();
                return resultData;
            }


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public Int32 DeleteMedicalInsurance(UserInfoDto user, string param)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                int result = 0;
                sqlConnection.Open();
                if (param.Any())
                {

                    string insertSql = $@"update [dbo].[住院医保信息] set IsDelete=1,DeleteTime=getDate(),DeleteUserId='{user.UserId}' where  [业务ID]={param}";
                    result = sqlConnection.Execute(insertSql);
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
        public Int32 SaveInformationInfo(UserInfoDto user, List<InformationDto> param, InformationParam info)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                Int32 counts = 0;
                sqlConnection.Open();
                if (param.Any())
                {
                    var outpatientNum = CommonHelp.ListToStr(param.Select(c => c.DirectoryCode).ToList());
                    string strSql =
                        $@"update [dbo].[HospitalGeneralCatalog] set  [IsDelete] =1 ,DeleteTime=getDate(),DeleteUserId='{user.UserId}' where [IsDelete]=0 
                                and [DirectoryCode] in(" + outpatientNum + ")";
                    sqlConnection.Execute(strSql);
                    string insertSql = "";
                    foreach (var item in param)
                    {

                        string str = $@"INSERT INTO [dbo].[HospitalGeneralCatalog]
                                   (id,DirectoryType,[OrganizationCode],[DirectoryCode],[DirectoryName]
                                   ,[MnemonicCode],[DirectoryCategoryName],[Remark] ,[CreateTime]
		                            ,[IsDelete],[DeleteTime],CreateUserId,FixedEncoding)
                             VALUES ('{Guid.NewGuid()}','{info.DirectoryType}','{info.OrganizationCode}','{item.DirectoryCode}','{item.DirectoryName}',
                                     '{item.MnemonicCode}','{item.DirectoryCategoryName}','{item.Remark}',getDate(),
                                       0, null,'{user.UserId}','{CommonHelp.GuidToStr(item.DirectoryCode)}');";
                        insertSql += str;

                    }

                    counts = sqlConnection.Execute(strSql + insertSql);

                    sqlConnection.Close();
                }

                return counts;
            }
        }
        /// <summary>
        /// 查询HIS系统中科室、医师、病区、床位的基本信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<QueryInformationInfoDto> QueryInformationInfo(InformationParam param)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var resultData = new List<QueryInformationInfoDto>();
                sqlConnection.Open();
                string sqlStr = @"select Id, [OrganizationCode],[DirectoryType], [DirectoryCode],[DirectoryName],[FixedEncoding],
                                [DirectoryCategoryName],[Remark] from [dbo].[HospitalGeneralCatalog] where IsDelete=0 ";
                if (!string.IsNullOrWhiteSpace(param.DirectoryName)) sqlStr += " and DirectoryName like '" + param.DirectoryName + "%'";
                if (!string.IsNullOrWhiteSpace(param.DirectoryType)) sqlStr += $" and DirectoryType='{param.DirectoryType}'";
                var data = sqlConnection.Query<QueryInformationInfoDto>(sqlStr);
                if (data != null) resultData = data.ToList();
                sqlConnection.Close();
                return resultData;
            }
        }

        public int DeleteDatabase(DeleteDatabaseParam param)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {

                sqlConnection.Open();
                string sqlStr = $@" update {param.TableName} set IsDelete=1 ,UpdateUserId='{param.User.UserId}',DeleteTime=GETDATE() 
                               where {param.Field}='{param.Value}' and IsDelete=0";
                var data = sqlConnection.Execute(sqlStr);

                sqlConnection.Close();
                return data;
            }
        }
        public List<T> QueryDatabase<T>(T t, DatabaseParam param)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {

                sqlConnection.Open();
                string sqlStr = $@"select * from {param.TableName} where {param.Field}='{param.Value}' and IsDelete=0";
                var data = sqlConnection.Query<T>(sqlStr);
                sqlConnection.Close();
                return (data != null && data.Any()) == true ? data.ToList() : null;
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
