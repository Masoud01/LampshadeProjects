using _0_Framework.Application;
using _0_Framework.Inrastuchture;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Comment;
using ShopManagement.Domain.CommentAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository;

public class CommentRepository : RepositoryBase<int, Comment>, ICommentRepository
{
    private readonly ShopContext _context;

    public CommentRepository(ShopContext context) : base(context)
    {
        _context = context;
    }


    public List<CommentViewModel> Search(CommentSearchModel searchModel)
    {
        var query = _context
            .Comments!.Include(x => x.Product)
            .Select(x => new CommentViewModel()
            {
                Id = x.Id,
                ProductName = x.Product.Name,
                Email = x.Email,
                Message = x.Email,
                Name = x.Name,
                ProductId = x.ProductId,
                IsConfirm = x.IsConfirm,
                IsCancel = x.IsCancel,
                CommentDate = x.CreateDate.ToFarsi()
            });
        if (!string.IsNullOrWhiteSpace(searchModel.Name))
        {
            query = query.Where(x => x.Name!.Equals(searchModel.Name));
        }

        if (!string.IsNullOrWhiteSpace(searchModel.Email))
        {
            query = query.Where(x => x.Email!.Equals(searchModel.Email));
        }

        return query.OrderByDescending(x => x.Id).ToList();
    }
}