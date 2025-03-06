namespace Shifts.Model;

public interface IDataRepository
{
    public interface IDataRepository
    {
        List<Shift> GetShifts();
        Shift GetShiftById(string shiftId);
    }
}