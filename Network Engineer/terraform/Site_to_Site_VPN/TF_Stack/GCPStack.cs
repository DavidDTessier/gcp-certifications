using System;
using Constructs;
using HashiCorp.Cdktf;
using HashiCorp.Cdktf.Providers.Google.Provider;
using HashiCorp.Cdktf.Providers.Google.Project;


namespace VPNDemoTerraform.TF_Stack
{
	public class GCPStack : TerraformStack
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
				Zone = "us-east1-a"
            });

			new Project(scope, "project", new ProjectConfig
			{
				Name = $"prj-{gcp_resource_prefix}-{Constants.PROJECT_NAME}",
				ProjectId = $"prj-{gcp_resource_prefix}-{Constants.PROJECT_NAME}-",
                //BillingAccount = billing_id
            });
		}
	}
}

