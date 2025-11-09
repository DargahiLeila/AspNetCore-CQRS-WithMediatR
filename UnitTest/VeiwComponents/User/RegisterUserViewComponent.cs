
using DomainModel.DTO.UserModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UnitTest.VeiwComponents.User
{
    [ViewComponent(Name = "RegisterUser")]
    public class RegisterUserViewComponent:ViewComponent
    {
       
        private readonly IMediator _mediator;

        public RegisterUserViewComponent( IMediator _mediator)
        {
          
            this._mediator = _mediator;
        }

        public IViewComponentResult Invoke()
        {
            return  View();
        }
    }
}
