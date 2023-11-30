using System.Text;
using System.Text.Json;

namespace database
{
    public class DataBase
    {
        public readonly static string fileName = AppDomain.CurrentDomain.BaseDirectory + "database.txt";
        public DataBase() { using StreamWriter w = File.AppendText(fileName); }
        public BidirectionalList<Entity> Load()
        {
            BidirectionalList<Entity> list = new();
            using (FileStream fileStream = File.OpenRead(fileName))
            using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8, true, 256)) //Initializes a new instance of the StreamReader class for the specified file name, with the specified character encoding, byte order mark detection option, and buffer size.
            {
                String className = "Student";
                String line;
                int lineIndex = 0;
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (lineIndex % 2 == 0)
                    {
                        className = line.Split(' ')[0];
                    }
                    else
                    {
                        Type EntityType = Type.GetType("database." + className) ?? throw new Exception("DataBase has unknown entity: " + className);
                        Entity element = (Entity)JsonSerializer.Deserialize(line, EntityType, new JsonSerializerOptions(JsonSerializerDefaults.Web));

                        list.Push(element);
                    }
                    lineIndex++;
                }
            }
            return list;
        }
        public void Save(BidirectionalList<Entity> listToSave)
        {
            using StreamWriter writer = new(fileName);
            int entityIndex = 0;
            while (entityIndex < listToSave.Length)
            {
                writer.WriteLine(listToSave[entityIndex].GetType().Name + " " + listToSave[entityIndex].LastName);
                writer.WriteLine(JsonSerializer.Serialize((object)listToSave[entityIndex]));
                entityIndex++;
            }
        }
    }
}


