using System;
using Constructs;
using HashiCorp.Cdktf;
using HashiCorp.Cdktf.Providers.Google.ProjectService;
using HashiCorp.Cdktf.Providers.Google.Project;
using HashiCorp.Cdktf.Providers.Google.Provider;

namespace VPNDemo.Stacks
{
	public class GCPStack : TerraformStack
    {
        private string gcp_resource_prefix = "gcp";
        private string gcp_location = "us-east-1";
        private string billing_id = "014C05-027CFB-2E6FD6";

        public GCPStack(Construct scope, string id) : base(scope, id)
        {
            new GoogleProvider(this, "provider", new GoogleProviderConfig
            {
                Region = "us-east-1",
                Zone = "us-east1-b"
            });

            var prj = new Project(this, "project", new ProjectConfig
            {
                Name = $"prj-{gcp_resource_prefix}-{Constants.PROJECT_NAME}",
                ProjectId = $"prj-{gcp_resource_prefix}-{Constants.PROJECT_NAME}-{Guid.NewGuid().ToString().Substring(0,6)}",
                BillingAccount = billing_id
            });

            new ProjectService(this, "compute-api", new ProjectServiceConfig
            {
                Service = "compute.googleapis.com",
                Project = prj.Name,
                Timeouts = new ProjectServiceTimeouts
                {
                    Create = "30m",
                    Delete = "40m",
                },
                DisableDependentServices = true,
                DisableOnDestroy = true
                
            });

            
        }
    }
}

