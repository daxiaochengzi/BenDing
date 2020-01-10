using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using BenDing.Domain.Models.Dto.SystemManage;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Enums;
using BenDing.Domain.Models.Params.SystemManage;
using BenDing.Domain.Models.Params.Web;
using BenDing.Repository.Interfaces.Web;
using Dapper;
using NFine.Code;

namespace BenDing.Repository.Providers.Web
{
    public class SystemManageRepository : ISystemManageRepository
    {/// <summary>
     /// 
     /// </summary>
        private readonly string _connectionString;
        private readonly Log _log;
        /// <summary>
        /// 构造函数
        /// </summary>

        public SystemManageRepository()
        {
            _log = LogFactory.GetLogger("ini".GetType().ToString());
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
                string insertSql=null;
                try
                {
                    sqlConnection.Open();
                    string querySql = $"select COUNT(*) from [dbo].[HospitalOperator] where [HisUserId]='{param.UserId}' ";
                    var resultNum = sqlConnection.QueryFirst<int>(querySql);
                    if (resultNum > 0)
                    {
                         insertSql =
                            $"update [dbo].[HospitalOperator] set HisUserAccount='{param.UserAccount}',HisUserPwd='{param.UserPwd}',UpdateTime=GETDATE(),UpdateUserId='{param.UserId}',OrganizationCode='{param.OrganizationCode}',ManufacturerNumber='{param.ManufacturerNumber}' where [HisUserId]='{param.UserId}'";
                    }
                    else
                    {
                            insertSql = $@"
                                   INSERT INTO [dbo].[HospitalOperator]
                                   ([Id] ,[FixedEncoding],[HisUserId],ManufacturerNumber,
                                   [HisUserAccount],[HisUserPwd],[CreateTime],[CreateUserId],[HisUserName],[IsDelete]
                                   )
                             VALUES('{Guid.NewGuid()}','{BitConverter.ToInt64(Guid.Parse(param.UserId).ToByteArray(), 0)}','{param.UserId}','{param.ManufacturerNumber}',
                                     '{param.UserAccount}','{param.UserPwd}',GETDATE(),'{param.UserId}','{param.HisUserName}',0)";
                    }
                    sqlConnection.Execute(insertSql);
                }
                catch (Exception e)
                {
                    _log.Debug(insertSql);
                    throw new Exception(e.Message);
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
                string sqlStr=null;
                try
                {
                    sqlConnection.Open();
                 
                    string querySql =
                        $"select COUNT(*) from [dbo].[HospitalOrganizationGrade] where [HospitalId]='{param.HospitalId}' ";
                    var resultNum = sqlConnection.QueryFirst<int>(querySql);
                    if (resultNum > 0)
                    {
                        sqlStr = $@"update  [dbo].[HospitalOrganizationGrade] 
                                set [OrganizationGrade]={(int)param.OrganizationGrade},[UpdateTime]=GETDATE(),AdministrativeArea='{param.AdministrativeArea}',UpdateUserId='{param.UserId}',
                                [MedicalInsuranceAccount] ='{param.MedicalInsuranceAccount}',[MedicalInsurancePwd]='{param.MedicalInsurancePwd}'
                                where IsDelete=0 and HospitalId='{param.HospitalId}'";
                    }
                    else
                    {
                        sqlStr = $@"INSERT INTO [dbo].[HospitalOrganizationGrade] (Id,HospitalId,[OrganizationGrade],[UpdateTime],[CreateUserId],IsDelete,[AdministrativeArea],[MedicalInsuranceAccount],[MedicalInsurancePwd])
                                 values('{Guid.NewGuid()}','{param.HospitalId}',{(int)param.OrganizationGrade},GETDATE(),'{param.UserId}',0,'{param.AdministrativeArea}','{param.MedicalInsuranceAccount}','{param.MedicalInsurancePwd}')";
                    }
                    sqlConnection.Execute(sqlStr);

                    sqlConnection.Close();
                }
                catch (Exception e)
                {
                    _log.Debug(sqlStr);
                    throw new Exception(e.Message);
                }
                
            }
        }
        /// <summary>
        /// 获取医院信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public HospitalOrganizationGradeDto QueryHospitalOrganizationGrade(string param)
        {
          
            //医院等级
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string querySql =
                    $"select   OrganizationGrade,AdministrativeArea,MedicalInsuranceAccount,MedicalInsurancePwd from [dbo].[HospitalOrganizationGrade] where IsDelete=0 and HospitalId='{param}'";
               var resultData = sqlConnection.QueryFirstOrDefault<HospitalOrganizationGradeDto>(querySql);
                sqlConnection.Close();
                if (resultData==null)throw  new Exception("当前医院未设置等级,请重新设置");
                if (resultData.MedicalInsuranceAccount == null) throw new Exception("当前医院未设置医保账号");
                return resultData;
            }

           
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
                string querySql = $"select top 1 [HisUserAccount],[HisUserPwd],ManufacturerNumber from [dbo].[HospitalOperator] where [HisUserId]='{param.UserId}' ";
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
                
                sqlConnection.Open();
                string querySql = @"select HisUserId,[HisUserName] from [dbo].[HospitalOperator]";
                var data = sqlConnection.Query<QueryHospitalOperatorAll>(querySql).ToList();
                sqlConnection.Close();
                return data;
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
                string querySql = null;
                try
                {
                    sqlConnection.Open();
                         querySql = $@"insert into  [dbo].[HospitalLog](
                                [Id]
                               ,[RelationId]
                               ,[JoinOrOldJson]
                               ,[ReturnOrNewJson]
                               ,[Remark]
                               ,[OrganizationCode]
                               ,[OrganizationName]
                               ,[CreateTime]
                               ,[IsDelete]
                               ,[CreateUserId]
                              )
                             values('{Guid.NewGuid()}','{param.RelationId}','{param.JoinOrOldJson}','{param.ReturnOrNewJson}',
                              '{param.Remark}','{param.User.OrganizationCode}','{param.User.OrganizationName}',getDate(),0,'{param.User.UserId}')";
                    var data = sqlConnection.Execute(querySql);
                    sqlConnection.Close();
                    return data;
                }
                catch (Exception e)
                {
                    _log.Debug(querySql);
                    throw new Exception(e.Message);
                }
            }
        }
    }
}
