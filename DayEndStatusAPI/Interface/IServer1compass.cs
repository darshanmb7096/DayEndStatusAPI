using DayEndStatusAPI.Models;

namespace DayEndStatusAPI.Interface
{
    public interface IServer1compass
    {
        ICollection<StatusMessage> GetStatusMessages();
    }
}
