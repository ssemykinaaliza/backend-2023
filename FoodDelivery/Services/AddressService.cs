using FoodDelivery.Models;
using FoodDelivery.Models.DTO;

namespace FoodDelivery.Services
{
public interface IAddressService
{
        /*int getAddress();*/
        AddressPagedDTO GetAddresses(int page, int pageSize, string search);
        IEnumerable<int> GetHierarchyObjectIds(int parentObjectId);
        HousesPagedDTO GetHousesByObjectIds(IEnumerable<int> objectIds, int page, int pageSize);
    }

public class AddressService : IAddressService
{
    private readonly Context2 _context;

    public AddressService(Context2 context)
    {
        _context = context;
    }

        /*public int getAddress()
        {
            int count = _context.Address.ToList().Count;
            return count;
        }*/
        public AddressPagedDTO GetAddresses(int page, int pageSize, string search)
        {
            var query = _context.Address.Where(a => a.isactive == 1);

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                query = query.Where(a => a.name.ToLower().Contains(search));
            }

            query = query.OrderBy(a => a.name);

            int totalItems = query.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var addresses = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var result = addresses.Select(a => new AddressDTO
            {
                id = a.id,
                objectid = a.objectid,
                objectguid = a.objectguid,
                typename = a.typename,
                name = a.name
            });

            return new AddressPagedDTO
            {
                Addresses = result,
                Pagination = new PageInfoDTO
                {
                    Size = pageSize,
                    Count = totalPages,
                    Current = page
                }
            };
        }

        public IEnumerable<int> GetHierarchyObjectIds(int parentObjectId)
        {
            return _context.Hierarchy
                .Where(h => h.parentobjid == parentObjectId)
                .Select(h => h.objectid)
                .ToList();
        }

        public HousesPagedDTO GetHousesByObjectIds(IEnumerable<int> objectIds, int page, int pageSize)
        {
            var query = _context.House.Where(a => a.isactive == 1);
            query = query.Where(h => objectIds.Contains(h.objectid));
            query = query.OrderBy(a => a.housenum);
            /*var result = query.Select(a => new HousesDTO
            {
                id = a.id,
                objectid = a.objectid,
                objectguid = a.objectguid,
                housenum = a.housenum,
                addnum1 = a.addnum1,
                addnum2 = a.addnum2,
                housetype = a.housetype,
                addtype1 = a.addtype1,
                addtype2 = a.addtype2,
                opertypeid = a.opertypeid,
            });
            return result;*/
            int totalItems = query.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var houses = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var result = houses.Select(a => new HousesDTO
            {
                id = a.id,
                objectid = a.objectid,
                objectguid = a.objectguid,
                housenum = a.housenum,
                addnum1 = a.addnum1,
                addnum2 = a.addnum2,
                housetype = a.housetype,
                addtype1 = a.addtype1,
                addtype2 = a.addtype2,
                opertypeid = a.opertypeid,
            });

            return new HousesPagedDTO
            {
                Houses = result,
                Pagination = new PageInfoDTO
                {
                    Size = pageSize,
                    Count = totalPages,
                    Current = page
                }
            };
        }
    }
}