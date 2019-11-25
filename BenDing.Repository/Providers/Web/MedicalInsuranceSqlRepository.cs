using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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

namespace BenDing.Repository.Providers.Web
{
    public class MedicalInsuranceSqlRepository : IMedicalInsuranceSqlRepository
    {
        private string _connectionString;
        private SqlConnection _sqlConnection;
        private ISystemManageRepository _iSystemManageRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="conStr"></param> 
        public MedicalInsuranceSqlRepository(ISystemManageRepository iSystemManageRepository)
        {
            _iSystemManageRepository = iSystemManageRepository;
            string conStr = ConfigurationManager.ConnectionStrings["NFineDbContext"].ToString();
            _connectionString = !string.IsNullOrWhiteSpace(conStr)
                ? conStr
                : throw new ArgumentNullException(nameof(conStr));

        }

        #region 备用
        ///// <summary>
        ///// 医保病人信息保存
        ///// </summary>
        ///// <param name="user"></param>
        ///// <param name="param"></param>
        ///// <returns></returns>
        //public async Task<Int32> SaveMedicalInsuranceResidentInfo(
        //    MedicalInsuranceResidentInfoParam param)
        //{
        //    Int32 counts = 0;
        //    using (var _sqlConnection = new SqlConnection(_connectionString))
        //    {
        //        _sqlConnection.Open();
        //        IDbTransaction transaction = _sqlConnection.BeginTransaction();
        //        try
        //        {
        //            string insertSql =
        //                $@"update [dbo].[MedicalInsuranceResidentInfo] set  [IsDelete] =1 ,DeleteTime=GETDATE(),DeleteUserId='{param.EmpID}' where [IsDelete]=0 
        //                        and BusinessId='{param.BusinessId}' and OrgCode='{param.OrgCode}' and DataId='{param.DataId}'";
        //            await _sqlConnection.ExecuteAsync(insertSql, null, transaction);
        //            string strSql = $@"
        //            INSERT INTO [dbo].[MedicalInsuranceResidentInfo]
        //                       ([DataAllId]
        //                       ,[ContentJson]
        //                       ,[ResultData]
        //                       ,[DataType]
        //                       ,[DataId]
        //                       ,IsDelete
        //                       ,[BusinessId]
        //                       ,[IdCard]
        //                       ,[OrgCode]
        //                       ,[CreateUserId]
        //                       ,[CreateTime]
        //                     )
        //                 VALUES
        //                       ('{param.DataAllId}','{param.ContentJson}',,'{param.ResultDatajson}','{param.DataType}','{param.DataId}',0,
        //                         '{param.BusinessId}','{param.IdCard}','{param.OrgCode}','{param.EmpID}',GETDATE())";
        //            counts = await _sqlConnection.ExecuteAsync(strSql, null, transaction);

        //            transaction.Commit();
        //        }
        //        catch (Exception e)
        //        {
        //            transaction.Rollback();
        //            throw new Exception(e.Message);
        //        }

        //        _sqlConnection.Close();
        //    }

        //    return counts;
        //}



        ///// <summary>
        ///// 单病种下载
        ///// </summary>
        ///// <param name="user"></param>
        ///// <param name="param"></param>
        ///// <returns></returns>
        //public async Task<Int32> SingleResidentInfoDownload(UserInfoDto user, List<SingleResidentInfoDto> param)
        //{
        //    using (var _sqlConnection = new SqlConnection(_connectionString))
        //    {
        //        int result = 0;

        //        _sqlConnection.Open();
        //        if (param.Any())
        //        {
        //            string insertSql = null;
        //            foreach (var item in param)
        //            {
        //                insertSql += $@"
        //                        INSERT INTO [dbo].[SingleResidentInfo]
        //                        ([Id],[SpecialDiseasesCode],[Name],[ProjectCode] ,[CreateTime],[CreateUserId])
        //                        VALUES( '{item.Id}','{item.SpecialDiseasesCode}','{item.Name}','{item.ProjectCode}',GETDATE())";
        //            }

        //            result = await _sqlConnection.ExecuteAsync(insertSql);
        //        }

        //        return result;
        //    }
        //}
        #endregion
        /// <summary>
        ///  更新医保病人结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<int> UpdateMedicalInsuranceResidentSettlement(UpdateMedicalInsuranceResidentSettlementParam param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                var resultData = new MedicalInsuranceResidentInfoDto();
                _sqlConnection.Open();
                string strSql= "";
                if (!string.IsNullOrWhiteSpace(param.CancelUserId))
                {
                    strSql = $@" update MedicalInsurance set SettlementUserId={param.UserId},SettlementTime=NULL,SettlementCancelTime=GETDATE(),
                                    SettlementCancelUserId='{param.CancelUserId}',OtherInfo='{param.OtherInfo}',MedicalInsuranceAllAmount={param.MedicalInsuranceAllAmount},
                                    SelfPayFeeAmount= {param.SelfPayFeeAmount},ReimbursementExpensesAmount={param.ReimbursementExpensesAmount},
                                    SettlementNo='{param.SettlementNo}',TransactionId='{param.TransactionId}'
                                    where Id='{param.Id}' ";
                }
                else
                {
                    strSql = $@" update MedicalInsurance set SettlementUserId={param.UserId},SettlementTime=GETDATE(),
                                    OtherInfo='{param.OtherInfo}',MedicalInsuranceAllAmount={param.MedicalInsuranceAllAmount},
                                    SelfPayFeeAmount= {param.SelfPayFeeAmount},ReimbursementExpensesAmount={param.ReimbursementExpensesAmount},
                                    SettlementNo='{param.SettlementNo}',TransactionId='{param.TransactionId}'
                                    where Id='{param.Id}' ";
                }
                
               
                var data = await _sqlConnection.ExecuteAsync(strSql);
                _sqlConnection.Close();
                return data;
            }
        }
        /// <summary>
        /// 住院病人明细查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<List<QueryInpatientInfoDetailDto>> InpatientInfoDetailQuery(
            InpatientInfoDetailQueryParam param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                var resultData = new List<QueryInpatientInfoDetailDto>();
                _sqlConnection.Open();

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


                var data = await _sqlConnection.QueryAsync<QueryInpatientInfoDetailDto>(strSql);

                if (data != null && data.Count() > 0)
                {
                    resultData = data.ToList();
                }

                _sqlConnection.Close();
                return resultData;
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
                if (!string.IsNullOrWhiteSpace(param.MedicalInsuranceHospitalizationNo) == true && param.IsModify == false)
                {
                    insertSql = $@"update [dbo].[MedicalInsurance] set 
                    MedicalInsuranceHospitalizationNo='{param.MedicalInsuranceHospitalizationNo}',IsDelete=0
                    where [Id]='{param.Id}'";
                }
                else if (param.IsModify)
                {
                    insertSql = $@"update [dbo].[MedicalInsurance] set 
                    AdmissionInfoJson='{param.AdmissionInfoJson}',[IsDelete]=0,
                    where [Id]='{param.Id}' and OrganizationCode='{user.OrganizationCode}'";
                }
                else
                {
                    insertSql = $@"INSERT INTO [dbo].[MedicalInsurance]([Id],[HisHospitalizationId],[InsuranceNo],[MedicalInsuranceAllAmount]
                               ,[AdmissionInfoJson],[ReimbursementExpenses] ,[SelfPayFee],[OtherInfo] 
		                       ,[CreateTime],[IsDelete] ,OrganizationCode,CreateUserId,OrganizationName,InsuranceType)
                           VALUES('{param.Id}','{param.HisHospitalizationId}','{param.InsuranceNo}', 0,
                                '{param.AdmissionInfoJson}',0,0,NULL,
                                GETDATE(),1,'{user.OrganizationCode}','{user.UserId}','{user.OrganizationName }',{param.InsuranceType});";
                    insertSql = $"update [dbo].[MedicalInsurance] set [IsDelete]=1,DeleteUserId='{user.UserId}',DeleteTime=GETDATE() where [HisHospitalizationId]='{param.HisHospitalizationId}';" + insertSql;

                }
                //var log = LogFactory.GetLogger("ini".GetType().ToString());
                //log.Debug(insertSql);
                var num = await _sqlConnection.ExecuteAsync(insertSql);

            }
        }
        /// <summary>
        /// 医保病人信息查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<MedicalInsuranceResidentInfoDto> QueryMedicalInsuranceResidentInfo(
            QueryMedicalInsuranceResidentInfoParam param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                var resultData = new MedicalInsuranceResidentInfoDto();
                _sqlConnection.Open();
                string strSql = @" SELECT [Id]
                              ,[HisHospitalizationId]
                              ,[InsuranceNo]
                              ,[MedicalInsuranceAllAmount]
                              ,[MedicalInsuranceHospitalizationNo]
                              ,[SelfPayFeeAmount]
                              ,[AdmissionInfoJson]
                              ,[ReimbursementExpensesAmount]
                              ,[OtherInfo]
                              ,[OrganizationCode]
                              ,[OrganizationName]
                              ,[InsuranceType]
                              ,[SettlementNo]
                              ,[TransactionId]
                            FROM [dbo].[MedicalInsurance]
                            where  IsDelete=0";
                if (!string.IsNullOrWhiteSpace(param.DataId))
                    strSql += $" and Id='{param.DataId}'";
                if (!string.IsNullOrWhiteSpace(param.BusinessId))
                    strSql += $" and HisHospitalizationId='{CommonHelp.GuidToStr(param.BusinessId)}'";
                if (!string.IsNullOrWhiteSpace(param.OrganizationCode))
                    strSql += $" and OrganizationCode='{param.OrganizationCode}'";
                var data = await _sqlConnection.QueryFirstOrDefaultAsync<MedicalInsuranceResidentInfoDto>(strSql);
                _sqlConnection.Close();
                return data != null ? data : resultData;
            }
        }
        /// <summary>
        /// 医保中心端项目下载
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
                    var projectCodeList = CommonHelp.ListToStr(param.Select(c => c.ProjectCode).ToList());

                    string insertSql = null;
                    foreach (var item in param)
                    {//判断日期格式是否正确
                        var projectName = FilteSqlStr(item.ProjectName);
                        insertSql += $@"INSERT INTO [dbo].[MedicalInsuranceProject]
                           (id,[ProjectCode],[ProjectName] ,[ProjectCodeType] ,[ProjectLevel],[WorkersSelfPayProportion]
                           ,[Unit],[MnemonicCode] ,[Formulation],[ResidentSelfPayProportion],[RestrictionSign]
                           ,[ZeroBlock],[OneBlock],[TwoBlock],[ThreeBlock],[FourBlock],[EffectiveSign],[ResidentOutpatientSign]
                           ,[ResidentOutpatientBlock],[Manufacturer] ,[QuasiFontSize] ,[Specification],[Remark],[NewCodeMark]
                           ,[NewUpdateTime],[StartTime] ,[EndTime],[LimitPaymentScope],[CreateTime],[CreateUserId],[IsDelete]
                           )
                          VALUES('{Guid.NewGuid()}','{item.ProjectCode}','{projectName}','{item.ProjectCodeType}','{item.ProjectLevel}',{CommonHelp.ValueToDecimal(item.WorkersSelfPayProportion)}
                                  ,'{item.Unit}','{item.MnemonicCode}', '{item.Formulation}',{CommonHelp.ValueToDecimal(item.ResidentSelfPayProportion)},'{item.RestrictionSign}'
                                  ,{CommonHelp.ValueToDecimal(item.ZeroBlock)},{CommonHelp.ValueToDecimal(item.OneBlock)},{CommonHelp.ValueToDecimal(item.TwoBlock)},{CommonHelp.ValueToDecimal(item.ThreeBlock)},{CommonHelp.ValueToDecimal(item.FourBlock)},'{item.EffectiveSign}','{item.ResidentOutpatientSign}'
                                  ,{CommonHelp.ValueToDecimal(item.ResidentOutpatientBlock)},'{item.Manufacturer}','{item.QuasiFontSize}','{item.Specification}','{item.Remark}','{item.NewCodeMark}'
                                  ,'{ DateTime.ParseExact(item.NewUpdateTime, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyy-MM-dd HH:mm:ss")}',
                                  NULL,NULL,'{item.LimitPaymentScope}',GETDATE(),'{user.UserId}',0
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
        public async Task<string> ProjectDownloadTimeMax()
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                string insertSql = "select max(NewUpdateTime) from [dbo].[MedicalInsuranceProject]";
                string result = await _sqlConnection.QueryFirstOrDefault(insertSql);
                _sqlConnection.Close();
                return result;
            }
        }
        /// <summary>
        /// 医保对码
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task MedicalInsurancePairCode(MedicalInsurancePairCodesUiParam param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                var resultData = new MedicalInsuranceResidentInfoDto();
                _sqlConnection.Open();

                if (param.PairCodeList.Any())
                {
                    IDbTransaction transaction = _sqlConnection.BeginTransaction();
                    try
                    {
                        string updateSql = "";
                        var pairCodeIdList = param.PairCodeList.Where(c => c.PairCodeId != null)
                            .Select(d => d.PairCodeId.ToString()).ToList();
                        var updateId = CommonHelp.ListToStr(pairCodeIdList);
                        //更新对码
                        if (pairCodeIdList.Any())
                        {
                            updateSql += $@"update [dbo].[ThreeCataloguePairCode] set [IsDelete]=1,
                             [DeleteUserId]='{param.UserId}',DeleteTime=GETDATE() where [Id] in ({updateId});";
                            await _sqlConnection.ExecuteAsync(updateSql, null, transaction);

                        }

                        var insertParam = param.PairCodeList.ToList();
                        string insertSql = "";
                        //新增对码
                        if (insertParam.Any())
                        {
                            foreach (var items in insertParam)
                            {
                                insertSql += $@"insert into [dbo].[ThreeCataloguePairCode]
                                           ([Id],[OrganizationCode],[OrganizationName],[DirectoryCategoryCode],[State],[ProjectCode],
                                            [FixedEncoding],[CreateTime],[IsDelete],[CreateUserId],[UploadState],[DirectoryCode]) values (
                                           '{Guid.NewGuid()}','{param.OrganizationCode}','{param.OrganizationName}','{items.DirectoryCategoryCode}',0,'{items.ProjectCode}',
                                           '{BitConverter.ToInt64(Guid.Parse(items.DirectoryCode).ToByteArray(), 0)}',GETDATE(),0,'{param.UserId}',0,'{items.DirectoryCode}') ";

                            }

                            await _sqlConnection.ExecuteAsync(insertSql, null, transaction);
                        }

                        transaction.Commit();

                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw new Exception(e.Message);
                    }
                }

                _sqlConnection.Close();

            }
        }
        /// <summary>
        /// 医保对码查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<List<QueryMedicalInsurancePairCodeDto>> QueryMedicalInsurancePairCode(
            QueryMedicalInsurancePairCodeParam param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                var resultData = new List<QueryMedicalInsurancePairCodeDto>();
                _sqlConnection.Open();

                if (param.DirectoryCodeList.Any())
                {
                    var updateId = CommonHelp.ListToStr(param.DirectoryCodeList);
                    string sqlStr = $@"
                            select a.FixedEncoding,a.DirectoryCode,b.ProjectCode,b.ProjectCodeType,
                            b.ProjectName,b.ProjectLevel,b.Formulation,b.Specification,b.Unit,
                            b.OneBlock,b.TwoBlock,b.ThreeBlock,b.FourBlock,b.Manufacturer,b.LimitPaymentScope,b.NewCodeMark,
                            b.ResidentOutpatientBlock,b.ResidentOutpatientSign,b.ResidentSelfPayProportion,b.WorkersSelfPayProportion
                            from [dbo].[ThreeCataloguePairCode] as   a
                            inner join [dbo].[MedicalInsuranceProject] as b on b.ProjectCode=a.ProjectCode
                            where a.OrganizationCode='{param.OrganizationCode}' and a.[DirectoryCode] in({updateId})
                              and a.IsDelete=0  and b.IsDelete=0 and b.EffectiveSign=1";
                    var data = await _sqlConnection.QueryAsync<QueryMedicalInsurancePairCodeDto>(sqlStr);
                    if (data != null && data.Any() == true)
                    {
                        resultData = data.ToList();
                    }
                }

                _sqlConnection.Close();
                return resultData;
            }
        }
        /// <summary>
        /// 目录对照中心
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<Dictionary<int, List<DirectoryComparisonManagementDto>>> DirectoryComparisonManagement(
            DirectoryComparisonManagementUiParam param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {

                _sqlConnection.Open();
                string querySql = "";
                string countSql = "";
                string whereSql = "";
                var resultData = new Dictionary<int, List<DirectoryComparisonManagementDto>>();
                if (param.State == 1)
                {
                    whereSql = $@" where not exists(select b.FixedEncoding from  [dbo].[ThreeCataloguePairCode] as b 
                                where b.OrganizationCode='{param.OrganizationCode}' and b.FixedEncoding=a.FixedEncoding and b.IsDelete=0 )";
                    querySql = @"select  a.[DirectoryCode],a.[DirectoryName],a.[MnemonicCode],a.[DirectoryCategoryCode],
                                a.[DirectoryCategoryName],a.[Unit],a.[Formulation],a.[Specification],a.ManufacturerName,a.FixedEncoding
                                 from [dbo].[HospitalThreeCatalogue]  as a ";
                    countSql = @"select COUNT(*) from[dbo].[HospitalThreeCatalogue] as a ";


                }
                else if (param.State == 2)
                {
                    querySql =
                        $@"select b.Id, a.[DirectoryCode],a.[DirectoryName],a.[MnemonicCode],a.[DirectoryCategoryCode],
                             a.[DirectoryCategoryName],a.[Unit],a.[Formulation],a.[Specification],a.ManufacturerName,a.FixedEncoding,b.CreateUserId as PairCodeUser ,
                             c.ProjectCode,c.ProjectName,c.QuasiFontSize,c.LimitPaymentScope,b.CreateTime as PairCodeTime,c.ProjectLevel,c.ProjectCodeType
                             from [dbo].[HospitalThreeCatalogue]  as a  join  [dbo].[ThreeCataloguePairCode] as b
                             on b.[FixedEncoding]=a.FixedEncoding join [dbo].[MedicalInsuranceProject] as c
                             on b.ProjectCode=c.ProjectCode
                             where b.OrganizationCode ='{param.OrganizationCode}' and b.IsDelete=0 ";
                    countSql = $@"select COUNT(*)
                             from [dbo].[HospitalThreeCatalogue]  as a  join  [dbo].[ThreeCataloguePairCode] as b
                             on b.[FixedEncoding]=a.FixedEncoding join [dbo].[MedicalInsuranceProject] as c
                             on b.ProjectCode=c.ProjectCode
                             where b.OrganizationCode ='{param.OrganizationCode}' and b.IsDelete=0 ";


                }
                else if (param.State == 0)
                {
                    querySql =
                        $@"select b.Id, a.[DirectoryCode],a.[DirectoryName],a.[MnemonicCode],a.[DirectoryCategoryCode],
                            a.[DirectoryCategoryName],a.[Unit],a.[Formulation],a.[Specification],a.ManufacturerName,a.FixedEncoding,b.CreateUserId as PairCodeUser ,
                            c.ProjectCode,c.ProjectName,c.QuasiFontSize,c.LimitPaymentScope,b.CreateTime as PairCodeTime,c.ProjectLevel,c.ProjectCodeType
                             from [dbo].[HospitalThreeCatalogue]  as a  left join (select * from [dbo].[ThreeCataloguePairCode] where OrganizationCode ='{param.OrganizationCode}'  and IsDelete=0) as b
                             on b.[FixedEncoding]=a.FixedEncoding left join [dbo].[MedicalInsuranceProject] as c
                             on b.ProjectCode=c.ProjectCode
                             where a.IsDelete='0' ";
                    countSql = $@"select COUNT(*)
                              from [dbo].[HospitalThreeCatalogue]  as a  left join  (select * from [dbo].[ThreeCataloguePairCode] where OrganizationCode ='{param.OrganizationCode}'  and IsDelete=0) as b
                             on b.[FixedEncoding]=a.FixedEncoding left join [dbo].[MedicalInsuranceProject] as c
                             on b.ProjectCode=c.ProjectCode
                             where a.IsDelete='0' ";


                }

                if (!string.IsNullOrWhiteSpace(param.DirectoryCode))
                {
                    whereSql += $" and a.DirectoryCode='{param.DirectoryCode}'";
                }

                if (!string.IsNullOrWhiteSpace(param.DirectoryCategoryCode))
                {
                    whereSql += $" and a.DirectoryCategoryCode='{param.DirectoryCategoryCode}'";
                }

                if (!string.IsNullOrWhiteSpace(param.DirectoryName))
                {
                    whereSql += "  and a.DirectoryName like '" + param.DirectoryName + "%'";
                }

                if (param.Limit != 0 && param.Page > 0)
                {
                    var skipCount = param.Limit * (param.Page - 1);
                    querySql += whereSql + " order by a.CreateTime desc OFFSET " + skipCount + " ROWS FETCH NEXT " +
                                param.Limit + " ROWS ONLY;";
                }

                string executeSql = countSql + whereSql + ";" + querySql;
                var result = await _sqlConnection.QueryMultipleAsync(executeSql);

                int totalPageCount = result.Read<int>().FirstOrDefault();
                var hospitalOperatorAll = await _iSystemManageRepository.QueryHospitalOperatorAll();
                var dataList = (from t in result.Read<DirectoryComparisonManagementDto>()
                    select new DirectoryComparisonManagementDto
                    {
                        Id = t.Id,
                        DirectoryCode = t.DirectoryCode,
                        DirectoryCategoryName = t.DirectoryCategoryName,
                        DirectoryName = t.DirectoryName,
                        FixedEncoding = t.FixedEncoding,
                        Formulation = t.Formulation,
                        LimitPaymentScope = t.LimitPaymentScope,
                        ManufacturerName = t.ManufacturerName,
                        MnemonicCode = t.MnemonicCode,
                        PairCodeTime = t.PairCodeTime,
                        ProjectName = t.ProjectName,
                        ProjectCode = t.ProjectCode,
                        ProjectLevel = t.ProjectLevel != null
                            ? ((ProjectLevel) Convert.ToInt32(t.ProjectLevel)).ToString()
                            : t.ProjectLevel,
                        ProjectCodeType = t.ProjectCodeType != null
                            ? ((ProjectCodeType) Convert.ToInt32(t.ProjectCodeType)).ToString()
                            : t.ProjectCodeType,
                        QuasiFontSize = t.QuasiFontSize,
                        Unit = t.Unit,
                        Specification = t.Specification,
                        Remark = t.Remark,
                        PairCodeUser = t.PairCodeUser != null
                            ? hospitalOperatorAll.Where(c => c.HisUserId == t.PairCodeUser).Select(d => d.HisUserName)
                                .FirstOrDefault()
                            : t.PairCodeUser,
                    }).ToList();
                resultData.Add(totalPageCount, dataList);
                _sqlConnection.Close();
                return resultData;

            }
        }
        /// <summary>
        /// 三大目录查询
        /// </summary>
        /// <param name="organizationCode"></param>
        /// <returns></returns>
        public async Task<List<QueryThreeCataloguePairCodeUploadDto>> ThreeCataloguePairCodeUpload(
            string organizationCode)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                var resultData = new List<QueryThreeCataloguePairCodeUploadDto>();
                _sqlConnection.Open();
                string querySql = $@"select a.[Id],[ProjectCode],[ProjectName],[ProjectCodeType],ProjectLevel,
                                    [RestrictionSign],[Remark],b.DirectoryCode,[DirectoryType]
                                    from [dbo].[MedicalInsuranceProject] as a inner join [dbo].[ThreeCataloguePairCode] as b
                                    on a.ProjectCode=b.MedicalInsuranceDirectoryCode where b.UploadState=0 and OrganizationCode='{organizationCode}'
                                    and IsDelete=0";
                var data = await _sqlConnection.QueryAsync<QueryThreeCataloguePairCodeUploadDto>(querySql);
                if (data != null && data.Count() > 0)
                {
                    resultData = data.ToList();
                }

                _sqlConnection.Close();
                return resultData;
            }
        }

        /// <summary>
        /// his上传更新数据
        /// </summary>
        /// <param name="organizationCode"></param>
        /// <returns></returns>
        public async Task<int> UpdateThreeCataloguePairCodeUpload(string organizationCode)
        {
            Int32 resultData = 0;
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {

                _sqlConnection.Open();
                string querySql = $@"update [dbo].[ThreeCataloguePairCode] set UploadState=1 where 
                                 UploadState=0 and OrganizationCode='{organizationCode}' and IsDelete=0";
                resultData = await _sqlConnection.ExecuteAsync(querySql);

                _sqlConnection.Close();

            }

            return resultData;
        }

        /// <summary>
        /// 处方上传数据更新
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<int> UpdateHospitalizationFee(List<UpdateHospitalizationFeeParam> param, bool batchConfirmFail, UserInfoDto user)
        {
            int resultData = 0;
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {

                _sqlConnection.Open();
               
                if (param.Any())
                {
                    string querySql = null;
                    if (batchConfirmFail == false)
                    {
                        foreach (var item in param)
                        {
                            querySql +=
                               $@"update [dbo].[HospitalizationFee] set [UploadMark]=1,BatchNumber='{item.BatchNumber}',UploadAmount={item.UploadAmount},
                               TransactionId='{item.TransactionId}',UploadUserId='{user.UserId}',UploadUserName='{user.UserName}',UploadTime=GETDATE()
                               where id='{item.Id}' and IsDelete=0;";
                        }
                    }
                    else //更新批次确认失败
                    {
                        var updateId = CommonHelp.ListToStr(param.Select(c=>c.Id.ToString()).ToList());
                        querySql = $"update [dbo].[HospitalizationFee] set [UploadMark]=0 where id in ({updateId});";

                    }
                    resultData = await _sqlConnection.ExecuteAsync(querySql);
                }

                _sqlConnection.Close();

            }

            return resultData;
        }
        public string FilteSqlStr(string Str)
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
