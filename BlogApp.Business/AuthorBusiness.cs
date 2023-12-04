using BlogApp.Model;
using BlogApp.Data;
using BlogApp.Data.Repository;

namespace BlogApp.Business
{
    public class AuthorBusiness
    {
        private readonly IRepository<Author> _authorRepository;

        public AuthorBusiness(IRepository<Author> _authorRepository)
        {
            this._authorRepository = _authorRepository;
        }

        public async Task<DbOperationResult> Create(Author model)
        {
            try
            {
                var operationResult = await _authorRepository.Insert(model);
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

        public async Task<DbOperationResult> Update(Author model)
        {
            try
            {
                var operationResult = await _authorRepository.Update(model);
                return operationResult;
            }
            catch (Exception ex)
            {
                return new DbOperationResult(false, ex.Message);
            }
        }

        public Author Get(int id)
        {
            var data = _authorRepository.ListQueryable
                .FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (data == null)
                return null;

            return data;
        }

        public List<Author> GetAll()
        {
            var data = _authorRepository.ListQueryableNoTracking
                .Where(x=>!x.IsDeleted)
                .ToList();

            if (data == null)
                return null;

            return data;
        }

        public DbOperationResult<Author> Login(string username, string password)
        {
            var data = _authorRepository.ListQueryable
                .FirstOrDefault();

            if (data == null)
                return new DbOperationResult<Author>(false, "", null);

            return new DbOperationResult<Author>(true, "", data);
        }

        public async Task<DbOperationResult> Delete(int id)
        {
            var data = _authorRepository.ListQueryable
                .Where(x => x.Id == id && !x.IsDeleted).FirstOrDefault();

            if (data == null)
                return new DbOperationResult(false, "Veri bulunamadı");

            try
            {
                data.IsDeleted = true;
                data.IsActive = false;
                var operationResult = await _authorRepository.Update(data);
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
            var data = _authorRepository.ListQueryable
                .Where(x => x.Id == id).FirstOrDefault();

            if (data == null)
                return new DbOperationResult(false, "Veri bulunamadı");

            try
            {
                data.IsActive = true;
                var operationResult = await _authorRepository.Update(data);
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
            var data = _authorRepository.ListQueryable
                .Where(x => x.Id == id && !x.IsDeleted).FirstOrDefault();

            if (data == null)
                return new DbOperationResult(false, "Veri bulunamadı");

            try
            {
                data.IsActive = false;
                var operationResult = await _authorRepository.Update(data);
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
