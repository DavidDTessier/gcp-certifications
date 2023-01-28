using System;
using Constructs;
using HashiCorp.Cdktf;
using HashiCorp.Cdktf.Providers.Azurerm.ResourceGroup;
using HashiCorp.Cdktf.Providers.Azurerm.VirtualNetwork;

namespace VPNDemo.Stacks
{
	public class AzureStack : TerraformStack
    {
        private string az_resource_prefix = "az";
        private string az_location = "eastus";

        public AzureStack(Construct scope, string id) : base(scope, id)
        {
            var az_rg = new ResourceGroup(this, "rg",
              new ResourceGroupConfig { Name = $"{az_resource_prefix}-{Constants.PROJECT_NAME}", Location = az_location });

            new VirtualNetwork(this, "vnet", new VirtualNetworkConfig
            {
                Name = $"{az_resource_prefix}-{Constants.PROJECT_NAME}-gcp-prv"
            })
        }


    }
}

