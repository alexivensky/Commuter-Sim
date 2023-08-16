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

/**
 * Commuter Simulator
 * by Alex Ivensky
 */


builder.Services
    .AddSingleton<Repository>()
    .AddGraphQLServer()
    .AddInMemorySubscriptions()
    .AddType<Train>()
    .AddType<Station>()
    .AddType<Track>()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddErrorFilter<GQLErrorFilter>();

WebApplication app = builder.Build();
app.UseRouting();
app.UseWebSockets();
app.MapGraphQL();

app.Run();


