using DAL.Data;
using DOMAIN.Entites;

namespace BLL.Services
{
	public interface IServiceMessage : IServiceBase<Message>
	{	
	}

	public class ManagerMessage : ManagerBase<Message, IDalMessage>, IServiceMessage
	{
		public ManagerMessage(IDalMessage dal) : base(dal)
		{

		}
	}
}
