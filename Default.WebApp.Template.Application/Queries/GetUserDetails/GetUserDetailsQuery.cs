using Default.WebApp.Template.Domain.Dtos;
using MediatR;

namespace Default.WebApp.Template.Application.Queries.GetUserDetails
{
    public class GetUserDetailsQuery : IRequest<UserDetails>
    {
        public string Username { get; set; }
    }
}