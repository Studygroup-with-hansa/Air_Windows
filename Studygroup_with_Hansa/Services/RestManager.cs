using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using Studygroup_with_Hansa.Models;
using Studygroup_with_Hansa.Properties;

namespace Studygroup_with_Hansa.Services
{
    public class RestManager
    {
        /// <summary>
        ///     RestAPI Manager
        /// </summary>
        /// <typeparam name="T">Request Type</typeparam>
        /// <param name="endPoint">Request EndPoint</param>
        /// <param name="method">Request Method</param>
        /// <param name="rParams">Request Parameters</param>
        /// <returns>Request Result</returns>
        public static Task<IRestResponse<T>> RestRequest<T>(string endPoint, Method method, List<ParamModel> rParams)
        {
            var client = new RestClient(App.ServerUrl)
            {
                Timeout = -1
            };
            var request = new RestRequest(endPoint, method);
            var requestParam = "";

            _ = request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            if (!string.IsNullOrEmpty(Settings.Default.Token))
                _ = request.AddHeader("Authorization", Settings.Default.Token);

            rParams.ForEach(e => requestParam += $"{e.Key}={e.Value}&");
            _ = request.AddParameter("application/x-www-form-urlencoded", requestParam,
                ParameterType.RequestBody);

            return client.ExecuteAsync<T>(request);
        }
    }
}