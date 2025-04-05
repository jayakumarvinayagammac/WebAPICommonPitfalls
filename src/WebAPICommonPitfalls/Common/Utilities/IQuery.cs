using MediatR; // Add the correct namespace for IRequest

namespace WebAPICommonPitfalls.Common.Utilities
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
        where TResponse : notnull{}
    
    public interface IQueryHandler<in TQuery, TResponse>
        : IRequestHandler<TQuery, TResponse>
            where TQuery : IQuery<TResponse>
            where TResponse : notnull
    { }
}