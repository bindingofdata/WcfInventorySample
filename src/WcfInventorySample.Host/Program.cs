using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using WcfInventorySample.Contracts;
using WcfInventorySample.Service;

namespace WcfInventorySample.Host
{
    /// <summary>
    /// Self-host console app for the inventory service. The same service is
    /// exposed twice: BasicHttpBinding (interoperable SOAP, for the ASP.NET
    /// web client) and NetTcpBinding (fast binary, for the WPF client).
    /// Note: if the HTTP address fails with an access-denied error, run
    /// Visual Studio as administrator once, or reserve the URL with:
    /// netsh http add urlacl url=http://+:8000/InventoryService user=Everyone
    /// </summary>
    class Program
    {
        static void Main()
        {
            var httpBase = new Uri("http://localhost:8000/InventoryService");
            var tcpBase = new Uri("net.tcp://localhost:8001/InventoryService");

            using (var host = new ServiceHost(typeof(InventoryService), httpBase, tcpBase))
            {
                host.AddServiceEndpoint(typeof(IInventoryService), new BasicHttpBinding(), string.Empty);
                host.AddServiceEndpoint(typeof(IInventoryService), new NetTcpBinding(), string.Empty);
                host.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });

                try
                {
                    host.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to start the service: " + ex.Message);
                    return;
                }

                Console.WriteLine("InventoryService is running.");
                Console.WriteLine("  SOAP (BasicHttp): http://localhost:8000/InventoryService");
                Console.WriteLine("  TCP  (NetTcp)  : net.tcp://localhost:8001/InventoryService");
                Console.WriteLine("Press ENTER to stop.");
                Console.ReadLine();
                host.Close();
            }
        }
    }
}
