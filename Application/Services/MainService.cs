using Infrastructure;

namespace Application.Services
{
    public class MainService<T> where T : class
    {
        public AppDbContext _AppDbContext { get; set; }

        public MainService(AppDbContext AppDbContext)
        {
            _AppDbContext = AppDbContext;
        }

        public IQueryable<T> GetAll()
        {
            return _AppDbContext.Set<T>().AsQueryable();
        }

        public async Task<T?> GetOne(object _Id)
        {
            return await _AppDbContext.Set<T>().FindAsync(_Id);
        }

        public async Task<bool> Add(T _product)
        {
            try
            {
                await _AppDbContext.Set<T>().AddAsync(_product);
                await _AppDbContext.SaveChangesAsync();
                return true;

            }
            catch
            {
                throw;
            }

        }
        public async Task<bool> Update(T _product)
        {
            try
            {
                _AppDbContext.Set<T>().Update(_product);
                await _AppDbContext.SaveChangesAsync();
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
                _AppDbContext.Set<T>().Remove(_product);
                await _AppDbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

