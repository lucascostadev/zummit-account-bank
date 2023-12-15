# Introduction 
You have been asked to develop a service to automatically convert values between currencies. The
application calling this service requires two modes of operation:
1. On-demand, between a specific pair of currencies. For example, from USD to NZD; and
2. In bulk, for all stale calculations in the system.
For on-demand requests, it is expected that most calls take less than a millisecond. For bulk calculations,
the system will need to potentially update millions of records once a day and should be able to do so in
minutes. The primary bottleneck for bulk operations is expected to be database update speed.
You are free to use any external exchange rate API, but the public feed produced by the European Central
Bank is recommended and can be read from here: https://www.ecb.europa.eu/stats/eurofxref/eurofxrefdaily.xml 

# Getting Started
1. How we need to run ?
	Only .NET framework
2. How run project ?
	Go to root folder and run `dotnet run`
3. In root folder we have a postman collection for tests.
4. How we view all de endpoints ?
	Go to `https://localhost:7057/swagger/index.html`