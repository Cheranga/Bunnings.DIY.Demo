# :zap: Azure Functions Build and Deployment

## :rocket: Goals

> :bulb: Modularized Deployments Using Azure Bicep and GitHub Actions

The team needs to have full responsibility about components deployed and why. If there are issues in a deployment the team should be able to easily 
identify where the bottleneck is.

> :bulb: To Locally Verify Build Automation To Avoid Unnecessary Build Failures

Build automation tasks can vary depending on the component you build. It's always better to run all the build tasks locally and verify whether it succeeds before pushing
the code.
