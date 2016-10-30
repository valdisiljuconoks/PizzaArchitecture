using System;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using Mediating.Sample.Features.UserManagement.Domain;
using Mediating.Sample.Infrastructure.Commands;

namespace Mediating.Sample.Features.UserManagement
{
    public class CreateUser
    {
        public class Command : ICommand
        {
            public string Username { get; set; }

            public string Email { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }
        }

        public class NeedToExecuteBeforeCommand : IPreExecuteCommandHandler<Command>
        {
            public void Handle(Command command)
            {
                // do some voodoo black magic here
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

        public class Handler : ICommandHandler<Command>
        {
            private readonly IUserStorage _storage;

            public Handler(IUserStorage storage)
            {
                if(storage == null)
                    throw new ArgumentNullException(nameof(storage));

                _storage = storage;
            }

            public void Handle(Command command)
            {
                // perform some business logic
                var newUser = User.Create(command.Username,
                                          command.Email,
                                          command.FirstName,
                                          command.LastName);

                _storage.Users.Add(newUser);
                _storage.Save(newUser);
            }
        }
    }
}
