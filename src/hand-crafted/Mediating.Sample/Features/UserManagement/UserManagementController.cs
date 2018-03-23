using System.Threading.Tasks;
using Mediating.Sample.Infrastructure.ControllerHelpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mediating.Sample.Features.UserManagement
{
    public class UserManagementController : Controller
    {
        private readonly IMediator _mediator;

        public UserManagementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ActionResult> List(GetAllUsersList.Query query)
        {
            var result =await  _mediator.Send(query);
            var model = new GetAllUsersList.ViewModel(result);

            return View(model);
        }

        public ActionResult Create()
        {
            var model = new CreateUser.Command();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateUser.Command command)
        {
            await _mediator.Send(command);

            return this.RedirectToActionJson("List");
        }
    }
}
