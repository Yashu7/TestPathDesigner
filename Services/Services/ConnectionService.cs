using System.Net;
using System.Net.Http;

namespace Services
{
    public class ConnectionService : IConnectionService
    {
        public bool IsConnected(string serviceName, string serviceAddress)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(serviceAddress);
            try
            {
                httpClient.Send(new HttpRequestMessage(HttpMethod.Head, ""));
            }
            catch(HttpRequestException ex)
            {
                return false;
            }
            return true;
        }

        public bool IsConnected(string serviceAddress)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(serviceAddress);
            try
            {
                httpClient.Send(new HttpRequestMessage(HttpMethod.Head, ""));
            }
            catch (HttpRequestException ex)
            {
                return false;
            }
            return true;
        }
    }
}
