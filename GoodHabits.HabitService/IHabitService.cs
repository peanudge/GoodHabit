namespace GoodHabits.HabitService;

using GoodHabits.Database.Entities;


public interface IHabitService
{
    Task<Habit> Create(string name, string description);
    Task<Habit?> GetById(int id);
    Task<IReadOnlyList<Habit>> GetAll();
}
