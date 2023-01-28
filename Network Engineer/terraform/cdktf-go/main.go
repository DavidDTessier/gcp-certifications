package main

import (
	"os"

	"github.com/aws/constructs-go/constructs/v10"
	jsii "github.com/aws/jsii-runtime-go"
	googleprovider "github.com/cdktf/cdktf-provider-google-go/google/v4/provider"
	storage "github.com/cdktf/cdktf-provider-google-go/google/v4/storagebucket"
	"github.com/hashicorp/terraform-cdk-go/cdktf"
)

func NewMyStack(scope constructs.Construct, id string) cdktf.TerraformStack {
	stack := cdktf.NewTerraformStack(scope, &id)

	cdktf.NewLocalBackend(stack, &cdktf.LocalBackendProps{
		Path: jsii.String("./tf.state"),
	})
	googleprovider.NewGoogleProvider(stack, jsii.String("google"), &googleprovider.GoogleProviderConfig{
		Zone:    jsii.String("us-central1-a"),
		Region:  jsii.String("us-central1"),
		Project: jsii.String("sec-eng-training"),
	})

	storage.NewStorageBucket(stack, jsii.String("bucket"), &storage.StorageBucketConfig{
		Name:     jsii.String("gcs-demo-dt-1234"),
		Location: jsii.String("us-central1"),
	})

	// The code that defines your stack goes here

	return stack
}

func main() {
	tempDir, err := os.MkdirTemp("", "magic-instance-maker-")
	if err != nil {
		return
	}
	defer os.RemoveAll(tempDir)

	app := cdktf.NewApp(&cdktf.AppOptions{Outdir: jsii.String("magic-instance-maker")})

	NewMyStack(app, "cdktf-go")

	app.Synth()
}
