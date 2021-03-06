﻿using Ghost.MVC.Models.Dtos;
using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace Ghost.MVC.Controllers
{
    public static class Utilities
    {
        /// <summary>
        /// 
        /// </summary>
        public static GameStateModel NextMove(GameStateModel state)
        {
            var nextState = PostData<GameStateModel, GameStateModel>(Properties.Settings.Default.nextMoveResource, state);
            return nextState;
        }

        /// <summary>
        /// 
        /// </summary>
        public static GameAnalysisModel Analize(GameStateModel state)
        {
            var analysis = PostData<GameAnalysisModel, GameStateModel>(Properties.Settings.Default.analyseResource, state);
            return analysis;
        }

        /// <summary>
        /// Helper to do POST
        /// </summary>
        /// <typeparam name="P">the type of the returned data</typeparam>
        /// <typeparam name="Q">the type of the sent data</typeparam>
        /// <param name="resourcePath">the API end-point, for example "/api/resource"</param>
        /// <param name="data">the data sent</param>
        public static P PostData<P, Q>(string resourcePath, Q data)
        {
            var result = default(P);

            var url = Properties.Settings.Default.ApiUrl + resourcePath;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(data);

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            if (IsSuccessStatusCode(httpResponse.StatusCode))
            {
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var responseBody = streamReader.ReadToEnd();
                    result = JsonConvert.DeserializeObject<P>(responseBody);
                }
            }

            return result;
        }

        #region Private
        private static bool IsSuccessStatusCode(HttpStatusCode code)
        {
            return ((int)code >= 200) && ((int)code <= 299);
        }        
        #endregion
    }
}
