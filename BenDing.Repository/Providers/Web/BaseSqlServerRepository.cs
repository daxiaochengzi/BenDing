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
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="conStr"></param> 
        public BaseSqlServerRepository()
        {
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
        /// <summary>
        /// 住院明细查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<List<OutpatientDetailQuery>> InpatientInfoDetailQuery(InpatientInfoDetailQueryParam param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                var resultData = new List<OutpatientDetailQuery>();
                _sqlConnection.Open();

                string strSql = @"SELECT [Id]
                                      ,[住院号]
                                      ,[费用明细ID]
                                      ,[项目名称]
                                      ,[项目编码]
                                      ,[项目类别名称]
                                      ,[项目类别编码]
                                      ,[单位]
                                      ,[剂型]
                                      ,[规格]
                                      ,[单价]
                                      ,[数量]
                                      ,[金额]
                                      ,[用量]
                                      ,[用法]
                                      ,[用药天数]
                                      ,[医院计价单位]
                                      ,[是否进口药品]
                                      ,[药品产地]
                                      ,[处方号]
                                      ,[费用单据类型]
                                      ,[开单科室名称]
                                      ,[开单科室编码]
                                      ,[开单医生姓名]
                                      ,[开单医生编码]
                                      ,[开单时间]
                                      ,[执行科室名称]
                                      ,[执行科室编码]
                                      ,[执行医生姓名]
                                      ,[执行医生编码]
                                      ,[执行时间]
                                      ,[处方医师]
                                      ,[经办人]
                                      ,[执业医师证号]
                                      ,[费用冲销ID]
                                      ,[OrganizationCode]
                                      ,[机构名称]
                                      ,[费用时间]
                                      ,[CreateTime]
                                      ,[CreateUserId]
                              FROM [dbo].[住院费用] where ";
                if (param.IdList != null && param.IdList.Any())
                {
                    var idlist = ListToStr(param.IdList);
                    strSql += $@" 费用明细ID in('{idlist}')";
                }
                else
                {
                    strSql += $@" 住院号 ='{param.HospitalizationNumber}'";
                }
                var data = await _sqlConnection.QueryAsync<OutpatientDetailQuery>(strSql);
                _sqlConnection.Close();
                return data.Count() > 0 ? data.ToList() : resultData;
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
        /// 添加用户登陆信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task AddHospitalOperator(AddHospitalOperatorParam param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {

                _sqlConnection.Open();

                string querySql = $"select COUNT(*) from [dbo].[HospitalOperator] where [HisUserId]='{param.UserId}' ";
                var resultNum = await _sqlConnection.QueryFirstAsync<int>(querySql);
                if (resultNum > 0)
                {
                    string updateSql = null;
                    updateSql = param.IsHis ? $"update [dbo].[HospitalOperator] set HisUserAccount='{param.UserAccount}',HisUserPwd='{param.UserPwd}',UpdateUserId='{param.UserId}',OrganizationCode='{param.OrganizationCode}',ManufacturerNumber='{param.ManufacturerNumber}' where [HisUserId]='{param.UserId}'"
                        : $"update [dbo].[HospitalOperator] set [MedicalInsuranceAccount]='{param.UserAccount}',[MedicalInsurancePwd]='{param.UserPwd}',UpdateUserId='{param.UserId}' where [HisUserId]='{param.UserId}'";
                    await _sqlConnection.ExecuteAsync(updateSql);
                }
                else
                {
                    string insertSql = null;
                    if (param.IsHis)
                    {
                        insertSql = $@"
                                   INSERT INTO [dbo].[HospitalOperator]
                                   ([Id] ,[FixedEncoding],[HisUserId],ManufacturerNumber,
                                   [HisUserAccount],[HisUserPwd],[CreateTime],[CreateUserId],[HisUserName]
                                   )
                             VALUES('{Guid.NewGuid()}','{BitConverter.ToInt64(Guid.Parse(param.UserId).ToByteArray(), 0)}','{param.UserId}','{param.ManufacturerNumber}',
                                     '{param.UserAccount}','{param.UserPwd}',GETDATE(),'{param.UserId}','{param.HisUserName}')";
                    }
                    else
                    {
                        insertSql = $@"
                                   INSERT INTO [dbo].[HospitalOperator]
                                   ([Id] ,[FixedEncoding],[HisUserId],
                                   [MedicalInsuranceAccount],[MedicalInsurancePwd] ,[CreateTime],[CreateUserId]
                                   )
                             VALUES('{Guid.NewGuid()}','{BitConverter.ToInt64(Guid.Parse(param.UserId).ToByteArray(), 0)}','{param.UserId}',
                                      '{param.UserAccount}','{param.UserPwd}',GETDATE(),'{param.UserId}')";
                    }
                    await _sqlConnection.ExecuteAsync(insertSql);

                }
            }
        }
        /// <summary>
        /// 操作员登陆信息查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<QueryHospitalOperatorDto> QueryHospitalOperator(QueryHospitalOperatorParam param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                var resultData = new QueryHospitalOperatorDto();
                _sqlConnection.Open();
                string querySql = $"select top 1 MedicalInsuranceAccount,[MedicalInsurancePwd],[HisUserAccount],[HisUserPwd],ManufacturerNumber from [dbo].[HospitalOperator] where [HisUserId]='{param.UserId}' ";
                var data = await _sqlConnection.QueryFirstAsync<QueryHospitalOperatorDto>(querySql);
                if (data != null)
                {
                    resultData = data;
                }

                _sqlConnection.Close();
                return resultData;
            }
        }
        /// <summary>
        /// 获取所有的操作人员
        /// </summary>
        /// <returns></returns>
        public async Task<List<QueryHospitalOperatorAll>> QueryHospitalOperatorAll()
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                var resultData = new List<QueryHospitalOperatorAll>();
                _sqlConnection.Open();
                string querySql = @"select HisUserId,[HisUserName] from [dbo].[HospitalOperator]";
                var data = await _sqlConnection.QueryAsync<QueryHospitalOperatorAll>(querySql);
                if (data != null && data.Count()>0)
                {
                    resultData = data.ToList();
                }

                _sqlConnection.Close();
                return resultData;
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
                        var pairCodeIdList = param.PairCodeList.Where(c => c.PairCodeId != null).Select(d=> d.PairCodeId.ToString()).ToList();
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
                                           ([Id],[OrganizationCode],[OrganizationName],[DirectoryType],[State],[MedicalInsuranceDirectoryCode],
                                            [HisFixedEncoding],[CreateTime],[IsDelete],[CreateUserId],[ProjectLevel],[ProjectCodeType],[UploadState]) values (
                                           '{Guid.NewGuid()}','{param.OrganizationCode}','{param.OrganizationName}','{items.DirectoryType}',0,'{items.MedicalInsuranceDirectoryCode}',
                                             '{items.HisDirectoryCode}',GETDATE(),0,'{param.UserId}',{Convert.ToInt32(items.ProjectLevel)},{Convert.ToInt32(items.ProjectCodeType)},0) ";
                              
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
                        whereSql = $@" where not exists(select b.HisFixedEncoding from  [dbo].[ThreeCataloguePairCode] as b 
                                where b.OrganizationCode='{param.OrganizationCode}' and b.HisFixedEncoding=a.FixedEncoding and b.IsDelete=0 )";
                        querySql = @"select a.Id, a.[DirectoryCode],a.[DirectoryName],a.[MnemonicCode],a.[DirectoryCategoryCode],
                                a.[DirectoryCategoryName],a.[Unit],a.[Formulation],a.[Specification],a.ManufacturerName,a.FixedEncoding
                                 from [dbo].[HospitalThreeCatalogue]  as a ";
                        countSql = @"select COUNT(*) from[dbo].[HospitalThreeCatalogue] as a ";
                    

                }
                else if(param.State == 2)
                {
                    querySql =
                            $@"select a.Id, a.[DirectoryCode],a.[DirectoryName],a.[MnemonicCode],a.[DirectoryCategoryCode],
                             a.[DirectoryCategoryName],a.[Unit],a.[Formulation],a.[Specification],a.ManufacturerName,a.FixedEncoding,b.CreateUserId as PairCodeUser ,
                             c.ProjectCode,c.ProjectName,c.QuasiFontSize,c.LimitPaymentScope,b.CreateTime as PairCodeTime,c.ProjectLevel,c.ProjectCodeType
                             from [dbo].[HospitalThreeCatalogue]  as a  join  [dbo].[ThreeCataloguePairCode] as b
                             on b.[HisFixedEncoding]=a.FixedEncoding join [dbo].[MedicalInsuranceProject] as c
                             on b.MedicalInsuranceDirectoryCode=c.ProjectCode
                             where b.OrganizationCode ='{param.OrganizationCode}' and b.IsDelete=0 ";
                    countSql = $@"select COUNT(*)
                             from [dbo].[HospitalThreeCatalogue]  as a  join  [dbo].[ThreeCataloguePairCode] as b
                             on b.[HisFixedEncoding]=a.FixedEncoding join [dbo].[MedicalInsuranceProject] as c
                             on b.MedicalInsuranceDirectoryCode=c.ProjectCode
                             where b.OrganizationCode ='{param.OrganizationCode}' and b.IsDelete=0 ";


                }
                else if (param.State == 0)
                {
                    querySql =
                        $@"select a.Id, a.[DirectoryCode],a.[DirectoryName],a.[MnemonicCode],a.[DirectoryCategoryCode],
                            a.[DirectoryCategoryName],a.[Unit],a.[Formulation],a.[Specification],a.ManufacturerName,a.FixedEncoding,b.CreateUserId as PairCodeUser ,
                            c.ProjectCode,c.ProjectName,c.QuasiFontSize,c.LimitPaymentScope,b.CreateTime as PairCodeTime,c.ProjectLevel,c.ProjectCodeType
                             from [dbo].[HospitalThreeCatalogue]  as a  left join (select * from [dbo].[ThreeCataloguePairCode] where OrganizationCode ='{param.OrganizationCode}'  and IsDelete=0) as b
                             on b.[HisFixedEncoding]=a.FixedEncoding left join [dbo].[MedicalInsuranceProject] as c
                             on b.MedicalInsuranceDirectoryCode=c.ProjectCode
                             where a.IsDelete='0' ";
                    countSql = $@"select COUNT(*)
                              from [dbo].[HospitalThreeCatalogue]  as a  left join  (select * from [dbo].[ThreeCataloguePairCode] where OrganizationCode ='{param.OrganizationCode}'  and IsDelete=0) as b
                             on b.[HisFixedEncoding]=a.FixedEncoding left join [dbo].[MedicalInsuranceProject] as c
                             on b.MedicalInsuranceDirectoryCode=c.ProjectCode
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
                if (param.rows != 0 && param.page > 0)
                {
                    var skipCount = param.rows * (param.page - 1);
                    querySql += whereSql + " order by a.CreateTime desc OFFSET " + skipCount + " ROWS FETCH NEXT " + param.rows + " ROWS ONLY;";
                }
                string executeSql = countSql + whereSql + ";" + querySql;
                var result = await _sqlConnection.QueryMultipleAsync(executeSql);
               
                int totalPageCount = result.Read<int>().FirstOrDefault();
               var hospitalOperatorAll= await QueryHospitalOperatorAll();
               var dataList = (from t in result.Read<DirectoryComparisonManagementDto>()
                                select new DirectoryComparisonManagementDto
                                {Id = t.Id,
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
                                 ProjectLevel = t.ProjectLevel!=null?((ProjectLevel)Convert.ToInt32(t.ProjectLevel)).ToString(): t.ProjectLevel,
                                 ProjectCodeType = t.ProjectCodeType != null ? ((ProjectCodeType)Convert.ToInt32(t.ProjectCodeType)).ToString():t.ProjectCodeType,
                                 QuasiFontSize = t.QuasiFontSize,
                                 Unit = t.Unit,
                                 Specification=t.Specification,
                                 Remark = t.Remark,
                                 PairCodeUser= t.PairCodeUser!=null? hospitalOperatorAll.Where(c=>c.HisUserId==t.PairCodeUser).Select(d=>d.HisUserName).FirstOrDefault(): t.PairCodeUser,
                                }).ToList();
                resultData.Add(totalPageCount, dataList);
                _sqlConnection.Close();
               
                return resultData;

            }
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
