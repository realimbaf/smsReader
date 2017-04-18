using System.Threading.Tasks;

namespace Wiki.Sms.Bll.Contracts
{
    public interface ISmsHandler
    {
        string Name { get;}
        Task Handle();
    }
}
