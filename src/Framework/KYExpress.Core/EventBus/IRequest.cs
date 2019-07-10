namespace KYExpress.Core.EventBus
{
   
    /// <summary>
    /// Marker interface to represent a request with a response
    /// </summary>
    /// <typeparam name="TResponse">Response type</typeparam>
    public interface IRequest<out TResponse> : IBaseRequest { }

    /// <summary>
    /// Allows for generic type constraints of objects implementing IRequest or IRequest{TResponse}
    /// </summary>
    public interface IBaseRequest { }
}