using GameClientSpace;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

// ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Реструктуризация" можно использовать для одновременного изменения имени класса "Service" в коде, SVC-файле и файле конфигурации.
public class Service : IService {

    public void SetData(Record[] data) {
        if (data == null)
            return;

        Array.Sort(data);

        string fileName = @"E:\Rooms\u513812\teterint.ru\www\wcf4\recs.xml";

        XmlSerializer formatter = new XmlSerializer(typeof(Record[]));

        using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate)) {
            formatter.Serialize(fs, data);
        }
    }

    public Record[] GetData() {
        string fileName = @"E:\Rooms\u513812\teterint.ru\www\wcf4\recs.xml";
        Record[] data;

        XmlSerializer formatter = new XmlSerializer(typeof(Record[]));

        using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate)) {
            data = (Record[])formatter.Deserialize(fs);
        }

        return data;
    }

    public void SetField(GameClient data, string room) {
        if (data == null)
            return;

        string fileName = @"E:\Rooms\u513812\teterint.ru\www\wcf4\rooms\" + room + ".xml";

        XmlSerializer formatter = new XmlSerializer(typeof(GameClient));

        using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate)) {
            formatter.Serialize(fs, data);
        }
    }

    public GameClient GetField(string room) {
        string fileName = @"E:\Rooms\u513812\teterint.ru\www\wcf4\rooms\" + room + ".xml";
        GameClient data;

        XmlSerializer formatter = new XmlSerializer(typeof(GameClient));

        using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate)) {
            data = (GameClient)formatter.Deserialize(fs);
        }

        return data;
    }

    public bool ContainsRoom(string room) {
        string fileName = @"E:\Rooms\u513812\teterint.ru\www\wcf4\rooms\" + room + ".xml";

        if (File.Exists(fileName)) {
            return true;
        }

        return false;
    }
}

[DataContract]
public class Record : IComparable<Record> {

    [DataMember]
    public string FirstPlayer { get; set; }

    [DataMember]
    public string SecondPlayer { get; set; }

    [DataMember]
    public int TotalScore { get; set; }

    [DataMember]
    public string Win { get; set; }

    [DataMember]
    public DateTime dateTime { get; set; }

    [DataMember]
    public int RoomCode { get; set; }

    public int CompareTo(Record other) {
        return other.TotalScore.CompareTo(TotalScore);
    }
}