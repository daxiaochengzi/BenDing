using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Enums;
using BenDing.Domain.Models.Params.SystemManage;
using BenDing.Domain.Models.Params.Web;
using BenDing.Repository.Interfaces.Web;
using Dapper;

namespace BenDing.Repository.Providers.Web
{
    public class SystemManageRepository : ISystemManageRepository
    {/// <summary>
     /// 
     /// </summary>
        private readonly string _connectionString;
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

        /// <param name="param"></param>
        /// <returns></returns>
        public void AddHospitalOperator(AddHospitalOperatorParam param)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {

                sqlConnection.Open();

                string querySql = $"select COUNT(*) from [dbo].[HospitalOperator] where [HisUserId]='{param.UserId}' ";
                var resultNum = sqlConnection.QueryFirst<int>(querySql);
                if (resultNum > 0)
                {
                    var updateSql = param.IsHis ? $"update [dbo].[HospitalOperator] set HisUserAccount='{param.UserAccount}',HisUserPwd='{param.UserPwd}',UpdateTime=GETDATE(),UpdateUserId='{param.UserId}',OrganizationCode='{param.OrganizationCode}',ManufacturerNumber='{param.ManufacturerNumber}' where [HisUserId]='{param.UserId}'"
                        : $"update [dbo].[HospitalOperator] set [MedicalInsuranceAccount]='{param.UserAccount}',[MedicalInsurancePwd]='{param.UserPwd}',UpdateTime=GETDATE(),UpdateUserId='{param.UserId}' where [HisUserId]='{param.UserId}'";
                    sqlConnection.Execute(updateSql);
                }
                else
                {
                    string insertSql;
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
                    sqlConnection.Execute(insertSql);

                }
            }
        }
        /// <summary>
        /// 设置医院等级
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public void AddHospitalOrganizationGrade(HospitalOrganizationGradeParam param)
        {
            //医院等级
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string sqlStr;
                string querySql =
                    $"select COUNT(*) from [dbo].[HospitalOrganizationGrade] where [HospitalId]='{param.HospitalId}' ";
                var resultNum = sqlConnection.QueryFirst<int>(querySql);
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
                sqlConnection.Execute(sqlStr);

                sqlConnection.Close();
            }
        }
        /// <summary>
        /// 获取医院等级
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public OrganizationGrade QueryHospitalOrganizationGrade(string param)
        {
            OrganizationGrade resultData;
            //医院等级
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string querySql =
                    $"select   OrganizationGrade from [dbo].[HospitalOrganizationGrade] where IsDelete=0 and HospitalId='{param}'";
                resultData = (OrganizationGrade)sqlConnection.QueryFirst<int>(querySql);
                sqlConnection.Close();
            }

            return resultData;
        }
        /// <summary>
        /// 操作员登陆信息查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public QueryHospitalOperatorDto QueryHospitalOperator(QueryHospitalOperatorParam param)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var resultData = new QueryHospitalOperatorDto();
                sqlConnection.Open();
                string querySql = $"select top 1 MedicalInsuranceAccount,[MedicalInsurancePwd],[HisUserAccount],[HisUserPwd],ManufacturerNumber from [dbo].[HospitalOperator] where [HisUserId]='{param.UserId}' ";
                var data = sqlConnection.QueryFirstOrDefault<QueryHospitalOperatorDto>(querySql);
                if (data != null)
                {
                    resultData = data;
                }

                sqlConnection.Close();
                return resultData;
            }
        }
        /// <summary>
        /// 获取所有的操作人员
        /// </summary>
        /// <returns></returns>
        public List<QueryHospitalOperatorAll> QueryHospitalOperatorAll()
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var resultData = new List<QueryHospitalOperatorAll>();
                sqlConnection.Open();
                string querySql = @"select HisUserId,[HisUserName] from [dbo].[HospitalOperator]";
                var data = sqlConnection.Query<QueryHospitalOperatorAll>(querySql);
                if (data != null && data.Any())
                {
                    resultData = data.ToList();
                }

                sqlConnection.Close();
                return resultData;
            }
        }

        /// <summary>
        /// 添加操作日志
        /// </summary>
        /// <returns></returns>
        public int AddHospitalLog(AddHospitalLogParam param)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {

                sqlConnection.Open();
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
                var data = sqlConnection.Execute(querySql);
                sqlConnection.Close();
                return data;
            }
        }
    }
}
