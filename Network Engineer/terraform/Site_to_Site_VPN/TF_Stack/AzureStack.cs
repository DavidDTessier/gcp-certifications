using System;
using Constructs;
using HashiCorp.Cdktf;
using HashiCorp.Cdktf.Providers.Azurerm.ResourceGroup;
using HashiCorp.Cdktf.Providers.Azurerm.VirtualNetwork;

namespace VPNDemoTerraform.TF_Stack
{
	public class AzureStack : TerraformStack
	{
        private string az_resource_prefix = "az";
        private string az_location = "eastus";

        public AzureStack(Construct scope, string id, string project_name) : base(scope, id)
        {
            var az_rg = new ResourceGroup(scope, "rg",
              new ResourceGroupConfig { Name = $"{az_resource_prefix}-{project_name}", Location = az_location });
            var az_vnet = new VirtualNetwork(scope, "az-demo-vnet", new VirtualNetworkConfig
            {

            });


        }
    }
}

