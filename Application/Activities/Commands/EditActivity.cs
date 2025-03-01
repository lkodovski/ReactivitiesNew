﻿using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Commands
{
    public class EditActivity
    {
        public class Command : IRequest
        {
            public required Activity Activity { get; set; }
        }

        public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command>
        {
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await context.Activities
                    .FindAsync([request.Activity.Id], cancellationToken) ?? throw new Exception("Cannot find activity");
                
               // mapper.Map(request.Activity, activity);

                // Debug log before mapping
                Console.WriteLine($"Before Mapping: {activity.Title} - {activity.Description}");

                mapper.Map(request.Activity, activity);

                // Debug log after mapping
                Console.WriteLine($"After Mapping: {activity.Title} - {activity.Description}");

                await context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
