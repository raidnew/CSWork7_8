using System;

namespace Work6_6
{
    public struct Worker
    {
        private static int _nextId = 0;
        private static char separator = '#';
        
        private int _id;
        private DateTime _dateAdded;
        private string _fullName;
        private byte _age;
        private ushort _height;
        private DateTime _dateOfBirth;
        private string _placeOfBirth;

        public int Id => _id;
        public DateTime DateAdded => _dateAdded;
        public string FullName => _fullName;
        public byte Age => _age;
        public ushort Height => _height;
        public DateTime DateOfBirth => _dateOfBirth;
        public string PlaceOfBirth => _placeOfBirth;

        public static void InitId(string serializedSrting)
        {
            try
            {
                Worker tempWorker = new Worker(serializedSrting);
                _nextId = (tempWorker.Id) + 1;
            }
            catch (FormatException e)
            {
                
            }
        }

        public Worker(string serializedString) : this()
        {
            ParseSerializedString(serializedString);
        }

        public Worker(string fullName, byte age, ushort heignt, DateTime dateOfBirth, string placeOfBirth)
        {
            _id = _nextId++;
            _fullName = fullName + _id;
            _age = age; //TODO may be calculate?
            _height = heignt;
            _dateOfBirth = dateOfBirth;
            _placeOfBirth = placeOfBirth;
            _dateAdded = DateTime.Now;
        }

        private void ParseSerializedString(string serializedString)
        {
            try
            {
                string[] data = serializedString.Split(separator);
                _id = Convert.ToInt32(data[0]);
                _dateAdded = DateTime.Parse(data[1]);
                _fullName = Convert.ToString(data[2]);
                _age = Convert.ToByte(data[3]);
                _height = Convert.ToUInt16(data[4]);
                _dateOfBirth = DateTime.Parse(data[5]);
                _placeOfBirth = Convert.ToString(data[6]);
            }
            catch (FormatException e)
            {
                
            }
        }
        public static string GetSerializedString(Worker worker)
        {
            string serializedString = "";
            bool first = true;
            
            foreach (var data in GetDataAsStringArray(worker))
            {
                if (!first) serializedString += separator;
                serializedString += data;
                first = false;
            }

            return serializedString;
        }
        private static string[] GetDataAsStringArray(Worker worker)
        {
            return new string[]
            {
                worker._id.ToString(),
                worker._dateAdded.ToString(),
                worker._fullName,
                worker._age.ToString(),
                worker._height.ToString(),
                worker._dateOfBirth.ToString(),
                worker._placeOfBirth,
            };
        }
        

    }
}