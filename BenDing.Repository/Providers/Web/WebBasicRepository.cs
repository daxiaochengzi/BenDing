using System;
using System.ServiceModel;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Repository.Interfaces.Web;
using BenDing.Repository.ServiceReference;
using Newtonsoft.Json;

namespace BenDing.Repository.Providers.Web
{
    public class WebBasicRepository : IWebBasicRepository
    {
        public BasicResultDto HIS_InterfaceList(string tradeCode, string inputParameter )
        {

            //11008
            var result = new BasicResultDto();
            // 创建 HTTP 绑定对象与设置最大传输接受数量
            var binding = new BasicHttpBinding { MaxReceivedMessageSize = 2147483647 };
            // 根据 WebService 的 URL 构建终端点对象
            var endpoint = new EndpointAddress("http://47.111.29.88:11013/WebService.asmx");
            // 创建调用接口的工厂，注意这里泛型只能传入接口 添加服务引用时生成的 webservice的接口 一般是 (XXXSoap)
            var factory = new ChannelFactory<WebServiceSoap>(binding, endpoint);
            // 从工厂获取具体的调用实例 
            var callClient = factory.CreateChannel();
            //var paramIni = new ExecuteSPRequest(new ExecuteSPRequestBody() {param = param});
            string resultData = callClient.HIS_Interface(tradeCode, inputParameter);
            if (!string.IsNullOrEmpty(resultData))
            {
                var resultDto = JsonConvert.DeserializeObject<ResultDataDto>(resultData);

                if (resultDto.Result == "0")
                {
                    throw new Exception(resultDto.Msg);
                }

                var basicResultDto = JsonConvert.DeserializeObject<BasicResultDto>(resultData);
                result = basicResultDto;


                return result;
            }

            //异步执行一些任务
            //return resultData.Body.ExecuteSPResult;
            //var account = JsonConvert.DeserializeObject<Account>(json);
            return result;



        }

        public ResultDataDto HIS_Interface(string tradeCode, string inputParameter)
        {

            //11008
            var result = new ResultDataDto();
            // 创建 HTTP 绑定对象与设置最大传输接受数量
            var binding = new BasicHttpBinding { MaxReceivedMessageSize = 2147483647 };
            // 根据 WebService 的 URL 构建终端点对象
            var endpoint = new EndpointAddress("http://47.111.29.88:11013/WebService.asmx");
            // 创建调用接口的工厂，注意这里泛型只能传入接口 添加服务引用时生成的 webservice的接口 一般是 (XXXSoap)
            var factory = new ChannelFactory<WebServiceSoap>(binding, endpoint);
            // 从工厂获取具体的调用实例 
            var callClient = factory.CreateChannel();
            //var paramIni = new ExecuteSPRequest(new ExecuteSPRequestBody() {param = param});
            string resultData = callClient.HIS_Interface(tradeCode, inputParameter);
            if (!string.IsNullOrEmpty(resultData))
            {
                var resultDto = JsonConvert.DeserializeObject<ResultDataDto>(resultData);

                if (resultDto.Result == "0")
                {
                    throw new Exception(resultDto.Msg);
                }
                result = resultDto;
                return result;
            }

            //异步执行一些任务
            //return resultData.Body.ExecuteSPResult;
            //var account = JsonConvert.DeserializeObject<Account>(json);
            return result;



        }
    }
}
