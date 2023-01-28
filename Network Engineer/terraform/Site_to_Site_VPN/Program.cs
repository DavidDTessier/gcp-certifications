using System;
using Constructs;
using HashiCorp.Cdktf;
using HashiCorp.Cdktf.Providers.Google.Project;
using HashiCorp.Cdktf.Providers.Google.Provider;



namespace VPNDemoTerraform
{
    class Program
    {
        class GCPStack : TerraformStack
        {
            private string gcp_resource_prefix = "gcp";
            private string gcp_location = "us-east-1";
            private string billing_id = "";

            public GCPStack(Construct scope, string id) : base(scope, id)
            {
                /* new CloudBackend(this, new CloudBackendProps
                 {
                     Hostname = Constants.TF_APP_HOSTNAME,
                     Organization = Constants.TF_ORG,
                     Workspaces = new NamedCloudWorkspace(Constants.TF_WORKSPACE_NAME)
                 });
                */
                new GoogleProvider(scope, "provider", new GoogleProviderConfig
                {
                    Region = "us-east-1",
                    Zone = "us-east1-b"
                });

                new Project(scope, "project", new ProjectConfig
                {
                    Name = $"prj-{gcp_resource_prefix}-{Constants.PROJECT_NAME}",
                    ProjectId = $"prj-{gcp_resource_prefix}-{Constants.PROJECT_NAME}-",
                    //BillingAccount = billing_id
                });
            }
        }

        public static void Main(string[] args)
        {
            App app = new App();

            //var az_stack = new AzureStack(app, $"az-{Constants.PROJECT_NAME}");
            new GCPStack(app, $"gcp-{Constants.PROJECT_NAME}");
            app.Synth();
            Console.WriteLine("App synth complete");
        }
    }
}
