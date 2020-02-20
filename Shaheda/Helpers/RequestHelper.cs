using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Shaheda.Commands;

namespace Shaheda.Helpers
{
    public static class RequestHelper
    {
        public static T SendRequest<T>(string url)
        {

            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException()).ReadToEnd();
            return JsonConvert.DeserializeObject<T>(responseString);
        }

        public static void PostCommand(CommandBase command, string url)
        {
            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            string jsonData = JsonConvert.SerializeObject(command);
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.ContentType = "application/Json";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse();
            httpResponse.Close();
        }
    }
}