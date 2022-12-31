using System;
using Constructs;
using HashiCorp.Cdktf;


namespace VPNDemoTerraform
{
    class Program
    {
        const string TF_WORKSPACE_NAME = "AzToGCP_VPN_CDK_Demo";


        public static void Main(string[] args)
        {
            App app = new App();
            var az_stack = new VPNDemoStack (app, TF_WORKSPACE_NAME);
            new CloudBackend(az_stack, new CloudBackendProps { Hostname = "app.terraform.io", Organization = "DavidTessierOrg", Workspaces = new NamedCloudWorkspace(TF_WORKSPACE_NAME) });
            app.Synth();
            Console.WriteLine("App synth complete");
        }
    }
}