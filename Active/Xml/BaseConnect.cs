using BenDing.Service;
using BenDingActive.Model.Params.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDingActive.Model.Dto.DifferentPlaces;
using BenDingActive.Service;

namespace BenDingActive.Xml
{
   public static class BaseConnect
    {
        public static void  Connect()
        {
            WorkersMedicalInsurance.ConnectAppServer_cxjb("cpq2677", "888888");
        }
        public static void DifferentPlacesConnect()
        {
            MedicalInsuranceDifferentPlaces.ConnectAppServer("cpq2677", "888888");
        }
        public static void SaveDataInfo(string dataType, string participationJson, string resultDataJson)
        {
            var Id = Guid.NewGuid().ToString("N");
            var sendParam = new MedicalInsuranceDataAllWebParam()
            {
                DataAllId = Id,
                BusinessId = Id,
                CreateUserId = "E075AC49FCE443778F897CF839F3B924",
                DataId = Id,
                DataType = dataType,
                HisMedicalInsuranceId = Id,
                IdCard = "6217212314001720070",
                OrgCode = "51072600000000000000000513435964",
                ParticipationJson = participationJson,
                Remark = "",
                ResultDataJson = resultDataJson

            };
            var data = HttpHelp.HttpPost(JsonConvert.SerializeObject(sendParam),
                "SaveMedicalInsuranceDataAll", new ApiJsonResultDatas());

        }

        public static DifferentPlacesDto GetDifferentPlaces()
        {
            var data = new DifferentPlacesDto()
                { };
            return data;
        }

    }
}
