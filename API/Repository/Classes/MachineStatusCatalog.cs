using API.Data;
using API.Repository.Interfaces;

namespace API.Repository.Classes
{
    public class MachineStatusCatalog : IMachineStatusCatalog

    {
        // We instace the DatabaseContext 
        private readonly DatabaseContext  _dbContext;

        public MachineStatusCatalog(DatabaseContext baseContext)
        {
            _dbContext = baseContext;
        }

        public bool CreateMachineStatusCatalog(Models.MachineStatusCatalog machine)
        {
            _dbContext.MachineStatusCatalog.Add(machine);
            return Save();
        }

        public bool DeleteMachineStatusCatalog(Models.MachineStatusCatalog machine)
        {
            _dbContext.MachineStatusCatalog.Remove(machine);
            return Save();
        }

        public bool ExistMachineStatusCatalog(string description)
        {
            // this sh**t returns a boolean
            return _dbContext.MachineStatusCatalog.Any(machine => machine.Description.ToLower().Trim() == description.ToLower().Trim());
        }

        public bool ExistsMachineStatusCatalog(int id)
        {
            return _dbContext.MachineStatusCatalog.Any(machine => machine.Id == id);

        }

        public Models.MachineStatusCatalog GetMachine(int id)
        {
            return _dbContext.MachineStatusCatalog.FirstOrDefault(machine => machine.Id == id);
        }

        public ICollection<Models.MachineStatusCatalog> GetAll()
        {
            return _dbContext.MachineStatusCatalog.OrderBy(machine => machine.Id).ToList();

        }

        public bool Save()
        {
            return _dbContext.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateMachineStatusCatalog(Models.MachineStatusCatalog machine)
        {
            _dbContext.MachineStatusCatalog.Update(machine);
            return Save();
        }
    }
}
