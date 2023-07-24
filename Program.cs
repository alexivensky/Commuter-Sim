using System;
using System.Numerics;
using System.Timers;
using Commuter_Sim;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using HotChocolate;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);




builder.Services
    .AddSingleton<Repository>()
    .AddGraphQLServer()
    .AddInMemorySubscriptions()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>();

var app = builder.Build();
app.UseRouting();
app.UseWebSockets();
app.MapGraphQL();

app.Run();

//GQLTimer g = new GQLTimer(1000);
//g.SetTimer();

