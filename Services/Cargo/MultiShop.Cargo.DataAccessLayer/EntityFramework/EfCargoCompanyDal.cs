using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Concrete;
using MultiShop.Cargo.DataAccessLayer.Repositories;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.DataAccessLayer.EntityFramework
{
    public class EfCargoCompanyDal : GenericRepository<CargoCompany>, ICargoCompanyDal
    {
        /// <summary>
        ///     : base(context) nedir? Türemiş sınıflar, temel sınıfların özelliklerini devralıp kullanabilirler. GenericRepository sınıfının ctor'u da olduğu için ve bu ctor boş olmadığı için yani bir CargoContext aldığı için türemiş sınıfın ctoru yani EfCargoCompanyDal'ın ctoru bu parametreyi temel sınıfın ctor'una iletmek zorundadır.
        /// </summary>
        public EfCargoCompanyDal(CargoContext context) : base(context)
        {

        }
    }
}
