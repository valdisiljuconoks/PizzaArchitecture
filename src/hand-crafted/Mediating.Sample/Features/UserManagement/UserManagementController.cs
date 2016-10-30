using Mediating.Sample.Infrastructure.ControllerHelpers;
using Mediating.Sample.Infrastructure.Mediator;
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

        public ActionResult List(GetAllUsersList.Query query)
        {
            var result = _mediator.Query(query);
            var model = new GetAllUsersList.ViewModel(result);

            return View(model);
        }

        public ActionResult Create()
        {
            var model = new CreateUser.Command();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateUser.Command command)
        {
            _mediator.Execute(command);

            return this.RedirectToActionJson("List");
        }
    }
}
