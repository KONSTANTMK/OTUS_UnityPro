using GameEngine.Data;
using GameEngine.Services;

namespace GameEngine.SaveLoad
{
    public class MoneySaveLoader : SaveLoader<MoneyService, MoneyData>
    {
        protected override MoneyData ConvertToData(MoneyService service)
        {
            return new MoneyData()
            {
                Money = service.Money,
            };
        }

        protected override void SetupData(MoneyService service, MoneyData data)
        {
            service.SetupMoney(data.Money);
        }
    }
}