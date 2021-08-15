# TLPokemon
T r u e Layer code challenge

## Quickly running the solution

1. Install Visual Studio Community Edition 2019. https://visualstudio.microsoft.com/thank-you-downloading-visual-studio/?sku=Community&rel=16
	- Make sure ASP.NET and web development feature is selected.
2. Open the project file at src\TLPokemon.Api\TLPokemon.Api.sln
	- If you don't have required framework it will ask you to download the required .NET framework.
	- If Docker is not installed on your machine Visual Studio will ask you to install it and run it with Linux containers mode.
		- It will ask you to install Windows subsystem for Linux https://docs.microsoft.com/en-us/windows/wsl/install-win10#step-4---download-the-linux-kernel-update-package
		- Make sure Hypervisor is installed and enabled on your machine https://docs.microsoft.com/en-us/virtualization/hyper-v-on-windows/quick-start/enable-hyper-v
3. Make sure the default debugging profile is Docker.
	- On Solution Explorer, right click on TLPokemon.Api project and select Properties
	- On Property window select Debug tab. On the Profile and Launch properties select Docker.
4. Hit F5 or click on the Run button on the toolbar.
	- This runs the API service on the background.
	- It starts a browser window with Swagger UI loaded.
5. Use Swagger UI to try the service.
	- Expand the endpoint you like to try.
	- Click Try it out button, the input fields will be enabled.
	- Enter Pokemon's name.
	- Click Execute to run the service
    - Inspect the Response.


## Production Considerations

- Implement an application level error handling and logging
- Implement retry logic on third party services consumption - possibly Polly https://github.com/App-vNext/Polly
- Implement authentication and/or authorisation e.g. OpenID / OAuth 2.0
- Refactor into separate projects as the abstraction complexity grows e.g. deploying as microservice.
- More testing e.g. load testing, performance benchmarking
- Improve documentation for consumers - apply Swagger/OpenAPI documentation https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-5.0
- Deploy behind cloud edge solutions to gain more features e.g. Cloudflare - DDOS protection, rate limiting, caching.
- Fun translation api_key should be stored in a secure store depends on the CI/CD pipeline.