using System;
using System.Collections.Generic;

namespace Work6_6
{
    public class Repository
    {
        private List<Worker> _workers;
        
        public Repository()
        {
            InitId();
            LoadAllWorkers();
            InitEvents();
        }

        ~Repository()
        {
            DeinitEvents();
        }

        private void InitEvents()
        {
            Menu.onAddNewWorker += AddNewWorker;
            Menu.onAddTestWorker += AddTestWorker;
            Menu.onShowAllWorkers += ShowAllWorkers;
            Menu.onRemoveWorker += RemoveWorker;
            Menu.onSelectWorkers += SelectWorkers;
        }
        
        private void DeinitEvents()
        {
            Menu.onAddNewWorker -= AddNewWorker;
            Menu.onAddTestWorker -= AddTestWorker;
            Menu.onShowAllWorkers -= ShowAllWorkers;
            Menu.onRemoveWorker -= RemoveWorker;
            Menu.onSelectWorkers -= SelectWorkers;
        }
        
        private Worker[] GetAllWorkers()
        {
            return _workers.ToArray();
        }
        private Worker GetWorkerById(int id)
        {
            return _workers.Find(e => e.Id == id);
        }
        private void RemoveWorkerByID(int id)
        {
            if (_workers.Remove(GetWorkerById(id)))
            {
                Console.WriteLine($"Workers ({id}) removed.");
            }
            else
            {
                Console.WriteLine($"Workers ({id}) not found.");
            }
            SaveAllWorkers();
        }
        
        private void RemoveWorker()
        {
            try
            {
                int id = Convert.ToInt32(Menu.GetStringFromConsole("Input worker id:"));
                RemoveWorkerByID(id);
            }
            catch (FormatException e)
            {
                Console.WriteLine("Invalid ID.");
            }
        }
        private Worker[] GetWorkersBetweenTwoDates(DateTime dateFrom, DateTime dateTo)
        {
            return _workers.FindAll(e => (e.DateOfBirth > dateFrom && e.DateOfBirth < dateTo)).ToArray();
        }

        private void SelectWorkers()
        {
            DateTime dateFrom = Convert.ToDateTime(Menu.GetStringFromConsole("Input start date:"));
            DateTime dateTo = Convert.ToDateTime(Menu.GetStringFromConsole("Input end date:"));
            Worker[] workers = GetWorkersBetweenTwoDates(dateFrom, dateTo);
            ShowWorkers(workers);
        }
        
        private void AddNewWorker()
        {
            AddWorker(InputNewWorker());
        }
        private void AddTestWorker()
        {
            AddWorker(GetDefaultWorker());
        }
        private Worker GetDefaultWorker()
        {
            return new Worker("Test test", 18, 170, DateTime.Now.AddYears(-18), "Place Of Birth");
        }
        private void AddWorker(Worker worker)
        {
            DataStorage.getInstance().AddData(Worker.GetSerializedString(worker));
        }
        private void ShowAllWorkers()
        {
            ShowWorkers(_workers.ToArray());
        }
        private void ShowWorkers(Worker[] workers)
        {
            foreach (Worker worker in workers)
                Console.WriteLine(Worker.GetSerializedString(worker));
        }
        private void SaveAllWorkers()
        {
            DataStorage.getInstance().ClearData();
            foreach (Worker worker in _workers)
                AddWorker(worker);
        }
        private void LoadAllWorkers()
        {
            _workers = new List<Worker>();
            string personLine = "";
            while (personLine != null)
            {
                personLine = DataStorage.getInstance().GetNextLine();
                if (personLine != null)
                    _workers.Add(new Worker(personLine));
            }
            DataStorage.getInstance().ResetFilePointer();
        }
        
        private void InitId()
        {
            string lastLine = DataStorage.getInstance().GetLastLine();
            Worker.InitId(lastLine);
        }
        private Worker InputNewWorker()
        {
            string fullname = "";
            byte age = 0;
            ushort height = 0;
            DateTime dateOfBirth = DateTime.MinValue;
            string placeOfBirth = "";

            bool inputComplete = false;

            void GetData()
            {
                try
                {
                    if (fullname == "")
                        fullname = Menu.GetStringFromConsole("Input full name: ");

                    if (age == 0)
                        age = Byte.Parse(Menu.GetStringFromConsole("Input age: "));

                    if (height == 0)
                        height = UInt16.Parse(Menu.GetStringFromConsole("Input height: "));

                    if (dateOfBirth == DateTime.MinValue)
                        dateOfBirth = DateTime.Parse(Menu.GetStringFromConsole("Input date of birth (dd/mm/yyyy): "));

                    if (placeOfBirth == "")
                        placeOfBirth = Menu.GetStringFromConsole("Input place of birth: ");

                    inputComplete = true;
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Invalid format. Try again.");
                }
            }

            while (!inputComplete)
            {
                GetData();
            }

            return new Worker(fullname, age, height, dateOfBirth, placeOfBirth);
        }


    }
}