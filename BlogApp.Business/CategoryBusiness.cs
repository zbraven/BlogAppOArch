using BlogApp.Model;
using BlogApp.Data;
using BlogApp.Data.Repository;

namespace BlogApp.Business
{
    public class CategoryBusiness
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryBusiness(IRepository<Category> _categoryRepository)
        {
            this._categoryRepository = _categoryRepository;
        }

        public async Task<DbOperationResult> Create(Category model)
        {
            try
            {
                var operationResult = await _categoryRepository.Insert(model);
                return operationResult;
            }
            catch (Exception ex)
            {
                return new DbOperationResult(false, ex.Message);
            }
        }

        public bool Test()
        {
            return true;
        }

        public async Task<DbOperationResult> Update(Category model)
        {
            try
            {
                var operationResult = await _categoryRepository.Update(model);
                return operationResult;
            }
            catch (Exception ex)
            {
                return new DbOperationResult(false, ex.Message);
            }
        }

        public Category Get(int id)
        {
            var data = _categoryRepository.ListQueryable
                .FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (data == null)
                return null;

            return data;
        }

        public List<Category> GetAll()
        {
            var data = _categoryRepository.ListQueryableNoTracking
                .Where(x=>!x.IsDeleted)
                .ToList();

            if (data == null)
                return null;

            return data;
        }

        public DbOperationResult<Category> Login(string username, string password)
        {
            var data = _categoryRepository.ListQueryable
                .FirstOrDefault();

            if (data == null)
                return new DbOperationResult<Category>(false, "", null);

            return new DbOperationResult<Category>(true, "", data);
        }

        public async Task<DbOperationResult> Delete(int id)
        {
            var data = _categoryRepository.ListQueryable
                .Where(x => x.Id == id && !x.IsDeleted).FirstOrDefault();

            if (data == null)
                return new DbOperationResult(false, "Veri bulunamadı");

            try
            {
                data.IsDeleted = true;
                data.IsActive = false;
                var operationResult = await _categoryRepository.Update(data);
                if (operationResult.IsSucceed)
                    operationResult.Message = "Veri Silindi";

                return operationResult;
            }
            catch (Exception ex)
            {
                return new DbOperationResult(false, ex.Message);
            }
        }

        public async Task<DbOperationResult> Active(int id)
        {
            var data = _categoryRepository.ListQueryable
                .Where(x => x.Id == id).FirstOrDefault();

            if (data == null)
                return new DbOperationResult(false, "Veri bulunamadı");

            try
            {
                data.IsActive = true;
                var operationResult = await _categoryRepository.Update(data);
                if (operationResult.IsSucceed)
                    operationResult.Message = "Veri aktif olarak işaretlendi";

                return operationResult;
            }
            catch (Exception ex)
            {
                return new DbOperationResult(false, ex.Message);
            }
        }

        public async Task<DbOperationResult> Passive(int id)
        {
            var data = _categoryRepository.ListQueryable
                .Where(x => x.Id == id && !x.IsDeleted).FirstOrDefault();

            if (data == null)
                return new DbOperationResult(false, "Veri bulunamadı");

            try
            {
                data.IsActive = false;
                var operationResult = await _categoryRepository.Update(data);
                if (operationResult.IsSucceed)
                    operationResult.Message = "Veri pasif olarak işaretlendi";

                return operationResult;
            }
            catch (Exception ex)
            {
                return new DbOperationResult(false, ex.Message);
            }
        }
    }
}
