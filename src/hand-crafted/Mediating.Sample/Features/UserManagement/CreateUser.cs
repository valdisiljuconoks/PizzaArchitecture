using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Mediating.Sample.Features.UserManagement.Domain;
using MediatR;
using MediatR.Pipeline;

namespace Mediating.Sample.Features.UserManagement
{
    public class CreateUser
    {
        public class Command : IRequest<Unit>
        {
            public string Username { get; set; }

            public string Email { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }
        }

        public class NeedToExecuteBeforeCommand : IRequestPreProcessor<Command>
        {
            public Task Process(Command request, CancellationToken cancellationToken)
            {
                return Task.CompletedTask;
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            private readonly IUserStorage _users;

            public Validator(IUserStorage users)
            {
                _users = users;

                // setup validation
                RuleFor(t => t.Username).NotEmpty();
                Custom(c => _users.Users.Any(u => u.Username == c.Username)
                                ? new ValidationFailure("Username", $"Duplicate user name - {c.Username}")
                                : null);

                RuleFor(t => t.Email).EmailAddress();
            }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly IUserStorage _storage;

            public Handler(IUserStorage storage)
            {
                _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            }

            public Task<Unit> Handle(Command command, CancellationToken cancellationToken)
            {
                // perform some business logic
                var newUser = User.Create(command.Username,
                                          command.Email,
                                          command.FirstName,
                                          command.LastName);

                _storage.Users.Add(newUser);
                _storage.Save(newUser);

                return Task.FromResult(Unit.Value);
            }
        }
    }
}
