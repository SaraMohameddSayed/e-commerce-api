using Infrastructure;

namespace Managers;

public class MainManager<T> where T : class
{
    public dbContext dbContext { get; set; }

    public MainManager(dbContext _dbContext)
    {
        dbContext = _dbContext;
    }

    public IQueryable<T> getAll()
    {
        return dbContext.Set<T>().AsQueryable();
    }

    public async Task<T?> getOne(object _Id)
    {
        return await dbContext.Set<T>().FindAsync(_Id);
    }

    public async Task<bool> Add(T _product)
    {
        try
        {
            dbContext.Set<T>().AddAsync(_product);
            await dbContext.SaveChangesAsync();
            return true;

        }
        catch (Exception)
        {
            throw;
        }

    }
    public async Task<bool> Update(T _product)
    {
        try
        {
            dbContext.Set<T>().Update(_product);
            await dbContext.SaveChangesAsync();
            return true;

        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<bool> Delete(T _product)
    {
        try
        {
            dbContext.Set<T>().Remove(_product);
            await dbContext.SaveChangesAsync();
            return true;

        }
        catch (Exception)
        {
            throw;
        }
    }
}

