using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interface
{
    public interface IConnectionService
    {
        public bool IsConnected(string serviceName, string serviceAddress);
    }
}
