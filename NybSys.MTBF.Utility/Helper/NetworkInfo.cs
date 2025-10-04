using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace NybSys.MTBF.Utility.Helper
{
    public class NetworkInfo 
    {

        private string _IPAddress = string.Empty;
        private string _MACAddress = string.Empty;
        private string _HostName = string.Empty;

        /// <summary>
        /// Get the value of IPAddress
        /// </summary>
        public string IPAddress
        {
            get { return _IPAddress; }
            set { _IPAddress = value; }
        }

        /// <summary>
        /// Get the value of MACAddress
        /// </summary>
        public string MACAddress
        {
            get { return _MACAddress; }
            set { _MACAddress = value; }
        }

        /// <summary>
        /// Get the value of HostName
        /// </summary>
        public string HostName
        {
            get { return _HostName; }
            set { _HostName = value; }
        }

        private string _InterfaceName;

        /// <summary>
        /// Get the value of InterfaceName
        /// </summary>
        public string InterfaceName
        {
            get { return _InterfaceName; }
            set { _InterfaceName = value; }
        }

        private string _InterfaceDescription;

        /// <summary>
        /// Get the value of InterfaceDescription
        /// </summary>
        public string InterfaceDescription
        {
            get { return _InterfaceDescription; }
            set { _InterfaceDescription = value; }
        }

        private string _protocol;

        /// <summary>
        /// Get the value of Protocol
        /// </summary>
        public string Protocol
        {
            get { return _protocol; }
            set { _protocol = value; }
        }

        private string _gatewayAddress;

        /// <summary>
        /// Get the value of GatewayAddress
        /// </summary>
        public string GatewayAddress
        {
            get { return _gatewayAddress; }
            set { _gatewayAddress = value; }
        }

        private string _PublicIPaddress;

        /// <summary>
        /// Get the value of publicIpAddress
        /// </summary>
        public string publicIpAddress
        {
            get { return _PublicIPaddress; }
            set { _PublicIPaddress = value; }
        }

    }

    }
