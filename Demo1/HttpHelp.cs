using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using zipkin4net.Transport.Http;

namespace Demo1
{
    public class HttpHelp
    {
        public async Task<string> GetAsync(string url, List<KeyValuePair<string, string>> headers = null,
            int timeout = 0, Encoding encoding = null)
        {
            using (HttpClient client = new HttpClient(new TracingHandler("demo1")))
            {
                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> header in headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }
                if (timeout > 0)
                {
                    client.Timeout = new TimeSpan(0, 0, timeout);
                }
                Byte[] resultBytes = await client.GetByteArrayAsync(url);
                return Encoding.Default.GetString(resultBytes);
            }
        }
    }
}
