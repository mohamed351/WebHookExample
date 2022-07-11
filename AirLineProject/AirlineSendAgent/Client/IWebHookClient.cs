using AirlineSendAgent.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineSendAgent.Client
{
    public interface IWebHookClient
    {

        Task SendWebHookNotification(FlightDetailUpdateDTO flightDetailUpdate);
    }
}
