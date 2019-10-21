
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Xml
{
   public static class BaseConnect
    {
        public static int Connect()
        {
          var data=  MedicalInsuranceDll.ConnectAppServer_cxjb("cpq2677", "888888");
            return data;
        }
        //public static void DifferentPlacesConnect()
        //{
        //    MedicalInsuranceDifferentPlaces.ConnectAppServer("cpq2677", "888888");
        //}


    }
}
