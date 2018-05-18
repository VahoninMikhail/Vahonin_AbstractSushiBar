using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using System.Collections.Generic;

namespace AbstractSushiBarService.Interfaces
{
    public interface IMessageInfoService
    {
        List<MessageInfoViewModel> GetList();

        MessageInfoViewModel GetElement(int id);

        void AddElement(MessageInfoBindingModel model);
    }
}
