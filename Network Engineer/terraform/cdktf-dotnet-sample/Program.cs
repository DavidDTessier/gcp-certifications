using System;
using Constructs;
using HashiCorp.Cdktf;
using HashiCorp.Cdktf.Providers.Google.Provider;
using HashiCorp.Cdktf.Providers.Google.Project;
using HashiCorp.Cdktf.Providers.Google.StorageBucket;
using VPNDemo.Stacks;

namespace MyCompany.MyApp
{
    class Program
    {
      

        public static void Main(string[] args)
        {
            App app = new App();
            new GCPStack(app, "google");
            app.Synth();
            Console.WriteLine("App synth complete");
        }
    }
}