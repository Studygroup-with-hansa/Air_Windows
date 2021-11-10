using System;
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
        public static async Task<ResultModel<T>> RestRequest<T>(string endPoint, Method method, List<ParamModel> rParams)
        {
            var client = new RestClient(App.ServerUrl)
            {
                Timeout = 10000
            };
            var request = new RestRequest(endPoint, method);
            var requestParam = "";
            IRestResponse<ResultModel<T>> response;

            _ = request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            if (!string.IsNullOrEmpty(Settings.Default.Token))
                _ = request.AddHeader("Authorization", "Token " + Settings.Default.Token);

            if (method == Method.GET || method == Method.PUT || method == Method.DELETE)
            {
                rParams?.ForEach(e => request.AddQueryParameter(e.Key, e.Value));
            }
            else {
                rParams?.ForEach(e => requestParam += $"{e.Key}={e.Value}&");
                _ = request.AddParameter("application/x-www-form-urlencoded", requestParam,
                    ParameterType.RequestBody);
            }

            try
            {
                response = await client.ExecuteAsync<ResultModel<T>>(request);
            }
            catch (TimeoutException e)
            {
                throw e.GetBaseException();
            }
            catch (Exception)
            {
                return default;
            }

            return response.Data;
        }
    }
}