using API.Models;

namespace API.Repository.Interfaces
{
    public interface IMachineStatusCatalog
    {
        ICollection<MachineStatusCatalog> GetAll();

        MachineStatusCatalog GetMachine(int id);

        bool ExistMachineStatusCatalog(string name);

        bool ExistsMachineStatusCatalog(int id);

        bool CreateMachineStatusCatalog(MachineStatusCatalog machine);

        bool UpdateMachineStatusCatalog(MachineStatusCatalog machine);

        bool DeleteMachineStatusCatalog(MachineStatusCatalog machine);

        bool Save();
    }
}
