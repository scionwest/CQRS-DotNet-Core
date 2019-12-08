# Getting Started
The FocusMark.Api.Projects Lambda is a serverless function built to handle incoming HTTP Requests from Amazon's API Gateway using the CQRS and Event Source patterns.

# Deployment
To get started, you will need to deploy a Lambda layer containing all of the dependencies of the project. To do this, execute the following from the project root folder.

`dotnet lambda publish-layer FocusMarkApiProjectsLayer --layer-type runtime-package-store --s3-bucket focusmark-dev-deployments`

For non-development environments, change the S3 bucket to a deployment bucket dedicated for the environment you are deploying into.

Next, run the following command to deploy the Lambda.

`dotnet lambda deploy-serverless`

This will deploy the serverless application using the `aws-lambda-tools-defaults.json` configuration, along with the `serverless.template` CloudFormation. You may pass the `--config-file aws-lambda-tools-prod.json` argument into the CLI in order to deploy into production, or substitute the json config file with any other config file setup for a different environment.