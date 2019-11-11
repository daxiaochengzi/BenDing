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
using BenDing.Domain.Models.Params.UI;
using BenDing.Domain.Models.Params.Web;
using BenDing.Repository.Interfaces.Web;
using Dapper;

namespace BenDing.Repository.Providers.Web
{
    public class BaseSqlServerRepository : IBaseSqlServerRepository
    {
        private string _connectionString;
        private SqlConnection _sqlConnection;
        private ISystemManageRepository _iSystemManageRepository;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="conStr"></param> 
        public BaseSqlServerRepository(ISystemManageRepository iSystemManageRepository)
        {
            _iSystemManageRepository = iSystemManageRepository;
            string conStr = ConfigurationManager.ConnectionStrings["NFineDbContext"].ToString();
            _connectionString = !string.IsNullOrWhiteSpace(conStr) ? conStr : throw new ArgumentNullException(nameof(conStr));

        }
        /// <summary>
        /// 医保病人信息保存
        /// </summary>
        /// <param name="user"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<Int32> SaveMedicalInsuranceResidentInfo(
            MedicalInsuranceResidentInfoParam param)
        {
            Int32 counts = 0;
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                IDbTransaction transaction = _sqlConnection.BeginTransaction();
                try
                {
                    string insertSql =
                        $@"update [dbo].[MedicalInsuranceResidentInfo] set  [IsDelete] =1 ,DeleteTime=GETDATE(),DeleteUserId='{param.EmpID}' where [IsDelete]=0 
                                and BusinessId='{param.BusinessId}' and OrgCode='{param.OrgCode}' and DataId='{param.DataId}'";
                    await _sqlConnection.ExecuteAsync(insertSql, null, transaction);
                    string strSql = $@"
                    INSERT INTO [dbo].[MedicalInsuranceResidentInfo]
                               ([DataAllId]
                               ,[ContentJson]
                               ,[ResultData]
                               ,[DataType]
                               ,[DataId]
                               ,IsDelete
                               ,[BusinessId]
                               ,[IdCard]
                               ,[OrgCode]
                               ,[CreateUserId]
                               ,[CreateTime]
                             )
                         VALUES
                               ('{param.DataAllId}','{param.ContentJson}',,'{param.ResultDatajson}','{param.DataType}','{param.DataId}',0,
                                 '{param.BusinessId}','{param.IdCard}','{param.OrgCode}','{param.EmpID}',GETDATE())";
                    counts = await _sqlConnection.ExecuteAsync(strSql, null, transaction);

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }

                _sqlConnection.Close();
            }

            return counts;
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
                string strSql = @" select   [DataAllId]
                      ,[ContentJson]
                      ,[DataType]
                      ,[DataId]
                      ,[BusinessId]
                      ,[IdCard]
                      ,[OrgCode]
                      ,[CreateUserId]
                      ,[CreateTime]
                      ,[DeleteTime]
                      ,[DeleteUserId] from [dbo].[MedicalInsuranceResidentInfo] where";

                strSql += $" IsDelete={param.IsDelete}";
                if (!string.IsNullOrWhiteSpace(param.DataId))
                    strSql += $" and DataId='{param.DataId}'";
                if (!string.IsNullOrWhiteSpace(param.BusinessId))
                    strSql += $" and BusinessId='{param.BusinessId}'";
                if (!string.IsNullOrWhiteSpace(param.OrgCode))
                    strSql += $" and OrgCode='{param.OrgCode}'";
                if (!string.IsNullOrWhiteSpace(param.IdCard))
                    strSql += $" and IdCard='{param.IdCard}'";
                var data = await _sqlConnection.QueryFirstOrDefaultAsync<MedicalInsuranceResidentInfoDto>(strSql);
                _sqlConnection.Close();
                return data != null ? data : resultData;
            }
        }
        /// <summary>
        ///  更新医保病人信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<int> UpdateMedicalInsuranceResidentInfo(
            UpdateMedicalInsuranceResidentInfoParam param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                var resultData = new MedicalInsuranceResidentInfoDto();
                _sqlConnection.Open();
                string strSql = @" update MedicalInsuranceResidentInfo set  ";

                if (!string.IsNullOrWhiteSpace(param.DataType))
                    strSql += $" DataType='{param.DataType}'";
                if (!string.IsNullOrWhiteSpace(param.ContentJson))
                    strSql += $" ,ContentJson='{param.ContentJson}'";
                if (!string.IsNullOrWhiteSpace(param.ResultDatajson))
                    strSql += $" ,ResultDatajson='{param.ResultDatajson}'";
                if (!string.IsNullOrWhiteSpace(param.DataId))
                    strSql += $" ,DataId='{param.DataId}'";
                if (!string.IsNullOrWhiteSpace(param.IdCard))
                    strSql += $" ,DataId='{param.IdCard}'";
                strSql += $" where IsDelete=0 and DataAllId='{param.DataAllId}'";
                var data = await _sqlConnection.ExecuteAsync(strSql);
                _sqlConnection.Close();
                return data;
            }
        }
        public async Task<List<QueryInpatientInfoDetailDto>> InpatientInfoDetailQuery(InpatientInfoDetailQueryParam param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                var resultData = new List<QueryInpatientInfoDetailDto>();
                _sqlConnection.Open();

                string strSql = @"select * from [dbo].[HospitalizationFee]  where  IsDelete=0 ";
                if (param.IdList != null && param.IdList.Any())
                {
                    var idlist = ListToStr(param.IdList);
                    strSql += $@" and Id in('{idlist}')";
                }
                if (!string.IsNullOrWhiteSpace(param.HospitalizationNumber))
                {
                    strSql += $@" and HospitalizationNo ='{param.HospitalizationNumber}'";
                }

                var data = await _sqlConnection.QueryAsync<QueryInpatientInfoDetailDto>(strSql);
                _sqlConnection.Close();
                if (data != null && data.Count() > 0)
                {
                    data = data.ToList();
                }
                return resultData;
            }
        }

        /// <summary>
        /// 单病种下载
        /// </summary>
        /// <param name="user"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<Int32> SingleResidentInfoDownload(UserInfoDto user, List<SingleResidentInfoDto> param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                int result = 0;

                _sqlConnection.Open();
                if (param.Any())
                {
                    string insertSql = null;
                    foreach (var item in param)
                    {
                        insertSql += $@"
                                INSERT INTO [dbo].[SingleResidentInfo]
                                ([Id],[SpecialDiseasesCode],[Name],[ProjectCode] ,[CreateTime],[CreateUserId])
                                VALUES( '{item.Id}','{item.SpecialDiseasesCode}','{item.Name}','{item.ProjectCode}',GETDATE())";
                    }
                    result = await _sqlConnection.ExecuteAsync(insertSql);
                }
                return result;
            }
        }
        public async Task<SingleResidentInfoDto> SingleResidentInfoQuery(SingleResidentInfQueryUiParam param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))

            {
                var resultData = new SingleResidentInfoDto();
                _sqlConnection.Open();
                string strSql = $@" select top 1
                       [Id],[SpecialDiseasesCode],[Name],[ProjectCode]
                       from [dbo].[SingleResidentInfo]
                       where IsDelete=0 and SpecialDiseasesCode='{param.SpecialDiseasesCode}'";
                var data = await _sqlConnection.QueryFirstOrDefaultAsync<SingleResidentInfoDto>(strSql);
                _sqlConnection.Close();
                return data != null ? data : resultData;
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
                        var pairCodeIdList = param.PairCodeList.Where(c => c.PairCodeId != null).Select(d => d.PairCodeId.ToString()).ToList();
                        var updateId = ListToStr(pairCodeIdList);
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
                                             '{BitConverter.ToInt64(Guid.Parse(items.DirectoryCode).ToByteArray(), 0)}',GETDATE(),0,'{param.UserId}',0,'{items.DirectoryCategoryCode}') ";

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
        /// 目录对照中心
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<Dictionary<int, List<DirectoryComparisonManagementDto>>> DirectoryComparisonManagement(DirectoryComparisonManagementUiParam param)
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
                    querySql += whereSql + " order by a.CreateTime desc OFFSET " + skipCount + " ROWS FETCH NEXT " + param.Limit + " ROWS ONLY;";
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
                                    ProjectLevel = t.ProjectLevel != null ? ((ProjectLevel)Convert.ToInt32(t.ProjectLevel)).ToString() : t.ProjectLevel,
                                    ProjectCodeType = t.ProjectCodeType != null ? ((ProjectCodeType)Convert.ToInt32(t.ProjectCodeType)).ToString() : t.ProjectCodeType,
                                    QuasiFontSize = t.QuasiFontSize,
                                    Unit = t.Unit,
                                    Specification = t.Specification,
                                    Remark = t.Remark,
                                    PairCodeUser = t.PairCodeUser != null ? hospitalOperatorAll.Where(c => c.HisUserId == t.PairCodeUser).Select(d => d.HisUserName).FirstOrDefault() : t.PairCodeUser,
                                }).ToList();
                resultData.Add(totalPageCount, dataList);
                _sqlConnection.Close();
                return resultData;

            }
        }

        public async Task<List<QueryThreeCataloguePairCodeUploadDto>> ThreeCataloguePairCodeUpload(string organizationCode)
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

        private string ListToStr(List<string> param)
        {
            string result = null;
            if (param.Any())
            {
                foreach (var item in param)
                {
                    result = "'" + item + "'" + ",";
                }
            }
            return result?.Substring(0, result.Length - 1);
        }
    }
}
