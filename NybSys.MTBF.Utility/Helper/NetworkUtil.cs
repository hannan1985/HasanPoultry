// Creator :

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace NybSys.MTBF.Utility.Helper
{
    public sealed class NetworkUtil 
    {

        /// <summary>
        /// To get Network Information.
        /// </summary>
        /// <returns></returns>
        public NetworkInfo GetNetworkInfo()
        {
            NetworkInfo netinfo = new NetworkInfo();

            foreach (NetworkInterface inter in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (inter.OperationalStatus == OperationalStatus.Up)
                {
                    if (inter.NetworkInterfaceType == NetworkInterfaceType.Ethernet | inter.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                    {
                        netinfo.InterfaceName = inter.Name;
                        netinfo.InterfaceDescription = inter.Description;
                        netinfo.MACAddress = inter.GetPhysicalAddress().ToString();
                        netinfo.Protocol = "Web client";
                        netinfo.GatewayAddress = "0.0.0.0";
                        netinfo.publicIpAddress = "";
                        netinfo.IPAddress = "";
                        netinfo.HostName = "";

                        return netinfo;
                    }
                }
            }

            return netinfo;
        }


    }


}
