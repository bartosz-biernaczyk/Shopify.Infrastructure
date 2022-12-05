# Shopify.Consul

## Introduction

**Shopify.Consul** is a service-discovery library that enables simple integration of WebApi with [Consul](https://www.consul.io/). 

This is a part of `Shopify` project infrastructure.

## Getting started

The library provides two configuration approaches to help the user to go through the configuration process to enable service discovery in your WebApi.

### Fluent approach

Thanks to the `IServiceCollection` extension, the library allows the user to go through the configuration using **fluent** and **guided** configuration-builder pattern.

Example:
```cs
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConsul(builder =>
    builder.Enable(new Uri("http://localhost:8500")) // Consul URI
    .WithAddress("localhost") // The address of the service
    .WithName("testService") // The default service name used for discovery 
    .WithPort(5210) // Port of the service
    .UseHttpClient() // Enables using HttpClientFactory with default key
    .WithPingEndpoint("http://host.docker.internal:5210/test") // Endpoint for checking the service health
    .WithTimeSettings(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(1), TimeSpan.FromMinutes(1))); // Timeout configurations

var app = builder.Build();
```

### Options-based approach

You can also integrate `Consul` service-discovery features by adding a new section into your `*.json` settings.

```json
"Consul": {
    "Enabled": true,
    "ConsulUrl": "http://localhost:8500",
    "ServiceName": "testService",
    "ServiceAddress": "localhost",
    "Port": 5210,
    "Tags": [ "tag1", "tag2" ],
    "PingUrl": "http://host.docker.internal:5210/test",
    "PingInterval": "00:00:10",
    "Timeout": "00:00:01",
    "DeregisterAfter": "00:01:00",
    "UseHttpClient": true,
    "ClientKey": "consul"
  }
```

After adding the configuration section, enable service discovery features using `IServiceCollection` extensions.

```cs
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConsul();

var app = builder.Build();
```