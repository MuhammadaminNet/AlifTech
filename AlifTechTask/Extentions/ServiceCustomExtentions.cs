using AlifTechTask.Data.IRepositories;
using AlifTechTask.Data.Repositories;
using AlifTechTask.Domain.Models.Transactions;
using AlifTechTask.Domain.Models.Users;
using AlifTechTask.Service.Interfaces;
using AlifTechTask.Service.Services;

namespace AlifTechTask.Api.Extentions
{
    public static class ServiceCustomExtentions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<Transaction>, Repository<Transaction>>();
        }
    }
}
