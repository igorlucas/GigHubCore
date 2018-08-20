using GigHubCore.Core.Dtos;
using GigHubCore.Core.Models;
using System.Linq;

namespace GigHubCore.Core.IRepositories
{
    public interface IAttendanceRepository
    {
        void AttendGig(AttendanceDto dto, string userId);

        void DeleteAttend(Attendance attendance);

        ILookup<int, Attendance> GetFutureAttendances(string userId, int gigId);

        ILookup<int, Attendance> GetFutureAttendances(string userId);

        Attendance GetAttendance(int gigId, string userId);
    }
}
