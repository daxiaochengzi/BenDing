/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Code;
using System;
using NFine.Domain.BenDing.Infrastructure;
using BenDing.Domain.Models.Dto.Web;

namespace NFine.Domain
{
    public class IEntity<TEntity>
    {
        public void Create()
        {
            var entity = this as ICreationAudited;
            entity.F_Id = Common.GuId();
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            if (LoginInfo != null)
            {
                entity.F_CreatorUserId = LoginInfo.UserId;
            }
            
            entity.F_CreatorTime = DateTime.Now;
        }
        //public void BenDingCreate(UserInfoDto user)
        //{
        //    var entity = this as IBenDingCreationAudited;
        //    if (entity != null)
        //    {
        //        entity.CreateUserId = user.UserId;
        //        entity.CreateTime = DateTime.Now;
               
        //    }
        //}
        //public void BenDingModify(UserInfoDto user,string id)
        //{
        //    var entity = this as IBenDingModificationAudited;
        //    if (entity != null)
        //    {
        //        entity.UpdateUserId = user.UserId;
        //        entity.UpdateTime = DateTime.Now;
        //        entity.Id = id;
              
        //    }
        //}
        //public void BenDingRemove(UserInfoDto user, string id)
        //{
        //    var entity = this as IBenDingDeleteAudited;
        //    if (entity != null)
        //    {
        //        entity.DeleteUserId = user.UserId;
        //        entity.DeleteTime = DateTime.Now;
        //        entity.IsDelete = true;
        //        entity.Id = id;
        //    }
        //}
        public void Modify(string keyValue)
        {
            var entity = this as IModificationAudited;
            entity.F_Id = keyValue;
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            if (LoginInfo != null)
            {
                entity.F_LastModifyUserId = LoginInfo.UserId;
            }
            entity.F_LastModifyTime = DateTime.Now;
        }
        public void Remove()
        {
            var entity = this as IDeleteAudited;
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            if (LoginInfo != null)
            {
                entity.F_DeleteUserId = LoginInfo.UserId;
            }
            entity.F_DeleteTime = DateTime.Now;
            entity.F_DeleteMark = true;
        }
    }
}
