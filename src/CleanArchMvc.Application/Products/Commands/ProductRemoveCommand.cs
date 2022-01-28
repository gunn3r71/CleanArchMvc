using MediatR;

namespace CleanArchMvc.Application.Products.Commands
{
    public class ProductRemoveCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}