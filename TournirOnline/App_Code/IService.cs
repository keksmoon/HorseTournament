using System.ServiceModel;
using GameClientSpace;

// ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Реструктуризация" можно использовать для одновременного изменения имени интерфейса "IService" в коде и файле конфигурации.
[ServiceContract]
public interface IService
{

	[OperationContract]
	void SetData(Record[] data);

	[OperationContract]
	Record[] GetData();

	[OperationContract]
	void SetField(GameClient data, string room);

	[OperationContract]
	GameClient GetField(string room);

	[OperationContract]
	bool ContainsRoom(string room);
}