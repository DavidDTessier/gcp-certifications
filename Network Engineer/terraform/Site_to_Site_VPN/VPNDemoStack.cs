using System;
using Constructs;
using HashiCorp.Cdktf;
using HashiCorp.Cdktf.Providers.Azurerm.VirtualNetwork;
using HashiCorp.Cdktf.Providers.Azurerm.ResourceGroup;
using VPNDemoTerraform.TF_Stack;


namespace VPNDemoTerraform
{
  
    class VPNDemoStack : TerraformStack
    {
       
        private string gcp_resource_prefix = "gcp";
        private string project_name = "vpn-demo";
   
        private string gcp_location = "us-east-1";

        public VPNDemoStack(Construct scope, string id) : base(scope, id)
        {
            var az_stack = new AzureStack(scope, id, project_name);
        }
    }
}