using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Enums;
using BenDing.Domain.Models.Params.SystemManage;
using BenDing.Domain.Models.Params.Web;
using BenDing.Repository.Interfaces.Web;
using Dapper;

namespace BenDing.Repository.Providers.Web
{
   public class SystemManageRepository: ISystemManageRepository
    {
        private string _connectionString;
        /// <summary>
        /// 构造函数
        /// </summary>
      
        public SystemManageRepository()
        {
            string conStr = ConfigurationManager.ConnectionStrings["NFineDbContext"].ToString();
            _connectionString = !string.IsNullOrWhiteSpace(conStr) ? conStr : throw new ArgumentNullException(nameof(conStr));

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
                    updateSql = param.IsHis ? $"update [dbo].[HospitalOperator] set HisUserAccount='{param.UserAccount}',HisUserPwd='{param.UserPwd}',UpdateTime=GETDATE(),UpdateUserId='{param.UserId}',OrganizationCode='{param.OrganizationCode}',ManufacturerNumber='{param.ManufacturerNumber}' where [HisUserId]='{param.UserId}'"
                        : $"update [dbo].[HospitalOperator] set [MedicalInsuranceAccount]='{param.UserAccount}',[MedicalInsurancePwd]='{param.UserPwd}',UpdateTime=GETDATE(),UpdateUserId='{param.UserId}' where [HisUserId]='{param.UserId}'";
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
                                   [HisUserAccount],[HisUserPwd],[CreateTime],[CreateUserId],[HisUserName],[IsDelete]
                                   )
                             VALUES('{Guid.NewGuid()}','{BitConverter.ToInt64(Guid.Parse(param.UserId).ToByteArray(), 0)}','{param.UserId}','{param.ManufacturerNumber}',
                                     '{param.UserAccount}','{param.UserPwd}',GETDATE(),'{param.UserId}','{param.HisUserName}',0)";
                    }
                    else
                    {
                        insertSql = $@"
                                   INSERT INTO [dbo].[HospitalOperator]
                                   ([Id] ,[FixedEncoding],[HisUserId],
                                   [MedicalInsuranceAccount],[MedicalInsurancePwd] ,[CreateTime],[CreateUserId],[IsDelete]
                                   )
                             VALUES('{Guid.NewGuid()}','{BitConverter.ToInt64(Guid.Parse(param.UserId).ToByteArray(), 0)}','{param.UserId}',
                                      '{param.UserAccount}','{param.UserPwd}',GETDATE(),'{param.UserId}',0)";
                    }
                    await _sqlConnection.ExecuteAsync(insertSql);

                }
            }
        }
        /// <summary>
        /// 设置医院等级
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task AddHospitalOrganizationGrade(HospitalOrganizationGradeParam param)
        {
            //医院等级
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                string sqlStr;
                string querySql =
                    $"select COUNT(*) from [dbo].[HospitalOrganizationGrade] where [HospitalId]='{param.HospitalId}' ";
                var resultNum = await _sqlConnection.QueryFirstAsync<int>(querySql);
                if (resultNum > 0)
                {
                    sqlStr = $@"update  [dbo].[HospitalOrganizationGrade] 
                                set [OrganizationGrade]={(int)param.OrganizationGrade},[UpdateTime]=GETDATE(),UpdateUserId='{param.UserId}'
                                where IsDelete=0 and HospitalId='{param.HospitalId}'";
                }
                else
                {
                    sqlStr = $@"INSERT INTO [dbo].[HospitalOrganizationGrade] (Id,HospitalId,[OrganizationGrade],[UpdateTime],[CreateUserId],IsDelete)
                                 values('{Guid.NewGuid()}','{param.HospitalId}',{(int)param.OrganizationGrade},GETDATE(),'{param.UserId}',0)";
                }
                await _sqlConnection.ExecuteAsync(sqlStr);

                _sqlConnection.Close();
            }
        }
        /// <summary>
        /// 获取医院等级
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<OrganizationGrade> QueryHospitalOrganizationGrade(string param)
        {
            var resultData = new OrganizationGrade();
            //医院等级
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                string sqlStr;
                string querySql =
                    $"select   OrganizationGrade from [dbo].[HospitalOrganizationGrade] where IsDelete=0 and HospitalId='{param}'";


                resultData= (OrganizationGrade)await _sqlConnection.QueryFirstAsync<int>(querySql) ;

                _sqlConnection.Close();
            }

            return resultData;
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
                var data = await _sqlConnection.QuerySingleAsync<QueryHospitalOperatorDto>(querySql);
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
                if (data != null && data.Count() > 0)
                {
                    resultData = data.ToList();
                }

                _sqlConnection.Close();
                return resultData;
            }
        }

        /// <summary>
        /// 添加操作日志
        /// </summary>
        /// <returns></returns>
        public async Task<int> AddHospitalLog(AddHospitalLogParam param)
        {
            using (var _sqlConnection = new SqlConnection(_connectionString))
            {
                
                _sqlConnection.Open();
                string querySql = $@"insert into  [dbo].[HospitalLog](
                                [Id]
                               ,[RelationId]
                               ,[JoinOrOldJson]
                               ,[ReturnOrNewJson]
                               ,[Remark]
                               ,[OrganizationCode]
                               ,[CreateTime]
                               ,[IsDelete]
                               ,[CreateUserId]
                              )
                             values('{Guid.NewGuid()}','{param.RelationId}','{param.JoinOrOldJson}','{param.ReturnOrNewJson}',
                              '{param.Remark}','{param.OrganizationCode}',GETDATE(),0,'{param.UserId}')";
                var data = await _sqlConnection.ExecuteAsync(querySql);
                _sqlConnection.Close();
                return data;
            }
        }
    }
}
