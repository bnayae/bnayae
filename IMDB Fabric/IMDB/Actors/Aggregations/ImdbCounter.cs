﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IMDB.Interfaces;
using Microsoft.ServiceFabric;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Communication;

namespace IMDB
{
    /// <summary>
    /// Counter of Movie or Star
    /// the Actor Id expected to be ImdbType as long
    /// </summary>
    public class ImdbCounter : Actor<CounterState>, IImdbCounter
    {
        public override Task OnActivateAsync()
        {
            if (Id.Kind != ActorIdKind.String)
                throw new NotSupportedException("Id should be either the Star or Movie name ");
            return base.OnActivateAsync();
        }

        public Task<int> GetCount() => Task.FromResult(State.Count);

        public Task IncrementAsync(ImdbType type, Profile profile)
        {
            State.Count++;

            var rate = new ProfileRate(profile, State.Count);

            var id = new ActorId(type.ToString());
            var proxy = ActorProxy.Create<IImdbTopRated>(id);
            proxy.OferCandidateAsync(rate);

            return Task.CompletedTask;
        }
    }
}