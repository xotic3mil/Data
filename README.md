# ProcrastiPlan
## A minor Project Management Application

### Contains
* Backend: .NET
* Frontend: VueJS with Vuetify Open Source UI Library.
* CRUD functionality
* WebAPI - Added search functionality on Employees & Projects
* Statistics based on Status and Service Distribution
* Calendar with upcomming projects and deadlines.
* Transaction Management
* xUnit tests with and without mock

![image](https://github.com/user-attachments/assets/ae25cf8c-bfa9-465c-be04-1a7cc8d20253)


## Table of Contents
- [Data Layer](#data-layer)
  - [Contexts](#contexts)
  - [Entities](#entities)
  - [Repositories](#Repositories)
  - [Interfaces](#Interfaces)
- [Business Layer](#business-layer)
  - [Services](#services)
- [WebAPI](#webapi)

## Data Layer
Responsible for managing database interactions while keeping a clear separation of concerns. It ensures that business logic remains independent of the database implementation, improving maintainability and scalability.
| Feature         | Benefits      |
| -------------   |:-------------:| 
| Encapsulation   | Keeps database logic separate from business logic. |
| Abstraction     | Business Layer works with interfaces, not concrete classes. | 
| Maintainability | Easier to modify or swap database providers.     |
| Testability     | Allows mocking repositories for unit tests.      |
| Reusability     | Standardized data access for different entities. |


### Contexts
AppDbContext

```
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<StatusTypesEntity> StatusTypes { get; set; }
    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<CustomerContactPersonEntity> ContactPerson { get; set; }
    public DbSet<EmployeeEntity> Employees { get; set; }
    public DbSet<ServiceEntity> Services { get; set; }
    public DbSet<RolesEntity> Roles { get; set; }
    public DbSet<UnitEntity> Units { get; set; }
    public DbSet<CurrencyEntity> Currencies { get; set; }
    }
```

### Entities
```
CurrencyEntity.cs
CustomerContactPersonEntity.cs
CustomerEntity.cs
EmployeeEntity.cs
ProjectEntity.cs
RolesEntity.cs
ServiceEntity.cs
StatusTypesEntity.cs
UnitEntity.cs
```
### Repositories

BaseRepository.cs

The BaseRepository class provides a reusable and consistent way to perform common data access operations for any entity type in the application. It leverages Entity Framework Core's DbContext and DbSet to interact with the database and supports transaction management to ensure data integrity.


```
public abstract class BaseRepository<TEntity>(AppDbContext context) : IBaseRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _context = context;
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
    private IDbContextTransaction _transaction = null!;

    #region Transaction Management

    public virtual async Task BeginTransactionAsync()
    {
        _transaction ??= await _context.Database.BeginTransactionAsync();
    }


    public virtual async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null!;
        }
    }

    public virtual async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null!;
        }
    }

    public virtual async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    #endregion


    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        if (entity == null)
            return null!;

        try
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        catch (Exception ex)
        {
            Debug.WriteLine($"Error Creating {nameof(TEntity)} :: {ex.Message}");
            return null!;

        }
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet
            .ToListAsync();
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        if (expression == null)
            return null!;

        return await _dbSet
            .FirstOrDefaultAsync(expression) ?? null!;
    }

    public virtual async Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity updatedEntity)
    {
        if (updatedEntity == null)
            return null!;
        try
        {
            var existingEntity = await _dbSet
                .FirstOrDefaultAsync(expression) ?? null!;
            if (existingEntity == null)
                return null!;

            _context.Entry(existingEntity).CurrentValues
                .SetValues(updatedEntity);

            return existingEntity;
        }

        catch (Exception ex)
        {
            Debug.WriteLine($"Error Updating {nameof(TEntity)} :: {ex.Message}");
            return null!;
        }
    }

    public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        if (expression == null)
            return false;

        try
        {
            var existingEntity = await _dbSet
                .FirstOrDefaultAsync(expression) ?? null!;

            if (existingEntity == null)
                return false;

            _dbSet.Remove(existingEntity);
            return true;
        }

        catch (Exception ex)
        {
            Debug.WriteLine($"Error Deleting {nameof(TEntity)} :: {ex.Message}");
            return false;
        }
    }
```

### Interfaces

the IBaseRepository<TEntity> interface defines a contract for repository classes:

```
public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<TEntity> CreateAsync(TEntity entity);
    Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
    Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity updatedEntity);
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    Task SaveChangesAsync();
}
```

## Business Layer

The services in the Business layer are responsible for handling CRUD operations (Create, Read, Update, Delete) for various entities such as services, roles, customers, projects, and units. They ensure that business logic is applied consistently and provide a clear API for interacting with the underlying data repositories. The tests ensure that these services behave as expected under different scenarios.

## Services

Main methods for all services

```
•	Create: Creates a new object
•	Delete: Deletes an existing object by its ID.
•	GetId: Retrieves a object by its ID.
•	GetSearch: Retrieves all object, optionally filtered by a search term.
•	Update: Updates an existing object.
```

## WebAPI

Unique controllers for all Endpoints handling [HttpGet] [HttpPost] [HttpPut] [HttpDelete]




