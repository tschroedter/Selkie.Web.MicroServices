using System.Diagnostics.CodeAnalysis;
using NLog;
using Topshelf;
using Topshelf.HostConfigurators;
using Topshelf.Nancy;
using Topshelf.ServiceConfigurators;

namespace Selkie.Web.MicroServices.BestTrail.SelfHosting
{
    [ExcludeFromCodeCoverage]
    internal class Host
    {
        internal Host(Logger logger,
                      int portNumber)
        {
            m_Logger = logger;
            m_PortNumber = portNumber;
        }

        private readonly Logger m_Logger;
        private readonly int m_PortNumber;

        internal void CreateHost()
        {
            string fullName = typeof( Service ).FullName;

            m_Logger.Info("Service '{0}' will be listening on http://localhost:{1}",
                          fullName,
                          m_PortNumber);

            Topshelf.Host host = HostFactory.New(x =>
                                                 {
                                                     x.Service <Service>(s =>
                                                                         {
                                                                             CreateAndConfigureService(s,
                                                                                                       x);
                                                                         });
                                                     x.StartAutomatically();
                                                     x.SetServiceName(fullName);
                                                     x.SetDisplayName(fullName);
                                                     x.RunAsNetworkService();
                                                 });

            host.Run();
        }

        private void CreateAndConfigureService(
            ServiceConfigurator <Service> s,
            HostConfigurator x)
        {
            s.ConstructUsing(
                             settings =>
                             new Service());
            s.WhenStarted(service => service.Start());
            s.WhenStopped(service => service.Stop());
            s.WithNancyEndpoint(x,
                                c =>
                                {
                                    c.AddHost(port : m_PortNumber);
                                    c.CreateUrlReservationsOnInstall
                                        ();
                                });
        }
    }
}