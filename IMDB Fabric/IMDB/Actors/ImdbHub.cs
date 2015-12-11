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
    public class ImdbHub : StatelessActor, IImdbHub
    {
        public Task SendStarAsync(Star data)
        {
            ActorEventSource.Current.ActorMessage(this, data.Name);

            // Clients can listen for the event 
            // (events shouldn't be used for Actor's internal communication)
            // when ready Rx 3 will be the publication mechanism
            IImdbEvents e = GetEvent<IImdbEvents>();
            e.LikeStar(data);

            return Task.FromResult(0);
        }

        public Task SendMovieAsync(Movie data)
        {
            IImdbEvents e = GetEvent<IImdbEvents>();

            // Clients can listen for the event 
            // (events shouldn't be used for Actor's internal communication)
            // when ready Rx 3 will be the publication mechanism
            e.LikeMovie(data);
            return Task.FromResult(0);
        }
    }
}
